using ATM.Data;
using ATM.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using ATM.AppServices.CustomerSetup.Dtos;
using System.ComponentModel;

namespace ATM.AppServices.BankAccountSetup.Dtos
{
    public class BankAccountDto : AuditInfo
    {
        public int BankAccountId { get; set; }
        public Guid BankAccountGuid { get; set; }

        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        [StringLength(MaxLength.Max_20)]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        [Required]
        [Display(Name = "Balance")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Display(Name = "Enables?")]
        public bool IsActive { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class CreateBankAccountDto : CreatedUser
    {
        public int CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerGuid { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        [StringLength(MaxLength.Max_20)]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        [Required]
        [Display(Name = "Balance")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateBankAccountDto : UpdatedUser
    {
        public int BankAccountId { get; set; }
        public string BankAccountGuid { get; set; }

        [Description("Enables?")]
        public bool IsActive { get; set; }

        [Description("Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class UpdateBalanceDto : UpdatedUser
    {
        public int BankAccountId { get; set; }
        public string BankAccountGuid { get; set; }

        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Display(Name = "Current Balance")]
        public decimal CurrentBalance { get; set; }

        [Display(Name = "Balance")]
        public decimal Balance { get; set; }
    }

    public class SearchBankAccountDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public int AccountType { get; set; } = 0;
    }

}
