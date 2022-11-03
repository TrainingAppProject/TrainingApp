using System;
namespace TrainingAppAPI.DTOs
{
    public class AssessmentDTO
    {
        public Guid ID { get; set; }
        public int CompanyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedTime { get; set; }
        public Guid CreatedID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedID { get; set; }

        public String OverallGrade { get; set; }
        public bool IsTaskMandatory { get; set; }
        public int State { get; set; }

        public Guid ExaminerID { get; set; }
        public UserDTO Examiner { get; set; }
        public DateTime? ExaminerSigned { get; set; }

        public Guid TraineeID { get; set; }
        public UserDTO Trainee { get; set; }
        public DateTime? TraineeSigned{ get; set; }

        public Guid TemplateID { get; set; }
        public TemplateDTO Template { get; set; }

        public IList<AssessmentElementDTO> Elements { get; set; }
        public void AddAssessmentElements(AssessmentElementDTO element)
        {
            Elements.Add(element);
        }
    }
}

