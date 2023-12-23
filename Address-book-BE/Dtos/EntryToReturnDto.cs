using Address_Book.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Address_book_BE.Dtos
{
    public class EntryToReturnDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public Job? Job { get; set; }
        public Department? Department { get; set; }
        [Required]
        [Phone]
        public string MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Photo { get; set; }
        public int? Age { get; set; }
    }
}
