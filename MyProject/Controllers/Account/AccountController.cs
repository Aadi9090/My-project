using MyProject.Data;
using MyProject.Models.Account;
using MyProject.Models.ViewModels;
using MyProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace MyProject.Controllers.Account
{

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IUserService userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               
                var data = await userService.ValidateUserAsync(model.UserName, model.Password);
                if (data != null)
                {
                    bool isValid = (data.UserName == model.UserName && data.Password == model.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.UserName) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("Username", model.UserName);
                        TempData["session"] = HttpContext.Session.GetString("Username");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Invalid Credentials ! Try again ";
                        return View(model);
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Credentials ! Try again ";
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while signing in.");
                return StatusCode(500, "Internal server error.");
            }
        }

        public IActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
             
                var existingUser = await userService.GetUserByUsernameAsync(model.UserName);
                if (existingUser != null)
                {
                 
                    TempData["msg"] = "User Already exists";
                    return View(model);
                }

              
                var user = new UserModel
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    Password = model.Password 
                };

                await userService.CreateUserAsync(user);
                return RedirectToAction("Login", "Account");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while signing up.");
                return StatusCode(500, "Internal server error.");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");

        }
    }
}
