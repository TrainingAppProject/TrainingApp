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
///     Nov 8, 2022 (Jei Yang): Create/Delete Template feature implementation
///     Nov 9, 2022 (Jei Yang): View Tasks feature implementation
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
                model.Templates = db.Templates.Where(t => t.State == (int)BasicStatus.Active).OrderByDescending(t => t.ModifiedDate).ToList();

                foreach (var template in model.Templates) {
                    template.Created = db.Users.Where(u => u.ID == template.CreatedID).FirstOrDefault();
                    template.Elements = db.TemplateElements.Where(e => e.TemplateID == template.ID).ToList(); //TBD - to be tested and removed later
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
                var elementsToDelete = db.TemplateElements.Where(e => e.TemplateID == id).ToList();

                //Check if there is any TemplateElements and Tasks for this Template.
                if (elementsToDelete.Count > 0)
                {
                    var tasksToDelete = elementsToDelete.Select(n => 
                            db.Tasks.Single(t => t.ID == n.TaskID)
                        ).ToList();

                    if (tasksToDelete.Count == 0)
                    {
                        throw new ArgumentException("The TemplateElement record with ID '" + id + "' is not associated to any Task record");
                    }
                    db.Tasks.RemoveRange(tasksToDelete);
                    db.TemplateElements.RemoveRange(elementsToDelete.AsEnumerable());
                }

                db.Templates.RemoveRange(db.Templates.Where(t => t.ID == id).AsEnumerable());

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

    // Get a list of tasks that are associated with the given template ID
    public IActionResult ViewTemplate(Guid templateID)
    {
        TemplateViewModel model = new TemplateViewModel();
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                model.Template = db.Templates.Where(t => t.ID == templateID).FirstOrDefault();
                model.Template.Elements = db.TemplateElements.Where(e => e.TemplateID == templateID).OrderBy(e => e.OrderNo).ToList();
                foreach(var element in model.Template.Elements)
                {
                    element.Task = db.Tasks.Where(t => t.ID == element.TaskID).FirstOrDefault();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }
        return View(model);
    }

    public async Task<IActionResult> CreateTask(TemplateViewModel model)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                //Create a Task
                TaskDTO task = new TaskDTO();
                task.ID = Guid.NewGuid();
                task.Name = model.TaskViewModel.Name;
                task.Description = model.TaskViewModel.Description;

                //Define an OrderNo for the new record - add it as the last item by default
                var numOfExistingElements = db.TemplateElements.Where(e => e.TemplateID == model.TaskViewModel.TemplateID).Count();
                
                //Create a TemplateElement with the new TaskID
                TemplateElementDTO element = new TemplateElementDTO();
                element.Id = Guid.NewGuid();
                element.TemplateID = model.TaskViewModel.TemplateID;
                element.TaskID = task.ID;

                element.OrderNo = numOfExistingElements + 1;

                db.Tasks.Add(task);
                db.TemplateElements.Add(element);

                await db.SaveChangesAsync();

                return RedirectToAction("ViewTemplate", "Template", new { templateID =  model.TaskViewModel.TemplateID });
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }
        return RedirectToAction("Error", "Home");
    }   


    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                //Given param 'id' is a TemplateElementID
                var elementToDelete = db.TemplateElements.Where(e => e.Id == id).FirstOrDefault();
                if (elementToDelete == null)
                {
                    throw new ArgumentException("Cannot find a Template Element associated to ID'" + id + "'");
                }
                var taskToDelete = db.Tasks.Where(t => t.ID == elementToDelete.TaskID).FirstOrDefault();
                if (taskToDelete == null)
                {
                    throw new ArgumentException("Cannot find a Task associated to ID'" + id + "'");
                }

                //Update the OrderNo of other tasks associated to the Template
                var otherElements = db.TemplateElements.Where(e => e.TemplateID == elementToDelete.TemplateID && e.Id != elementToDelete.Id).ToList();
                foreach (var element in otherElements)
                {
                    if (element.OrderNo > elementToDelete.OrderNo)
                    {
                        element.OrderNo--;
                    }
                }   
                db.TemplateElements.UpdateRange(otherElements);

                //Remove records
                db.Tasks.Remove(taskToDelete);
                db.TemplateElements.Remove(elementToDelete);

                await db.SaveChangesAsync();

                return RedirectToAction("ViewTemplate", "Template", new { templateID = elementToDelete.TemplateID });
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
        }
        return RedirectToAction("Error", "Home");

    }
}


 