using System;
namespace TrainingApp.DTOs
{
    public class TemplateElementDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }
        
        public Guid TemplateID { get; set; }
        public TemplateDTO Template { get; set; }

        public Guid TaskID { get; set; }
        public TaskDTO Task { get; set; }
    }
}

