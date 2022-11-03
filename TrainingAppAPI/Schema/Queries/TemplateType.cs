using System;
namespace TrainingAppAPI.Schema.Queries
{
    public class TemplateType
    {
        public Guid ID { get; set; }
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsTaskMandatory { get; set; }
       
        public int State { get; set; }
        public string? ScriptNumber { get; set; }

        [IsProjected(true)]
        public int GradeID { get; set; }

        public IEnumerable<TemplateElementType> Elements { get; set; }

    }
}

