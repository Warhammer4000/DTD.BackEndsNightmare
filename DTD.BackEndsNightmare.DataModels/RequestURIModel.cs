

using System.Collections.Generic;

namespace DTD.BackEndsNightmare.DataModels
{
    public class RequestURIModel
    {
        public enum RequestVerb
        {
            GET,POST,PUT,DELETE
        }
        public string RequestUri { get; set; }
        public RequestVerb Verb { get; set; }
        public string RequestData { get; set; }
        public string ContentType { get; set; } 
        public int Sequence { get; set; }

        public bool RequireHeaders { get; set; }
        public Dictionary<string, string> Headers;

        public bool HasToken { get; set; }
        public string TokenName { get; set; }
        public bool IsAuthToken { get; set; }


        public RequestURIModel()
        {
            Headers=new Dictionary<string, string>();
            ContentType = "application/json";
        }


    }
}
