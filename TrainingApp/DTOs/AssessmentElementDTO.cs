using System;

/// <summary>
/// Module purpose: Data Transfer Object for Assessment Element data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     
/// </summary>
namespace TrainingApp.DTOs
{
    public class AssessmentElementDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }

        public Guid AssessmentID { get; set; }
        public AssessmentDTO Assessment { get; set; }

        //Just for the information, not for relationship
        public Guid TemplateElementID { get; set; }

        public Guid AssessmentTaskID { get; set; }
        public AssessmentTaskDTO Task { get; set; }

        public Guid AssessmentGradeID { get; set; }
        public AssessmentGradeDTO Grade { get; set; }
    }
}

