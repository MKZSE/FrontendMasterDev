using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using frontendForMasterdev.Models;
using frontendForMasterDev.Services;
using System.Reflection;
using System.Text;
using FrontendForMasterdev.Models;

namespace frontendForMasterDev.Controllers;

public class HomeController : Controller
{
    private readonly Request _request;
    private readonly PostRequest _postrequest;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, Request request)
    {
        _logger = logger;
        _request = request;

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

   


    public async Task<IActionResult> GetApps()
    {

        var GetApps = await _request.GetApps(postfix: "GetApps");

        return View("ShowGetApps", GetApps);
    }

    public async Task<IActionResult> GetLogs(int appId)
    {

        var GetLogs = await _request.GetLogs(postfix: "GetLogs", appId: appId);

        return View("ShowGetLogs", GetLogs);
    }

    public IActionResult ShowSendLogs()
    {
        return View();
    }
    public IActionResult ShowAddNewApplication()
    {
        return View();
    }
    public IActionResult ShowGetApps()
    {
        return View();
    }
    public IActionResult ShowGetLogs()
    {
        return View();
    }
    public IActionResult ShowGetNewerVersion()
    {
        return View();
    }
    public IActionResult ShowGetUpdate()
    {
        return View();
    }
        public IActionResult ShowUploadUpdate()
    {
        return View();
    }



    
    public async Task<IActionResult> SendLogs(int appId, string message)
    {

        await _request.SendLogs(postfix: "SendLogs", appId: appId, message: message);

        return View("ShowSendLogs");
    }

    public async Task<IActionResult> GetUpdatedEnabled(int i)
    {
        var Enabled = await _request.UpdateEnabled(postfix: "UpdatesEnabled", i: i);
        ViewBag.Enabled = Enabled;  // Przekazanie wartoœci do ViewBag
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> GetUpdate(string nazwa_aplikacji, string version)
    {
        var fileStream = await _request.GetUpdate(postfix: "GetUpdate", appname: nazwa_aplikacji, version: version);

        if (fileStream == null)
        {
            return NotFound("Nie znaleziono pliku dla podanej aplikacji i wersji.");
        }

        return File(fileStream, "application/octet-stream", $"{nazwa_aplikacji}_{version}.zip");
    }

    [HttpPost]
    public async Task<IActionResult> AddNewApplication(string postfix, string appname, string directoryname, string addres, string iisAppName, string iisAppPoolName, string pgsqlConnectionString)
    {

        var GetZipFile = await _postrequest.AddNewApplication(postfix: "AddNewApplication", appname: appname, directoryname: directoryname, addres: addres, iisAppName: iisAppName, iisAppPoolName: iisAppPoolName, pgsqlConnectionString: pgsqlConnectionString);

        return File(GetZipFile, "text/plain", $"{appname}ConsoleApp.zip");
    }
    [HttpPost]
    public async Task<IActionResult> UploadUpdate(string version, int who, int app_id, IFormFile file)
    {

        MemoryStream memorystream = new MemoryStream();
        await file.CopyToAsync(memorystream);

        await _postrequest.UploadUpdate(postfix: "GetNewerVersion", version: version, who: who, appId: app_id, updateFile: memorystream.ToArray());

        return View();

    }
    [HttpPost]
    public async Task<IActionResult> GetNewerVersion(string appname, string version)
    {
        // Wywo³anie metody, która sprawdza dostêpnoœæ nowszej wersji
        var newerVersionInfo = await _request.GetNewerVersion(postfix: "GetNewerVersion", appname: appname, version: version);

        // Przekazujemy obiekt modelu (np. z informacjami o dostêpnej wersji) do widoku
        return View("ShowGetNewerVersion", newerVersionInfo);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadNewerVersion(string appname, string version)
    {
        var fileStream = await _request.GetUpdate(postfix: "GetUpdate", appname: appname, version: version);

        if (fileStream == null)
        {
            return NotFound("Nie znaleziono pliku dla podanej aplikacji i wersji.");
        }

        // Zwracamy FileResult, który spowoduje pobranie pliku przez przegl¹darkê
        return File(fileStream, "application/octet-stream", $"{appname}_{version}.zip");
    }
}