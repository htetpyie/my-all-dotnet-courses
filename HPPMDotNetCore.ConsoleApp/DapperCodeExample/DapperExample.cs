using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using HPPMDotNetCore.Queries;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ConsoleApp.DapperCodeExample
{
    public class DapperExample
    {
        private static DapperService service;

        public static void CreateDapperService()
        {
            if (service == null) 
                service = new DapperService(Config.CreateConnection().ConnectionString);
        }

        public static async Task RunAsync()
        {
            //if (service == null) service = new DapperService(Config.SqlConnectionStringBuilder);
            CreateDapperService();

            BlogDataModel model = new BlogDataModel
            {
                Blog_Title = "The Benefits of Meditation",
                Blog_Author = "Sarah Johnson",
                Blog_Content = "Meditation is a practice that ...",
            };
            #region Examples
            //string createQuery = @$"INSERT INTO tbl_blog
            //                        (blog_title, 
            //                         blog_author, 
            //                         blog_content)
            // //                        VALUES( '{model.Blog_Title}',
            // //                                '{model.Blog_Author}', 
            // //                                '{model.Blog_Content}' )";

            //// await service.ExecuteAsync(createQuery);

            // string createQuery2 = @$"INSERT INTO tbl_blog
            //(blog_title, 
            // blog_author, 
            // blog_content)
            //VALUES( '@Blog_Title',
            //        '@Blog_Author', 
            //        '@Blog_Content' )";

            //await service.ExecuteAsync(createQuery2, new
            //{
            //    Blog_Title = model.Blog_Title,
            //    Blog_Author = model.Blog_Author,
            //    Blog_Content = model.Blog_Content
            //});
            #endregion

            //GetAll
            await GetAsync();
            //Pagination 
            await GetAsync(2, 10);
            //Get Item
            await GetItemAsync(12);
            // Create
            await CreateAsync();
            //update
            await UpdateAsync();
            //Delete
            await DeleteAsync();

            //Using Procedures
            await GetByProcAsync();
            await GetItemByProcAsync();
            await CreateByProcAsync();
            await UpdateByProcAsync();
            await DeleteByProcAsync();

        }        

        //Get all
        public static async Task<List<BlogDataModel>> GetAsync()
        {
            string query = BlogQuery.GetListQuery;
            var list = await service.GetAsync<BlogDataModel>(query);

            Console.WriteLine($"The list {list.ToJson(true)} ");
            return list;
        }

        // Pagination
        public static async Task<List<BlogDataModel>> GetAsync(int pageNo = 1, int pageSize = 10)
        {
            int skip = (pageNo - 1) * pageSize;
            string query = @"select * from tbl_blog 
                              order by blog_id desc
                              offset @skip rows
                              fetch next @pageSize rows only";
            object param = new
            {
                skip = skip,
                pageSize = pageSize,
            };
               
            var list = await service.GetAsync<BlogDataModel>(query, param);

            Console.WriteLine(@$"The list of pageNo {pageNo}, pageSize {pageSize} is 
                            {list.ToJson(true)} ");
            return list;
        }

        // Get Item
        public static async Task<BlogDataModel> GetItemAsync(int id = 0)
        {
            string query = BlogQuery.GetItemQuery;
            var item = await service.GetFirstOrDefaultAsync<BlogDataModel>(query, new {blog_id = id });
            Console.WriteLine($"Get By id = {id} is {item.ToJson(true)} ");
            return item;
        }

        //Create
        public static async Task<int> CreateAsync()
        {
            BlogDataModel model = BlogDataModel.Create();
            string query = BlogQuery.InsertQuery;

            object param = new
            {
                Blog_Title = model.Blog_Title,
                Blog_Author = model.Blog_Author,
                Blog_Content = model.Blog_Content
            };

            int result = await service.ExecuteAsync(query, param);
            Console.WriteLine(result > 0 ? "Create Success" : "Create Failed");
            return result;
        }

        // Update
        public static async Task<int> UpdateAsync()
        {
            BlogDataModel model = BlogDataModel.Update();
            string query = BlogQuery.UpdateQuery;

            object param = new
            {
                Blog_Title = model.Blog_Title,
                Blog_Author = model.Blog_Author,
                Blog_Content = model.Blog_Content,
                Blog_Id = model.Blog_Id
            };

            int result = await service.ExecuteAsync(query, param);
            Console.WriteLine(result > 0 ? "Update Success" : "Update Failed");
            return result;
        }

        // Delete
        public static async Task<int> DeleteAsync()
        {
            int blog_id = 12;
            string query =  BlogQuery.DeleteQuery;
            object param = new { id = blog_id };

            int result = await service.ExecuteAsync(query, param);
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
            return result;
        }
    
        // Select All StoredPorcedure
        public static async Task<List<BlogDataModel>> GetByProcAsync()
        {
            const string _proc = "Sp_BlogSelect";
            var list = await service.GetAsync<BlogDataModel>(_proc, CommandType.StoredProcedure);
            
            Console.WriteLine($"The list executed by stored procedure is \n {list.ToJson(true)} ");
            return list;
        }
        
        //Get Item Using StoredPorcedure
        public static async Task<BlogDataModel> GetItemByProcAsync()
        {
            int id = 1;
            const string _proc = "Sp_BlogGetItem";
            object param = new { Blog_Id = id };

            var item = await service.GetFirstOrDefaultAsync<BlogDataModel>(_proc, param, CommandType.StoredProcedure);
            Console.WriteLine($"Get By id is {item.ToJson(true)} ");
            return item;
        }

        // Create Using StoredPorcedure
        private async static Task<int> CreateByProcAsync()
        {
            BlogDataModel model = BlogDataModel.Create();
            const string _proc = "Sp_BlogCreate";
            object param = new
            {
                @Blog_Title = model.Blog_Title,
                @Blog_Author = model.Blog_Author,
                @Blog_Content = model.Blog_Content,
            };
            
            int result = await service.ExecuteAsync(_proc, param, CommandType.StoredProcedure);
            Console.WriteLine(result > 0 ? "Create Success" : "Create Failed");
            return result;
        }
        
        // Update Using Stored Proc
        private async static Task<int> UpdateByProcAsync()
        {
            BlogDataModel model = BlogDataModel.Update();
            const string _proc = "Sp_BlogUpdate";
            object param = new
            {
                Blog_Title = model.Blog_Title,
                Blog_Author = model.Blog_Author,
                Blog_Content = model.Blog_Content,
                Blog_Id = model.Blog_Id
            };

            int result = await service.ExecuteAsync(_proc, param, CommandType.StoredProcedure);
            Console.WriteLine(result > 0 ? "Update Success" : "Update Failed");
            return result;
        }

        // Delete Using Stored Proc
        private async static Task<int> DeleteByProcAsync()
        {
            int id = 2;
            const string _proc = "Sp_BlogUpdate";
            object param = new
            {
                Blog_Id = id
            };

            int result = await service.ExecuteAsync(_proc, param, CommandType.StoredProcedure);
            Console.WriteLine(result > 0 ? "Delete Success" : "Delete Failed");
            return result;
        }
    }
}
