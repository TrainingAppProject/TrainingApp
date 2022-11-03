using System;
using TrainingAppAPI.DTOs;
using TrainingAppAPI.Services.Templates;

namespace TrainingAppAPI.Schema.Queries
{
    public class Query
    {
        private readonly TemplateRepository _templateRepository;

        public Query(TemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public async Task<IEnumerable<TemplateType>> GetTemplates()
        {
            IEnumerable<TemplateDTO> templateDTOs = await _templateRepository.GetAll();
            return templateDTOs.Select(t => new TemplateType()
            {
                ID = t.ID,
                Name = t.Name,
                
            });
        }

        public async Task<TemplateType> GetTemplateByIdAsync(Guid id)
        {
            TemplateDTO templateDTO = await _templateRepository.GetByID(id);

            return new TemplateType()
            {
                ID = templateDTO.ID,
                Name = templateDTO.Name
            };
        }
    }
}

