using System.ComponentModel.DataAnnotations;

namespace FINS.DTO.Account
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}