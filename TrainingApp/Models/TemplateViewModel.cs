/// <summary>
/// Module purpose: Template view model associated to TemplateDTO
/// Authors: Hansol Lee / Jei Yang
/// Date: Nov 6, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///
/// </summary>
using System;
using TrainingApp.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections.ObjectModel;

namespace TrainingApp.Models
{
    public class TemplateViewModel
    {
        public IEnumerable<TemplateDTO> Templates { get; set; }
        public TemplateDTO Template { get; set; }
        public TaskViewModel TaskViewModel { get; set;}
        public string TargetAction { get; set; } = "Create";
        

    }

    //View Template Details (Task)
    public class TaskViewModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid TemplateID { get; set;}

        public Guid TemplateElementID { get; set; }

        public Guid TaskID { get; set; }

        public int OrderNo { get; set; }
    }

    public class TemplateFilters
    {
        public string CreateStarDate { get; set; }

        public string CreateEndDate { get; set; }

        public string ModifyStartDate { get; set; }

        public string ModifyEndDate { get; set; }

        public string Status { get; set; }

        public string GradingSchema { get; set; }

        public string searchString { get; set; }

        public string IsPublished { get; set; }
    }
}