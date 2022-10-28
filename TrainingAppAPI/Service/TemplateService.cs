using System;
using TrainingAppAPI.Enum;
using TrainingAppAPI.IService;
using TrainingAppAPI.Models;

namespace TrainingAppAPI.Service
{
    public class TemplateService : ITemplateService
    {
        public List<Template> GetTemplates()
        {
            List<Template> templates = new List<Template>();
            
            for (int i = 0; i< 10; i++)
            {
                templates.Add(new Template()
                {
                    ID = i,
                    CompanyID = 1,
                    Name = "Template " + i,
                    Description = "This is template " + i,
                    CreatedTime = DateTime.UtcNow,
                    CreatedID = 1,
                    ModifiedDate = DateTime.UtcNow,
                    IsTaskMandatory = true,
                    GradeID = 1,
                    State = (int)BaseState.Active,
                    ScriptNumber = "script number " + i
                }) ;
            }

            return templates;
        }
    }
}

