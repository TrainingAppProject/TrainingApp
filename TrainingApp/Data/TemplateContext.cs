using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainingAppAPI.Models;

namespace TrainingApp.Data;

public class TemplateContext : DbContext
{
  public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
  {
  }
  public DbSet<Template> Movie { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Template>().OwnsMany(m => m.Elements).HasData(
        new TemplateElement
        {
          Id = 1,
          OrderNo = 1,
          TaskID = 0,
          TemplateID = new Guid("72d95bfd-1dac-4bc2-adc1-f28fd43777fd")
        });
    modelBuilder.Entity<Template>().HasData(
        new Template
        {
          ID = new Guid("72d95bfd-1dac-4bc2-adc1-f28fd43777fd"),
          Name = "Template 1"
        },

    );
  }

}
        


