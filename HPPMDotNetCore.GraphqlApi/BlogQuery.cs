using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPPMDotNetCore.GraphqlApi
{
    public class BlogQuery
    {
        public async Task<List<BlogDataModel>> GetBlogs([FromServices] EFDbContext dbContext, int pageNo, int pageSize)
        {
            var blogList = await dbContext
                .Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id)
                .ToPagination(pageNo, pageSize)
                .ToListAsync();
            return blogList;
        }

        public async Task<BlogDataModel> AddBlog([FromServices] EFDbContext dbContext, BlogDataModel blogDataModel)
        {
            await dbContext.Blogs.AddAsync(blogDataModel);
            int result = await dbContext.SaveChangesAsync();
            return blogDataModel;
        }

        public async Task<BlogDataModel> UpdateBlog(
            [FromServices] EFDbContext dbContext, 
            string id,
            BlogDataModel blogDataModel)
        {
            bool isInt = int.TryParse(id, out int getById);

            if (!isInt)
            {
                return new BlogDataModel();
            }

            var model = await dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == getById);

            if (model == null) return new BlogDataModel();
            
            model.Blog_Title = blogDataModel.Blog_Title;
            model.Blog_Author = blogDataModel.Blog_Author;
            model.Blog_Content = blogDataModel.Blog_Content;

            dbContext.Blogs.Update(model);
            dbContext.Entry(model).State = EntityState.Modified;
            int result = await dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<BlogDataModel> DeleteBLog(
            [FromServices] EFDbContext dbContext, 
            string id)
        {
            bool isInt = int.TryParse(id, out int getById);

            if (!isInt) return new BlogDataModel();
            
            var model = await dbContext
                .Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Blog_Id == getById);
            
            if (model == null) return new BlogDataModel();
            
            dbContext.Blogs.Remove(model);
            dbContext.Entry(model).State = EntityState.Deleted;
            int result = await dbContext.SaveChangesAsync();

            return model;
        }
    }

    public static class DevCode
    {
        public static IQueryable<TSource> ToPagination<TSource>(this IQueryable<TSource> source, int pageNo,
            int pageSize) where TSource : class
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            return source.Skip(skipRowCount).Take(pageSize);
        }
    }
}