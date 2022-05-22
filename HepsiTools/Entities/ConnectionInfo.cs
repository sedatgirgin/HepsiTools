using HepsiTools.Helper;
using System.Collections.Generic;

namespace HepsiTools.Entities
{
    public class ConnectionInfo
    {
        public int Id { get; set; }
        public CompanyType CompanyType { get; set; }
        public string SupplierId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CargoCompanyId { get; set; }
        public string CustomResourceName { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<CompetitionAnalyses> CompetitionAnalyses { get; set; }

    }
}
