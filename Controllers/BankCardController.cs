using ATM.AppServices.BankAccountSetup;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.BankCardSetup;
using ATM.AppServices.BankCardSetup.Dtos;
using ATM.AppServices.CustomerSetup;
using ATM.AppServices.PredefinedSeedData;
using ATM.Areas.Identity.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class BankCardController : Controller
    {
        private readonly IBankCardAppService _bankCardAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly IBankAccountAppService _bankAccountAppService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BankCardController(IBankCardAppService bankCardAppService, ICustomerAppService customerAppService,
            IBankAccountAppService bankAccountAppService, UserManager<ApplicationUser> userManager)
        {
            _bankCardAppService = bankCardAppService;
            _customerAppService = customerAppService;
            _bankAccountAppService = bankAccountAppService;
            _userManager = userManager;
        }

        // GET: BankCardController
        public ActionResult Index()
        {
            var result = _bankCardAppService.GetAll(new SearchBankCardDto());
            return View(result);
        }

        // GET: BankCardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<ActionResult> CreateBankCardModal(string customerGuid, string bankAccountGuid)
        {
            await GetDropdownItems(customerGuid);
            var customer = await _customerAppService.GetDetailByGuid(customerGuid);
            var bankAccunt = await _bankAccountAppService.GetDetailByGuid(bankAccountGuid);
            CreateBankCardDto createBankAccount = new CreateBankCardDto()
            {
                CustomerId = customer.CustomerId,
                Customer = customer,
                BankAccountId = bankAccunt.BankAccountId,
                BankAccountGuid = bankAccountGuid,
                BankAccount = bankAccunt
            };
            return PartialView("_CreateBankCardModal", createBankAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBankCard(CreateBankCardDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                string message = await _bankCardAppService.CheckDuplicateOnCreate(input.CustomerId, input.BankAccountId, input.BankCardNumber);
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.DuplicatedCardNumber;
                    return View();
                }

                input.CreatedUserId = loginUser.Id;
                var bankCard = await _bankCardAppService.CreateBankCard(input);

                if (bankCard.BankCardId != 0)
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.CreateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.CreateFail;

                return Redirect(Url.Action("Detail", "BankAccount", new { guid = input.BankAccountGuid }));
            }
            catch
            {
                return View();
            }
        }

        // GET: BankCardController/Edit/5
        public async Task<ActionResult> UpdateBankCardModal(string guid)
        {
            var bankCard = await _bankCardAppService.GetDetailByGuid(guid);
            UpdateBankCardDto updateBankCardDto = new UpdateBankCardDto()
            {
                BankCardId = bankCard.BankCardId,
                BankAccountGuid = bankCard.BankAccount.BankAccountGuid.ToString(),
                ValidDate = bankCard.ValidDate
            };
            return PartialView("~/Views/BankCard/_UpdateBankCardModal.cshtml", updateBankCardDto);
        }

        // POST: BankCardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBankCard(UpdateBankCardDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                input.UpdatedUserId = loginUser.Id;
                var bankCard = await _bankCardAppService.UpdateBankCard(input);

                if (bankCard.BankCardId != 0)
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.UpdateSuccess;
                else
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.UpdateFail;

                if (User.IsInRole(RoleNames.Customer))
                    return Redirect(Url.Action("Index", "Home"));
                else
                    return Redirect(Url.Action("Detail", "BankAccount", new { guid = input.BankAccountGuid }));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> UpdateBankCardPINModal(string guid)
        {
            var bankCard = await _bankCardAppService.GetDetailByGuid(guid);
            UpdateBankCardPINDto updateBankCardPIN = new UpdateBankCardPINDto()
            {
                BankCardId = bankCard.BankCardId,
                BankAccountGuid = bankCard.BankAccount.BankAccountGuid.ToString(),
                PIN = bankCard.PIN
            };
            return PartialView("~/Views/BankCard/_UpdateBankCardPINModal.cshtml", updateBankCardPIN);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBankCardPIN(UpdateBankCardPINDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                input.UpdatedUserId = loginUser.Id;
                var bankCard = await _bankCardAppService.UpdateBankCardPIN(input);

                if (bankCard.BankCardId != 0)
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.UpdatePin;
                else
                    TempData[SMessage.SuccessMessage] = SBankCardMessage.UpdatePinFail;

                string backUrl = Url.Action("Detail", "BankAccount", new { guid = input.BankAccountGuid });
                if (User.IsInRole(RoleNames.Customer))
                    return Redirect(Url.Action("Index", "Home"));
                else
                    return Redirect(Url.Action("Detail", "BankAccount", new { guid = input.BankAccountGuid }));
            }
            catch
            {
                return View();
            }
        }

        public async Task GetDropdownItems(string customerGuid)
        {
            ViewData[SCustomerMessage.Customers] = _customerAppService.GetAllCustomers();
            ViewData[SBankCardMessage.BankAccounts] = await _bankAccountAppService.GetBankAccountByCustomerId(customerGuid);
        }
    }
}
