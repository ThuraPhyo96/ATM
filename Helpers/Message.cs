namespace ATM.Helpers
{
    public struct SMessage
    {
        public const string SuccessMessage = "_SuccessMessage";
        public const string FailMessage = "_FailMessage";
    }

    public struct SCustomerMessage
    {
        public const string CreateSuccess = "Customer has been successfully created.";
        public const string CreateFail = "Error: Creation customer failed!";
        public const string Updateuccess = "Customer has been successfully updated.";
        public const string UpdateFail = "Error: Update customer failed!";
        public const string Customers = "Customers";

        public const string DuplicatedNRIC = "Error: NRIC already exist and does not allow duplication!";
    }

    public struct SPartialViews
    {
        public const string ActionAlert = "_ActionMessage";
        public const string AccessDenied = "AccessDenied";
    }

    public struct SUserMessage
    {
        public const string CreateSuccess = "Login account has been successfully created.";
        public const string CreateFail = "Error: Creation login account failed!";
        public const string Updateuccess = "Login account has been successfully updated.";
        public const string UpdateFail = "Error: Update login account failed!";

        public const string DuplicatedAccount = "Error: Username already exist and does not allow duplication!";
    }

    public struct SBankAccountMessage
    {
        public const string CreateSuccess = "Bank account has been successfully created.";
        public const string CreateFail = "Error: Creation bank account failed!";
        public const string UpdateSuccess = "Bank account has been successfully updated.";
        public const string UpdateFail = "Error: Update Bank account failed!";
        public const string UpdateBalanceSuccess = "Balance has been successfully updated.";
        public const string UpdateBalanceFail = "Error: Update Balance failed!";
        public const string AccountTypes = "AccountTypes";
        public const string InsufficientBalanceFail = "Error: Insufficient Balance!";

        public const string DuplicatedAccount = "Error: Account number already exist and does not allow duplication!";
    }

    public struct SBankCardMessage
    {
        public const string CreateSuccess = "Bank card has been successfully created.";
        public const string CreateFail = "Error: Creation bank card failed!";
        public const string UpdateSuccess = "Bank card has been successfully updated.";
        public const string UpdateFail = "Error: Update Bank card failed!";
        public const string UpdatePin = "PIN has been successfully updated.";
        public const string UpdatePinFail = "Error: Update PIN failed!";
        public const string BankAccounts = "BankAccounts";

        public const string DuplicatedCardNumber = "Error: Bank card number already exist and does not allow duplication!";
        public const string ValidCard = "Error: Invalid card!";
    }
}
