using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TrainingApp.DTOs;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.Services;

namespace TrainingApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbContextFactory<TrainingDbContext> _context;

        public UserController(ILogger<HomeController> logger, IDbContextFactory<TrainingDbContext> context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            try
            {
                using (TrainingDbContext db = _context.CreateDbContext())
                {
                    model.Users = db.Users.Where(u => u.State == (int)BasicStatus.Active).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR", ex.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            try
            {
                using (TrainingDbContext db = _context.CreateDbContext())
                {
                    UserDTO existUser = db.Users.Where(u => u.UserName == model.User.UserName).FirstOrDefault();
                    if (existUser != null)
                        throw new ArgumentException("This user is already existing");

                    UserDTO user = new UserDTO();
                    user.ID = Guid.NewGuid();
                    user.FirstName = model.User.FirstName;
                    user.LastName = model.User.LastName;
                    user.UserName = model.User.UserName;
                    user.UserCode = model.User.UserCode;
                    user.CreateTime = DateTime.UtcNow;
                    user.Role = model.User.Role;
                    user.State = (int)BasicStatus.Active;
                    user.CompanyID = 1;

                    db.Users.Add(user);

                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR", ex.Message);
            }
            return RedirectToAction("Error", "Home");
        }
    }
}

