using HPPMDotNetCore.DbService;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ConsoleApp.RetfitClientCodeExample
{
    public class RefitClientExample
    {
        public async Task RunAsync()
        {
            var api = RestService.For<IBlogApi>("https://localhost:44347");
            var lst = await api.GetBlogs(1, 10);
            var item = await api.GetBlog(3);
            item.ToLog();
            lst.ToLog();

            var post = await api.PostBlog(
                new Models.BlogDataModel
                {
                    Blog_Title = "Refit Title",
                    Blog_Author= "Refit Author",
                    Blog_Content = "Refit Content",
                });
            post.ToLog();
        }
    }
}
