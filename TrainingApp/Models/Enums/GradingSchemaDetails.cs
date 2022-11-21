using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TrainingApp.Models.Enums
{
   
    public enum GradingSchemaPassFail
    {
        Pass,
        Fail
    };

    public enum GradingSchemaScores
    {
        Faril = 1,
        BelowStandard = 2,
        Standard = 3,
        AboveStandard = 4,
        Excellent = 5,
    };
}

