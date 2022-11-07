using System;
/// <summary>
/// Module purpose: Data Transfer Object for Template Element data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     
/// </summary>
namespace TrainingApp.DTOs
{
    public class TemplateElementDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }
        
        public Guid TemplateID { get; set; }
        public TemplateDTO Template { get; set; }

        public Guid TaskID { get; set; }
        public TaskDTO Task { get; set; }
    }
}

