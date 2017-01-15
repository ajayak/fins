﻿using System.ComponentModel.DataAnnotations;

namespace FINS.DTO.Account
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}