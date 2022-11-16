using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingApp.DTOs;
using TrainingApp.Models;
using TrainingApp.Models.Enums;
using TrainingApp.Services;

/// <summary>
/// Module purpose: Controller that connects Acount view with db
/// Authors: Hansol Lee / Jei Yang
/// Date: Oct 26, 2022
/// Source: Created for the COMP7022 project
/// Revision History:
///     Nov 5, 2022 (Hansol Lee) - Created functions for authentication and encrpytion
/// </summary>
namespace TrainingApp.Controllers
{
    [AllowAnonymous]
    public class AccountController: Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbContextFactory<TrainingDbContext> _context;

        public AccountController(ILogger<HomeController> logger, IDbContextFactory<TrainingDbContext> context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult LoginScreen()
        {
            return View("Login");
        }

        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            try
            {
                using (TrainingDbContext db = _context.CreateDbContext())
                {
                    UserDTO user = db.Users.Where(u=>u.UserName == model.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.UserName), "Username does not exist");
                        return View("Login", model);
                    }

                    if (!string.IsNullOrWhiteSpace(user.Password) &&
                        model.Password != Decrypt(user.Password))
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.Password), "Password wrong");
                        return View("Login", model);
                    }

                    //First time login set the password
                    if (user.Password == null) {
                        user.Password = Encrypt(model.Password);
                        await db.SaveChangesAsync();
                    }

                    await SignInUserIdentity(user);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR", ex.Message);
                model.ErrorMessage = ex.Message;
                return View("Login", model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Login");
        }

        private async Task SignInUserIdentity(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserRoles", user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        private string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }

            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return cipherText;
        }
    }
}

