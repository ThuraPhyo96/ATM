using ATM.AppServices.BalanceHistorySetup.Dtos;
using ATM.AppServices.BankCardSetup.Dtos;
using ATM.Data;
using ATM.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.AppServices.BalanceHistorySetup
{
    public class BalanceHistoryAppService : IBalanceHistoryAppService
    {
        private readonly ATMContext _context;
        private readonly IMapper _mapper;

        public BalanceHistoryAppService(ATMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Get
        public IReadOnlyList<BalanceHistoryDto> GetAllByAccountId(SearchBalanceHistoryDto input)
        {
            var predicate = PredicateBuilder.True<BalanceHistory>();

            if (!string.IsNullOrEmpty(input.CustomerGuid))
                predicate = predicate.And(p => p.Customer.CustomerGuid.ToString().Equals(input.CustomerGuid));

            if (!string.IsNullOrEmpty(input.BankAccountGuid))
                predicate = predicate.And(p => p.BankAccount.BankAccountGuid.ToString().Equals(input.BankAccountGuid));

            if (input.HistoryType != 0)
                predicate = predicate.And(p => p.HistoryType.Equals(input.HistoryType));

            if (input.FromTransactionDate.HasValue)
                predicate = predicate.And(p => p.TransactionDate >= input.FromTransactionDate.Value);

            if (input.ToTransactionDate.HasValue)
                predicate = predicate.And(p => p.TransactionDate <= input.ToTransactionDate.Value);

            var objs = _context.BalanceHistories
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(x => x.CreationTime).ThenByDescending(x => x.TransactionDate)
                .AsQueryable();

            return _mapper.Map<List<BalanceHistoryDto>>(objs);
        }
        #endregion
    }
}
