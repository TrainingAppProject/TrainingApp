using System;
using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Controllers
{
    public class AccountController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}

