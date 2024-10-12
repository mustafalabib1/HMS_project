using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DALProject.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HMSdbcontext _dbContext;

        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            HMSdbcontext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            // 1. Apply Any Pending Migrations
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            // 2. Create Roles if they are not created

            if (!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Doctor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Nurse)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Pharmacist)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Receptionist)).GetAwaiter().GetResult();

                // 3. If there were no roles, Create Default Admin User
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin",
                    Email = "admin@hmsproject.com"
                }, "Admin#123").GetAwaiter().GetResult();

                AppUser user = _dbContext.AppUsers.FirstOrDefault(u => u.Email == "admin@hmsproject.com");
                _userManager.AddToRoleAsync(user, Roles.Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
