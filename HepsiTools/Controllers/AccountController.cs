using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HepsiTools.Entities;
using HepsiTools.ResultMessages;
using HepsiTools.Models;
using HepsiTools.Business.Abstract;
using HepsiTools.Helper;

namespace HepsiTools.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;
        private readonly ILisansRepository _lisansRepository;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMailService mailService, ILisansRepository lisansRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mailService = mailService;
            _lisansRepository = lisansRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                if (!user.Equals(null) && roles.Count>0)
                {
                   var lisans = _lisansRepository.Get(i => i.UserId == user.Id && i.IsActive == true);
                    if (lisans.EndDate < DateTime.Now)
                    {
                        lisans.IsActive = false;
                        _lisansRepository.Update(lisans);
                    }

                    return new Result("Başarılı", new { token = await GenerateJSONWebToken(user, roles.FirstOrDefault())});
                }
            }
            return new ErrorResult("Lütfen giriş bilgilerinizi kontrol edin.");
        }

        [AllowAnonymous]
        [HttpPut("AddUser")]
        public async Task<IActionResult> AddUserAsync(UserModel model)
        {
            if(!ModelState.IsValid) return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            User appUser = new User
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            if (_userManager.FindByNameAsync(model.Email).Result == null)
            {
                var identityUserResult = _userManager.CreateAsync(appUser, model.NewPassword).Result;
                if (identityUserResult.Succeeded)
                {
                    var currentUser = _userManager.FindByEmailAsync(model.Email).Result;

                    await _userManager.AddToRoleAsync(currentUser, RoleType.User.ToString());

                    Lisans lisans = new Lisans();
                    lisans.UserId = currentUser.Id;
                    lisans.IsActive = true;
                    lisans.StartDate = DateTime.Now;
                    lisans.EndDate = DateTime.Now.AddDays(15);
                    await _lisansRepository.InsertAsync(lisans);

                    return new Result(identityUserResult.Succeeded.ToString());
                }
                return new ErrorResult("Kullanıcı eklemede hata oluştu..");
            }
            return new ErrorResult("Email adresine kayıtlı bir hesap bulunmaktadır.");
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            if (!ModelState.IsValid) return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ErrorResult("E-posta ile ilişkilendirilmiş kullanıcı bulunmamaktadır.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return new Result("Şifreniz başarıyla sıfırlandı.");

            }
            return new ErrorResult("Şifre sıfırlama işlemi gerçekleştirilemedi.");
        }

        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            if (!ModelState.IsValid) return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new ErrorResult("E-posta ile ilişkilendirilmiş kullanıcı bulunmamaktadır.");
                }
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                MailRequest request = new MailRequest();

                request.Body = _config["BaseUrl"]+"?email="+ model.Email + "&token=" + token;
                request.ToEmail = model.Email;
                request.Subject = "Şifre Resetleme Onay Maili";
                await _mailService.SendEmailAsync(request);

                if (string.IsNullOrEmpty(token))
                {
                    return new Result("Mail adresinize onay mesajı gönderildi.");
                } 
            }
            catch (Exception ex)
            {
                return new ErrorResult("Mail adresinize onay maili gönderilemedi.", ex.Message);
            }
            return new ErrorResult("Mail adresinize onay maili gönderilemedi.");
        }

        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            if (!ModelState.IsValid) return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return new ErrorResult($"{_userManager.GetUserId(User)} Id'li kullanıcı bulunamadı.");
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in changePasswordResult.Errors)
                {
                    if (error.Code == "PasswordMismatch")
                    {
                        errors.Add("Lütfen eski şifrenizi kontrol edin.");
                    }
                    else
                    {
                        errors.Add(error.Description);
                    }
                }
                return new ErrorResult(string.Join("\n", errors));
            }
            return new Result("Şifreniz başarıyla değiştirildi.");
        }
        private async Task<string> GenerateJSONWebToken(User userInfo, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Typ, role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
              claims,
              expires: expires,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
