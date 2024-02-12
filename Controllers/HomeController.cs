using ATM.AppServices.CustomerSetup;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Areas.Identity.Data;
using ATM.Helpers;
using ATM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerAppService _customerAppService;

        public HomeController(ILogger<HomeController> logger, IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager,
             ICustomerAppService customerAppService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _customerAppService = customerAppService;
        }

        public async Task<IActionResult> Index()
        {
            CustomerDto customer = new CustomerDto();
            string roleName = string.Empty;
            var currentUser = await _userManager.GetUserAsync(User);

            if (User.IsInRole(RoleNames.SuperAdmin))
            {
                roleName = RoleNames.SuperAdmin;
            }

            if (User.IsInRole(RoleNames.Customer))
            {
                customer = await _customerAppService.GetDetailByLoginUserId(currentUser.Id);
                roleName = RoleNames.Customer;
            }

            ViewBag.RoleName = roleName;
            return View(customer);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
