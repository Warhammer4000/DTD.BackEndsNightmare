

using System.Collections.Generic;

namespace DTD.BackEndsNightmare.DataModels
{
    public class Project
    {
        public string Name { get; set; }
        public ProjectConfiguration ProjectConfiguration { get; set; }
        public Dictionary<string,string> GlobalHeaders { get; set; }


        public List<RequestURIModel> RequestUris { get; set; }

        public Project()
        {
            GlobalHeaders=new Dictionary<string, string>();
            RequestUris=new List<RequestURIModel>();
        }


        public void AddGlobalHeader(string key,string value)
        {
            GlobalHeaders.Add(key,value);
        }

    }
}
