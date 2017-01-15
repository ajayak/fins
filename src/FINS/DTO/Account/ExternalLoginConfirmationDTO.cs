using System.ComponentModel.DataAnnotations;

namespace FINS.DTO.Account
{
    public class ExternalLoginConfirmationDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}