using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Concrate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HepsiTools.Business.Concrate
{
    public class CompetitionAnalysesRepository : GenericRepository<CompetitionAnalyses>, ICompetitionAnalysesRepository
    {
        public IQueryable<CompetitionAnalyses> GetCompetitionsByUserAsync(string userId)
        {
            var extraDatas = _context.Set<Company>();
            int conpanyId = extraDatas.Where(i=>i.UserId == userId).FirstOrDefault().Id;

            var query = entities.AsQueryable();
            query = query.Include(c => c.Company);
            query = query.Where(c => c.CompanyId == conpanyId);

            return query;
        }
    }
}
