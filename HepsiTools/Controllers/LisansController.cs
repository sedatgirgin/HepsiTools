using HepsiTools.Business.Abstract;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HepsiTools.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LisansController : ControllerBase
    {
        private readonly ILisansRepository _lisansRepository;
        private readonly IUserLisansRepository _userLisansRepository;
        public LisansController(ILisansRepository lisansRepository, IUserLisansRepository userLisansRepository)
        {
            _lisansRepository = lisansRepository;
            _userLisansRepository = userLisansRepository;
        }

        [HttpPost("InsertLisans")]
        public async Task<IActionResult> InsertLisansAsync(LisansModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var result = _lisansRepository.Insert(new Entities.Lisans() { Name = model.Name });
            if (!result.Equals(null))
            {
                return new Result("Başarılı");
            }
            return new ErrorResult("Lütfen giriş bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetLisans")]
        public async Task<IActionResult> GetLisansAsync()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            var result = _lisansRepository.GetAll();

            if (!result.Equals(null))
            {
                return new Result("Başarılı", new { Lisans = result });
            }
            return new ErrorResult("Lütfen giriş bilgilerinizi kontrol edin.");
        }



        [HttpPost("InsertUserLisans")]
        public async Task<IActionResult> InsertUserLisansAsync(UserLisansModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var result = _userLisansRepository.Insert(new Entities.UserLisans() { UserId = model.UserId, LisansId = model.LisansId, IsActive = true, StartDate = model.StartDate, EndDate = model.EndDate });

            if (!result.Equals(null))
            {
                return new Result("Başarılı");
            }
            return new ErrorResult("Lütfen giriş bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetUserLisans")]
        public async Task<IActionResult> GetUserLisansAsync(string userId)
        {
            //var claimsIdentity = this.User.Identity as ClaimsIdentity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userId))
                return new ErrorResult("Hatalı istek");

            var result = _userLisansRepository.GetList(i => i.UserId == userId && i.StartDate > DateTime.Now && i.EndDate < DateTime.Now);

            if (!result.Equals(null))
            {
                return new Result("Başarılı", new { UserLisans = result });
            }
            return new ErrorResult("Lütfen giriş bilgilerinizi kontrol edin.");
        }

    }
}
