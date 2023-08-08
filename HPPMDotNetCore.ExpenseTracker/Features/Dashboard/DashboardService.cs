using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using HPPMDotNetCore.ExpenseTracker.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HPPMDotNetCore.ExpenseTracker.Features.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly DapperService _dapperService;
        private readonly EfDbContext _context;

        public DashboardService(DapperService dapperService, EfDbContext context)
        {
            this._dapperService = dapperService;
            _context = context;
        }

        public string GetCurrentBalance()
        {
            decimal totalIncome = _context
                .Income
                .AsNoTracking()
                .Sum(x => x.IncomeAmount);

            decimal totalExpsense = _context
                .Expense
                .AsNoTracking()
                .Sum(x => x.ExpenseAmount);
            decimal result = totalIncome - totalExpsense;

            return result.ToString("N2");
        }

        #region Expense

        public async Task<List<ExpenseRespModel>> GetExpenseInCurrentMonth()
        {
            List<ExpenseRespModel> result = new List<ExpenseRespModel>();
            try
            {
                string query = @"select top 10 * from Tbl_Expense e
                            where e.IsDelete = 0 and 
                            Month(e.CreatedDate) = MONTH(GetDate())
                            order by e.ExpenseId desc";

                result = await _dapperService.GetAsync<ExpenseRespModel>(query);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        public async Task<MonthlyDataModel> GetMonthlyData(DateTime date)
        {
            object param = new
            {
                date = date
            };
            
            string incomeQuery = @"select COALESCE(SUM(IncomeAmount),0) as IncomeAmount 
                            from Tbl_Income
                            where Year(CreatedDate) = Year(@date) and
                            Month(CreatedDate) = Month(@date)";
            double income = await _dapperService
                .GetFirstOrDefaultAsync<double>(incomeQuery, param);

            string expenseQuery = $@"select COALESCE(SUM(ExpenseAmount),0) as ExpenseAmount 
                         from Tbl_Expense
                         where Year(CreatedDate) = Year(@date) and
                             Month(CreatedDate) = Month(@date)";
            double expense = await _dapperService
                .GetFirstOrDefaultAsync<double>(expenseQuery, param);

            if (income < 1 && expense < 1)
                return default;

            double saving = income - expense;

            MonthlyDataModel model = new MonthlyDataModel
            {
                Income = income.ToString("N2"),
                Expense = expense.ToString("N2"),
                Saving = saving.ToString("N2"),
                Date = date.ToString("MMM yyyy")
            };
            return model;
        }

        public async Task<ExpenseChartRespModel> GetTop5MaxExpense()
        {
            ExpenseChartRespModel response = new ExpenseChartRespModel();
            try
            {
                string query = @"select top 5 e.ExpenseName, sum(e.ExpenseAmount) as ExpenseAmount
                            from Tbl_Expense e
                            where e.IsDelete = 0 
                            group by e.ExpenseName
                            order by ExpenseAmount desc";
                var dataList = await _dapperService.GetAsync<ExpenseRespModel>(query);

                var series = dataList.Select(x => x.ExpenseAmount).ToList();
                var labels = dataList.Select(x => x.ExpenseName).ToList();

                response = new ExpenseChartRespModel
                {
                    Series = series,
                    Labels = labels
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return response;
        }

        #endregion

        #region Income

        public async Task<List<IncomeRespModel>> GetIncomeInCurrentMonth()
        {
            List<IncomeRespModel> result = new List<IncomeRespModel>();
            try
            {
                string query = @"select top 10 * from Tbl_Income ic
                            where ic.IsDelete = 0 and Month(ic.CreatedDate) = MONTH(GetDate())
                            order by ic.IncomeId desc";

                result = await _dapperService.GetAsync<IncomeRespModel>(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }

        public async Task<PieChartRespModel> GetTop5MaxIncome()
        {
            PieChartRespModel response = new PieChartRespModel();
            try
            {
                string query = @"select top 5 i.IncomeName,
                            sum(i.IncomeAmount) as IncomeAmount
                            from Tbl_Income i
                            where i.IsDelete = 0 
                            group by i.IncomeName
                            order by IncomeAmount desc";
                var dataList = await _dapperService.GetAsync<IncomeRespModel>(query);

                var result = dataList.Select(x =>
                        new ChartModel
                        {
                            Label = x.IncomeName,
                            Value = x.IncomeAmount
                        })
                    .ToList();

                response.Data = result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return response;
        }

        #endregion

        public async Task<List<RecentTransaction>> GetRecentTransaction()
        {
            List<RecentTransaction> dataList = new List<RecentTransaction>();
            try
            {
                string query = "select * from Vw_IncomeExpense;";
                dataList = await _dapperService.GetAsync<RecentTransaction>(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return dataList;
        }
    }

    public class RecentTransaction
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }

        public string AmountAsString
        {
            get => this.Amount.ToString("N2");
        }

        public string CreatedDateAsString
        {
            get => this.CreatedDate.ToString("MMMM dd");
        }
    }

    public class MonthlyDataModel
    {
        public string Income { get; set; }
        public string Expense { get; set; }
        public string Saving { get; set; }
        public string Date { get; set; }
    }
}