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
}

