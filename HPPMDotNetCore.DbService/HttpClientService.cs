using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HPPMDotNetCore.DbService
{
    public class HttpClientService
    {
        public HttpClientService() { }
        //public HttpClientService(string baseAddress) { }  

        public async Task<T> ExecuteAsync<T>(
            string endpoints,
            EnumHttpMethod httpMethod,
            object reqModel = null)
        {
            T model = default;
            HttpClient client = new HttpClient();   
            HttpContent content = null;
            if(reqModel != null) {
                content = new StringContent(reqModel.ToJson(), Encoding.UTF8, Application.Json);
            }
            HttpResponseMessage response = null;
            switch (httpMethod)
            {
                case EnumHttpMethod.Post:
                    response = await client.PostAsync(endpoints, content);
                    break;
                case EnumHttpMethod.Put:
                    response = await client.PutAsync(endpoints, content);
                    break;
                case EnumHttpMethod.Patch:
                    response = await client.PatchAsync(endpoints, content);
                    break;
                case EnumHttpMethod.Delete:
                    response = await client.DeleteAsync(endpoints);
                    break;
                case EnumHttpMethod.Get:
                default:
                    response = await client.GetAsync(endpoints);
                    break;
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                model = jsonStr.ToObject<T>();
            }
            return model;
        }
    }
}
