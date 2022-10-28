using System;
namespace TrainingAppAPI.Models
{
    public class Template
    {
        
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsTaskMandatory { get; set; }
        public int GradeID { get; set; }
        public int State { get; set; }
        public string? ScriptNumber { get; set; }
    }
}

