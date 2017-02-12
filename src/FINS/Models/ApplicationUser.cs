using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FINS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        /// <summary>
        /// Organization Id of which user is a member of.
        /// </summary>
        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
