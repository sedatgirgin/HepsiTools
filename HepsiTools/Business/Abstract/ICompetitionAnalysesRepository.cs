using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Abstract;
using System.Linq;

namespace HepsiTools.Business.Abstract
{
    public interface ICompetitionAnalysesRepository : IGenericRepository<CompetitionAnalyses>
    {
        IQueryable<CompetitionAnalyses> GetCompetitionsByUserAsync(string userId);

    }
}
