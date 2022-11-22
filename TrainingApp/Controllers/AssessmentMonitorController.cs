using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.Services;
namespace TrainingApp.Controllers;

/// <summary>
/// Module purpose: Controller that connects Assessment Monitor view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     
/// </summary>
[Authorize]
public class AssessmentMonitorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDbContextFactory<TrainingDbContext> _context;

    public AssessmentMonitorController(
        ILogger<HomeController> logger,
        IDbContextFactory<TrainingDbContext> context)
    {
        _logger = logger;
        _context= context;
    }

    public IActionResult Index()
    {
        AssessmentViewModel model = new AssessmentViewModel();
        using (TrainingDbContext db = _context.CreateDbContext())
        {
            //TBD
            model.Trainees = db.Users.Where(u => u.Role.Contains("Trainee")).ToList(); //TBD
            model.Templates = db.Templates.Where(t => t.State == (int)BasicStatus.Active).ToList(); //TBD
            model.Assessments = db.Assessments.Where(a => a.State == (int)BasicStatus.Active).ToList(); //TBD
            foreach (var assessment in model.Assessments)
            {
                assessment.Examiner = db.Users.Where(u => u.ID == assessment.ExaminerID).FirstOrDefault();
                assessment.Trainee = db.Users.Where(u => u.ID == assessment.TraineeID).FirstOrDefault();
                assessment.Template = db.Templates.Where(t => t.ID == assessment.TemplateID).FirstOrDefault();

            }
            model.Users = db.Users.Where(u => u.CompanyID == 1).ToList(); //TBD
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult FilterAssessments([FromBody] AssessmentFilters filter)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                AssessmentViewModel model = new AssessmentViewModel();
                var assessments = db.Assessments.Where(a => a.State == (int)BasicStatus.Active).ToList(); //TBD

                foreach (var assessment in assessments)
                {
                    assessment.Examiner = db.Users.Where(u => u.ID == assessment.ExaminerID).FirstOrDefault();
                    assessment.Trainee = db.Users.Where(u => u.ID == assessment.TraineeID).FirstOrDefault();
                    assessment.Template = db.Templates.Where(t => t.ID == assessment.TemplateID).FirstOrDefault();

                }
                //create date
                if (!string.IsNullOrWhiteSpace(filter.CreateStartDate) &&
                    !string.IsNullOrWhiteSpace(filter.CreateEndDate))
                {
                    DateTime start = ConvertStringToDate(filter.CreateStartDate);
                    DateTime end = ConvertStringToDate(filter.CreateEndDate);
                    if (start == end)
                        end = end.AddDays(1);

                    assessments = assessments.Where(a =>
                        a.CreatedTime >= start && a.CreatedTime < end).ToList();
                }

                //modify date
                if (!string.IsNullOrWhiteSpace(filter.ModifyStartDate) &&
                    !string.IsNullOrWhiteSpace(filter.ModifyEndDate))
                {
                    DateTime start = ConvertStringToDate(filter.ModifyStartDate);
                    DateTime end = ConvertStringToDate(filter.ModifyEndDate);
                    if (start == end)
                        end = end.AddDays(1);

                    assessments = assessments.Where(a =>
                        a.ModifiedDate >= start && a.ModifiedDate < end).ToList();
                }

                //result filter
                if (!string.IsNullOrWhiteSpace(filter.Result))
                {
                    //TBD - currently Result is either 'Pass' or 'Fail'.
                    //The following code may be updated depending on the value of OveralLGrade and PassGrade, etc.
                    assessments = assessments.Where(a =>
                        a.OverallGrade == filter.Result).ToList();
                }

                //grading schema filter
                if (!string.IsNullOrWhiteSpace(filter.GradingSchema))
                {
                    Enum.TryParse(filter.GradingSchema, out GradingSchema grading);
                    assessments = assessments.Where(a => a.Template.GradingSchema == grading).ToList();
                }

                model.Assessments = assessments;

                return PartialView("_AssessmentListBody", model);
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, responseText = ex.Message });
            _logger.LogInformation("ERROR", ex.Message);
        }
    }

    private DateTime ConvertStringToDate(string date)
    {
        // Parse the string you have, to create a datetime.
        DateTime tempDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture);

        // Set time to 0
        return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 0, 0, 0);
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

