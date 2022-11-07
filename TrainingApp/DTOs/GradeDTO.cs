using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Module purpose: Data Transfer Object for Grade data model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Oct 31, 2022 (Hansol Lee): Initial creation / model definition
///     
/// </summary>
namespace TrainingApp.DTOs
{
    public class GradeDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public Guid GradeID { get; set; }

        public IEnumerable<GradeElementDTO> Elements { get; set; }
    }
}

