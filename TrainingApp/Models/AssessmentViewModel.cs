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
    }

    public class EvaluationViewModel
    {
        public AssessmentDTO Assessment { get; set; }
        public IEnumerable<AssessmentElementDTO> Elements { get; set; }

        public UserDTO Examiner { get; set; }
        public UserDTO Trainee { get; set; }
    }

    public class SignAssessmentModel
    {
        public string AssessmentID { get; set; }

        public string UserID { get; set; }

        public string UserRole { get; set; }

        public string Password { get; set; }
    }
}

