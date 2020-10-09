
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTD.BackEndsNightmare.DataModels;

namespace DTD.BackEndsNightmare.Core
{
    internal class ApiAssistant
    {
        private HttpClient client;
        private RequestURIModel Model;
        private string BaseUrl;

        public ApiAssistant(string baseurl,Dictionary<string,string> headers, RequestURIModel uriModel)
        {
            client = new HttpClient();
            BaseUrl = baseurl;
            Model = uriModel;
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key,header.Value);
            }
            

        }


        public async Task<HttpResponseMessage> GET()
        {
            HttpResponseMessage message = await client.GetAsync(BaseUrl + Model.RequestUri);

            return message;
        }

        public async Task<HttpResponseMessage> POST()
        {
            HttpContent content=new StringContent(Model.RequestData,Encoding.UTF8,Model.ContentType);
            HttpResponseMessage message = await client.PostAsync(BaseUrl + Model.RequestUri, content);
            
            return message;
        }



    }
}
