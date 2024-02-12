using ATM.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ATM.Data
{
    public class BankAccount : AuditInfo
    {
        public int BankAccountId { get; set; }
        public Guid BankAccountGuid { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [StringLength(MaxLength.Max_20)]
        public string AccountNumber { get; set; }

        public int AccountType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        public bool IsActive { get; set; }

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }

        public List<BankCard> BankCards { get; set; } = new List<BankCard>();

        public List<BalanceHistory> BalanceHistories { get; set; } = new List<BalanceHistory>();
    }

    [Flags]
    public enum EBankAccountType
    {
        [Description("e-Saving Account")]
        Saving = 1,
        [Description("Special Account")]
        Special = 2
    }
}
