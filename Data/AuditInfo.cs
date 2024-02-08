using Microsoft.AspNetCore.Identity;
using System;

namespace ATM.Data
{
    public class AuditInfo
    {
        public string CreatedUserId { get; set; }
        public IdentityUser CreatedUser { get; set; }
        public DateTime? CreationTime { get; set; }

        public string UpdatedUserId { get; set; }
        public IdentityUser UpdatedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
