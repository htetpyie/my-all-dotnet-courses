using HPPMDotNetCore.Models;
using HPPMDotNetCore.Models.ApiModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.DbService
{
    public interface IBlogApi
    {
        [Get("/api/blog/{pageNo}/{pageSize}")]
        Task<ResponseModel> GetBlogs(int pageNo, int pageSize);

        [Get("/api/blog/{id}")]
        Task<ResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<ResponseModel> PostBlog(BlogDataModel blog);

        [Put("/api/blog/{id}")]
        Task<ResponseModel> PutBlog(int id, BlogDataModel blog);

        [Patch("/api/blog/{id}")]
        Task<ResponseModel> PatchBlog(int id, BlogDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<ResponseModel> DeleteBlog(int id);
    }
}
