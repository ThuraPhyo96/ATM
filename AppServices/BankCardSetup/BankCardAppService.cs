using ATM.AppServices.BankAccountSetup.Dtos;
using ATM.AppServices.BankCardSetup.Dtos;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.Data;
using ATM.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.AppServices.BankCardSetup
{
    public class BankCardAppService : IBankCardAppService
    {
        private readonly ATMContext _context;
        private readonly IMapper _mapper;

        public BankCardAppService(ATMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get
        public async Task<BankCardDto> GetDetailById(int id)
        {
            var obj = await _context.BankCards.FindAsync(id);
            if (obj == null)
            {
                return new BankCardDto();
            }
            var existingObj = await _context.BankCards
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.BankAccount)
                .FirstOrDefaultAsync(x => x.BankCardId == id);
            return _mapper.Map<BankCardDto>(existingObj);
        }

        public async Task<BankCardDto> GetDetailByGuid(string guid)
        {
            if (!await _context.BankCards.AnyAsync(x => x.BankCardGuid.ToString() == guid))
            {
                return new BankCardDto();
            }
            var existingObj = await _context.BankCards
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.BankAccount)
                .FirstOrDefaultAsync(x => x.BankCardGuid.ToString() == guid);
            return _mapper.Map<BankCardDto>(existingObj);
        }

        public IReadOnlyList<BankCardDto> GetAll(SearchBankCardDto input)
        {
            var predicate = PredicateBuilder.True<BankCard>();

            if (!string.IsNullOrEmpty(input.CustomerName))
                predicate = predicate.And(p => p.Customer.FirstName.Contains(input.CustomerName));

            if (!string.IsNullOrEmpty(input.AccountNumber))
                predicate = predicate.And(p => p.BankAccount.AccountNumber.Contains(input.AccountNumber));

            if (!string.IsNullOrEmpty(input.CardNumber))
                predicate = predicate.And(p => p.BankCardNumber.Contains(input.CardNumber));

            if (input.FromValidDate.HasValue)
                predicate = predicate.And(p => p.ValidDate >= input.FromValidDate);

            if (input.ToValidDate.HasValue)
                predicate = predicate.And(p => p.ValidDate <= input.ToValidDate);

            var objs = _context.BankCards
                .AsNoTracking()
                .Where(predicate)
                .Include(x => x.Customer)
                .Include(x => x.BankAccount)
                .Include(x => x.UpdatedUser)
                .OrderBy(x => x.Customer.FirstName).ThenByDescending(x => x.CreationTime)
                .AsQueryable();

            return _mapper.Map<List<BankCardDto>>(objs);
        }
        #endregion

        #region Create
        public async Task<BankCardDto> CreateBankCard(CreateBankCardDto input)
        {
            if (input == null)
            {
                return new BankCardDto();
            }
            var obj = _mapper.Map<BankCard>(input);
            await _context.BankCards.AddAsync(obj);
            _context.SaveChanges();
            return _mapper.Map<BankCardDto>(obj);
        }

        public async Task<string> CheckDuplicateOnCreate(string cardNumber)
        {
            if (await _context.BankCards.AnyAsync(x => x.BankCardNumber == cardNumber))
                return SBankCardMessage.DuplicatedCardNumber;
            else
                return string.Empty;
        }
        #endregion

        #region Update
        public async Task<BankCardDto> UpdateBankCard(UpdateBankCardDto input)
        {
            if (!await _context.BankCards.AnyAsync(x => x.BankAccountId == input.BankCardId))
            {
                return new BankCardDto();
            }

            var existingObj = await _context.BankCards.FirstOrDefaultAsync(x => x.BankAccountId == input.BankCardId);
            _mapper.Map(input, existingObj);
            _context.BankCards.Update(existingObj);
            _context.SaveChanges();
            return _mapper.Map<BankCardDto>(existingObj);
        }
        #endregion
    }
}
