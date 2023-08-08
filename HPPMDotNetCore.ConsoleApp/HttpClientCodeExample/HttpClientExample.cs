using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HPPMDotNetCore.DbService;
using HPPMDotNetCore.Models;
using Microsoft.Identity.Client;

namespace HPPMDotNetCore.ConsoleApp.HttpClientCodeExample
{
    public class HttpClientExample
    {

        private string _url;

        public HttpClientExample()
        {
            this.SetUrl();
        }

        //request to jsonplaceholder fake rest api
        public void SetUrl()
        {
            _url = "https://jsonplaceholder.typicode.com/posts";
        }

        public async Task RunAsync()
        {
            #region Examples
            //string endpoints = "https://jsonplaceholder.typicode.com/posts";
            //HttpClient client = new HttpClient();
            //// client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            //var response = await client.GetAsync(endpoints);
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonStr = await response.Content.ReadAsStringAsync();
            //    List<PostDataModel> lst = jsonStr.ToObject<List<PostDataModel>>();
            //    lst.ToLog();
            //}


            //string createJson = "{\r\n              \"userId\": 1,\r\n              \"id\": 1,\r\n              \"title\": \"sunt aut facere repellat provident occaecati excepturi optio reprehenderit\",\r\n              \"body\": \"quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto\"\r\n            }";
            //PostDataModel createModel = createJson.ToObject<PostDataModel>();
            //HttpContent content =
            //    new StringContent(createModel.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            //response = await client.PostAsync(endpoints, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonStr = await response.Content.ReadAsStringAsync();
            //    PostDataModel newCreatedModel = jsonStr.ToObject<PostDataModel>();
            //}
            #endregion            
            await GetAsync<List<PostDataModel>>(this._url);

            await GetAsync<PostDataModel>($"{this._url}/2");

            string createJson = "{\"userId\": 1,\r\n    \"id\": 10,\r\n    \"title\": \"optio molestias id quia eum\",\r\n    \"body\": \"quo et expedita modi cum officia vel magni\\ndoloribus qui repudiandae\\nvero nisi sit\\nquos veniam quod sed accusamus veritatis error\"\r\n } ";
            PostDataModel createModel = createJson.ToObject<PostDataModel>();
            await PostAsync<PostDataModel>(this._url, createModel);

            string updateJson = "{\"userId\": 1,\r\n    \"id\": 10,\r\n    \"title\": \"update title\",\r\n    \"body\": \"updated body\"\r\n } ";
            await PutAsync<PostDataModel>($"{this._url}/2", updateJson.ToObject<PostDataModel>());

            string patchJson = "{\"userId\": 1,    \r\n    \"title\": \"update title\" \r\n } ";
            await PatchAsync<PostDataModel>($"{this._url}/3", patchJson.ToObject<PostDataModel>());

            await DeleteAsync<PostDataModel>($"{this._url}/3");

            HttpClientService service = new HttpClientService();
            await service.ExecuteAsync<List<PostDataModel>>(this._url, EnumHttpMethod.Get);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            T obj = default;
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content.ReadAsStringAsync();
                obj = resultJson.ToObject<T>();
                obj.ToLog();
            }
            return obj;
        }

        public async Task<T> PostAsync<T>(string url, T obj)
        {
            T resultObj = default;
            using HttpClient client = new HttpClient();
            using HttpContent content = new StringContent
                (obj.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            using HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content.ReadAsStringAsync();
                resultObj = resultJson.ToObject<T>();
            }
            return resultObj;
        }

        public async Task<T> PutAsync<T>(string url, T obj)
        {
            T resultObj = default;
            using HttpClient client = new HttpClient();
            using HttpContent content = new StringContent
                (obj.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            using HttpResponseMessage response = await client.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content?.ReadAsStringAsync();
                resultObj = resultJson.ToObject<T>();
            }
            return resultObj;
        }

        public async Task<T> PatchAsync<T>(string url, T obj)
        {
            T resultObj = default;
            using HttpClient client = new HttpClient();
            using HttpContent content = new StringContent
                (obj.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            using HttpResponseMessage response = await client.PatchAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content?.ReadAsStringAsync();
                resultObj = resultJson.ToObject<T>();
            }
            return resultObj;
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            T resultObj = default;
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string resultJson = await response.Content.ReadAsStringAsync();
                resultObj = resultJson.ToObject<T>();
            }
            return resultObj;
        }

    }
}