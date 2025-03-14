﻿using frontendForMasterdev.Models;
using Newtonsoft.Json;

namespace frontendForMasterDev.Services
{
    public class Request
    {
        private readonly string _url;
        public Request()
        {
            _url = "https://erponeupdator.masterdev.pl/Masterdev_Updater/";
        }
        public async Task<List<GetAppModel>> GetApps(string postfix)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix);

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            var response = await client.SendAsync(requestMessage);
            //var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            var data = await response.Content.ReadAsStringAsync();
            List<GetAppModel> json = new();
            try
            {
                json = JsonConvert.DeserializeObject<List<GetAppModel>>(data);
            }
            catch
            {
                var temp = JsonConvert.DeserializeObject<GetAppModel>(data);
                json.Add(temp);
            }

            return json;

        }

        public async Task<List<GetLogsModel>> GetLogs(string postfix, int appId)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix + "?appId=" + appId);

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");


            var response = await client.SendAsync(requestMessage);
            //var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            var data = await response.Content.ReadAsStringAsync();
            List<GetLogsModel> json = new();
            try
            {
                json = JsonConvert.DeserializeObject<List<GetLogsModel>>(data);
            }
            catch
            {
                var temp = JsonConvert.DeserializeObject<GetLogsModel>(data);
                json.Add(temp);
            }

            return json;

        }

        public async Task<Stream> GetUpdate(string postfix, string appname, string version)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix + $"?appname={appname}&version={version}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            var response = await client.SendAsync(requestMessage);

            return await response.Content.ReadAsStreamAsync();

           

        }

        public async Task<string> UpdateEnabled(string postfix, int i)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix + $"?i={i}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            var response = await client.SendAsync(requestMessage);
            //var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            var data = await response.Content.ReadAsStringAsync();


            return data;

        }

        public async Task SendLogs(string postfix, int appId, string message)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix + $"?appId={appId}&message={message}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");


            await client.SendAsync(requestMessage);


        }

        public async Task<GetNewerVersion> GetNewerVersion(string postfix, string appname, string version)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix + $"?appname={appname}&version={version}");
            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            var response = await client.SendAsync(requestMessage);
            var data = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<GetNewerVersion>(data);

            return json;

        }

        public async Task<List<GetUsers>> GetUsers(string postfix)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url + postfix);
            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            var response = await client.SendAsync(requestMessage);
            var data = await response.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<List<GetUsers>>(data);

            return json;

        }


    }
}