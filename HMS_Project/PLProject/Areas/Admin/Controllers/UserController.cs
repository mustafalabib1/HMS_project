using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using X.PagedList;
using PLProject.Areas.Admin.ViewModels;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DALProject.Data.Contexts;

namespace PLProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HMSdbcontext _dbContext;


        public UserController(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUnitOfWork unitOfWork,
            HMSdbcontext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        #region Roles Views
        public async Task<IActionResult> roles()
        {
            var roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            return View(roles);
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(string filterRole, int page = 1)
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

            if (!string.IsNullOrEmpty(filterRole))
            {
                users = users.Where(user => user.Roles.Contains(filterRole)).ToList();
            }

            users = users
                .OrderByDescending(user => user.Roles.Contains("Admin"))
                .ThenBy(user => user.UserName)
                .ToList();

            int pageSize = 8;
            var pagedUsers = users.ToPagedList(page, pageSize);
            ViewData["filterRole"] = filterRole;

            return View(pagedUsers);
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

            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                {
                    await AddToTable(role.RoleName, user.Id);
                }
            }

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
            if (!ModelState.IsValid)
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
            List<string> rolesRemoved = new List<string>();
            List<string> rolesAdded = new List<string>();

            // Iterate through the roles in model.Roles
            foreach (var role in model.Roles)
            {
                // If the role is selected but not already in userRoles, add it
                if (role.IsSelected && !userRoles.Contains(role.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                    rolesAdded.Add(role.RoleName);
                }

                // If the role is not selected but is in userRoles, remove it
                else if (!role.IsSelected && userRoles.Contains(role.RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                    rolesRemoved.Add(role.RoleName);
                }
            }

            // Perform table updates only after all roles are processed
            if (rolesRemoved.Any() || rolesAdded.Any())
            {
                foreach (var role in rolesRemoved)
                {
                    RemoveFromTable(role, user.Id);
                }

                foreach (var role in rolesAdded)
                {
                    await AddToTable(role, user.Id);
                }

                // If the current user is modifying their own roles, reset their session
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && currentUser == user)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(currentUser, isPersistent: false);
                }
            }

            return RedirectToAction(nameof(Index));

        }
        #endregion

        private async Task AddToTable(string tableName, string UserId)
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(tableName))
            {
                return;
            }
            var _user = await _userManager.FindByIdAsync(UserId);

            switch (tableName)
            {
                case "Doctor":
                    _user.Doctor = new Doctor { UserId = UserId };
                    break;
                case "Receptionist":
                    _user.Receptionist = new Receptionist { UserId = UserId };
                    break;
                case "Patient":
                    _user.Patient = new Patient { UserId = UserId };
                    break;
                case "Nurse":
                    _user.Nurse = new Nurse { UserId = UserId };
                    break;
                case "Pharmacist":
                    _user.Pharmacist = new Pharmacist { UserId = UserId };
                    break;
            }

            await _userManager.UpdateAsync(_user);
        }

        private void RemoveFromTable(string tableName, string UserId)
        {
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(tableName))
            {
                return;
            }

            switch (tableName)
            {
                case "Doctor":
                    var doctor = _unitOfWork.Repository<Doctor>().Get(UserId);
                    _unitOfWork.Repository<Doctor>().Delete(doctor);
                    _unitOfWork.Complete();
                    break;
                case "Receptionist":
                    var receptionist = _unitOfWork.Repository<Receptionist>().Get(UserId);
                    _unitOfWork.Repository<Receptionist>().Delete(receptionist);
                    _unitOfWork.Complete();
                    break;
                case "Patient":
                    var patient = _unitOfWork.Repository<Patient>().Get(UserId);
                    _unitOfWork.Repository<Patient>().Delete(patient);
                    _unitOfWork.Complete();
                    break;
                case "Nurse":
                    var nurse = _unitOfWork.Repository<Nurse>().Get(UserId);
                    _unitOfWork.Repository<Nurse>().Delete(nurse);
                    _unitOfWork.Complete();
                    break;
                case "Pharmacist":
                    var pharmacist = _unitOfWork.Repository<Pharmacist>().Get(UserId);
                    _unitOfWork.Repository<Pharmacist>().Delete(pharmacist);
                    _unitOfWork.Complete();
                    break;
            }
        }
    }
}

