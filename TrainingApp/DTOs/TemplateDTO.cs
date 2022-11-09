using System;
using System.ComponentModel.DataAnnotations;
using TrainingApp.Models.Enums;
/// <summary>
/// Module purpose: Data Transfer Object for Template data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     Nov 7, 2022 (Jei Yang): Updated for template /task association
/// </summary>
namespace TrainingApp.DTOs
{
    public class TemplateDTO
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid CreatedID { get; set; }
        public UserDTO Created {get; set;}
        public DateTime ModifiedDate { get; set; }
        public bool IsTaskMandatory { get; set; }
        public int State { get; set; }
        public string? ScriptNumber { get; set; }

        public int CompanyID { get; set; }
        public CompanyDTO Company { get; set; }
        
        public IList<TemplateElementDTO> Elements { get; set; }
        public void AddTemplateElements(TemplateElementDTO element)
        {
            Elements.Add(element);
        }
        //[EnumDataType(typeof(GradingSchema))]
        public GradingSchema GradingSchema {get; set; } //Associated to Enum GradingSchema
        
        public int AttemptsAllowedPerTask {get; set;}
    }
}

