using HPPMDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.Xml;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult PieChart()
        {
            PieChartResponseModel model = new PieChartResponseModel
            {
                Labels = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" },
                Series = new List<int> { 44, 55, 13, 43, 22 }
            };
            return View(model);
        }

        public IActionResult LineChart()
        {
            var seryA = new SeryModel
            {
                Name = "Series A",
                Data = new List<double> { 1.4, 2, 2.5, 1.5, 2.5, 2.8, 3.8, 4.6 }
            };
            var seryB = new SeryModel
            {
                Name = "Series B",
                Data = new List<double> { 20, 29, 37, 36, 44, 45, 50, 58 }
            };

            LineChartResponseModel model = new LineChartResponseModel
            {
                Series = new List<SeryModel> { seryA, seryB },
                Categories = new List<string> { "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016" }
            };

            return View(model);
        }

        public IActionResult AreaChart()
        {

            SeryModel sery1 = new SeryModel
            {
                Name = "Series 1",
                Data = new List<double> { 45, 52, 38, 45, 19, 23, 2 }
            };
            AreaChartResponseModel model = new AreaChartResponseModel
            {
                Series = new List<SeryModel> { sery1 },
                Categories = new List<string> { "01 Jan", "02 Jan", "03 Jan", "04 Jan", "05 Jan", "06 Jan", "07 Jan" }
            };
            return View(model);
        }

        public IActionResult ColumnChart()
        {
            SeryModel sery1 = new SeryModel
            {
                Name = "Net Profit",
                Data = new List<double> { 44, 55, 57, 56, 61, 58, 63, 60, 66 }
            };

            SeryModel sery2 = new SeryModel
            {
                Name = "Revenue",
                Data = new List<double> { 76, 85, 101, 98, 87, 105, 91, 114, 94 }
            };

            SeryModel sery3 = new SeryModel
            {
                Name = "Free Cash Flow",
                Data = new List<double> { 35, 41, 36, 26, 45, 48, 52, 53, 41 }
            };

            ColumnChartResponseModel response = new ColumnChartResponseModel
            {
                Series = new List<SeryModel> { sery1, sery2, sery3 },
                Categories = new List<string> { "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct" }
            };
            return View(response);
        }

        public IActionResult BarChart()
        {
            BarSeryModel sery1 = new BarSeryModel
            {
                Data = new List<int> { 44, 55, 41, 64, 22, 43, 21 }
            };
            BarSeryModel sery2 = new BarSeryModel
            {
                Data = new List<int> { 53, 32, 33, 52, 13, 44, 32 }
            };

            BarChartResponseModel response = new BarChartResponseModel
            {
                Series = new List<BarSeryModel> { sery1, sery2 },
                Categories = new List<string> { "2001", " 2002", " 2003", " 2004", " 2005", " 2006", " 2007" }
            };

            return View(response);
        }

        public IActionResult BoxPlotChart()
        {
            return View();
        }
    }
}
