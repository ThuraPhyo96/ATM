using ATM.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using ATM.AppServices.Authentication.Dtos;
using Microsoft.Extensions.DependencyInjection;
using ATM.Helpers;

namespace ATM.AppServices.Authentication
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationAppService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateUser(CreateApplicationUserDto input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = input.UserName,
                    Email = input.UserName,
                    IsActive = true,
                    UserType = input.UserType,
                    EmailConfirmed = true,
                };
                await _userManager.CreateAsync(user, input.Password);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            await AssignRoleToUser(user.Id, input.RoleName);
            return user.Id;
        }

        public async Task<IdentityResult> AssignRoleToUser(string uid, string role)
        {
            IdentityResult IR;
            var user = await _userManager.FindByIdAsync(uid) ?? throw new Exception("The user password was probably not strong enough!");
            IR = await _userManager.AddToRoleAsync(user, role);
            return IR;
        }

        public async Task<string> CheckDuplidcateUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
                return SUserMessage.DuplicatedAccount;
            else
                return string.Empty;
        }
    }
}
