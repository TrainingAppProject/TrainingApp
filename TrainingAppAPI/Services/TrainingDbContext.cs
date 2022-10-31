using System;
using Microsoft.EntityFrameworkCore;
using TrainingAppAPI.DTOs;

namespace TrainingAppAPI.Services
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

    }

}


