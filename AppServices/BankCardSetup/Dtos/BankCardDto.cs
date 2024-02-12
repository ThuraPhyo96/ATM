using ATM.Data;
using ATM.Helpers;
using System.ComponentModel.DataAnnotations;
using System;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.AppServices.BankAccountSetup.Dtos;
using System.ComponentModel;

namespace ATM.AppServices.BankCardSetup.Dtos
{
    public class BankCardDto : AuditInfo
    {
        public int BankCardId { get; set; }
        public Guid BankCardGuid { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }

        [Display(Name = "Bank Account")]
        public int BankAccountId { get; set; }
        public BankAccountDto BankAccount { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [StringLength(MaxLength.Max_20)]
        public string BankCardNumber { get; set; }

        [Required]
        [Display(Name = "PIN")]
        public string PIN { get; set; }

        [Required]
        [Display(Name = "Valid Date")]
        public DateTime ValidDate { get; set; }

        [Display(Name = "Enables?")]
        public bool IsActive { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class CreateBankCardDto : CreatedUser
    {
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }

        public CustomerDto Customer { get; set; }

        [Display(Name = "Bank Account Number")]
        public int BankAccountId { get; set; }
        public string BankAccountGuid { get; set; }
        public BankAccountDto BankAccount { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [StringLength(MaxLength.Max_20)]
        public string BankCardNumber { get; set; }

        [Required]
        [Display(Name = "PIN")]
        public string PIN { get; set; }

        [Required]
        [Display(Name = "Valid Date")]
        public DateTime ValidDate { get; set; }

        [Display(Name = "Enables?")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class UpdateBankCardDto : UpdatedUser
    {
        public int BankCardId { get; set; }
        public Guid BankCardGuid { get; set; }

        [Required]
        [Display(Name = "PIN")]
        public string PIN { get; set; }

        [Display(Name = "Enables?")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class SearchBankCardDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public DateTime? FromValidDate { get; set; } = null;
        public DateTime? ToValidDate { get; set; } = null;
    }

    public class CheckBankCardDto
    {
        public string CustomerGuid { get; set; }

        public string BankAccountGuid { get; set; }

        public int ActionType { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [StringLength(MaxLength.Max_20)]
        public string BankCardNumber { get; set; }

        [Required]
        [Display(Name = "PIN")]
        public string PIN { get; set; }
    }
}
