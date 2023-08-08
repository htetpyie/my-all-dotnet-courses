using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using HPPMDotNetCore.MvcApp.Hubs;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class ProductOrderController : Controller
    {
        private readonly EFDbContext _dbContext;
        private readonly DapperService _dapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ProductOrderController
            (EFDbContext dbContext, 
            DapperService dapper, 
            IHubContext<ChatHub> hubContext)
        {
            _dbContext = dbContext;
            _dapper = dapper;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OrderIndex()
        {
            var list = await GetOrderList();
            ProductOrderListResponseModel response = new ProductOrderListResponseModel
            {
                Orders = list
            };
            return View(response);
        }
        public async Task<IActionResult> Save(ProductOrderDataModel model)
        {
            await _dbContext.ProductOrders.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            var list = await GetOrderList();
            var count = await GetOrderCount();

            await _hubContext.Clients.All.SendAsync("RecieveOrderCount", new { count }.ToJson());
            await _hubContext.Clients.All.SendAsync("RecieveOrderList", list.ToJson());

            return Json(new { result = "Success" });
        }

        public async Task<List<OrderModel>> GetOrderList()
        {
            //var list = await (from po in _dbContext.ProductOrders
            //           join p in _dbContext.Products on po.ProductId equals p.ProductId
            //           select new OrderModel
            //           {
            //               ProductName = p.ProductName,
            //               ProductPrice =p.ProductPrice,
            //               ProductQuantity = po.ProductQuantity
            //           }).ToListAsync();
            string query = @$"select p.ProductId, 
                                    p.ProductName, 
                                    p.ProductPrice, 
                                    SUM(po.ProductQuantity) ProductQuantity
                            from Tbl_ProductOrder po
                            join Tbl_Product p on po.ProductId = p.ProductId
                            group by p.ProductId, p.ProductName, p.ProductPrice
                            having SUM(po.ProductQuantity) > 0
                            order by SUM(po.ProductQuantity) desc";

            var list = await _dapper.GetAsync<OrderModel>(query);
            return list;
        }

        [HttpGet]
        public async Task<int> GetOrderCount()
        {
            int count = await _dbContext
                        .ProductOrders
                        .AsNoTracking()
                        .SumAsync(x => x.ProductQuantity);
            return count;
        }
    }
}
