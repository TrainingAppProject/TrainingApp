using System;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TrainingApp.DTOs;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.Services;

/// <summary>
/// Module purpose: Controller that connects User view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Nov 5, 2022 (Hansol Lee): Added functions for user creation
/// </summary>
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

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            try
            {
                using (TrainingDbContext db = _context.CreateDbContext())
                {
                    //Edit 
                    if (model.User.ID != null && model.User.ID != Guid.Empty)
                        await EditUser(db, model);
                    //Create
                    else
                        await CreateUser(db, model);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR", ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult GetUserForm(string id)
        {
            UserViewModel model = new UserViewModel();

            //This is create user form
            if (id == "0")
            {
                model.TargetAction = "Create";
                return PartialView("_UpdateUserModalBody", model);
            }

            //this is edit user form
            bool isValidID = Guid.TryParse(id, out Guid userid);
            if (!isValidID)
                throw new ArgumentException("Invalid user ID");

            try
            {
                using (TrainingDbContext db = _context.CreateDbContext())
                {
                    UserDTO user = db.Users.FirstOrDefault(u => u.ID == userid &&
                        u.State == (int)BasicStatus.Active);
                    if (user == null)
                        throw new ArgumentNullException("User does not exist");

                    model.TargetAction = "Edit";
                    model.User = new UserDTO();
                    model.User.ID = user.ID;
                    model.User.FirstName = user.FirstName;
                    model.User.LastName = user.LastName;
                    model.User.Role = user.Role;
                    model.User.UserName = user.UserName;
                    model.User.UserCode = user.UserCode;

                    return PartialView("_UpdateUserModalBody", model);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message });
                _logger.LogInformation("ERROR", ex.Message);
            }
        }

        private async Task CreateUser(TrainingDbContext db, UserViewModel model)
        {
            UserDTO existUser = db.Users.Where(u => u.UserName == model.User.UserName).FirstOrDefault();
            if (existUser != null)
                throw new ArgumentException("This user is already existing");

            UserDTO user = new UserDTO();
            user.ID = Guid.NewGuid();
            user.UserName = model.User.UserName;
            user.CreateTime = DateTime.UtcNow;
            user.State = (int)BasicStatus.Active;
            user.CompanyID = 1;

            SetUser(user, model);

            db.Users.Add(user);
            db.Entry(user).State = EntityState.Added;
            await db.SaveChangesAsync();
        }

        private async Task EditUser(TrainingDbContext db, UserViewModel model)
        {
            //TODO if selected user is current user and role changes => sign out??


            UserDTO user = db.Users.Where(u => u.ID == model.User.ID).FirstOrDefault();
            if (user == null)
                throw new ArgumentNullException("User does not exist");

            SetUser(user, model);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }


        private void SetUser(UserDTO user, UserViewModel model)
        {
            user.FirstName = model.User.FirstName;
            user.LastName = model.User.LastName;
            user.UserCode = model.User.UserCode;
            user.Role = model.User.Role;
        }
    }
}

