using System;
using System.Collections.Generic;

namespace HPPMDotNetCore.ExpenseTracker.Features
{
    public class DataTableRequestModel
    {
        public string Draw { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }

    public class DataTableResponseModel<T>
    {
        public string draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<T> data { get; set; }
    }
}