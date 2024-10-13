using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.Areas.Admin.ViewModels;

namespace PLProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new AppUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Roles = _userManager.GetRolesAsync(user).Result
            }
                ).ToListAsync();
            return View(users);
        }
        public async Task<IActionResult> roles()
        {
            var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            return View(roles);
        }
    }
}
