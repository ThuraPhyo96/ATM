using ATM.AppServices.CustomerSetup;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Areas.Identity.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerAppService _customerAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerController(ICustomerAppService customerAppService, UserManager<ApplicationUser> userManager)
        {
            _customerAppService = customerAppService;
            _userManager = userManager;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            var results = _customerAppService.GetAll(new SearchCustomerDto());
            return View(results);
        }

        // GET: CustomerController/Details/5
        public async Task<ActionResult> Details(string guid)
        {
            CustomerDto result = await _customerAppService.GetDetailByGuid(guid);
            return View(result);
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View(new CreateCustomerDto());
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCustomerDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                string message = await _customerAppService.CheckDuplicateOnCreate(input.NRIC);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.DuplicatedNRIC;
                    return View();
                }

                input.CreatedUserId = loginUser.Id;
                var customer = await _customerAppService.CreateCustomer(input);

                if (customer.CustomerId != 0)
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.CreateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.CreateFail;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<ActionResult> Edit(string guid)
        {
            CustomerDto result = await _customerAppService.GetDetailByGuid(guid);

            if (result == null)
                return View(SPartialViews.AccessDenied);

            return View(result);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCustomerDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                string message = await _customerAppService.CheckDuplicateOnUpdate(input.CustomerId, input.NRIC);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.DuplicatedNRIC;
                    return View();
                }

                input.UpdatedUserId = loginUser.Id;
                var customer = await _customerAppService.UpdateCustomer(input);

                if (customer.CustomerId != 0)
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.Updateuccess;
                else
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.UpdateFail;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
