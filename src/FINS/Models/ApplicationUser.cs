using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FINS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
