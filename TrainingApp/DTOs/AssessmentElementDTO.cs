using System;

namespace TrainingApp.DTOs
{
    public class AssessmentElementDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }

        public Guid AssessmentID { get; set; }
        public AssessmentDTO Assessment { get; set; }

        public Guid TemplateElementID { get; set; }

        public Guid AssessmentTaskID { get; set; }
        public AssessmentTaskDTO Task { get; set; }
    }
}

