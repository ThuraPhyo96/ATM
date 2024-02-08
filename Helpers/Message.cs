﻿namespace ATM.Helpers
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

        public const string DuplicatedNRIC = "Error: NRIC already exist and does not allow duplication!";
    }

    public struct SPartialViews
    {
        public const string ActionAlert = "_ActionMessage";
        public const string AccessDenied = "AccessDenied";
    }
}
