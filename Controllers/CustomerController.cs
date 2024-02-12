using ATM.AppServices.Authentication;
using ATM.AppServices.Authentication.Dtos;
using ATM.AppServices.BankAccountSetup;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.BankCardSetup;
using ATM.AppServices.BankCardSetup.Dtos;
using ATM.AppServices.CustomerSetup;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.AppServices.PredefinedSeedData;
using ATM.Areas.Identity.Data;
using ATM.Data;
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
        private readonly IAuthenticationAppService _authenticationAppService;
        private readonly IBankAccountAppService _bankAccountAppService;
        private readonly IBankCardAppService _bankCardAppService;

        public CustomerController(ICustomerAppService customerAppService, UserManager<ApplicationUser> userManager,
            IAuthenticationAppService authenticationAppService,
            IBankAccountAppService bankAccountAppService,
            IBankCardAppService bankCardAppService)
        {
            _customerAppService = customerAppService;
            _userManager = userManager;
            _authenticationAppService = authenticationAppService;
            _bankAccountAppService = bankAccountAppService;
            _bankCardAppService = bankCardAppService;
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
        public async Task<ActionResult> Detail(string guid)
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

        // POST: CustomerController/CreateLoginAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLoginAccount(CreateApplicationUserDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                var customer = await _customerAppService.GetDetailByGuid(input.CustomerGuid);
                input.UserName = customer.FirstName[..1] + customer.MobileNumber;

                string message = await _authenticationAppService.CheckDuplidcateUser(input.UserName);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[SMessage.SuccessMessage] = SCustomerMessage.DuplicatedNRIC;
                    return View();
                }

                input.Email = customer.Email;
                input.Password = "Sky@123wall";
                input.RoleName = RoleNames.Customer;
                input.CreatedUserId = loginUser.Id;
                var userId = await _authenticationAppService.CreateUser(input);

                if (!string.IsNullOrEmpty(userId))
                {
                    await _customerAppService.UpdateLoginAccountToCustomer(new UpdateLoginAccountDto(customer.CustomerId, userId, loginUser.Id));
                    TempData[SMessage.SuccessMessage] = SUserMessage.CreateSuccess;
                }
                else
                    TempData[SMessage.SuccessMessage] = SUserMessage.CreateFail;

                return RedirectToAction(nameof(Detail), new { guid = customer.CustomerGuid.ToString() });
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBankAccountModal(string guid)
        {
            CustomerDto result = await _customerAppService.GetDetailByGuid(guid);
            ViewData[SBankAccountMessage.AccountTypes] = AccountTypes.GetAll();

            CreateBankAccountDto createBankAccount = new CreateBankAccountDto()
            {
                CustomerId = result.CustomerId,
                CustomerGuid = result.CustomerGuid.ToString(),
                Balance = 1000
            };

            return PartialView("_CreateBankAccountModal", createBankAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBankAccount(CreateBankAccountDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                string message = await _bankAccountAppService.CheckDuplicateOnCreate(input.AccountNumber);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.DuplicatedAccount;
                    return View();
                }

                input.CreatedUserId = loginUser.Id;
                var bankAccount = await _bankAccountAppService.CreateBankAccount(input);

                if (bankAccount.BankAccountId != 0)
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.CreateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.CreateFail;

                return RedirectToAction(nameof(Detail), new { guid = input.CustomerGuid });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Profile(string guid)
        {
            CustomerDto result = await _customerAppService.GetDetailByGuid(guid);

            if (result == null)
                return View(SPartialViews.AccessDenied);

            return View(result);
        }


        //#region Bank Card
        //[HttpPost]
        //public async Task<ActionResult> CreateBankCardModal(string guid)
        //{
        //    CustomerDto result = await _customerAppService.GetDetailByGuid(guid);

        //    CreateBankCardDto bankCard = new CreateBankCardDto()
        //    {
        //        CustomerId = result.CustomerId
        //    };

        //    return PartialView("_CreateBankCardModal", bankCard);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateBankCard(CreateBankCardDto input)
        //{
        //    try
        //    {
        //        var loginUser = await _userManager.GetUserAsync(User);
        //        if (loginUser == null)
        //            return View();

        //        string message = await _bankCardAppService.CheckDuplicateOnCreate(input.BankCardNumber);
        //        if (!string.IsNullOrEmpty(message))
        //        {
        //            TempData[SMessage.SuccessMessage] = SBankCardMessage.DuplicatedCardNumber;
        //            return View();
        //        }

        //        input.CreatedUserId = loginUser.Id;
        //        var bankCard = await _bankCardAppService.CreateBankCard(input);

        //        if (bankCard.BankCardId != 0)
        //            TempData[SMessage.SuccessMessage] = SBankCardMessage.CreateSuccess;
        //        else
        //            TempData[SMessage.SuccessMessage] = SBankCardMessage.CreateFail;

        //        return RedirectToAction(nameof(Detail), new { guid = input.CustomerGuid });
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public void GetBankCardDropdownItems(string guid)
        //{
        //    ViewData[SBankCardMessage.BankAccounts] = _bankAccountAppService.GetBankAccountByCustomerId(guid);
        //}
        //#endregion
    }
}
