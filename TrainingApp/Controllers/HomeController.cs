using System.Diagnostics;
using System.Security.Claims;
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
        var claim = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == "UserRoles").FirstOrDefault();
        string userroles = claim.Value;

        List<string> roles = userroles.Split(',').ToList();

        if (roles == null || roles.Count() < 1)
            throw new Exception("role is empty");

        if (roles.Count() == 1 && roles.Contains("Trainee"))
            return RedirectToAction("Index", "Assessment");

        return RedirectToAction("Index", "AssessmentMonitor");
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

