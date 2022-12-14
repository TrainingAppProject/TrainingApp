/// <summary>
/// Module purpose: User view model associated to UserDTO
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///
/// </summary>
using System;
using TrainingApp.DTOs;

namespace TrainingApp.Models
{
    public class UserViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public UserDTO User { get; set; }
        public string TargetAction { get; set; }
    }
}

