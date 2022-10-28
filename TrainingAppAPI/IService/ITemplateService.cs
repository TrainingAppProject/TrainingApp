using System;
using TrainingAppAPI.Models;

namespace TrainingAppAPI.IService
{
    public interface ITemplateService
    {
        List<Template> GetTemplates();
    }
}

