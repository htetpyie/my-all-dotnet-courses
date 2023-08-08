using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HPPMDotNetCore.ConsoleApp.AdoDotNetCodeExample
{
    public class AdoDotNetExample
    {
        private static AdoDotNetService service;
        public static void Run()
        {
            AdoDotNetService service = new AdoDotNetService();

            // Retrieve
            var lst = service.GetList<BlogDataModel>("select top 10 * from tbl_blog");
            Console.WriteLine(JsonConvert.SerializeObject(lst, Formatting.Indented));

            // Get By Id          
            GetById(200);

            //Create
            Create();

            //Update
            Update();

            //Delete
            Delete();

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddData("@blog_id", "1");

            //IDictionary<string, object> parameters = new Dictionary<string, object>()
            //{
            //    {"@blog_id", "1"  }
            //};
            service.GetList<BlogDataModel>("select top 10 * from tbl_blog where blog_id = @blog_id", parameters);
        }

        public static void GetById(int id = 1)
        {
            var item = service.GetItem<BlogDataModel>(@$"SELECT blog_id,
                                                                blog_title, 
                                                                blog_author, 
                                                                blog_content 
                                                          FROM tbl_blog 
                                                          WHERE blog_id = {id}");
            Console.WriteLine($"Get by id = {id} => {JsonConvert.SerializeObject(item, Formatting.Indented)}");
        }

        public static void Create()
        {
            BlogDataModel model = BlogDataModel.Create();

            string createQuery = @$"INSERT INTO tbl_blog
                                    (blog_title, 
                                     blog_author, 
                                     blog_content)
                                    VALUES( '{model.Blog_Title}',
                                            '{model.Blog_Author}', 
                                            '{model.Blog_Content}' )";

            int creationResult = service.Execute(createQuery);
            Console.WriteLine(creationResult > 0 ? "Creation Succeess !" : "Creation Failed !");
        }

        public static void Update(int id = 0)
        {
            BlogDataModel model = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = "5 Reasons to Travel Solo",
                Blog_Author = "David Lee",
                Blog_Content = "Traveling solo can be  ...",
            };

            string updateQuery = @$"UPDATE tbl_blog
                                    SET blog_title = '{model.Blog_Title}', 
                                        blog_author = '{model.Blog_Author}', 
                                        blog_content = '{model.Blog_Author}' 
                                    WHERE blog_id = {model.Blog_Id}";

            int updateResult = service.Execute(updateQuery);
            Console.WriteLine(updateResult > 0 ? "Update Success !" : "Update Failed !");
        }

        public static void Delete(int id = 0)
        {
            string deleteQuery = $"DELETE FROM tbl_blog WHERE blog_id = {id}";

            int deleteResult = service.Execute(deleteQuery);
            Console.WriteLine(deleteResult > 0 ? "Delete Success !" : "Delete Failed !");
        }
        
        public static List<BlogDataModel> GetListUsingProc(int pageNo = 1, int pageSize = 10)
        {
            const string _porc = "Sp_BlogPagination";
            object obj = new { PageNo = pageNo, PageSize = pageSize };
            Dictionary<string, object> param = obj.ToDictonary();

            var list = service.GetList<BlogDataModel>(_porc,param, CommandType.StoredProcedure);
            Console.WriteLine(list.ToJson(true));
            return list;
        }        
    }
}
