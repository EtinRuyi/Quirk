using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;

namespace Quirk.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                };

                // Check if the password is not null or empty before calling CreateAsync
                if (!string.IsNullOrEmpty(registerViewModel.Password))
                {
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        var role = await _userManager.AddToRoleAsync(user, "User");

                        if (role.Succeeded)
                        {
                            return RedirectToAction("Register");
                        }
                        else
                        {
                            // Handle role assignment failure
                            ModelState.AddModelError(string.Empty, "Failed to assign role to the user.");
                        }
                    }
                    else
                    {
                        // Handle user creation failure
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    // Handle case where password is null or empty
                    ModelState.AddModelError(nameof(registerViewModel.Password), "Password cannot be null or empty.");
                }
            }

            // If ModelState is not valid or there was an issue with user creation, return to the view with errors
            return View();
        }



        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = registerViewModel.UserName,
        //            Email = registerViewModel.Email,
        //        };
        //        var result = await _userManager.CreateAsync(user, registerViewModel.Password);
        //        if (result.Succeeded)
        //        {
        //            var role = await _userManager.AddToRoleAsync(user, "User");
        //            if (role.Succeeded)
        //            {
        //                return RedirectToAction("Register");
        //            }
        //        }
        //    }
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var signIn = await _signInManager.PasswordSignInAsync(loginViewModel.UserName,
                loginViewModel.Password, false, false);
            if (signIn != null && signIn.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
