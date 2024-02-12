using ATM.AppServices.BankAccountSetup;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.BankCardSetup;
using ATM.AppServices.BankCardSetup.Dtos;
using ATM.Areas.Identity.Data;
using ATM.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Threading.Tasks;

namespace ATM.Controllers
{
    public class CardLoginController : Controller
    {
        private readonly IBankCardAppService _bankCardAppService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBankAccountAppService _bankAccountAppService;

        public CardLoginController(IBankCardAppService bankCardAppService, UserManager<ApplicationUser> userManager,
            IBankAccountAppService bankAccountAppService)
        {
            _bankCardAppService = bankCardAppService;
            _userManager = userManager;
            _bankAccountAppService = bankAccountAppService;
        }

        // GET: CardLoginController
        public ActionResult Index(string cguid, string aguid, int type)
        {
            return View(new CheckBankCardDto() { CustomerGuid = cguid, BankAccountGuid = aguid, ActionType = type });
        }

        // GET: CardLoginController/Details/5
        public async Task<ActionResult> CheckCredential(CheckBankCardDto input)
        {
            string isValid = await _bankCardAppService.CheckValidCardNumber(input);
            if (string.IsNullOrEmpty(isValid))
            {
                return Redirect(Url.Action("CardAction", "CardLogin", new { cardNumber = input.BankCardNumber, actionType = input.ActionType }));
            }
            else
            {
                TempData[SMessage.SuccessMessage] = isValid;
                return RedirectToAction(nameof(Index), new { cguid = input.CustomerGuid, aguid = input.BankAccountGuid, type = input.ActionType });
            }
        }

        // GET: CardLoginController/Create
        public ActionResult CardAction(string cardNumber, int actionType)
        {
            UpdateBalanceByCustomerDto result = new UpdateBalanceByCustomerDto() { BankCardNumber = cardNumber, ActionType = actionType };
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBalance(UpdateBalanceByCustomerDto input)
        {
            try
            {
                var loginUser = await _userManager.GetUserAsync(User);
                if (loginUser == null)
                    return View();

                if (input.ActionType == (int)EBalanceHistoryType.Withdraw)
                {
                    string message = await _bankAccountAppService.CheckEnoughBalance(input);
                    if (!string.IsNullOrEmpty(message))
                    {
                        TempData[SMessage.SuccessMessage] = message;
                        return RedirectToAction(nameof(Index));
                    }
                }

                input.UpdatedUserId = loginUser.Id;
                var bankAccount = await _bankAccountAppService.UpdateBalanceByCustomer(input);
                TempData[SMessage.SuccessMessage] = BalanceHistoryHelper.GetCardActionMessage(input.ActionType);

                return Redirect(Url.Action("Index", "Home"));
            }
            catch
            {
                return View();
            }
        }

        // GET: CardLoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CardLoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CardLoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CardLoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
