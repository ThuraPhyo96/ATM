using ATM.Data;

namespace ATM.Helpers
{
    public static class BalanceHistoryHelper
    {
        public static string GetBalanceAmount(decimal amount, int type)
        {
            return type switch
            {
                (int)EBalanceHistoryType.Withdraw => $"<apan style='color: red;'>-{amount}</span>",
                (int)EBalanceHistoryType.Deposite => $"<apan style='color: green;'>+{amount}</span>",
                _ => "Empty",
            };
        }

        public static string GetCardActionType(int type)
        {
            return type switch
            {
                (int)EBalanceHistoryType.Withdraw => EnumHelper.GetDescription(EBalanceHistoryType.Withdraw),
                (int)EBalanceHistoryType.Deposite => EnumHelper.GetDescription(EBalanceHistoryType.Deposite),
                _ => "Empty",
            };
        }

        public static string GetCardActionMessage(int type)
        {
            return type switch
            {
                (int)EBalanceHistoryType.Withdraw => $"{EnumHelper.GetDescription(EBalanceHistoryType.Withdraw)} money successful.",
                (int)EBalanceHistoryType.Deposite => $"{EnumHelper.GetDescription(EBalanceHistoryType.Deposite)} money successful.",
                _ => "Empty",
            };
        }
    }
}
