using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Shared
{
    public class Department: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
