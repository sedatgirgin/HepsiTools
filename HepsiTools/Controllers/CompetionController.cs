using AutoMapper;
using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.Helper;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;

        public CompetionController(ICompetitionAnalsesHistoryRepository competitionAnalsesHistoryRepository, ICompanyRepository companyRepository, ICompetitionAnalysesRepository competitionAnalysesRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _competitionAnalsesHistoryRepository = competitionAnalsesHistoryRepository;
            _competitionAnalysesRepository = competitionAnalysesRepository;
            _mapper = mapper;
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
                return new Result("Başarılı", _mapper.Map<CompanyModel>(model));
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetCompanyList")]
        public async Task<IActionResult> GetCompanyListAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _companyRepository.GetList(i => i.UserId == _userId);

            return new Result("Başarılı", new { Companies = _mapper.Map<CompanyModel>(result) });
        }

        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _companyRepository.Get(i => i.UserId == _userId && i.Id == id);
            var data  = _companyRepository.Delete(result);

            if (data != null)
            {
                return new Result("Başarılı");
            }
            return new ErrorResult("Bilgiler uyuşmamaktadır");
        }

        [HttpPost("AddCompetition")]
        public async Task<IActionResult> AddCompetitionAsync(CompetitionAnalysesInsertModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            CompetitionAnalyses competitionAnalyses = _mapper.Map<CompetitionAnalyses>(model);

            var result = _competitionAnalysesRepository.Insert(competitionAnalyses);

            if (!result.Equals(null))
            {
                _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory()
                {
                    HistoryType = HistoryType.Other,
                    CompetitionAnalysesId = competitionAnalyses.Id,
                    Note = "Yeni bir rekabet eklendi"
                });

                return new Result("Başarılı", _mapper.Map<CompetitionAnalysesModel>(result));
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpPost("UpdateCompetition")]
        public async Task<IActionResult> UpdateCompetitionAsync(CompetitionAnalysesModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            CompetitionAnalyses competitionAnalyses = _competitionAnalysesRepository.Get(model.Id);

            if (competitionAnalyses == null)
            {
                return new ErrorResult("Uygun competition bulunamadı");
            }

            if (model.StatusType != 0 && competitionAnalyses.StatusType != (StatusType)model.StatusType)
            {
                _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory() { 
                    HistoryType = HistoryType.Other,
                    CompetitionAnalysesId = competitionAnalyses.Id,
                    Note = competitionAnalyses.StatusType.ToString() + " durum degişikliği " + ((StatusType)model.StatusType).ToString()
                });

                competitionAnalyses.StatusType = (StatusType)model.StatusType;
            }

           var result = _competitionAnalysesRepository.Update(competitionAnalyses);

            if (!result.Equals(null))
            {
                return new Result("Başarılı", _mapper.Map<CompetitionAnalysesModel>(result));
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }


        [HttpDelete("DeleteCompetition")]
        public async Task<IActionResult> DeleteCompetitionAsync(int id)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            CompetitionAnalyses competitionAnalyses = _competitionAnalysesRepository.Get(id);
            var result = _competitionAnalysesRepository.Delete(competitionAnalyses);

            if (!result.Equals(null))
            {
                return new Result("Başarılı", _mapper.Map<CompetitionAnalysesModel>(result));
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetCompetitionsByCompany")]
        public async Task<IActionResult> GetCompetitionsByCompanyAsync(int companyId)
        {
            var result = _competitionAnalysesRepository.GetList(i=>i.CompanyId == companyId);

            if (!result.Equals(null))
            {
                return new Result("Başarılı", result);
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetCompetitionsByUser")]
        public async Task<IActionResult> GetCompetitionsByUserAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _companyRepository.GetCompetitionsByUserAsync(_userId);

            if (!result.Equals(null) && result.ToList().Count>0)
            {
                return new Result("Başarılı", result.ToList());
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }

        [HttpGet("GetAnalsesHistory")]
        public async Task<IActionResult> GetAnalsesHistoryAsync(int competitionId)
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _competitionAnalsesHistoryRepository.GetList(i=>i.CompetitionAnalysesId == competitionId);

            return new Result("Başarılı", result);
        }

    }
}
