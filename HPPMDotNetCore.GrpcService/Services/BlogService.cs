using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace HPPMDotNetCore.GrpcService.Services
{
    public class BlogService : BlogApiService.BlogApiServiceBase
    {
        private readonly EFDbContext _db;

        public BlogService(EFDbContext db)
        {
            _db = db;
        }

        public override async Task<BlogResponseReply> GetBlogs(BlogPageRequest request, ServerCallContext context)
        {
            var blogList = await _db
                  .Blogs
                  .AsNoTracking()
                  .OrderByDescending(x => x.Blog_Id)
                  .ToPagination(request.PageNo, request.PageSize)
                  .ToListAsync();
            BlogResponseReply responseReply = new BlogResponseReply();
            responseReply.Blogs.AddRange(blogList.Select(x => new BlogReply
            {
                BlogAuthor = x.Blog_Author,
                BlogContent = x.Blog_Content,
                BlogId = x.Blog_Id,
                BlogTitle = x.Blog_Title,
            }));
            return responseReply;
        }

        public override async Task<BlogReply> AddBlog(BlogRequest request, ServerCallContext context)
        {
            BlogDataModel blogDataModel = new BlogDataModel
            {
                Blog_Author = request.BlogAuthor,
                Blog_Content = request.BlogContent,
                Blog_Title = request.BlogTitle
            };
            await _db.Blogs.AddAsync(blogDataModel);
            int result = await _db.SaveChangesAsync();
            BlogReply reply = new BlogReply()
            {
                BlogId = blogDataModel.Blog_Id,
                BlogTitle = blogDataModel.Blog_Title,
                BlogAuthor = blogDataModel.Blog_Author,
                BlogContent = blogDataModel.Blog_Content,
            };

            return reply;
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
