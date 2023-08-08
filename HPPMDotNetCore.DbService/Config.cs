using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace HPPMDotNetCore.DbService
{
    public static class Config
    {
        //public static DbConnectionStringBuilder SqlConnectionStringBuilder { get; set; } = new SqlConnectionStringBuilder
        //{
        //    DataSource = ".", // server name
        //    InitialCatalog = "testdb", // database
        //    UserID = "sa",
        //    Password = "sa@123",
        //    TrustServerCertificate = true
        //};

        public static string GetConnectionString()
        {
            return SqlConnectionStringBuilder.ConnectionString;
        }

        public static SqlConnection CreateConnection() =>
            new SqlConnection(GetConnectionString());

        public static SqlConnectionStringBuilder SqlConnectionStringBuilder { get; set; } = new SqlConnectionStringBuilder
        {
            DataSource = "HPPM", // server name
            InitialCatalog = "testdb", // database
            UserID = "sa",
            Password = "111111",
            TrustServerCertificate = true
        };
    }
}
