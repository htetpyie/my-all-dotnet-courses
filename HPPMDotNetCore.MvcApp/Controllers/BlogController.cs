using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPPMDotNetCore.MvcApp.Controllers
{
    public class BlogController : BaseController
    {
        private readonly EFDbContext _dbContext;

        public BlogController(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //https://localhost:3000/blog
        //https://localhost:3000/blog/index
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            return View("BlogIndex");
        }

        [ActionName("List")]
        public async Task<IActionResult> BlogList(int pageNo = 1, int pageSize = 10)
        {
            var blogList = await _dbContext
               .Blogs
               .AsNoTracking()
               .OrderByDescending(x => x.Blog_Id)
               .ToPagination(pageNo, pageSize)
               .ToListAsync();

            //ResponseModel model = BaseResponseModel.GetSuccess(blogList);

            BlogListResponseModel model = new BlogListResponseModel();
            model.Resopnse = BaseResponseModel.GetSuccess();
            model.Blogs = blogList;
            return View("BlogList", model);
        }

        //Using Jquery Datatable
        [ActionName("blogTable")]
        public IActionResult GetBlogs()
        {
            var query = _dbContext.Blogs.AsNoTracking();
            var dt = GetDataTableFormDataFromRequest();

            string searchValue = dt.SearchValue;
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(m => 
                        m.Blog_Title.Contains(searchValue) ||
                        m.Blog_Author.Contains(searchValue) ||
                        m.Blog_Content.Contains(searchValue));
            }

            var model = query.Execute(dt);
            return Ok(model);
        }

        [ActionName("Create")]
        public IActionResult CreateBlog()
        {
            return View("CreateBlog");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> SaveBlog(BlogDataModel blogDataModel)
        {
            await _dbContext.Blogs.AddAsync(blogDataModel);
            int result = await _dbContext.SaveChangesAsync();

            //return View("CreateBlog");
            return Redirect("/Blog/List");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> EditBlog(string id)
        {
            bool isInt = int.TryParse(id, out int getById);
            var model = await _dbContext
               .Blogs
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Blog_Id == getById);
            if (model == null)
                return Redirect("/Blog/List");

            return View("EditBlog", model);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateBlog(string id, BlogDataModel blogDataModel)
        {
            bool isInt = int.TryParse(id, out int getById);
            if (blogDataModel == null || getById == 0)
                return Redirect("/Blog/List");

            var model = await _dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == getById);

            if (model == null)
                return Redirect("/Blog/List");

            model.Blog_Title = blogDataModel.Blog_Title;
            model.Blog_Author = blogDataModel.Blog_Author;
            model.Blog_Content = blogDataModel.Blog_Content;

            _dbContext.Blogs.Update(model);
            _dbContext.Entry(model).State = EntityState.Modified;
            int result = await _dbContext.SaveChangesAsync();
            return Redirect("/Blog/List");
        }

        public async Task<IActionResult> DeleteBlog(string id)
        {
            bool isInt = int.TryParse(id, out int getById);
            var model = await _dbContext
               .Blogs
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Blog_Id == getById);
            if (model == null)
                return Redirect("/Blog/List");

            _dbContext.Blogs.Remove(model);
            _dbContext.Entry(model).State = EntityState.Deleted;
            int result = await _dbContext.SaveChangesAsync();
            return Redirect("/Blog/List");
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteBlogById(string id)
        {
            bool isInt = int.TryParse(id, out int getById);
            var model = await _dbContext
               .Blogs
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Blog_Id == getById);
            if (model == null)
                return Redirect("/Blog/List");

            _dbContext.Blogs.Remove(model);
            _dbContext.Entry(model).State = EntityState.Deleted;
            int result = await _dbContext.SaveChangesAsync();
            return Json(new { IsSuccess = result > 0 });
        }
    }
}
