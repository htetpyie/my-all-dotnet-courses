using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Services
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public DapperService(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("DbConnection");
        //}

        // Get List
        public async Task<List<T>> GetAsync<T>(string queryOrProc,
            object param = null,
            CommandType cmdType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = await db.QueryAsync<T>(queryOrProc, param, commandType: cmdType);
            return lst.ToList();
        }

        //Get Item
        public async Task<T> GetFirstOrDefaultAsync<T>(string query, object param = null,
            CommandType cmdType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = await db.QueryFirstOrDefaultAsync<T>(query, param, commandType: cmdType);
            return item;
        }

        // Execute
        public async Task<int> ExecuteAsync(string query, object param = null,
            CommandType cmdType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var result = await db.ExecuteAsync(query, param, commandType: cmdType);
            return result;
        }
    }
}
