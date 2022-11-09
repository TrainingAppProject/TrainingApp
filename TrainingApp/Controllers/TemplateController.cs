using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Helper;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.DTOs;
using TrainingApp.Services;
using System.Reflection;

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
    private readonly IDbContextFactory<TrainingDbContext> _context;
    //TrainingAPI trainingapi = new TrainingAPI();

    public TemplateController(ILogger<HomeController> logger, IDbContextFactory<TrainingDbContext> context)
    {
        _logger = logger;
        _context= context;
    }

    public IActionResult Index()
    {
        TemplateViewModel model = new TemplateViewModel();
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                model.Templates = db.Templates.Where(t => t.State == (int)BasicStatus.Active).ToList();

                foreach (var template in model.Templates) {
                    template.Created = db.Users.Where(u => u.ID == template.CreatedID).FirstOrDefault();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}

