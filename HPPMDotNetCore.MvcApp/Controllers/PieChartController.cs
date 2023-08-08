using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.MvcApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class PieChartController : Controller
    {
        private readonly EFDbContext _dbContext;
        private readonly IHubContext<ChatHub> _hubcontext;

        public PieChartController(EFDbContext dbContext, IHubContext<ChatHub> hubcontext)
        {
            _hubcontext = hubcontext;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Report()
        {
            var model = await GetPieChart();
            return View(model);
        }

        private async Task<PieChartResponseModel> GetPieChart()
        {
            var lst = await _dbContext.PieCharts.AsNoTracking().ToListAsync();
            var labels = lst.Select(x => x.PieChartLabel).ToList();
            var series = lst.Select(x => x.PieChartData).ToList();
            PieChartResponseModel model = new PieChartResponseModel
            {
                Labels = labels,
                Series = series
            };
            return model;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        public IActionResult CreatePieChart()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> SavePieChart(PieChartDataModel pieChart)
        {
            await _dbContext.PieCharts.AddAsync(pieChart);
            int result = await _dbContext.SaveChangesAsync();

            var model = await GetPieChart();

            await _hubcontext.Clients.All.SendAsync("ReceiveChart", model.ToJson());
            return Json(pieChart);
        }
    }
}
