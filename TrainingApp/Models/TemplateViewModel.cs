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

    }
}