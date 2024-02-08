using ATM.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Data
{
    public class UpdatedUser
    {
        [StringLength(MaxLength.Max_450)]
        public string UpdatedUserId { get; set; }

        public IdentityUser UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; } = DateTime.Now;
    }
}
