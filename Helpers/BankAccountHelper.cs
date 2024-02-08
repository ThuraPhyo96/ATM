using ATM.Data;
using Newtonsoft.Json.Linq;

namespace ATM.Helpers
{
    public static class BankAccountHelper
    {
        public static string GetAccountType(int accountType)
        {
            return accountType switch
            {
                (int)EBankAccountType.Saving => EnumHelper.GetDescription(EBankAccountType.Saving),
                (int)EBankAccountType.Special => EnumHelper.GetDescription(EBankAccountType.Special),
                _ => "Empty",
            };
        }
    }
}
