﻿using ATM.Helpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM.Data
{
    public class BalanceHistory : AuditInfo
    {
        public int BalanceHistoryId { get; set; }
        public Guid BalanceHistoryGuid { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int HistoryType { get; set; }

        public bool IsActive { get; set; }

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    [Flags]
    public enum EBalanceHistoryType
    {
        [Description("Withdraw")]
        Withdraw = 1,
        [Description("Deposite")]
        Special = 2
    }
}
