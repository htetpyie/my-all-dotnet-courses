using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features;

namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    public interface IExpenseService
    {
        Task<int> Delete(long id);
        Task<ExpenseRespModel> GetExpense(long id);
        Task<ExpenseListRespModel> GetExpenseList(int pageNo, int pageSize);
        DataTableResponseModel<ExpenseRespModel> GetExpenseList(DataTableRequestModel dt);
        Task<int> Save(ExpenseReqModel model);
        Task<int> Update(long id, ExpenseReqModel model);
        Task<string> GetIncomNameAndAmount(long incomeId);
        Task GenerateExpense();
    }
}