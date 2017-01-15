using System.ComponentModel.DataAnnotations;

namespace FINS.DTO.Account
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName{ get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}