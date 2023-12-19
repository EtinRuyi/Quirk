using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminUsersController(IUserRepository userRepository, 
            UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAll();
            var userVm = new UserViewModel();
            userVm.Users = new List<User>();
            foreach (var user in users)
            {
                userVm.Users.Add(new Models.ViewModels.User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                });
            }
            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            var user = new IdentityUser
            {
                UserName = userViewModel.UserName,
                Email = userViewModel.Email,
            };
            var result = await _userManager.CreateAsync(user, userViewModel.Password);
            if (result != null)
            {
                if (result.Succeeded)
                {
                    var role = new List<string> { "User" };
                    if (userViewModel.AdminRoleCheckBox)
                    {
                        role.Add("Admin");
                    }
                    
                    result = await _userManager.AddToRolesAsync(user, role);

                    if (result != null && result.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }
            return View();
        }
    }
}
