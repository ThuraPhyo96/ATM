using ATM.AppServices.BalanceHistorySetup;
using ATM.AppServices.BalanceHistorySetup.Dtos;
using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Data;
using ATM.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.AppServices.BankAccountSetup
{
    public class BankAccountAppService : IBankAccountAppService
    {
        protected readonly ATMContext _context;
        protected readonly IMapper _mapper;
        protected readonly IBalanceHistoryAppService _balanceHistoryAppService;

        public BankAccountAppService(ATMContext context, IMapper mapper, IBalanceHistoryAppService balanceHistoryAppService)
        {
            _context = context;
            _mapper = mapper;
            _balanceHistoryAppService = balanceHistoryAppService;
        }

        #region Get
        public async Task<BankAccountDto> GetDetailById(int id)
        {
            var obj = await _context.BankAccounts.FindAsync(id);
            if (obj == null)
            {
                return new BankAccountDto();
            }
            var existingObj = await _context.BankAccounts
                .AsNoTracking()
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.BankAccountId == id);
            return _mapper.Map<BankAccountDto>(existingObj);
        }

        public async Task<BankAccountDto> GetDetailByGuid(string guid)
        {
            if (!await _context.BankAccounts.AnyAsync(x => x.BankAccountGuid.ToString() == guid))
            {
                return new BankAccountDto();
            }
            var existingObj = await _context.BankAccounts
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.BankCards)
                .FirstOrDefaultAsync(x => x.BankAccountGuid.ToString() == guid);
            return _mapper.Map<BankAccountDto>(existingObj);
        }

        public List<BankAccountDto> GetAll(SearchBankAccountDto input)
        {
            var predicate = PredicateBuilder.True<BankAccount>();

            if (!string.IsNullOrEmpty(input.CustomerName))
                predicate = predicate.And(p => p.Customer.FirstName.Contains(input.CustomerName));

            if (!string.IsNullOrEmpty(input.AccountNumber))
                predicate = predicate.And(p => p.AccountNumber.Contains(input.AccountNumber));

            if (input.AccountType != 0)
                predicate = predicate.And(p => p.AccountType == input.AccountType);

            var objs = _context.BankAccounts
                .AsNoTracking()
                .Where(predicate)
                .Include(x => x.Customer)
                .Include(x => x.UpdatedUser)
                .OrderBy(x => x.Customer.FirstName).ThenByDescending(x => x.CreationTime)
                .AsQueryable();

            return _mapper.Map<List<BankAccountDto>>(objs);
        }

        public async Task<IReadOnlyList<SelectListItem>> GetBankAccountByCustomerId(string guid)
        {
            if (!await _context.BankAccounts.AnyAsync(x => x.Customer.CustomerGuid.ToString() == guid))
            {
                return new List<SelectListItem>();
            }
            return await _context.BankAccounts
                .AsNoTracking()
                .Where(x => x.Customer.CustomerGuid.ToString() == guid)
                .Select(x => new SelectListItem()
                {
                    Value = x.BankAccountGuid.ToString(),
                    Text = x.AccountNumber
                }).ToListAsync();
        }
        #endregion

        #region Create
        public async Task<BankAccountDto> CreateBankAccount(CreateBankAccountDto input)
        {
            if (input == null)
            {
                return new BankAccountDto();
            }
            var obj = _mapper.Map<BankAccount>(input);
            await _context.BankAccounts.AddAsync(obj);
            _context.SaveChanges();
            return _mapper.Map<BankAccountDto>(obj);
        }

        public async Task<string> CheckDuplicateOnCreate(string accountNo)
        {
            if (await _context.BankAccounts.AnyAsync(x => x.AccountNumber == accountNo))
                return SBankAccountMessage.DuplicatedAccount;
            else
                return string.Empty;
        }
        #endregion

        #region Update
        public async Task<BankAccountDto> UpdateBankAccount(UpdateBankAccountDto input)
        {
            if (!await _context.BankAccounts.AnyAsync(x => x.BankAccountId == input.BankAccountId))
            {
                return new BankAccountDto();
            }

            var existingObj = await _context.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountId == input.BankAccountId);
            _mapper.Map(input, existingObj);
            _context.BankAccounts.Update(existingObj);
            _context.SaveChanges();
            return _mapper.Map<BankAccountDto>(existingObj);
        }

        public async Task<BankAccountDto> UpdateBalance(UpdateBalanceDto input)
        {
            if (!await _context.BankAccounts.AnyAsync(x => x.BankAccountId == input.BankAccountId))
            {
                return new BankAccountDto();
            }

            var existingObj = await _context.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountId == input.BankAccountId);
            input.Balance += existingObj.Balance;

            _mapper.Map(input, existingObj);
            _context.BankAccounts.Update(existingObj);
            _context.SaveChanges();
            return _mapper.Map<BankAccountDto>(existingObj);
        }

        public async Task<BankAccountDto> UpdateBalanceByCustomer(UpdateBalanceByCustomerDto input)
        {
            if (!await _context.BankCards.AnyAsync(x => x.BankCardNumber == input.BankCardNumber))
            {
                return new BankAccountDto();
            }

            var card = await _context.BankCards.FirstOrDefaultAsync(x => x.BankCardNumber == input.BankCardNumber);
            var account = await _context.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountId == card.BankAccountId);

            if (input.ActionType == (int)EBalanceHistoryType.Withdraw)
                account.Balance -= input.Balance;
            else if (input.ActionType == (int)EBalanceHistoryType.Deposite)
                account.Balance += input.Balance;

            CreateBalanceHistoryDto createBalanceHistory = new CreateBalanceHistoryDto(account.CustomerId, account.BankAccountId, input.Balance, input.ActionType);
            var balanceHistory = _mapper.Map<BalanceHistory>(createBalanceHistory);

            await _context.BalanceHistories.AddAsync(balanceHistory);
            _context.BankAccounts.Update(account);

            _context.SaveChanges();
            return _mapper.Map<BankAccountDto>(account);
        }

        public async Task<string> CheckEnoughBalance(UpdateBalanceByCustomerDto input)
        {
            var card = await _context.BankCards.FirstOrDefaultAsync(x => x.BankCardNumber == input.BankCardNumber);
            var account = await _context.BankAccounts.FirstOrDefaultAsync(x => x.BankAccountId == card.BankAccountId);

            if (input.Balance > account.Balance - 1000)
                return SBankAccountMessage.InsufficientBalanceFail;
            else
                return string.Empty;
        }
        #endregion
    }
}
