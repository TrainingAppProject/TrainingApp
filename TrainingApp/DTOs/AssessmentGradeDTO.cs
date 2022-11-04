using System;
namespace TrainingApp.DTOs
{
    public class AssessmentGradeDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int State { get; set; }

        public Guid AssessmentElementID { get; set; }
        public AssessmentElementDTO AssessmentElement { get; set; }


        public IList<AssessmentGradeElementDTO> AssessmentGradeElements { get; set; }
    }
}

