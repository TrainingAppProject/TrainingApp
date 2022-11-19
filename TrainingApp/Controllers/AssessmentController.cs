using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.DTOs;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.Services;

namespace TrainingApp.Controllers;
/// <summary>
/// Module purpose: Controller that connects Assessment view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
/// 
/// </summary>
[Authorize]
public class AssessmentController : Controller
{
    private readonly ILogger<AssessmentController> _logger;
    private readonly IDbContextFactory<TrainingDbContext> _context;

    public AssessmentController(ILogger<AssessmentController> logger, IDbContextFactory<TrainingDbContext> context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        AssessmentViewModel model = new AssessmentViewModel();
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                model.Trainees = db.Users.Where(u => u.Role.Contains("Trainee")).ToList();
                model.Templates = db.Templates.Where(t => t.State == (int)BasicStatus.Active).ToList();
                model.Assessments = db.Assessments.Where(a => a.State == (int)BasicStatus.Active).ToList();
                model.Users = db.Users.Where(u => u.CompanyID == 1).ToList();
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, responseText = ex.Message });
            _logger.LogInformation("ERROR", ex.Message);
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAssessment(AssessmentViewModel model)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                string userName = User.FindFirstValue(ClaimTypes.Name);
                UserDTO currentUser = db.Users.Where(u => u.UserName == userName).FirstOrDefault();

                UserDTO trainee = db.Users.Where(t => t.ID == model.Assessment.TraineeID).FirstOrDefault();
                if (trainee == null)
                    throw new ArgumentException("Cannot find trainee");

                TemplateDTO template = db.Templates.Where(t => t.ID == model.Assessment.TemplateID).FirstOrDefault();
                if (template == null)
                    throw new ArgumentException("Cannot find template");

                DateTime now = DateTime.UtcNow;

                AssessmentDTO assessment = new AssessmentDTO();
                assessment.ID = Guid.NewGuid();
                assessment.Name = model.Assessment.Name;
                assessment.CompanyID = 1;
                assessment.Description = model.Assessment.Description;
                assessment.CreatedTime = now;
                assessment.CreatedID = currentUser.ID;
                assessment.ModifiedDate = now;
                assessment.ModifiedID = currentUser.ID;
                assessment.ExaminerID = currentUser.ID;
                assessment.TraineeID = trainee.ID;
                assessment.TemplateID = template.ID;
                assessment.PassGrade = template.GradingSchema.ToString();
                assessment.IsTaskMandatory = template.IsTaskMandatory;
                assessment.State = (int)BasicStatus.Active;
                
                db.Assessments.Add(assessment);
                db.Entry(assessment).State = EntityState.Added;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
            return RedirectToAction("Error", "Home");
        }
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

