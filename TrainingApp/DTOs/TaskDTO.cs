using System;

/// <summary>
/// Module purpose: Data Transfer Object for Task data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     
/// </summary>
namespace TrainingApp.DTOs
{
    public class TaskDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}

