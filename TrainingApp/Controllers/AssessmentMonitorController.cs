using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Models;
using TrainingApp.DTOs;
using TrainingApp.Models.Enums;
using TrainingApp.Services;
using System.Security.Claims;

namespace TrainingApp.Controllers;

/// <summary>
/// Module purpose: Controller that connects Assessment Monitor view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Nov 21, 2022 (Jei Yang): Added functions for Edit / Delete functionality of Assessment
///s
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
                var assessments = db.Assessments.ToList(); //TBD

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

                //status filter
                if (!string.IsNullOrWhiteSpace(filter.Status))
                {
                    Enum.TryParse(filter.Status, out BasicStatus statusIndex);
                    assessments = assessments.Where(a =>
                        a.State == (int) statusIndex).ToList();
                }

                //result filter
                if (!string.IsNullOrWhiteSpace(filter.Result))
                {
                    //TBD - currently Result is 'Pass', 'PartialPass' or 'Fail'.
                    //The following code may be updated depending on the value of OveralLGrade and PassGrade, etc.
                    if (filter.Result == "NotSet")
                    {
                        assessments = assessments.Where(a => a.OverallGrade == null).ToList();
                    }
                    else
                    {
                        assessments = assessments.Where(a => a.OverallGrade == filter.Result).ToList();
                    }
                    
                }

                //grading schema filter
                if (!string.IsNullOrWhiteSpace(filter.GradingSchema))
                {
                    Enum.TryParse(filter.GradingSchema, out GradingSchema grading);
                    assessments = assessments.Where(a => a.Template.GradingSchema == grading).ToList();
                }

                foreach (var assessment in assessments)
                {
                    assessment.Examiner = db.Users.Where(u => u.ID == assessment.ExaminerID).FirstOrDefault();
                    assessment.Trainee = db.Users.Where(u => u.ID == assessment.TraineeID).FirstOrDefault();
                    assessment.Template = db.Templates.Where(t => t.ID == assessment.TemplateID).FirstOrDefault();

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

    [HttpGet]
    public IActionResult GetAssessmentForm(string assessmentID)
    {
        AssessmentViewModel model = new AssessmentViewModel();
        bool isValidID = Guid.TryParse(assessmentID, out Guid assessmentIDGuid);
        if (!isValidID)
            throw new ArgumentException("Invalid Assessment ID");

        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                var assessmentToEdit = db.Assessments.FirstOrDefault(a => a.ID == assessmentIDGuid);
                model.Assessment = new AssessmentDTO();

                model.Assessment.ID = assessmentToEdit.ID;
                model.Assessment.Name = assessmentToEdit.Name;
                model.Assessment.Description = assessmentToEdit.Description;
                model.Assessment.State = assessmentToEdit.State;
                model.Assessment.Purpose = assessmentToEdit.Purpose;
                model.Assessment.CreatedTime = assessmentToEdit.CreatedTime;
                model.Assessment.CreatedID = assessmentToEdit.CreatedID;
                model.Assessment.ModifiedDate = assessmentToEdit.ModifiedDate;
                model.Assessment.ModifiedID = assessmentToEdit.ModifiedID;
                model.Assessment.ExaminerID = assessmentToEdit.ExaminerID;
                model.Assessment.Examiner = db.Users.FirstOrDefault(u => u.ID == assessmentToEdit.ExaminerID);
                model.Assessment.ExaminerSigned = assessmentToEdit.ExaminerSigned;
                model.Assessment.TraineeID = assessmentToEdit.TraineeID;
                model.Assessment.Trainee = db.Users.FirstOrDefault(u => u.ID == assessmentToEdit.TraineeID);
                model.Assessment.TraineeSigned = assessmentToEdit.TraineeSigned;
                model.Assessment.Template = db.Templates.FirstOrDefault(t => t.ID == assessmentToEdit.TemplateID);
                model.Assessment.OverallGrade = assessmentToEdit.OverallGrade;
                model.Assessment.PassGrade = assessmentToEdit.PassGrade;
                model.Assessment.IsTaskMandatory = assessmentToEdit.IsTaskMandatory;
                
                return PartialView("_EditAssessment", model);
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, responseText = ex.Message });
        }
    }

    public async Task<IActionResult> EditAssessment(AssessmentViewModel model)
    {

        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                AssessmentDTO assessment = db.Assessments.FirstOrDefault(a => a.ID == model.Assessment.ID);
                assessment.ID = model.Assessment.ID;
                
                var claim = (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault();
                string currentUserName = claim.Value;

                assessment.Name = model.Assessment.Name;
                assessment.Description = model.Assessment.Description;
                assessment.ExaminerID = model.Assessment.ExaminerID;
                assessment.TraineeID = model.Assessment.TraineeID;
                var modified = db.Users.FirstOrDefault(u => u.UserName == currentUserName);
                assessment.ModifiedID = modified.ID;
                assessment.ModifiedDate = DateTime.UtcNow;
                assessment.State = model.Assessment.State;
                assessment.Purpose = model.Assessment.Purpose;
                
                db.Assessments.Update(assessment);
                //TBD - Add more fields to be updated if necessary
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

        }
        catch (Exception ex)
        {
            return Json(new { success = false, responseText = ex.Message });
        }
        return RedirectToAction("Error", "Home");
    }

    public IActionResult EvaluationView(Guid id)
    {
        EvaluationViewModel model = new EvaluationViewModel();
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                AssessmentDTO assessment = db.Assessments.Where(a=>a.ID == id)
                    .FirstOrDefault() ?? throw new ArgumentException("Cannot find assessment");
                model.Assessment = assessment;

                List<AssessmentElementDTO> elements = db.AssessmentElements
                    .Where(a => a.AssessmentID == assessment.ID)
                    .Include(e=>e.Task)
                    .Include(e=>e.Grade).ToList();
                model.Elements = elements;

                UserDTO examiner = db.Users.Where(u => u.ID == assessment.ExaminerID).FirstOrDefault();
                model.Examiner = examiner;

                UserDTO trainee = db.Users.Where(u => u.ID == assessment.TraineeID).FirstOrDefault();
                model.Trainee = trainee;

            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
            return Json(new { success = false, responseText = ex.Message });
        }
        return View(model);
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

    [HttpPost]
    public async Task<IActionResult> DeleteAssessment(Guid id)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {

                var assessmentToDelete = db.Assessments.FirstOrDefault(a => a.ID == id);
                if (assessmentToDelete.State == (int) BasicStatus.Active)
                {
                    throw new ArgumentException("The assessment has to be in 'Closed' state in order to be deleted.");
                }
                else if (assessmentToDelete.State == (int) BasicStatus.Delete)
                {
                    throw new ArgumentException("The assessment is already in 'Delete' state.");
                }
                assessmentToDelete.State = (int) BasicStatus.Delete;
                db.Assessments.Update(assessmentToDelete);

                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Assessment");
            }
        }
        catch (Exception ex)
        {
            //_logger.LogInformation("ERROR", ex.Message);
            
            var result = new JsonResult(new { Message = ex.Message, StatusCode = 500 });
            result.StatusCode = 500;
            return result;
        }
        return RedirectToAction("Error", "Home");

    }
}

