using ATM.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Data
{
    public class CreatedUser
    {
        [StringLength(MaxLength.Max_450)]
        public string CreatedUserId { get; set; }

        public IdentityUser CreatedBy { get; set; }
    }
}
