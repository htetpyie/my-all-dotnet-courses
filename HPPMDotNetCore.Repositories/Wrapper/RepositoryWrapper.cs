using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Repositories.Repo;

namespace HPPMDotNetCore.Repositories.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private EFDbContext _db;
        private IBlogRepository _blog;
        private IExpenseTrackerRepository _expenseTracker;

        public IBlogRepository Blog
        {
            get
            {
                if (_blog == null)
                {
                    _blog = new BlogRepository(_db);
                }
                return _blog;
            }
        }

        public IExpenseTrackerRepository ExpenseTracker
        {
            get
            {
                if (_expenseTracker == null)
                {
                    _expenseTracker = new ExpenseTrackerRepository(_db);
                }
                return _expenseTracker;
            }
        }

        public RepositoryWrapper(EFDbContext db)
        {
            _db = db;
        }
    }
}
