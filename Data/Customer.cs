using ATM.Areas.Identity.Data;
using ATM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATM.Data
{
    public class Customer : AuditInfo
    {
        public int CustomerId { get; set; }
        public Guid CustomerGuid { get; set; }

        [StringLength(MaxLength.Max_100)]
        public string FirstName { get; set; }

        [StringLength(MaxLength.Max_100)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(MaxLength.Max_50)]
        public string NRIC { get; set; }

        [StringLength(MaxLength.Max_100)]
        public string FatherName { get; set; }

        [StringLength(MaxLength.Max_100)]
        public string Email { get; set; }

        [StringLength(MaxLength.Max_100)]
        public string JobTitle { get; set; }

        [StringLength(MaxLength.Max_1000)]
        public string Address { get; set; }

        public string LoginUserId { get; set; }
        public ApplicationUser LoginUser { get; set; }

        public bool IsActive { get; set; }

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }

        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public List<BalanceHistory> BalanceHistories { get; set; } = new List<BalanceHistory>();
    }
}
