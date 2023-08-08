using System;
using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.Dashboard
{
    public interface IDashboardService
    {
        string GetCurrentBalance();
        Task<List<ExpenseRespModel>> GetExpenseInCurrentMonth();
        Task<List<IncomeRespModel>> GetIncomeInCurrentMonth();
        Task<PieChartRespModel> GetTop5MaxIncome();
        Task<ExpenseChartRespModel> GetTop5MaxExpense();
        Task<List<RecentTransaction>> GetRecentTransaction();
        Task<MonthlyDataModel> GetMonthlyData(DateTime date);
    }
}