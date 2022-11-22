﻿using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.DTOs;
using TrainingApp.Helper;
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
                model.Assessments = db.Assessments.Where(a => a.State == (int)BasicStatus.Active)
                    .OrderByDescending(a=>a.CreatedTime).ToList();
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

    //TODO: modulize function
    [HttpPost]
    public async Task<IActionResult> CreateAssessment(AssessmentViewModel model)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                string userName = User.FindFirstValue(ClaimTypes.Name);
                UserDTO currentUser = db.Users.Where(u => u.UserName == userName)
                    .FirstOrDefault() ?? throw new ArgumentException("Cannot find current user");

                UserDTO trainee = db.Users.Where(t => t.ID == model.Assessment.TraineeID)
                    .FirstOrDefault() ?? throw new ArgumentException("Cannot find trainee");

                TemplateDTO template = db.Templates.Where(t => t.ID == model.Assessment.TemplateID)
                    .FirstOrDefault() ?? throw new ArgumentException("Cannot find template");

                IEnumerable<TemplateElementDTO> templateElements
                    = db.TemplateElements.Where(e => e.TemplateID == template.ID).Include(e => e.Task).ToList();

                DateTime now = DateTime.UtcNow;

                //Create assessment
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

                List<AssessmentElementDTO> assessmentElements = new List<AssessmentElementDTO>();
                List<AssessmentTaskDTO> assessmenTasks = new List<AssessmentTaskDTO>();

                foreach (TemplateElementDTO templateElement in templateElements)
                {
                    AssessmentElementDTO assessmentelement = new AssessmentElementDTO();
                    assessmentelement.Id = Guid.NewGuid();
                    assessmentelement.AssessmentID = assessment.ID;
                    assessmentelement.Assessment = assessment;
                    assessmentelement.OrderNo = templateElement.OrderNo;
                    assessmentelement.TemplateElementID = templateElement.Id;

                    if (templateElement.Task != null)
                    {
                        AssessmentTaskDTO assessmentTask = new AssessmentTaskDTO();

                        assessmentTask.Id = Guid.NewGuid();
                        assessmentTask.Name = templateElement.Task.Name;
                        assessmentTask.Description = templateElement.Task.Description;
                        assessmentTask.AssessmentElement = assessmentelement;
                        assessmentTask.AssessmentElementID = assessmentelement.Id;
                        db.AssessmentTasks.Add(assessmentTask);

                        assessmentelement.AssessmentTaskID = assessmentTask.Id;
                        assessmentelement.Task = assessmentTask;
                    }
                    assessmentElements.Add(assessmentelement);

                    CreateAssessmentGrading(db, template, assessmentelement, now);
                }

                db.AssessmentTasks.AddRange(assessmenTasks);
                db.AssessmentElements.AddRange(assessmentElements);

                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
            
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
            return RedirectToAction("Error", "Home");
        }
    }

    private void CreateAssessmentGrading(
        TrainingDbContext db,
        TemplateDTO template,
        AssessmentElementDTO assessmentelement,
        DateTime now)
    {
        List<AssessmentGradeDTO> grades = new List<AssessmentGradeDTO>();

        if (template.GradingSchema == GradingSchema.PassFail)
        {
            foreach (var gradeDetail in Enum.GetValues(typeof(GradingSchemaPassFail)))
            {
                AssessmentGradeDTO grade = new AssessmentGradeDTO();
                grade.ID = Guid.NewGuid();
                grade.Name = ((GradingSchemaPassFail)gradeDetail).ToString();
                grade.State = (int)BasicStatus.Active;
                grade.CreatedDate = now;
                grade.Grade = GradingSchemaPassFail.Pass.ToString();
                grade.ModifiedDate = now;
                grade.AssessmentElement = assessmentelement;
                grade.AssessmentElementID = assessmentelement.Id;

                grades.Add(grade);
            }
        }
        else if (template.GradingSchema == GradingSchema.Score)
        {
            foreach (var gradeDetail in Enum.GetValues(typeof(GradingSchemaScores)))
            {
                AssessmentGradeDTO grade = new AssessmentGradeDTO();
                grade.ID = Guid.NewGuid();
                grade.Name = ((GradingSchemaPassFail)gradeDetail).ToString();
                grade.State = (int)BasicStatus.Active;
                grade.CreatedDate = now;
                grade.Grade = GradingSchemaPassFail.Pass.ToString();
                grade.ModifiedDate = now;
                grade.AssessmentElement = assessmentelement;
                grade.AssessmentElementID = assessmentelement.Id;

                grades.Add(grade);
            }
        }
        db.AssessmentGrades.AddRange(grades);
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

    [HttpPost]
    public IActionResult signAssessment([FromBody]SignAssessmentModel model)
    {
        try
        {
            using (TrainingDbContext db = _context.CreateDbContext())
            {
                bool isValidID = Guid.TryParse(model.UserID, out Guid userGuid);
                if (!isValidID)
                    throw new Exception("Invalid User ID");

                UserDTO user = db.Users.Where(u => u.ID == userGuid)
                    .FirstOrDefault() ?? throw new ArgumentNullException("User does not exist");

                if (model.Password != PasswordEncoder.Decrypt(user.Password))
                {
                    throw new Exception("Password does not match");
                }

                bool isValidAssessmentID = Guid.TryParse(model.AssessmentID, out Guid assessmentGuid);
                if (!isValidAssessmentID)
                    throw new Exception("Invalid Assessment ID");

                AssessmentDTO assessment = db.Assessments.Where(a=>a.ID == assessmentGuid)
                    .FirstOrDefault() ?? throw new ArgumentNullException("Assessment does not exist");

                if (model.UserRole == "Examiner")
                    assessment.ExaminerSigned = DateTime.UtcNow;
                else if (model.UserRole == "Trainee")
                    assessment.TraineeSigned = DateTime.UtcNow;

                db.Assessments.Update(assessment);
                db.SaveChanges();

                return Json(new { success = true });
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation("ERROR", ex.Message);
            return Json(new { success = false, responseText = ex.Message });
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

