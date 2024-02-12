using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using ATM.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using ATM.Areas.Identity.Data;

namespace ATM.AppServices.Authorization
{
    public class CustomerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Customer>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Customer resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != CustomerPermissions.ViewDashboard)
            {
                return Task.CompletedTask;
            }

            if (resource.LoginUserId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
