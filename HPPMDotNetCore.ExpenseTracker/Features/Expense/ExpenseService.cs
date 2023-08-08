using HPPMDotNetCore.ExpenseTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features.Income;

namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly EfDbContext _context;
        private readonly ILogger<ExpenseService> _logger;

        public ExpenseService(EfDbContext context,
            ILogger<ExpenseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task GenerateExpense()
        {
            for (int i = 1000; i >= 1; i--)
            {
                Random random = new Random();
                await  _context.Expense.AddAsync(new ExpenseDataModel()
                {
                    IncomeId = random.Next(1,100),
                    ExpenseName = i.ToString(),
                    ExpenseAmount = random.Next(1000,100000),
                    CreatedDate = DateTime.Now.AddDays(-i) 
                });
            }
        }
        
        public async Task<ExpenseRespModel> GetExpense(long id)
        {
            ExpenseRespModel model = new ExpenseRespModel();
            try
            {
                var data = await _context
                    .Expense
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IsDelete == false &&
                                              x.ExpenseId == id);
                model = data.Change();
                model.IncomeName = await GetIncomNameAndAmount(data.IncomeId);

                _logger.LogInformation("GetExpense Success");
            }
            catch (Exception ex)
            {
                _logger.LogError("GetExpense Failed" + ex.Message);
            }

            return model;
        }

        public async Task<ExpenseListRespModel> GetExpenseList
            (int pageNo, int pageSize)

        {
            List<ExpenseRespModel> modelList = new List<ExpenseRespModel>();
            try
            {
                var dataList = await _context
                    .Expense
                    .AsNoTracking()
                    .Where(x => x.IsDelete == false)
                    .Pagination(pageNo, pageSize)
                    .ToListAsync();
                modelList = dataList.Select(x => x.Change()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpenseList Error" + ex.Message);
            }

            return new ExpenseListRespModel
            {
                ExpenseList = modelList
            };
        }

        public DataTableResponseModel<ExpenseRespModel> GetExpenseList
            (DataTableRequestModel dt)
        {
            DataTableResponseModel<ExpenseRespModel> response =
                new DataTableResponseModel<ExpenseRespModel>();
            try
            {
                var query = from e in _context.Expense
                    join i in _context.Income
                        on e.IncomeId equals i.IncomeId
                        into expInGp
                    from ie in expInGp.DefaultIfEmpty()
                    orderby e.ExpenseId descending
                    select new ExpenseRespModel
                    {
                        ExpenseId = e.ExpenseId,
                        IncomeId = e.IncomeId,
                        ExpenseAmount = e.ExpenseAmount,
                        UserId = e.UserId,
                        ExpenseName = e.ExpenseName,
                        IncomeName = ie.IncomeName ?? "From All Income"
                    };

                string searchValue = dt.SearchValue;
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(m =>
                        m.ExpenseName.Contains(searchValue) ||
                        m.ExpenseAmount.ToString().Contains(searchValue));
                }

                response = query.Execute(dt);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return response;
        }

        public async Task<int> Save(ExpenseReqModel model)
        {
            int result = 0;
            try
            {
                ExpenseDataModel data = model.Change();
                data.CreatedDate = DateTime.Now;

                await _context.Expense.AddAsync(data);
                result = await _context.SaveChangesAsync();

                _logger.LogInformation(result > 0 ? "Save Success" : "Save Result is less than 0");
            }
            catch (Exception ex)
            {
                _logger.LogError("Expense Save Error" + ex.Message);
            }

            return result;
        }

        public async Task<int> Update(long id, ExpenseReqModel model)
        {
            int result = 0;
            try
            {
                var data = await _context
                    .Expense
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IsDelete == false &&
                                              x.ExpenseId == id);
                if (data == null)
                {
                    _logger.LogWarning("Content Not found to update");
                    return 0;
                }

                data.ExpenseName = model.ExpenseName;
                data.IncomeId = model.IncomeId;
                data.ExpenseAmount = model.ExpenseAmount;
                data.ModifiedDate = DateTime.Now;

                _context.Expense.Update(data);
                _context.Entry(data).State = EntityState.Modified;

                result = await _context.SaveChangesAsync();
                _logger.LogInformation(result > 0 ? "Update Success" : "Update Result is less than 0");
            }
            catch (Exception ex)
            {
                _logger.LogError("Update error" + ex.Message);
            }

            return result;
        }

        public async Task<int> Delete(long id)
        {
            int result = 0;
            try
            {
                var data = await _context
                    .Expense
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IsDelete == false &&
                                              x.ExpenseId == id);
                if (data == null) return 0;

                data.ModifiedDate = DateTime.Now;
                data.IsDelete = true;

                _context.Expense.Update(data);
                _context.Entry(data).State = EntityState.Deleted;
                result = await _context.SaveChangesAsync();
                _logger.LogInformation(result > 0 ? "Delete Expense success" : "delete result is less than 0");
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete Failed " + ex.Message);
            }

            return result;
        }

        public async Task<string> GetIncomNameAndAmount(long incomeId)
        {
            string incomeAmount = (incomeId == 0)
                ? await GetTotalIncomeAsString()
                : await GetIncomeAmountAsString(incomeId);

            string incomeName = (incomeId == 0)
                ? "From All Income"
                : await GetIncomeName(incomeId);

            return $"{incomeName} ({incomeAmount})";
        }

        private async Task<string> GetTotalIncomeAsString()
        {
            decimal totalIncome = await _context
                .Income
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .SumAsync(x => x.IncomeAmount);
            return totalIncome.ToString("N2");
        }

        private async Task<string> GetIncomeAmountAsString(long incomeId)
        {
            IncomeDataModel income = await GetIncomeById(incomeId);
            if (income == null) return "";
            return income.IncomeAmount.ToString("N2");
        }

        private async Task<string> GetIncomeName(long incomeId)
        {
            IncomeDataModel income = await GetIncomeById(incomeId);
            return income.IncomeName;
        }

        private async Task<IncomeDataModel> GetIncomeById(long incomeId)
        {
            return await _context
                .Income
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IncomeId == incomeId);
        }
    }
}