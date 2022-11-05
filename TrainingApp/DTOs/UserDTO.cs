using System;
namespace TrainingApp.DTOs
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string? UserCode { get; set; }
        public DateTime CreateTime { get; set; }
        public int State { get; set; }

        public int CompanyID { get; set; }
        public CompanyDTO Company { get; set; }
    }
}

