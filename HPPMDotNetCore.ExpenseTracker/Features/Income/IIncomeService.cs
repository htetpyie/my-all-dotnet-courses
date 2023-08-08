using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public interface IIncomeService
    {
        Task<int> Delete(long id);
        Task<int> Generate();
        Task<IncomeListRespModel> GetAllIncome();
        Task<IncomeRespModel> GetIncome(long id);
        DataTableResponseModel<IncomeRespModel> GetIncomeList(DataTableRequestModel dt);
        Task<IncomeListRespModel> GetIncomeList(int pageNo, int pageSize, string searchValue);
        Task<int> Save(IncomeReqModel model);
        Task<int> Update(long id, IncomeReqModel model);
    }
}