using System;

namespace TrainingApp.DTOs
{
    public class GradeElementDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsFail { get; set; }

        public Guid GradeID { get; set; }
        public GradeDTO Grade { get; set; }
    }
}

