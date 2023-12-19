using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quirk.Models.ViewModels;
using Quirk.Repositories;

namespace Quirk.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AdminUsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
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
    }
}
