using HepsiTools.Business.Abstract;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IConnectionInfoRepository _connectionInfoRepository;
        private readonly ICompetitionCompanyRepository _competitionCompanyRepository;
        private readonly ICompetitionAnalysesRepository _competitionAnalysesRepository;

        
        public CompetionController(IConnectionInfoRepository connectionInfoRepository, ICompetitionCompanyRepository competitionCompanyRepository, ICompetitionAnalysesRepository competitionAnalysesRepository)
        {
            _connectionInfoRepository = connectionInfoRepository;
            _competitionAnalysesRepository = competitionAnalysesRepository;
            _competitionCompanyRepository = competitionCompanyRepository;
        }




    }
}
