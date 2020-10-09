
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DTD.BackEndsNightmare.Core;
using DTD.BackEndsNightmare.DataModels;
using Newtonsoft.Json.Linq;

namespace DTD.BackendsNightmare.ConsoleApp
{
    class Program
    {
        private static Project project;
        static void Main(string[] args)
        {
            project=new Project();
            project.Name = "Berger Value Club";
            project.ProjectConfiguration=new ProjectConfiguration($"https://vcapi.bergerbd.com:8080/api/",true);
            project.AddGlobalHeader("AppHeaderSecretKey", "APPHEADERSECRETKEY12345");

            RequestURIModel LoginUriModel=new RequestURIModel();
            LoginUriModel.HasToken = true;
            LoginUriModel.IsAuthToken = true;
            LoginUriModel.TokenName = "token";
            LoginUriModel.RequestUri = "app/auth/login";
            LoginUriModel.Verb = RequestURIModel.RequestVerb.POST;
            LoginUriModel.RequestData = "{\r\n  \"phoneNumber\": \"01912995783\",\r\n  \"password\": \"12345\",\r\n  \"rememberMe\": true,\r\n  \"fcmToken\": \"string\"\r\n}";

            project.RequestUris.Add(LoginUriModel);

            RequestURIModel GetLeadModel = new RequestURIModel();

            GetLeadModel.RequestUri = "ambassadorapp/GetLeadByUser";
            GetLeadModel.Verb = RequestURIModel.RequestVerb.GET;
           

            project.RequestUris.Add(GetLeadModel);





            Task.Run(() => ShowOutputs(project));
           

            Console.ReadKey();
        }


        static async void ShowOutputs(Project project)
        {

            HttpRequestController controller = new HttpRequestController();



            foreach (var urimodels in project.RequestUris)
            {
                var message = await controller.ExecuteRequest(project.ProjectConfiguration.BaseUrl, urimodels, project.GlobalHeaders);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(urimodels.RequestUri);
                if (message.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tPassed ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tFailed ");
                }

                

                if (urimodels.HasToken)
                {
                   string data=  await message.Content.ReadAsStringAsync();
                   JObject jObject=JObject.Parse(data);

                  
                   List<JToken> expandedNodes = jObject.DescendantsAndSelf().ToList();
                   
                   string token = "";

                   foreach (var node in expandedNodes)
                   {
                       token = node.SelectToken(urimodels.TokenName)?.ToString();
                       if(!string.IsNullOrEmpty(token))break;
                   }

                   if (urimodels.IsAuthToken)
                   {
                        project.AddGlobalHeader("Authorization","bearer "+token);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("JWT Token Added");
                   }
                   else
                   {
                       project.AddGlobalHeader(urimodels.TokenName,token);
                   }

                }
            }
        }


       


    }
}
