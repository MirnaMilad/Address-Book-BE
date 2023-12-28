using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Core.Entities
{
    public class Entry:baseEntity
    {
        public string FullName { get; set; }
        public int? JobId { get; set; } //Fk
        public Job? Job { get; set; }
        public int? DepartmentId { get; set; }   //Fk
        public Department? Department { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public int? Age { get; set; }
    }
}
