using System;
namespace TrainingApp.DTOs
{
    public class AssessmentGradeElementDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsFail { get; set; }

        public Guid AssessmentGradeID { get; set; }
        public AssessmentGradeDTO AssessmentGrade { get; set; }
    }
}

