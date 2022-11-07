using System;

/// <summary>
/// Module purpose: Login view model
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///
/// </summary>
namespace TrainingApp.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}

