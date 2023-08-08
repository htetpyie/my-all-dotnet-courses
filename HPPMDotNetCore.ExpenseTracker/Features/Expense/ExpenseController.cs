using HPPMDotNetCore.ExpenseTracker.Features.Income;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features.Dashboard;
using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;

namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService _iExpenseService;
        private readonly IIncomeService _iIncomeService;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController
        (
            IExpenseService iExpenseService,
            IIncomeService iIncomeService,
            ILogger<ExpenseController> logger,
            IHubContext<BalanceHub> hub,
            IDashboardService dashboardService)
            : base(hub, dashboardService)
        {
            _iExpenseService = iExpenseService;
            _iIncomeService = iIncomeService;
            _logger = logger;
        }

        public IActionResult ExpenseIndex()
        {
            return View();
        }

        public async Task<IActionResult> GenerateExpense()
        {
            await _iExpenseService.GenerateExpense();
            return View();
        }
        public async Task<IActionResult> GetExpenseList(int pageNo, int pageSize)
        {
            ExpenseListRespModel respList = new ExpenseListRespModel();
            try
            {
                respList = await _iExpenseService.GetExpenseList(pageNo, pageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Json(respList);
        }

        [ActionName("ExpenseList")]
        [HttpPost]
        public IActionResult GetExpenseList()
        {
            DataTableResponseModel<ExpenseRespModel> response =
                new DataTableResponseModel<ExpenseRespModel>();
            var dt = GetDataTableFormDataFromRequest();
            try
            {
                response = _iExpenseService.GetExpenseList(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in");
                _logger.LogError("Exception in Exepense List =>" + ex.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveExpense(ExpenseReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                int result = await _iExpenseService.Save(model);

                response = result > 0
                    ? Base.GetSuccess("Expense is created successfully.")
                    : Base.GetError("Expense creation error.");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                Base.GetError("Something went wrong. Please try again later");
            }

            return Json(response);
        }

        public async Task<IActionResult> EditExpense(string id)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool isInt = int.TryParse(id, out int expenseId);
                ExpenseRespModel resp = await _iExpenseService.GetExpense(expenseId);
                ExpenseReqModel model = resp.Change();

                response = (model == null)
                    ? Base.GetError("Expense can't be found.")
                    : Base.GetSuccess("", "", model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response = Base.GetError("Something went wrong. Please try again later.");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExpense(string id, ExpenseReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();

            try
            {
                bool isInt = int.TryParse(id, out int ExpenseId);

                int result = await _iExpenseService.Update(ExpenseId, model);
                response = result > 0
                    ? Base.GetSuccess("Expense is updated successfully.")
                    : Base.GetError("Expense updating error.");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response = Base.GetError("Something went wrong! Please try again later.");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool isInt = int.TryParse(id, out int ExpenseId);

                int result = await _iExpenseService.Delete(ExpenseId);
                response = result > 0
                    ? Base.GetSuccess("Expense is deleted successfully.")
                    : Base.GetError("Expense deletion error.");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response = Base.GetError("Someting went wrong. Please try again later.");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetIncomeList(int pageNo, int pageSize, string searchValue = "")
        {
            var response = new IncomeListRespModel();
            try
            {
                response = await _iIncomeService.GetIncomeList(pageNo, pageSize, searchValue);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Json(response);
        }

        public async Task<IActionResult> GetIncome(string incomeId)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool isLong = long.TryParse(incomeId, out long id);
                var incomeRespModel = await _iIncomeService.GetIncome(id);
                if (incomeRespModel == null)
                {
                    response = Base.GetError("Income could not be found.");
                }
                else
                {
                    string incomeName = await _iExpenseService
                        .GetIncomNameAndAmount(incomeRespModel.IncomeId);

                    var income = new
                    {
                        IncomeId = incomeRespModel.IncomeId,
                        incomeName = incomeName
                    };

                    response = Base.GetSuccess("", "", income);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response = Base.GetError("Something went wrong. Please try again later.");
            }

            return Json(response);
        }
    }
}