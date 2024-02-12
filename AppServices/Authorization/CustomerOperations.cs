using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.VisualBasic;

namespace ATM.AppServices.Authorization
{
    public static class CustomerOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = CustomerPermissions.ViewDashboard };
        //public static OperationAuthorizationRequirement Read =
        //  new OperationAuthorizationRequirement { Name = AdminPermissions.ViewCustomer };
        //public static OperationAuthorizationRequirement Update =
        //  new OperationAuthorizationRequirement { Name = AdminPermissions.ViewBankAccount };
        //public static OperationAuthorizationRequirement Delete =
        //  new OperationAuthorizationRequirement { Name = AdminPermissions.ViewBankCard };
    }

    //public class Constants
    //{
    //    public static readonly string WithdrawMoneyName = "Create";
    //    public static readonly string DepositMoneyName = "Read";
    //    public static readonly string ViewHistoryName = "Update";
    //    public static readonly string DeleteOperationName = "Delete";
    //    public static readonly string ApproveOperationName = "Approve";
    //    public static readonly string RejectOperationName = "Reject";

    //    public static readonly string ContactAdministratorsRole = "ContactAdministrators";
    //    public static readonly string ContactManagersRole = "ContactManagers";
    //}

    public class CustomerPermissions
    {
        public static readonly string ViewDashboard = "ViewDashboard";
    }
}
