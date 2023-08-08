using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using HPPMDotNetCore.ExpenseTracker.Services;
using HPPMDotNetCore.ExpenseTracker;
using System.Linq;
using HPPMDotNetCore.ExpenseTracker.Features;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public class IncomeService : IIncomeService
    {
        private readonly EfDbContext _context;
        private readonly ILogger<IncomeService> _logger;

        public IncomeService(EfDbContext context,
            ILogger<IncomeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IncomeRespModel> GetIncome(long id)
        {
            var data = await _context
                .Income
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsDelete == false && x.IncomeId == id);

            var model = data.Change();

            return model;
        }

        public async Task<IncomeListRespModel> GetAllIncome()
        {
            var dataList = await _context
                .Income
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .ToListAsync();
            var resultList = dataList.Select(x => x.Change()).ToList();
            return new IncomeListRespModel
            {
                IncomeList = resultList
            };
        }

        public async Task<IncomeListRespModel> GetIncomeList
            (int pageNo, int pageSize, string searchValue)
        {
            IncomeListRespModel responseList = new IncomeListRespModel();
            try
            {
                var query = _context
                    .Income
                    .AsNoTracking()
                    .Where(x => x.IsDelete == false);

                //searching
                if (!searchValue.IsNullOrEmpty())
                {
                    searchValue = searchValue.Trim().ToLower();
                    query = query.Where(x =>
                        x.IncomeName.ToLower()
                            .Contains(searchValue));
                }

                //Ordering
                query = query.OrderByDescending(x => x.IncomeId);

                var dataList = await query
                    .Pagination(pageNo, pageSize)
                    .ToListAsync();

                var modelList = dataList.Select(x => x.Change()).ToList();

                PageSetting pageSetting = await query
                    .ExecutePageSetting(pageNo, pageSize, searchValue);

                var totalIncome = await GetTotalIncome();

                responseList = new IncomeListRespModel
                {
                    IncomeList = modelList,
                    PageSetting = pageSetting,
                    TotalIncome = totalIncome.ToString("N2"),
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
            }

            return responseList;
        }

        public DataTableResponseModel<IncomeRespModel> GetIncomeList(
            DataTableRequestModel dt)
        {
            var query = _context
                .Income
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.IncomeId)
                .AsQueryable();

            string searchValue = dt.SearchValue;
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(m =>
                    m.IncomeName.Contains(searchValue) ||
                    m.IncomeAmount.ToString().Contains(searchValue));
            }

            var response = query.Execute(dt);
            var result = response.Change();

            return result;
        }

        public async Task<int> Save(IncomeReqModel model)
        {
            try
            {
                IncomeDataModel data = model.Change();
                data.CreatedDate = DateTime.Now;

                await _context.Income.AddAsync(data);
                int result = await _context.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

        public async Task<int> Update(long id, IncomeReqModel model)
        {
            var data = await _context
                .Income
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsDelete == false && x.IncomeId == id);
            if (data == null) return 0;

            data.IncomeName = model.IncomeName;
            data.IncomeAmount = model.IncomeAmount;
            data.ModifiedDate = DateTime.Now;

            _context.Income.Update(data);
            _context.Entry(data).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(long id)
        {
            if (await IsIncomeIdUsed(id)) return -2;
            var data = await _context
                .Income
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsDelete == false && x.IncomeId == id);
            if (data == null) return 0;

            data.ModifiedDate = DateTime.Now;
            data.IsDelete = true;

            _context.Income.Update(data);
            _context.Entry(data).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Generate()
        {
            for (int i = 1000; i >= 1; i--)
            {
                Random random = new Random();
                await _context.Income.AddAsync(new IncomeDataModel
                {
                    IncomeName = i.ToString(),
                    IncomeAmount = random.Next(1000, 1000000),
                    CreatedDate = DateTime.Now.AddDays(-1 * i)
                });
            }

            return await _context.SaveChangesAsync();
        }

        private async Task<decimal> GetTotalIncome()
        {
            decimal result = 0;
            try
            {
                result = await _context
                    .Income
                    .AsNoTracking()
                    .SumAsync(x => x.IncomeAmount);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return result;
        }

        private async Task<bool> IsIncomeIdUsed(long incomeId)
        {
            return await _context
                .Expense
                .AsNoTracking()
                .AnyAsync(x =>
                    x.IsDelete == false &&
                    x.IncomeId == incomeId);
        }
    }
}