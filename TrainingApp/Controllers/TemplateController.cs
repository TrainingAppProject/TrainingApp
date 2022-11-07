using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Helper;
using TrainingApp.Models;

namespace TrainingApp.Controllers;

/// <summary>
/// Module purpose: Controller that connects Template view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     
/// </summary>
[Authorize]
public class TemplateController : Controller
{
    private readonly ILogger<HomeController> _logger;
    TrainingAPI trainingapi = new TrainingAPI();

    public TemplateController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

