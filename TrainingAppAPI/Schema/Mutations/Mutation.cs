using System;
using System.ComponentModel.Design;
using HotChocolate.Subscriptions;
using TrainingAppAPI.DTOs;
using TrainingAppAPI.Schema.Mutations;
using TrainingAppAPI.Schema.Queries;
using TrainingAppAPI.Services.Templates;

namespace TrainingAppAPI.Schema
{
    public class Mutation
    {
        private readonly TemplateRepository _templatesRepository;

        public Mutation(TemplateRepository templateRepository)
        {
            _templatesRepository = templateRepository;
        }

        public async Task<TemplateResult> CreateTemplate(TemplateType templateInput, [Service] ITopicEventSender topicEventSender)
        {
            TemplateDTO templateDTO = new TemplateDTO()
            {
                CompanyID = templateInput.CompanyID,
                Name = templateInput.Name,

            };

            templateDTO = await _templatesRepository.Create(templateDTO);

            TemplateResult template = new TemplateResult()
            {
                ID = templateDTO.ID,
                Name = templateDTO.Name
            };

            await topicEventSender.SendAsync(nameof(Subscription.TemplateCreated), template);

            return template;
        }

        public async Task<TemplateResult> Update(Guid id, TemplateType templateInput, [Service] ITopicEventSender topicEventSender)
        {
            TemplateDTO templateDTO = new TemplateDTO()
            {
                ID = id,
                Name = templateInput.Name,

            };

            templateDTO = await _templatesRepository.Update(templateDTO);

            TemplateResult template = new TemplateResult()
            {
                ID = templateDTO.ID,
                Name = templateDTO.Name

            };

            string updateTemplateTopic = $"{template.ID}_{nameof(Subscription.TemplateUpdated)}";
            await topicEventSender.SendAsync(Update, template);

            return template;
        }

        public async Task<bool> DeleteTemplate(Guid id)
        {
            try
            {
                return await _templatesRepository.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}

