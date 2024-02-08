﻿using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Data;
using ATM.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.AppServices.BankAccountSetup
{
    public class BankAccountAppService : IBankAccountAppService
    {
        protected readonly ATMContext _context;
        protected readonly IMapper _mapper;

        public BankAccountAppService(ATMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public IReadOnlyList<SelectListItem> GetAllCustomers()
        {
            var objs = _context.Customers
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .AsQueryable();

            var customers = _mapper.Map<List<CustomerDto>>(objs);

            return customers.Select(c => new SelectListItem
            {
                Text = $"{c.FirstName} | {c.NRIC} | {c.MobileNumber}",
                Value = c.CustomerGuid.ToString()
            }).ToList();
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
        #endregion
    }
}
