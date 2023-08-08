using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using System;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features.Dashboard;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public class IncomeController : BaseController
    {
        private readonly IIncomeService _iIncomeService;

        public IncomeController(
            IHubContext<BalanceHub> hub,
            IDashboardService dashboardService,
            IIncomeService iIncomeService
        ) :
            base(hub, dashboardService)
        {
            _iIncomeService = iIncomeService;
        }

        public async Task<IActionResult> GenerateIncome()
        {
            await _iIncomeService.Generate();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IncomePagination(
            int pageNo = 1, int pageSize = 10, string searchValue = "")
        {
            var model = await _iIncomeService
                .GetIncomeList(pageNo, pageSize, searchValue);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IncomeList(
            int pageNo = 1,
            int pageSize = 10,
            string searchValue = ""
        )
        {
            var model = await _iIncomeService
                .GetIncomeList(pageNo, pageSize, searchValue);
            return Json(model);
        }

        [ActionName("IncomeList")]
        [HttpPost]
        public IActionResult GetIncomeList()
        {
            DataTableResponseModel<IncomeRespModel> response =
                new DataTableResponseModel<IncomeRespModel>();
            try
            {
                var dt = GetDataTableFormDataFromRequest();
                response = _iIncomeService.GetIncomeList(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(response);
        }

        public IActionResult IncomeIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveIncome(IncomeReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                model.UserId = 1;
                int result = await _iIncomeService.Save(model);
                response = result > 0
                    ? Base.GetSuccess("Income is created successfully !")
                    : Base.GetError("Income creation error!");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = Base.GetError("Something went wrong. Please try again later.");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> EditIncome(string id)
        {
            IncomeReqModel model = new IncomeReqModel();
            try
            {
                bool isInt = int.TryParse(id, out int incomeId);
                IncomeRespModel resp = await _iIncomeService.GetIncome(incomeId);
                model = resp.Change();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(model);
        }

        [HttpPost]
        [ActionName("UpdateIncome")]
        public async Task<IActionResult> UpdateIncome(string id, IncomeReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool isInt = int.TryParse(id, out int incomeId);

                int result = await _iIncomeService.Update(incomeId, model);

                response = result > 0
                    ? Base.GetSuccess("Income is updated successfully!")
                    : Base.GetSuccess("Error in income updating!");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = Base.GetError("Something went wrong. Please try again later !");
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIncome(string id)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool isInt = int.TryParse(id, out int incomeId);
                int result = await _iIncomeService.Delete(incomeId);

                if (result == -2) //Used in expensed
                {
                    response = Base
                        .GetError("Income is used in expense. " +
                                  "Please delete that expense and try again.");
                    return Json(response);
                }

                response = result > 0
                    ? Base.GetSuccess("Income is deleted successfully!")
                    : Base.GetError("Income deletion error!");

                await SendCurrentBalance();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response = Base.GetError("Someting went wrong. Please try again later!");
            }

            return Json(response);
        }
    }
}