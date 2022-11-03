using System;
using Microsoft.EntityFrameworkCore;
using TrainingAppAPI.DTOs;

namespace TrainingAppAPI.Services.Templates
{
    public class TemplateRepository
    {
        private readonly IDbContextFactory<TrainingDbContext> _contextFactory;

        public TemplateRepository(IDbContextFactory<TrainingDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TemplateDTO>> GetAll()
        {
            using (TrainingDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Templates
                    .Include(t=>t.Elements)
                    .ToListAsync();
            }
        }

        public async Task<TemplateDTO> GetByID(Guid tempalteID)
        {
            using (TrainingDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Templates
                    .Include(t=>t.Elements)
                    .FirstOrDefaultAsync(t=>t.ID == tempalteID);
            }
        }

        public async Task<TemplateDTO> Create(TemplateDTO template)
        {
            using (TrainingDbContext context = _contextFactory.CreateDbContext())
            {
                context.Templates.Add(template);
                await context.SaveChangesAsync();

                return template;
            }
        }

        public async Task<TemplateDTO> Update(TemplateDTO template)
        {
            using (TrainingDbContext context = _contextFactory.CreateDbContext())
            {
                context.Templates.Update(template);
                await context.SaveChangesAsync();

                return template;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (TrainingDbContext context = _contextFactory.CreateDbContext())
            {
                TemplateDTO template = new TemplateDTO()
                {
                    ID = id
                };

                context.Templates.Remove(template);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}

