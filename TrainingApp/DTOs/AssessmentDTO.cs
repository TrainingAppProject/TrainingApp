using System;

/// <summary>
/// Module purpose: Data Transfer Object for Assessment data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     Nov 7, 2011 (Jei  Yang): Added PassGrade
/// </summary>
namespace TrainingApp.DTOs
{
    public class AssessmentDTO
    {
        public Guid ID { get; set; }
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedTime { get; set; }
        public Guid CreatedID { get; set; }
        public UserDTO Created { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedID { get; set; }

        public string? OverallGrade { get; set; }
        public bool IsTaskMandatory { get; set; }
        public int State { get; set; }

        public Guid ExaminerID { get; set; }
        public UserDTO Examiner { get; set; }
        public DateTime? ExaminerSigned { get; set; }

        public Guid TraineeID { get; set; }
        public UserDTO Trainee { get; set; }
        public DateTime? TraineeSigned{ get; set; }

        public Guid TemplateID { get; set; }
        public TemplateDTO Template { get; set; }

        public IList<AssessmentElementDTO> AssessmentElements { get; set; }

        public string? PassGrade {get;set; } //Numeric value between 1-5 if Grading Schema is 2(Score), Pass/Fail if Gradng Schema is 1(Pass/Fail)
    }
}

