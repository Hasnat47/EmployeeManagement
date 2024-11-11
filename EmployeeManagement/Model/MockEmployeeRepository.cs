using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>() {
                new Employee(){Id=1,Name="Hasnat",Email="hasnat@gmail.com",Department=Dept.HR},
                new Employee(){Id=2,Name="Karim",Email="kaim@gamila.com",Department=Dept.IT},
                new Employee(){Id=3,Name="TG",Email="tg@gmail.com",Department=Dept.Payroll}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(r => r.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee= _employeeList.FirstOrDefault(r => r.Id == id);
            if (employee!=null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(r => r.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = _employeeList.FirstOrDefault(r => r.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
