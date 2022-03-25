using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagementApp.Shared
{
    public class Employee: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public double Salary { get; set; }
        public byte[]? Picture { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Bio { get; set; }

        [Range(1,1000, ErrorMessage = "Please select Department")]
        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
