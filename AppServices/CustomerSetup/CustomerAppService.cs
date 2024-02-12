using ATM.AppServices.BalanceHistorySetup.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Data;
using ATM.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATM.AppServices.CustomerSetup
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ATMContext _context;
        private readonly IMapper _mapper;

        public CustomerAppService(ATMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get
        public async Task<CustomerDto> GetDetailById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return new CustomerDto();
            }
            var existingObj = await _context.Customers
                .AsNoTracking()
                .Include(x => x.LoginUser)
                .Include(x => x.BankAccounts).ThenInclude(x => x.BankCards)
                .Include(x => x.BalanceHistories)
                .FirstOrDefaultAsync(x => x.CustomerId == id);
            return _mapper.Map<CustomerDto>(existingObj);
        }

        public async Task<CustomerDto> GetDetailByGuid(string guid)
        {
            if (!await _context.Customers.AnyAsync(x => x.CustomerGuid.ToString() == guid))
            {
                return new CustomerDto();
            }
            var existingObj = await _context.Customers
                .AsNoTracking()
                .Include(x => x.LoginUser)
                .Include(x => x.BankAccounts).ThenInclude(x => x.BankCards)
                .Include(x => x.BalanceHistories)
                .FirstOrDefaultAsync(x => x.CustomerGuid.ToString() == guid);
            return _mapper.Map<CustomerDto>(existingObj);
        }

        public List<CustomerDto> GetAll(SearchCustomerDto input)
        {
            var predicate = PredicateBuilder.True<Customer>();

            if (!string.IsNullOrEmpty(input.NRIC))
                predicate = predicate.And(p => p.NRIC.Contains(input.NRIC));

            var objs = _context.Customers
                .AsNoTracking()
                .Where(predicate)
                .Include(x => x.CreatedUser)
                .Include(x => x.UpdatedUser)
                .OrderBy(x => x.FirstName)
                .AsQueryable();

            return _mapper.Map<List<CustomerDto>>(objs);
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

        public async Task<CustomerDto> GetDetailByLoginUserId(string loginUserId)
        {
            if (!await _context.Customers.AnyAsync(x => x.LoginUserId == loginUserId))
            {
                return new CustomerDto();
            }

            var existingObj = await _context.Customers
                .AsNoTracking()
                .Include(x => x.LoginUser)
                .Include(x => x.BankAccounts).ThenInclude(x => x.BankCards)
                .Include(x => x.BalanceHistories)
                .FirstOrDefaultAsync(x => x.LoginUserId == loginUserId);
            return _mapper.Map<CustomerDto>(existingObj);
        }

        public IReadOnlyList<DashboardItemDto> GetAll()
        {
            DateTime today = DateTime.Today;    
            DateTime lastSevenDay = today.AddDays(-7);

            var objs = _context.Customers
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .AsQueryable();

            List<CustomerDto> customers = _mapper.Map<List<CustomerDto>>(objs);
            decimal todayCount = customers.Where(x => x.CreationTime.Value.ToString("dd-MM-yyyy") == today.ToString("dd-MM-yyyy")).Count();
            decimal lastSevenDayCount  = customers.Where(x => x.CreationTime >= lastSevenDay && x.CreationTime < today).Count();
            decimal currentMonthAmount = customers.Where(x => x.CreationTime.Value.Month == today.Month).Count();

            return new List<DashboardItemDto>()
            {
                new DashboardItemDto("Today", todayCount),
                new DashboardItemDto("Last 7 Days", lastSevenDayCount),
                new DashboardItemDto("This Month", currentMonthAmount)
            };
        }
        #endregion

        #region Create
        public async Task<CustomerDto> CreateCustomer(CreateCustomerDto input)
        {
            if (input == null)
            {
                return new CustomerDto();
            }
            var obj = _mapper.Map<Customer>(input);
            await _context.Customers.AddAsync(obj);
            _context.SaveChanges();
            return _mapper.Map<CustomerDto>(obj);
        }

        public async Task<string> CheckDuplicateOnCreate(string nric)
        {
            if (await _context.Customers.AnyAsync(x => x.NRIC == nric))
                return SCustomerMessage.DuplicatedNRIC;
            else
                return string.Empty;
        }
        #endregion

        #region Update
        public async Task<CustomerDto> UpdateCustomer(UpdateCustomerDto input)
        {
            if (!await _context.Customers.AnyAsync(x => x.CustomerId == input.CustomerId))
            {
                return new CustomerDto();
            }

            var existingObj = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == input.CustomerId);
            _mapper.Map(input, existingObj);
            _context.Customers.Update(existingObj);
            _context.SaveChanges();
            return _mapper.Map<CustomerDto>(existingObj);
        }

        public async Task<string> CheckDuplicateOnUpdate(int id, string nric)
        {
            if (await _context.Customers.AnyAsync(x => x.CustomerId != id && x.NRIC == nric))
                return SCustomerMessage.DuplicatedNRIC;
            else
                return string.Empty;
        }

        public async Task<CustomerDto> UpdateLoginAccountToCustomer(UpdateLoginAccountDto input)
        {
            if (!await _context.Customers.AnyAsync(x => x.CustomerId == input.CustomerId))
            {
                return new CustomerDto();
            }

            var existingObj = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == input.CustomerId);
            _mapper.Map(input, existingObj);
            _context.Customers.Update(existingObj);
            _context.SaveChanges();
            return _mapper.Map<CustomerDto>(existingObj);
        }
        #endregion
    }
}
