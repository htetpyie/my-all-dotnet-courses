using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features.Income;

namespace HPPMDotNetCore.ExpenseTracker.Features.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardService dashboardService,
            ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CurrentBalance()
        {
            string balance = "0.00";
            try
            {
                balance = _dashboardService.GetCurrentBalance();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(balance);
        }

        [HttpPost]
        public async Task<IActionResult> GetIncomeInCurrentMonth()
        {
            List<IncomeRespModel> incomeList = new List<IncomeRespModel>();
            try
            {
                incomeList = await _dashboardService.GetIncomeInCurrentMonth();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(incomeList);
        }

        [HttpPost]
        public async Task<IActionResult> GetExpenseInCurrentMonth()
        {
            List<ExpenseRespModel> expenseList = new List<ExpenseRespModel>();
            try
            {
                expenseList = await _dashboardService.GetExpenseInCurrentMonth();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Json(expenseList);
        }

        [HttpPost]
        public async Task<IActionResult> GetTop5MaxIncome()
        {
            PieChartRespModel result = new PieChartRespModel();
            try
            {
                result = await _dashboardService.GetTop5MaxIncome();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetTop5MaxExpense()
        {
            ExpenseChartRespModel result = new ExpenseChartRespModel();
            try
            {
                result = await _dashboardService.GetTop5MaxExpense();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(result);
        }

        public async Task<IActionResult> GetLatestTransaction()
        {
            List<RecentTransaction> dataList = new List<RecentTransaction>();
            try
            {
                dataList = await _dashboardService.GetRecentTransaction();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(dataList);
        }

        public async Task<IActionResult> GetMonthlyData(int count)
        {
            List<MonthlyDataModel> list = new List<MonthlyDataModel>();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    DateTime date = DateTime.Now.AddMonths(-i);
                    MonthlyDataModel model = await _dashboardService.GetMonthlyData(date);
                    list.Add(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return Json(list);
        }
    }
}