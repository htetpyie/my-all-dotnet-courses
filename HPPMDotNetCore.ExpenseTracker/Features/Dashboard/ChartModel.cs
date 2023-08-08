using System.Collections.Generic;
using System.Net;

namespace HPPMDotNetCore.ExpenseTracker.Features.Dashboard
{
    public class ChartModel
    {
        public string Label { get; set; }
        public decimal Value { get; set; } 
    }

    public class PieChartRespModel
    {
        public List<ChartModel> Data { get; set; }
    }

    public class ExpenseChartRespModel
    {
        public List<decimal> Series { get; set; }
        public List<string> Labels { get; set; }
    }
}
