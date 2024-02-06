using ATM.Areas.Identity.Data;
using ATM.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.AppServices.PredefinedSeedData
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string userPw)
        {
            using var context = new ATMContext(serviceProvider.GetRequiredService<DbContextOptions<ATMContext>>());
            // For sample purposes seed both with the same password.
            // Password is set with the following:
            // dotnet user-secrets set SeedUserPW <pw>
            // The admin user can do anything
            var superAdminID = await InitialUser(serviceProvider, userPw, "super@admin.com", (int)EUserType.SuperAdmin);
            await InitialRole(serviceProvider, superAdminID, RoleNames.SuperAdmin);
        }

        private static async Task<string> InitialUser(IServiceProvider serviceProvider, string userPw, string UserName, int userType)
        {
            var userManager = serviceProvider.GetService<UserManager<ATMUserAccount>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ATMUserAccount
                {
                    UserName = UserName,
                    Email = UserName,   
                    IsActive = true,
                    UserType = userType,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, userPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> InitialRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ATMUserAccount>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
