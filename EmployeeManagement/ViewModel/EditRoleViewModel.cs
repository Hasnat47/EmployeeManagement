﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        public string Id { get; set;}

        [Required(ErrorMessage ="Role Name is Required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
