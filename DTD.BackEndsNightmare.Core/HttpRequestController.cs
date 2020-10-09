using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTD.BackEndsNightmare.DataModels;

namespace DTD.BackEndsNightmare.Core
{
    public class HttpRequestController
    {
       
        public async Task<HttpResponseMessage> ExecuteRequest(string baseUrl,RequestURIModel uriModel,Dictionary<string,string> globalHeaders)
        {
            ApiAssistant apiAssistant=new ApiAssistant(baseUrl,globalHeaders,uriModel);
            switch (uriModel.Verb)
            {
                case RequestURIModel.RequestVerb.GET:
                    return await apiAssistant.GET();
                case RequestURIModel.RequestVerb.POST:
                    return await apiAssistant.POST();
                case RequestURIModel.RequestVerb.PUT:
                    return null;
                case RequestURIModel.RequestVerb.DELETE:
                    return null;
                default:
                    return null;
            }
        }


    }
}
