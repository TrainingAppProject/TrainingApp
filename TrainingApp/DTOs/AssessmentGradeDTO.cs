using System;

/// <summary>
/// Module purpose: Data Transfer Object for Assessment Grade data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     
/// </summary>
namespace TrainingApp.DTOs
{
    public class AssessmentGradeDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime? CreatedDate { get; set;}

        public DateTime? ModifiedDate { get; set; }
        public string? Grade { get; set; }
        public Guid AssessmentElementID { get; set; }
        public AssessmentElementDTO AssessmentElement { get; set; }

    }
}

