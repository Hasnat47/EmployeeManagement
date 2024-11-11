using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Hasnat",
                    Email = "hasnat@gmail.com",
                    Department = Dept.IT
                },
                new Employee
                {
                    Id = 2,
                    Name = "Karim",
                    Email = "Karim@gmail.com",
                    Department = Dept.HR
                }
                );
        }
    }
}
