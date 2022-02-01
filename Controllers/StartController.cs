using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dt191g_moment2_MVC.Models;
using Newtonsoft.Json;

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
        DateTime date = DateTime.Now;
        string dateToday = date.ToString("yyyy-MM-dd");
        // ViewBag
        ViewBag.date = dateToday;
        return View();
    }

    [HttpGet("Spel")]
    public IActionResult Games()
    {
        // Hämta data från json-fil och skapa objekt
        var jsonData = System.IO.File.ReadAllText("wwwroot/allGames.json");
        var jsonObj = JsonConvert.DeserializeObject<IEnumerable<GamesModel>>(jsonData);
        // Skicka med parameter
        ViewBag.games = jsonObj;
        return View();
    }

    [HttpPost]
    public IActionResult Games(GamesModel model)
    {
        if (ModelState.IsValid)
        {
            // Lagra
            Console.WriteLine("Valid");
        }
        else
        {
            // Annat
            Console.WriteLine("Not vaild");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}