using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;

namespace Quirk.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(user, "User");
                if (role.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }
            return View();
        }
    }
}
