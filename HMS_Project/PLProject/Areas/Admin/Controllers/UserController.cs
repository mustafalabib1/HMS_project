using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        #region Roles Views
        public async Task<IActionResult> roles()
        {
            var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            return View(roles);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new AppUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult()
            }
                ).ToListAsync();

            users = users
                .OrderByDescending(user => user.Roles.Contains("Admin"))
                .ThenBy(user => user.UserName)
                .ToList();

            return View(users);
        }
        #endregion

        #region Add User
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(role => new RoleViewModel { RoleId = role.Id, RoleName = role.Name }).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                Roles = roles
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // if no roles selected
            if (!model.Roles.Any(role => role.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select at least one role");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "UserName already exists");
                return View(model);
            }

            // Now the Model Should be fine
            var user = new AppUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.BirthDate,
                Gender = model.Gender
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles ", error.Description);
                }
                return View(model);
            }

            await _userManager.AddToRolesAsync(user, model.Roles.Where(role => role.IsSelected).Select(role => role.RoleName));

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit User

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var viewModel = new ProfileFormViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail != null && userWithSameEmail.Id != user.Id)
            {
                ModelState.AddModelError("Email", "This Email is already taken");
                return View(model);
            }

            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            if (userWithSameUserName != null && userWithSameUserName.Id != user.Id)
            {
                ModelState.AddModelError("Email", "This UserName is already taken");
                return View(model);
            }

            user.FullName = model.FullName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address ?? string.Empty;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Manage Roles
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
