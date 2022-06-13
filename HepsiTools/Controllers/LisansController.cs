using HepsiTools.Business.Abstract;
using HepsiTools.Helper;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public LisansController(ILisansRepository lisansRepository)
        {
            _lisansRepository = lisansRepository;
        }

        [HttpGet("GetUserLisansStatus")]
        public async Task<IActionResult> GetUserLisansStatusAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lisans = _lisansRepository.GetList(i => i.UserId == _userId);

            if (lisans!= null && lisans.Count>0) {

               var activeLisans = lisans.Where(i => i.IsActive == true).FirstOrDefault();

                if(activeLisans != null){

                    if (activeLisans.EndDate < DateTime.Now.AddDays(5))
                    {
                        return new Result("Lisansınızın süresi dolmak üzere", new { ExpireDay = (activeLisans.EndDate.Day - DateTime.Now.Day), LisansStatus  = LisansExpirateType.LicenseExpiresSoon });
                    }

                    return new Result("Başarılı",new{ ExpireDay = (activeLisans.EndDate.Day - DateTime.Now.Day), LisansStatus = LisansExpirateType.Successful });
                }
                else
                {
                    return new Result("Aktif lisansınız bulunmamaktadır.", new { ExpireDay = 0,LisansStatus = LisansExpirateType.LisansFinish });
                }
            } else
            {
                return new Result("Lisansınız bulunmamaktadır.", new { ExpireDay = 0, LisansStatus = LisansExpirateType.None});
            }
        }
    }
}
