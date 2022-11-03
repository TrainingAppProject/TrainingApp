using System;
using Microsoft.AspNetCore.Mvc;
using TrainingAppAPI.DTOs;
using TrainingAppAPI.Services;

namespace TrainingAppAPI.Controllers
{
    [Route("api/[Controller]")]
    public class TemplateController : Controller
    {
        private TrainingDbContext _context;

        public TemplateController(TrainingDbContext context)
        {
            context = _context;
        }

        [HttpGet]
        public List<TemplateDTO> Get()
        {
            return _context.Templates.ToList();
        }
    }
}

