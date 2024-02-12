using ATM.AppServices.BankCardSetup.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATM.AppServices.BankCardSetup
{
    public interface IBankCardAppService
    {
        Task<BankCardDto> GetDetailById(int id);
        Task<BankCardDto> GetDetailByGuid(string guid);
        IReadOnlyList<BankCardDto> GetAll(SearchBankCardDto input);
        Task<BankCardDto> CreateBankCard(CreateBankCardDto input);
        Task<string> CheckDuplicateOnCreate(int cid, int accountid, string cardNumber);
        Task<BankCardDto> UpdateBankCard(UpdateBankCardDto input);
        Task<string> CheckValidCardNumber(CheckBankCardDto input);
    }
}
