using ATM.AppServices.BalanceHistorySetup;
using ATM.AppServices.BalanceHistorySetup.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ATM.Controllers
{
    public class BalanceHistoryController : Controller
    {
        private readonly IBalanceHistoryAppService _balanceHistoryAppService;

        public BalanceHistoryController(IBalanceHistoryAppService balanceHistoryAppService)
        {
            _balanceHistoryAppService = balanceHistoryAppService;
        }

        // GET: BalanceHistoryController
        public ActionResult Index(string cguid, string aguid)
        {
            ViewBag.BackUrl = Url.Action("Index", "Home");
            DateTime today = DateTime.Today;
            SearchBalanceHistoryDto search = new SearchBalanceHistoryDto()
            {
                CustomerGuid = cguid,
                BankAccountGuid = aguid,
                FromTransactionDate = today,
                ToTransactionDate = today.AddDays(6)
            };
            var result = _balanceHistoryAppService.GetAllByAccountId(search);
            return View(result);
        }

        // GET: BalanceHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BalanceHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BalanceHistoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: BalanceHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BalanceHistoryController/Edit/5
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

        // GET: BalanceHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BalanceHistoryController/Delete/5
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
