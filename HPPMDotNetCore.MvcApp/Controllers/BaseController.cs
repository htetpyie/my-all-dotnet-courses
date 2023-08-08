using HPPMDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class BaseController : Controller
    {
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

        //public DataTableResponseModel<T> GetDataTableResponse<T>(DataTableRequestModel dt,
        //    IQueryable<T> queryData)
        //{
        //    string sotrColumn = dt.SortColumn;
        //    int recordsTotal = 0;
        //    string sortColumnDirection = dt.SortColumnDirection;

        //    if (!sotrColumn.IsNullOrEmpty() && !sortColumnDirection.IsNullOrEmpty())
        //    {
        //        queryData = queryData
        //            .OrderBy(sotrColumn + " " + sortColumnDirection);
        //    }

        //    recordsTotal = queryData.Count();

        //    var data = queryData
        //        .Skip(dt.Skip)
        //        .Take(dt.PageSize)
        //        .ToList();

        //    var jsonData = new DataTableResponseModel<T>
        //    {
        //        draw = dt.Draw,
        //        recordsFiltered = recordsTotal,
        //        recordsTotal = recordsTotal,
        //        data = data
        //    };

        //    return jsonData;
        //}
    }

    public static class BaseControllerExtension
    {
        public static DataTableResponseModel<T> Execute<T>(
            this IQueryable<T> query, DataTableRequestModel dataTableRequestModel)
        {
            //string sortColumn = dataTableRequestModel.SortColumn;
            //string sortColumnDirection = dataTableRequestModel.SortColumnDirection;
            //if (!sortColumn.IsNullOrEmpty() && !sortColumnDirection.IsNullOrEmpty())
            //{
            //    query = query
            //        .OrderBy(sortColumn + " " + sortColumnDirection);
            //}

            int resultCount = query.Count();
            var resultData = query
                .Skip(dataTableRequestModel.Skip)
                .Take(dataTableRequestModel.PageSize)
                .ToList();

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
