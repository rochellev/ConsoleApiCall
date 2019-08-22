using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApiCall
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            var client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
            // 2
            var request = new RestRequest("home.json?api-key=kWzndQdzsfwvunGur5JBnqeFhzCH0yg1", Method.GET);
            // 3
            var response = new RestResponse();

            // 4
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            Console.WriteLine(jsonResponse["results"]);
        }

        // 5
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}