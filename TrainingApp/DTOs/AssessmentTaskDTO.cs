﻿using System;
namespace TrainingApp.DTOs
{
    public class AssessmentTaskDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid AssessmentElementID { get; set; }
        public AssessmentElementDTO AssessmentElement { get; set; }
    }
}

