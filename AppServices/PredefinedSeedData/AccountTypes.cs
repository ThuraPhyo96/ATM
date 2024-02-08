using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ATM.AppServices.PredefinedSeedData
{
    public static class AccountTypes
    {
        public static IReadOnlyList<SelectListItem> GetAll()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {Value = "1", Text = "e-Saving Account"},
                new SelectListItem() {Value = "2", Text = "Special Account"}
            };
        }
    }
}
