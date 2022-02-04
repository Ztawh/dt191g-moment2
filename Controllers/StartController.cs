using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dt191g_moment2_MVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace dt191g_moment2_MVC.Controllers;

public class StartController : Controller
{
    private readonly ILogger<StartController> _logger;

    public StartController(ILogger<StartController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        DateTime dateObj = DateTime.Now;
        string dateToday = dateObj.ToString("yyyy-MM-dd");

        // Spara cookie i en dag
        HttpContext.Response.Cookies.Append("date", dateToday, new CookieOptions { 
                Expires = DateTime.Now.AddDays(1)});

        ViewData["date"] = dateToday;
        
        return View();
    }

    [HttpGet("Spel")]
    public IActionResult Games()
    {
        // Hämta data från json-fil och skapa objekt
        var jsonData = System.IO.File.ReadAllText("wwwroot/allGames.json");
        var jsonObj = JsonConvert.DeserializeObject<IEnumerable<GamesModel>>(jsonData);
        ViewBag.games = jsonObj;

        var cookie = HttpContext.Request.Cookies["date"];
        ViewData["date"] = cookie;
        return View();
    }
    
    [HttpPost("Spel")]
    public IActionResult Games(GamesModel model)
    {
        // Hämta data från json-fil och skapa objekt
        var jsonData = System.IO.File.ReadAllText("wwwroot/allGames.json");
        var jsonObj = JsonConvert.DeserializeObject<IEnumerable<GamesModel>>(jsonData);
        ViewBag.games = jsonObj;
        
        if (ModelState.IsValid)
        {
            // Lagra
            var jsonObjList = JsonConvert.DeserializeObject<List<GamesModel>>(jsonData);

            if (jsonObjList != null)
            {
                jsonObjList.Add(model);
            }
            
            System.IO.File.WriteAllText("wwwroot/allGames.json",
                JsonConvert.SerializeObject(jsonObjList, Formatting.Indented));
            
            ModelState.Clear();
            // Ladda om sidan för att uppdatera listan med spel för användaren
            return RedirectToAction("Games");
        }
        else
        {
            // Ladda inte om sidan för att visa felmeddelanden
            return View();
        }
    }

    [HttpGet("Speltips")]
    public IActionResult GameTip()
    {
        // Hämta data från json-fil och skapa objekt
        var jsonData = System.IO.File.ReadAllText("wwwroot/allGames.json");
        var jsonObj = JsonConvert.DeserializeObject<List<GamesModel>>(jsonData);

        // Hämta cookie (datum)
        var cookie = HttpContext.Request.Cookies["date"];
        ViewData["date"] = cookie;
        return View(jsonObj);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}