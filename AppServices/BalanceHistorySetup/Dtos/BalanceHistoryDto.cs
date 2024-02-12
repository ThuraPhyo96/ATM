using ATM.Data;
using ATM.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using ATM.AppServices.CustomerSetup.Dtos;
using ATM.AppServices.BankAccountSetup.Dtos;
using System.Threading;

namespace ATM.AppServices.BalanceHistorySetup.Dtos
{
    public class BalanceHistoryDto : AuditInfo
    {
        public int BalanceHistoryId { get; set; }
        public Guid BalanceHistoryGuid { get; set; }

        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }

        public int BankAccountId { get; set; }
        public BankAccountDto BankAccount { get; set; }

        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int HistoryType { get; set; }

        public bool IsActive { get; set; }

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }

    public class CreateBalanceHistoryDto
    {
        public int CustomerId { get; set; }
        public int BankAccountId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Today;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int HistoryType { get; set; }
        public bool IsActive { get; set; } = true;

        public CreateBalanceHistoryDto()
        {

        }

        public CreateBalanceHistoryDto(int customerId, int bankAccountId, decimal amount, int historyType)
        {
            CustomerId = customerId;
            BankAccountId = bankAccountId;
            Amount = amount;
            HistoryType = historyType;
        }
    }

    public class SearchBalanceHistoryDto
    {
        public string CustomerGuid { get; set; } = string.Empty;
        public string BankAccountGuid { get; set; } = string.Empty;
        public int HistoryType { get; set; } = 0;
        public DateTime? FromTransactionDate { get; set; } = null;
        public DateTime? ToTransactionDate { get; set; } = null;
    }
}
