using ATM.AppServices.BankAccountSetup;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.CustomerSetup;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.AppServices.PredefinedSeedData;
using ATM.Areas.Identity.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountAppService _bankAccountAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerAppService _customerAppService;

        public BankAccountController(IBankAccountAppService bankAccountAppService, UserManager<ApplicationUser> userManager,
            ICustomerAppService customerAppService)
        {
            _bankAccountAppService = bankAccountAppService;
            _userManager = userManager;
            _customerAppService = customerAppService;
        }

        // GET: BankAccountController
        public ActionResult Index()
        {
            var results = _bankAccountAppService.GetAll(new SearchBankAccountDto());
            return View(results);
        }

        // GET: BankAccountController/Create
        public ActionResult CreateModal()
        {
            GetDropdownItems();
            CreateBankAccountDto createBankAccount = new CreateBankAccountDto()
            {
                Balance = 1000
            };
            return PartialView("_CreateModal", createBankAccount);
        }

        // POST: BankAccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBankAccountDto input)
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

                var customer = await _customerAppService.GetDetailByGuid(input.CustomerGuid);
                input.CreatedUserId = loginUser.Id;
                input.CustomerId = customer.CustomerId;
                var bankAccount = await _bankAccountAppService.CreateBankAccount(input);

                if (bankAccount.BankAccountId != 0)
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.CreateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.CreateFail;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BankAccountController/Edit/5
        public async Task<ActionResult> EditModal(string guid)
        {
            GetDropdownItems();
            var result = await _bankAccountAppService.GetDetailByGuid(guid);
            return PartialView("_EditModal", result);
        }

        // POST: BankAccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateBankAccountDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                input.UpdatedUserId = loginUser.Id;
                var bankAccount = await _bankAccountAppService.UpdateBankAccount(input);

                if (bankAccount.BankAccountId != 0)
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.UpdateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.UpdateFail;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BankAccountController/Delete/5
        public async Task<ActionResult> UpdateBalanceModal(string guid)
        {
            var result = await _bankAccountAppService.GetDetailByGuid(guid);
            UpdateBalanceDto updateBalance = new UpdateBalanceDto()
            {
                BankAccountId = result.BankAccountId,
                BankAccountGuid = result.BankAccountGuid.ToString(),
                AccountNumber = result.AccountNumber,
                CurrentBalance = result.Balance
            };
            return PartialView("_UpdateBalanceModal", updateBalance);
        }

        // POST: BankAccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBalance(UpdateBalanceDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                input.UpdatedUserId = loginUser.Id;
                var bankAccount = await _bankAccountAppService.UpdateBalance(input);

                if (bankAccount.BankAccountId != 0)
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.UpdateBalanceSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankAccountMessage.UpdateBalanceFail;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public void GetDropdownItems()
        {
            ViewData[SCustomerMessage.Customers] = _bankAccountAppService.GetAllCustomers();
            ViewData[SBankAccountMessage.AccountTypes] = AccountTypes.GetAll();
        }
    }
}
