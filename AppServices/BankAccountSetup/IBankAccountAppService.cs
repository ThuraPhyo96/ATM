using ATM.AppServices.BankAccountSetup.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.AppServices.BankAccountSetup
{
    public interface IBankAccountAppService
    {
        Task<BankAccountDto> GetDetailById(int id);
        Task<BankAccountDto> GetDetailByGuid(string guid);
        List<BankAccountDto> GetAll(SearchBankAccountDto input);
        Task<BankAccountDto> CreateBankAccount(CreateBankAccountDto input);
        Task<string> CheckDuplicateOnCreate(string accountNo);
        Task<BankAccountDto> UpdateBankAccount(UpdateBankAccountDto input);
        Task<BankAccountDto> UpdateBalance(UpdateBalanceDto input);
        Task<IReadOnlyList<SelectListItem>> GetBankAccountByCustomerId(string guid);
        Task<BankAccountDto> UpdateBalanceByCustomer(UpdateBalanceByCustomerDto input);
        Task<string> CheckEnoughBalance(UpdateBalanceByCustomerDto input);
    }
}
