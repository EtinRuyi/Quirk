//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Quirk.Models.ViewModels;
//using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;

//namespace Quirk.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;

//        }
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }



//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new IdentityUser
//                {
//                    UserName = registerViewModel.UserName,
//                    Email = registerViewModel.Email,
//                };

//                // Check if the password is not null or empty before calling CreateAsync
//                if (!string.IsNullOrEmpty(registerViewModel.Password))
//                {
//                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

//                    if (result.Succeeded)
//                    {
//                        var role = await _userManager.AddToRoleAsync(user, "User");

//                        if (role.Succeeded)
//                        {
//                            return RedirectToAction("Register");
//                        }
//                        else
//                        {
//                            // Handle role assignment failure
//                            ModelState.AddModelError(string.Empty, "Failed to assign role to the user.");
//                        }
//                    }
//                    else
//                    {
//                        // Handle user creation failure
//                        foreach (var error in result.Errors)
//                        {
//                            ModelState.AddModelError(string.Empty, error.Description);
//                        }
//                    }
//                }
//                else
//                {
//                    // Handle case where password is null or empty
//                    ModelState.AddModelError(nameof(registerViewModel.Password), "Password cannot be null or empty.");
//                }
//            }

//            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "Auth", new { returnUrl = registerViewModel.ReturnUrl });
//            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
//            return Challenge(properties, "Google");
//        }

//        [HttpGet]
//        public IActionResult Login(string ReturnUrl)
//        {
//            var model = new LoginViewModel
//            {
//                ReturnUrl = ReturnUrl
//            };
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }
//            var signIn = await _signInManager.PasswordSignInAsync(loginViewModel.UserName,
//                loginViewModel.Password, false, false);
//            if (signIn != null && signIn.Succeeded)
//            {
//                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
//                {
//                    return Redirect(loginViewModel.ReturnUrl);
//                }
//                return RedirectToAction("Index", "Home");
//            }
//            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "Auth", new { returnUrl = loginViewModel.ReturnUrl });
//            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
//            return Challenge(properties, "Google");
//        }

//        [HttpGet]
//        public async Task<IActionResult> GoogleLoginCallback(string returnUrl = null, string remoteError = null)
//        {
//            if (remoteError != null)
//            {
//                // Handle the case where there is a remote error during Google login
//                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
//                return View("Login");
//            }

//            // Retrieve the information about the user from the external login provider (Google)
//            var info = await _signInManager.GetExternalLoginInfoAsync();

//            if (info == null)
//            {
//                // Handle the case where there is no information about the user from the external provider
//                ModelState.AddModelError(string.Empty, "Error loading external login information.");
//                return View("Login");
//            }

//            // Check if the user is already registered
//            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

//            if (signInResult.Succeeded)
//            {
//                // User is already registered, sign them in
//                return RedirectToLocal(returnUrl);
//            }
//            else
//            {
//                // User is not registered, create a new user account
//                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

//                var user = new IdentityUser
//                {
//                    UserName = email,
//                    Email = email,
//                };

//                var createResult = await _userManager.CreateAsync(user);

//                if (createResult.Succeeded)
//                {
//                    // Add user claims from external provider (Google)
//                    var addLoginResult = await _userManager.AddLoginAsync(user, info);
//                    if (addLoginResult.Succeeded)
//                    {
//                        await _signInManager.SignInAsync(user, isPersistent: false);
//                        return RedirectToLocal(returnUrl);
//                    }
//                }

//                // If user creation or login fails, handle errors
//                foreach (var error in createResult.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }

//                return View("Login");
//            }
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }


//        [HttpGet]
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }

//    }
//}




using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

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

                if (!string.IsNullOrEmpty(registerViewModel.Password))
                {
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {
                        var role = await _userManager.AddToRoleAsync(user, "User");

                        if (role.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);

                            // Redirect to the home page
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to assign role to the user.");
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(registerViewModel.Password), "Password cannot be null or empty.");
                }
            }

            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "Auth", new { returnUrl = registerViewModel.ReturnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

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

            if (signIn.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "Auth", new { returnUrl = loginViewModel.ReturnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet]
        public IActionResult GoogleLogin(string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(GoogleLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet]
        public async Task<IActionResult> GoogleLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");
                return View("Login");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                var user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                };

                var createResult = await _userManager.CreateAsync(user);

                if (createResult.Succeeded)
                {
                    var addLoginResult = await _userManager.AddLoginAsync(user, info);
                    if (addLoginResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Login");
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
