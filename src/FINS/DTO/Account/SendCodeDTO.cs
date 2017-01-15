using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FINS.DTO.Account
{
    public class SendCodeDTO
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}