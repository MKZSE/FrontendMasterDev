using Newtonsoft.Json;
using System.Net.Http.Headers;
namespace frontendForMasterDev.Services
{
    public class PostRequest
    {

        private readonly string _url;
        public PostRequest()
        {
            _url = "https://erponeupdator.masterdev.pl/Masterdev_Updater/";
        }
        public async Task<Stream> AddNewApplication(string postfix, string appname, string directoryname, string addres, string iisAppName, string iisAppPoolName, string pgsqlConnectionString)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url + postfix + $"?appname={appname}&directoryname={directoryname}&address={addres}&iisAppName={iisAppName}&iisAppPoolName={iisAppPoolName}&pgsqlConnectionString={pgsqlConnectionString}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");


            var response = await client.SendAsync(requestMessage);

            var file = await response.Content.ReadAsStreamAsync();

            return file;


        }
        public async Task UploadUpdate(string postfix, string version, int who, int appId, byte[] updateFile)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url + postfix + $"?version={version}&who={who}&app_id={appId}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            requestMessage.Content = new ByteArrayContent(updateFile);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            await client.SendAsync(requestMessage);

            return;

        }
    }
}