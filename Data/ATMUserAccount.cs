using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ATM.Data;
using ATM.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ATM.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ATMUserAccount class
    public class ATMUserAccount : IdentityUser
    {
        public int UserType { get; set; }
        public bool IsActive { get; set; }

        #region Audit Log Info
        [StringLength(MaxLength.Max_450)]
        public string CreatedUserId { get; set; }
        public DateTime? CreationTime { get; set; }

        [StringLength(MaxLength.Max_450)]
        public string UpdatedUserId { get; set; }
        public DateTime? UpdatedTime { get; set; }
        #endregion

        public ATMUserAccount()
        {

        }
    }

    public enum EUserType
    {
        SuperAdmin = 1,
        Customer = 2
    }
}
