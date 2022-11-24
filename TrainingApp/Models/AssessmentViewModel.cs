/// <summary>
/// Module purpose: User view model associated to UserDTO
/// Authors: Hansol Lee / Jei Yang
/// Date: Nov 18, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///
/// </summary>
using System;
using TrainingApp.DTOs;

namespace TrainingApp.Models
{
    public class AssessmentViewModel
    {
        public IEnumerable<AssessmentDTO> Assessments { get; set; }
        public AssessmentDTO Assessment { get; set; }

        public IEnumerable<UserDTO> Trainees { get; set; }
        public IEnumerable<TemplateDTO> Templates { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }

        public bool IsTraineeView { get; set; }
    }


    public class AssessmentFilters
    {
        public string CreateStartDate { get; set; }

        public string CreateEndDate { get; set; }

        public string ModifyStartDate { get; set; }

        public string ModifyEndDate { get; set; }

        public string Status { get; set; }
        
        public string Result { get; set; }

        public string GradingSchema { get; set; }

        public string searchString { get; set; }
    }
    
    public class EvaluationViewModel
    {
        public AssessmentDTO Assessment { get; set; }
        public IEnumerable<AssessmentElementDTO> Elements { get; set; }

        public UserDTO Examiner { get; set; }
        public UserDTO Trainee { get; set; }

        public bool IsEditable { get; set; }
        public bool IsTrainee { get; set; }
    }

    public class UpdateAssessmentModel
    {
        public string AssessmentID { get; set; }

        public string UserID { get; set; }

        public string UserRole { get; set; }

        public string Password { get; set; }

        public string OverallGrade { get; set; }

        public string Grade { get; set; }

        public string GradeSchema { get; set; }

        public string ElementID { get; set; }
    }

}

