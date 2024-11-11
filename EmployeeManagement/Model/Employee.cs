using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Display(Name ="Official Email")]
        [Required]
        public string Email { get; set; }
        [Required]
        public Dept Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
