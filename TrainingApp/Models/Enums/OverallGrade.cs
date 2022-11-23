using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainingApp.Models.Enums
{
    public enum OverallGrade
    {
        [Display(Name = "Pass")]
        Pass,
        [Display(Name = "Partial Pass")]
        PartialPass,
        [Display(Name = "Fail")]
        Fail
    }

}

