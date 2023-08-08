using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Text;
using System.Text.Json.Serialization;

namespace HPPMDotNetCore.DbService
{
    public class AdoDotNetService
    {
        public int Execute(string query)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.Text;
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }

        //DataSet
        //DataTable
        //DataRow
        //DataColumn

        public DataSet GetLists(string query)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            connection.Close();

            return ds;
        }

        public DataTable GetList(string query)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            connection.Close();

            return dt;
        }

        public DataRow GetItem(string query)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            connection.Close();

            return dt.Rows[0];
        }

        public T GetItem<T>(string query)
        {
            //using SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            //connection.Open();

            //SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adp.Fill(dt);

            //connection.Close();

            //var jsonStr = JsonConvert.SerializeObject(dt);
            //T item = JsonConvert.DeserializeObject<List<T>>(jsonStr)[0]; // may be array index out of bound exception

            //return item;
            var lst = GetList<T>(query);

            return (lst == null || lst.Count == 0) ? default : lst[0];
        }

        public List<T> GetList<T>(string query)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            connection.Close();

            var jsonStr = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr);

            return lst;
        }

        public List<T> GetList<T>(string query, IDictionary<string, object> parameters = null)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            connection.Close();

            var jsonStr = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr);

            return lst;
        }

        //for stored procedure
        public List<T> GetList<T>(string query,
            IDictionary<string, object> parameters = null,
            CommandType cmdType = CommandType.Text)
        {
            using SqlConnection connection = Config.CreateConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = cmdType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            connection.Close();

            var jsonStr = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(jsonStr);

            return lst;
        }
    }
}
