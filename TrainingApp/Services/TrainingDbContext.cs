/// <summary>
/// Module purpose: DbContext file for the project which defines all data models
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project / Auto-generated
/// Revision History:
///
/// </summary>
using System;
using Microsoft.EntityFrameworkCore;
using TrainingApp.DTOs;

namespace TrainingApp.Services
{
    public class TrainingDbContext : DbContext
    {

        public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
            : base(options) { }

        public DbSet<GradeDTO> Grades { get; set; }
        public DbSet<GradeElementDTO> GradeElements { get; set; }
        public DbSet<TaskDTO> Tasks { get; set; }

        public DbSet<TemplateDTO> Templates { get; set; }
        public DbSet<TemplateElementDTO> TemplateElements { get; set; }

        public DbSet<UserDTO> Users { get; set; }
        public DbSet<CompanyDTO> Companies { get; set; }

        public DbSet<AssessmentDTO> Assessments { get; set; }
        public DbSet<AssessmentElementDTO> AssessmentElements { get; set; }
        public DbSet<AssessmentGradeDTO> AssessmentGrades { get; set; }
        public DbSet<AssessmentGradeElementDTO> AssessmentGradeElements { get; set; }
        public DbSet<AssessmentTaskDTO> AssessmentTasks { get; set; }


    }

}


