using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Concrate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiTools.Business.Concrate
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public IQueryable<Company> GetCompetitionsByUserAsync(string userId)
        {
            var query = entities.AsQueryable();
            query = query.Where(c => c.UserId == userId);
            query = query.Include(c => c.CompetitionAnalyses);

            return query;
        }
    }
}
