using HPPMDotNetCore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.DbService
{
    public class RepoDBService
    {
        private SqlConnection con { get => Config.CreateConnection();}    

        public RepoDBService()
        {
            GlobalConfiguration.Setup().UseSqlServer();
        }
        
        public async Task<T> GetItemAsync<T>(int id) where T : class
        {
            var item =  await con.QueryAsync<T>(id);
            return item.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAsync<T>()where T: class
        {
            var itemList = await con.QueryAllAsync<T>();
            return itemList;
        }

        public async Task<int> CreateAsync<T>(T model) where T: class
        {
            object result = await con.InsertAsync<T>(model);
            return result != default ? 1 : -1;
        }

        public async Task<int> UpdateAsync<T>(T model) where T: class
        {
            int result = await con.UpdateAsync<T>(model);
            return result;
        }

        public async Task<int> DeleteAsync<T>(T model) where T : class
        {
            int result = await con.DeleteAsync<T>(model);
            return result;
        }

        public async Task<IEnumerable<T>> ExecuteAsync<T>(string query, 
            object param = null, 
            CommandType cmdType = CommandType.Text
            ) where T: class
        {
            var result = await  con.ExecuteQueryAsync<T>(query, param, commandType: cmdType);
            return result;
        }

        public async Task<int> ExecuteNoQueryAsync(string query , object param = null, 
            CommandType cmdType = CommandType.Text)
        {
            int result =  await con.ExecuteNonQueryAsync(query, param, commandType: cmdType);
            return result;
        }

    }
}
