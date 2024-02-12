using Microsoft.AspNetCore.Http;
using System;

namespace ATM.Helpers
{
    public static class DateFormatHelper
    {
        public static DateTime? ConvertToFormatDate(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime dateValue))
                return dateValue;
            else
                return null;
        }
    }
}
