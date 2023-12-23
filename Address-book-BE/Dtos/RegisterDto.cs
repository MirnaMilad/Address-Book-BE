using System.ComponentModel.DataAnnotations;

namespace Address_book_BE.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Password Must Contain 1 Uppercase , 1 Lowercase , 1 Digit , 1 Special Character")]
        public string Password { get; set; }
    }
}
