using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiTools.Business.Abstract
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        IQueryable<Company> GetCompetitionsByUserAsync(string userId);

    }
}
