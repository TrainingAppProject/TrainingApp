using System;

namespace TrainingApp.DTOs
{
    public class AssessmentElementDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }

        public Guid AssessmentID { get; set; }
        public AssessmentDTO Assessment { get; set; }

        //Just for the information, not for relationship
        public Guid TemplateElementID { get; set; }

        public Guid AssessmentTaskID { get; set; }
        public AssessmentTaskDTO Task { get; set; }

        public Guid AssessmentGradeID { get; set; }
        public AssessmentGradeDTO Grade { get; set; }
    }
}

