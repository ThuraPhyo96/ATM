﻿using ATM.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace ATM.AppServices.Authentication.Dtos
{
    public class ApplicationUserDto : IdentityUser
    {
        public int UserType { get; set; }

        [Description("Enables?")]
        public bool IsActive { get; set; }

        #region Audit Log Info
        [StringLength(MaxLength.Max_450)]
        public string CreatedUserId { get; set; }
        public DateTime? CreationTime { get; set; }

        [StringLength(MaxLength.Max_450)]
        public string UpdatedUserId { get; set; }
        public DateTime? UpdatedTime { get; set; }
        #endregion
    }

    public class CreateApplicationUserDto : IdentityUser
    {
        public string CustomerGuid { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; } = true;

        #region Audit Log Info
        [StringLength(MaxLength.Max_450)]
        public string CreatedUserId { get; set; }
        public DateTime? CreationTime { get; set; } = DateTime.Now;
        #endregion
        public string RoleName { get; set; }

        public CreateApplicationUserDto()
        {

        }
    }
}