using HepsiTools.DataAccess;

namespace HepsiTools.GenericRepositories.Concrate
{
    public class RepositoryBase
    {
        protected static ToolDbContext _context;
        private static object obj = new object();
        public RepositoryBase()
        {
            CreateContext();
        }
        private static void CreateContext()
        {
            if (_context == null)
            {
                //lock = eger 2 request geldiyse biri bitmeden diğerine devam etmiyor.
                lock (obj)
                {
                    _context = new ToolDbContext();
                }
            }
        }
    }
}
