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
using System.Security.Claims;

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

    public async Task<IActionResult> CreateTemplate(TemplateViewModel model)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                if (model.Template.GradingSchema == 0) {
                    throw new ArgumentException("Grading Schema is not defined");
                }

                //Grab currently logged in user's username
                var claim = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
                string currentUserName = claim.Value;


                TemplateDTO template = new TemplateDTO();
                template.ID = Guid.NewGuid();
                template.Created = db.Users.Where(u => u.UserName == currentUserName).FirstOrDefault();
                template.CreatedID = template.Created.ID;
                template.CreatedTime = DateTime.UtcNow;
                template.Description = model.Template.Description;
                template.Name = model.Template.Name;
                template.IsTaskMandatory = model.Template.IsTaskMandatory;
                template.ModifiedDate = DateTime.UtcNow;
                template.ScriptNumber = model.Template.ScriptNumber;
                template.State = (int) BasicStatus.Active;
                template.AttemptsAllowedPerTask = model.Template.AttemptsAllowedPerTask;
                template.GradingSchema = model.Template.GradingSchema;
                template.Company = db.Companies.Where(c => c.ID == template.Created.CompanyID).FirstOrDefault();
                template.CompanyID = template.Company.ID;

                db.Templates.Add(template);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }
        return RedirectToAction("Error", "Home");
    }    

    [HttpPost]
    public async Task<IActionResult> DeleteTemplate(Guid id)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                var elements = db.TemplateElements.Where(e => e.TemplateID == id).ToList();

                db.TemplateElements.RemoveRange(db.TemplateElements.Where(e => e.TemplateID == id).AsEnumerable().ToList());
                db.Templates.RemoveRange(db.Templates.Where(t => t.ID == id).AsEnumerable().ToList());
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Template");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }
        return RedirectToAction("Error", "Home");

    }
}


