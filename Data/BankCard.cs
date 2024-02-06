using ATM.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Data
{
    public class BankCard : AuditInfo
    {
        public int BankCardId { get; set; }
        public Guid BankCardGuid { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public string PIN { get; set; }

        public DateTime ValidDate { get; set; }

        public bool IsActive { get; set; }

        [StringLength(MaxLength.Max_500)]
        public string Remark { get; set; }
    }
}
