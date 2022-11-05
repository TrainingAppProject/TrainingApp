using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Models;

namespace TrainingApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //session expire
        //RedirectToAction("LogIn", "Account", new { area = "Admin" });

        //admin role
        //RedirectToAction("Index", "User", new { area = "Admin" });
        return RedirectToAction("Index", "AssessmentMonitor");

        //exminer role
        //RedirectToAction("Inex", "AssessmentMonitor", new { area = "Admin" });

        //trainee role
        //RedirectToAction("Index", "Assessment", new { area = "Admin" });
        //return View();
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
}

