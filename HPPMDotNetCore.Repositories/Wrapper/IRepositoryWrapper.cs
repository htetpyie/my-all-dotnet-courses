using HPPMDotNetCore.Repositories.Repo;

namespace HPPMDotNetCore.Repositories.Wrapper
{
    public interface IRepositoryWrapper
    {
        IBlogRepository Blog { get; }
        IExpenseTrackerRepository ExpenseTracker { get; }
    }
}