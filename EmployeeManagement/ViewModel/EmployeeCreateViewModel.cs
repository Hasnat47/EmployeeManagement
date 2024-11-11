using EmployeeManagement.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeCreateViewModel
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Official Email")]
        [Required]
//        [RegularExpression(@"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.", ErrorMessage = "Invalid mail format.")]
        public string Email { get; set; }
        [Required]
        public Dept Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
