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
        public async Task<byte[]> AddNewApplication(string postfix, string appname, string directoryname, string addres, string iisAppName, string iisAppPoolName, string pgsqlConnectionString)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url + postfix + $"?appName={appname}&directoryname={directoryname}&addres={addres}&iisAppName={iisAppName}&iisAppPoolName={iisAppPoolName}&pgsqlConnectionString={pgsqlConnectionString}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");


            var response = await client.SendAsync(requestMessage);
            var data = await response.Content.ReadAsByteArrayAsync();

            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memoryStream);

            foreach (var bit in data)
            {
                writer.Write(bit);
            }

            return memoryStream.ToArray();


        }
        public async Task UploadUpdate(string postfix, string version, int who, int appId, byte[] updateFile)
        {
            HttpClient client = new();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url + postfix + $"?appId={appId}&who={who}&version={version}");

            requestMessage.Headers.Add("accept", "*/*");
            requestMessage.Headers.Add("x-api-key", "x");

            requestMessage.Content = new ByteArrayContent(updateFile);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            await client.SendAsync(requestMessage);

            return;

        }
    }
}