using Microsoft.AspNetCore.Identity;
using System;

namespace ATM.Helpers
{
    public static class CustomerHelper
    {
        public static string GetCreatedUserInfo(DateTime? createdTime)
        {
            if (!createdTime.HasValue)
                return string.Empty;
            else
                return $"<div>Created on {createdTime: dd-MMM-yyyy}</div>";
        }

        public static string GetUpdatedUserInfo(DateTime? updatedTime)
        {
            if (!updatedTime.HasValue)
                return string.Empty;
            else
                return $"<div>Last updated on {updatedTime: dd-MMM-yyyy}</div>";
        }
    }
}
