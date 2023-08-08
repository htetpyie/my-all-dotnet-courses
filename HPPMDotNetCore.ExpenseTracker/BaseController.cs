using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using HPPMDotNetCore.ExpenseTracker.Features;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker.Features.Dashboard;
using HPPMDotNetCore.ExpenseTracker.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HPPMDotNetCore.ExpenseTracker
{
    public class BaseController : Controller
    {
        private readonly IHubContext<BalanceHub> _hubContext;
        private readonly IDashboardService _iDashboardService;

        public BaseController(IHubContext<BalanceHub> hubContext, 
            IDashboardService iDashboardService)
        {
            _hubContext = hubContext;
            _iDashboardService = iDashboardService;
        }

        public BaseController(){}
        protected DataTableRequestModel GetDataTableFormDataFromRequest()
        {
            var draw = Request
                .Form["draw"]
                .FirstOrDefault();
            var start = Request
                .Form["start"]
                .FirstOrDefault();
            var length = Request
                .Form["length"]
                .FirstOrDefault();

            var sortColumn = Request
                .Form["columns[" + Request.Form["order[0][column]"]
                    .FirstOrDefault() + "][name]"]
                .FirstOrDefault();

            var sortColumnDirection = Request
                .Form["order[0][dir]"]
                .FirstOrDefault();

            var searchValue = Request
                .Form["search[value]"]
                .FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            return new DataTableRequestModel
            {
                Draw = draw,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection,
                SearchValue = searchValue,
                Skip = skip,
                PageSize = pageSize
            };
        }

        protected async Task SendCurrentBalance()
        {
            string balance = _iDashboardService.GetCurrentBalance();
            await _hubContext.Clients.All.SendAsync("RecieveBalance", Json(balance));
        }
       
    }

    public static class BaseControllerExtension
    {
        public static DataTableResponseModel<T> Execute<T>
        (
            this IQueryable<T> query,
            DataTableRequestModel dataTableRequestModel
        )
        {
            //string sortColumn = dataTableRequestModel.SortColumn;
            //string sortColumnDirection = dataTableRequestModel.SortColumnDirection;
            //if (!sortColumn.IsNullOrEmpty() && !sortColumnDirection.IsNullOrEmpty())
            //{
            //    query = query
            //        .OrderBy(sortColumn + " " + sortColumnDirection);
            //}

            int resultCount = query.Count();
            List<T> resultData = default;
            if (dataTableRequestModel.PageSize == -1)
            {
                resultData = query.ToList();
            }
            else
            {
                resultData = query
                    .Skip(dataTableRequestModel.Skip)
                    .Take(dataTableRequestModel.PageSize)
                    .ToList();
            }


            var model = new DataTableResponseModel<T>
            {
                draw = dataTableRequestModel.Draw,
                recordsFiltered = resultCount,
                recordsTotal = resultCount,
                data = resultData
            };

            return model;
        }
    }
}