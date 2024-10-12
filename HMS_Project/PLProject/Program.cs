using BLLProject;
using BLLProject.Interfaces;
using BLLProject.Repositories;
using DALProject.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Core.Types;
using DALProject.model;
using DALProject.DbInitializer;

namespace PLProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            #region DbContext
            builder.Services.AddDbContext<HMSdbcontext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); 
            #endregion

            #region Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<HMSdbcontext>()
        .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configure the cookie settings used for authentication middleware.
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            }); 
            #endregion

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<HMSdbcontextProcedures>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Always add authentication before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #region Update and Initialize Database
            // Applies pending migrations also if there are no roles, create the default
            // admin user with email = admin@hmsproject.com and password = Admin#123

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.Initialize();
            }
            #endregion

            app.Run();
        }
    }
}
