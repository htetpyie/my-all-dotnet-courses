using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using RestSharp;

namespace HPPMDotNetCore.DbService
{
    public class RestClientService
    {
        public RestClientService() { }

        public async Task<T> ExecuteAsync<T>(
            string endpoints,
            Method httpMethod,
            object reqModel = null)
        {
            T model = default;
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(endpoints, httpMethod);
            if (reqModel != null)
            {
                request.AddBody(reqModel);
            }
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content;
                model = jsonStr.ToObject<T>();
            }
            return model;
        }
    }
}
