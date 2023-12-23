﻿using System.ComponentModel.DataAnnotations;

namespace Address_book_BE.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}