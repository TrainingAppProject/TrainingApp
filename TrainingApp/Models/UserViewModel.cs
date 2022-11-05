using System;
using TrainingApp.DTOs;

namespace TrainingApp.Models
{
    public class UserViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public UserDTO User { get; set; }
    }
}

