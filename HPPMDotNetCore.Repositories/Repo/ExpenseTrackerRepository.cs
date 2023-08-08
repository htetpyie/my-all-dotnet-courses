using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Repositories.Base;

namespace HPPMDotNetCore.Repositories.Repo
{
    public class ExpenseTrackerRepository : RepositoryBase<ExpenseTrackerDataModel>, IExpenseTrackerRepository
    {
        public ExpenseTrackerRepository(EFDbContext db) : base(db)
        {
        }
    }
}
