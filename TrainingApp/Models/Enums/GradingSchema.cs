/// <summary>
/// Module purpose: Enum to indicate grading schema of assessment records
/// Authors: Hansol Lee / Jei Yang
/// Date: Nov 6, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
/// 
/// </summary>
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainingApp.Models.Enums
{
    public enum GradingSchema 
    {
        //BASIC - Assessments are graded as either 'Pass' or 'Fail'
        [Display(Name = "BASIC - Pass / Fail")]
        PassFail = 1,
        //ADVANCED - Assessments are graded with numeric values between 1-5
        [Display(Name = "ADVANCED - Score")]
        Score = 2
    }
}

