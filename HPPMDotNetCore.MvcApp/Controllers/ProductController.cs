using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class ProductController : BaseController
    {

        private readonly EFDbContext _dbContext;

        public ProductController(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult ProductIndex()
        {
            return View();
        }

        [ActionName("productList")]
        public IActionResult ProductList()
        {
            var query = _dbContext.Products.AsNoTracking();
            var dt = GetDataTableFormDataFromRequest();

            string searchValue = dt.SearchValue;
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(m =>
                        m.ProductCode.Contains(searchValue) ||
                        m.ProductName.Contains(searchValue) ||
                        m.ProductPrice.ToString().Contains(searchValue));
            }

            var model = query.Execute(dt);
            return Ok(model);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public async Task<IActionResult> Save(ProductDataModel model)
        {
            await _dbContext.Products.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            
            return Json(model);
        }

        public async Task<ProductListResponseModel> GetProductList()
        {
            List<ProductDataModel> productList = await _dbContext
                           .Products.AsNoTracking().ToListAsync();
            ProductListResponseModel response = new ProductListResponseModel
            {
                ProductList = productList
            };
            return response;
        }
    }
}
