using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.Helper;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace HepsiTools.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CompetionController : ControllerBase
    {
        private readonly ICompetitionAnalsesHistoryRepository _competitionAnalsesHistoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompetitionAnalysesRepository _competitionAnalysesRepository;

        
        public CompetionController(ICompetitionAnalsesHistoryRepository competitionAnalsesHistoryRepository, ICompanyRepository companyRepository, ICompetitionAnalysesRepository competitionAnalysesRepository)
        {
            _companyRepository = companyRepository;
            _competitionAnalsesHistoryRepository = competitionAnalsesHistoryRepository;
            _competitionAnalysesRepository = competitionAnalysesRepository;
        }
  
       [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompanyAsync(CompanyInsertModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _companyRepository.Insert(new Company()
            {
                SupplierId = model.SupplierId,
                CustomResourceName = model.CustomResourceName,
                CompanyType = (SourceType)model.CompanyType,
                UserId = _userId,
                CargoCompanyId = model.CargoCompanyId,
                UserName = model.UserName,
                Password = model.Password,
            });

            if (!result.Equals(null))
            {
                return new Result("Başarılı", result);
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetCompany")]
        public async Task<IActionResult> GetCompanyAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _companyRepository.GetList(i => i.UserId == _userId);

            return new Result("Başarılı", new { Companies = result });
        }

    }
}
