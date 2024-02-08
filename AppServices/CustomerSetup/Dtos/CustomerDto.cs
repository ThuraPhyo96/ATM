using ATM.Areas.Identity.Data;
using ATM.Data;
using ATM.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using ATM.AppServices.Authentication;
using ATM.AppServices.Authentication.Dtos;
using ATM.AppServices.BankAccountSetup.Dtos;

namespace ATM.AppServices.CustomerSetup.Dtos
{
    public class CustomerDto : AuditInfo
    {
        public int CustomerId { get; set; }

        [Display(Name = "Customer")]
        public Guid CustomerGuid { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(MaxLength.Max_100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "NRIC")]
        [StringLength(MaxLength.Max_50)]
        public string NRIC { get; set; }

        [Display(Name = "Father Name")]
        [StringLength(MaxLength.Max_100)]
        public string FatherName { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        [StringLength(MaxLength.Max_100)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [StringLength(MaxLength.Max_12)]
        public string MobileNumber { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(MaxLength.Max_100)]
        public string JobTitle { get; set; }

        [Display(Name = "Address")]
        [StringLength(MaxLength.Max_1000)]
        public string Address { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }

        [Display(Name = "Enables?")]
        public bool IsActive { get; set; }

        public ApplicationUserDto LoginUser { get; set; }

        public IReadOnlyList<BankAccountDto> BankAccounts { get; set; } = new List<BankAccountDto>();

        public CreateBankAccountDto CreateBankAccount { get; set; }
    }

    public class CreateCustomerDto : CreatedUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(MaxLength.Max_100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(MaxLength.Max_100)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "NRIC")]
        [StringLength(MaxLength.Max_50)]
        public string NRIC { get; set; }

        [Display(Name = "Father Name")]
        [StringLength(MaxLength.Max_100)]
        public string FatherName { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [StringLength(MaxLength.Max_12)]
        public string MobileNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        [StringLength(MaxLength.Max_100)]
        public string Email { get; set; }

        [Display(Name = "Job Title")]
        [StringLength(MaxLength.Max_100)]
        public string JobTitle { get; set; }

        [Display(Name = "Address")]
        [StringLength(MaxLength.Max_1000)]
        public string Address { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class UpdateCustomerDto : UpdatedUser
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

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class SearchCustomerDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; } = null;
        public string NRIC { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateLoginAccountDto : UpdatedUser
    {
        public int CustomerId { get; set; }
        public string LoginUserId { get; set; }

        public UpdateLoginAccountDto()
        {

        }

        public UpdateLoginAccountDto(int customerId, string loginUserId, string updatedUserId)
        {
            CustomerId = customerId;
            LoginUserId = loginUserId;
            UpdatedUserId = updatedUserId;
        }
    }
}
