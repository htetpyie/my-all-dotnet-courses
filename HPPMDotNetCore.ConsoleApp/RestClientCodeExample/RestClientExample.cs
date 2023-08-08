using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ConsoleApp.RestClientCodeExample
{
    public class RestClientExample
    {
        private string _url = "https://jsonplaceholder.typicode.com/posts";
        private readonly RestClientService _restService;
        public RestClientExample()
        {
            if(_restService == null)
            {
                _restService = new RestClientService();
            }
            
        }

        public async Task RunAsync()
        {
            string createJson = "{\"userId\": 1,\r\n    \"id\": 10,\r\n    \"title\": \"optio molestias id quia eum\",\r\n    \"body\": \"quo et expedita modi cum officia vel magni\\ndoloribus qui repudiandae\\nvero nisi sit\\nquos veniam quod sed accusamus veritatis error\"\r\n } ";
            PostDataModel createModel = createJson.ToObject<PostDataModel>();
            var result = await _restService.ExecuteAsync<PostDataModel>(this._url, Method.Post, createModel);
            result.ToLog();

            string updateJson = "{\"userId\": 1,\r\n    \"id\": 10,\r\n    \"title\": \"update title\",\r\n    \"body\": \"updated body\"\r\n } ";
            result = await _restService.ExecuteAsync<PostDataModel>($"{this._url}/2", Method.Put,updateJson.ToObject<PostDataModel>());
            result.ToLog();
            
            string patchJson = "{\"userId\": 1,    \r\n    \"title\": \"update title\" \r\n } ";
            result = await _restService.ExecuteAsync<PostDataModel>($"{this._url}/3", Method.Patch ,patchJson.ToObject<PostDataModel>());
            result.ToLog();

            result =  await _restService.ExecuteAsync<PostDataModel>($"{this._url}/3", Method.Delete);
            result.ToLog();
        }
    }
}
