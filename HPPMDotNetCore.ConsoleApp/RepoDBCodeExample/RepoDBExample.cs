using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HPPMDotNetCore.ConsoleApp.RepoDBCodeExample
{
    public class RepoDBExample
    {
        private readonly RepoDBService _repoDBService;

        public RepoDBExample()
        {
            _repoDBService ??= new RepoDBService();
        }

        public async void RunAsync()
        {
            //Get All
            var items = await  _repoDBService.GetAsync<BlogDataModel>();

            //Get Item
            var item = await _repoDBService.GetItemAsync<BlogDataModel>(1);

            // insert
            BlogDataModel blogDataModel = BlogDataModel.Create();
            await _repoDBService.CreateAsync(blogDataModel);

            //update
            BlogDataModel update = BlogDataModel.Update();
            await _repoDBService.UpdateAsync<BlogDataModel>(update);

            //delete 
            await _repoDBService.DeleteAsync<BlogDataModel>(update);

            //Execute
            string query = "select * from tbl_blog";
            var result = await _repoDBService.ExecuteAsync<BlogDataModel>(query);

            //Execute No Query
            string dleteQuery = "delete from tbl_blog where blog_id = 2";
            await _repoDBService.ExecuteNoQueryAsync(dleteQuery);

        }
    }
}
