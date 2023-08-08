using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepoDb.Exceptions;
using RepoDb.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly EFDbContext _dbContext;

        public BlogController(ILogger<BlogController> logger, EFDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetBlogs(int pageNo, int pageSize)
        {
            //int skipRowCount = (pageNo - 1) * pageSize;
            //var blogList = await _dbContext
            //    .Blogs
            //    .AsNoTracking()
            //    .Skip(skipRowCount)
            //    .Take(pageSize)
            //    .ToListAsync();

            var blogList = await _dbContext
                .Blogs
                .AsNoTracking()
                .ToPagination(pageNo, pageSize)
                .ToListAsync();

            ResponseModel response = BaseResponseModel.GetSuccess(blogList);
            //_logger.LogInformation(response.ToJson(true));
            _logger.LogWarning(response.ToJson(true));
            return Ok(response.ToJson());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            if (id == 0) return BadRequest();

            var data = await _dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (data == null) return NotFound();

            ResponseModel response = BaseResponseModel.GetSuccess(data);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostItem([FromBody] BlogDataModel item)
        {
            if (item == null) return BadRequest();

            await _dbContext.Blogs.AddAsync(item);
            int result = await _dbContext.SaveChangesAsync();

            ResponseModel response = result > 0 ?
                BaseResponseModel.GetSuccess("Blog Create Success.") : BaseResponseModel.GetError();

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem([FromBody] BlogDataModel model, int id)
        {
            if (model == null || id == 0) return BadRequest();

            var data = await _dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (data == null) return NotFound();

            data.Blog_Title = model.Blog_Title;
            data.Blog_Author = model.Blog_Author;
            data.Blog_Content = model.Blog_Content;

            _dbContext.Blogs.Update(data);
            _dbContext.Entry(data).State = EntityState.Modified;
            int result = await _dbContext.SaveChangesAsync();

            ResponseModel response = result > 0 ?
                BaseResponseModel.GetSuccess("Blog Update Success") : BaseResponseModel.GetError();

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchItem([FromBody] BlogDataModel model, int id)
        {
            if (model == null || id == 0) return BadRequest();

            var data = await _dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (data == null) return NotFound();

            if (!model.Blog_Title.IsNullOrEmpty())
                data.Blog_Title = model.Blog_Title;

            if (!model.Blog_Author.IsNullOrEmpty())
                data.Blog_Author = model.Blog_Author;

            if (!model.Blog_Content.IsNullOrEmpty())
                data.Blog_Content = model.Blog_Content;

            _dbContext.Blogs.Update(data);
            _dbContext.Entry(data).State = EntityState.Modified;
            int result = await _dbContext.SaveChangesAsync();

            ResponseModel response = result > 0 ?
                BaseResponseModel.GetSuccess("Blog Patch Success") : BaseResponseModel.GetError();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            var data = await _dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == id);

            if (data == null) return NotFound();

            _dbContext.Blogs.Remove(data);
            _dbContext.Entry(data).State = EntityState.Deleted;
            int result = await _dbContext.SaveChangesAsync();

            ResponseModel response = result > 0 ?
                BaseResponseModel.GetSuccess("Blog Delete Success") : BaseResponseModel.GetError();

            return Ok(response);
        }
    }
}
