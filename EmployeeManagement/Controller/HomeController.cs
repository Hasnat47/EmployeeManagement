using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }
        public ViewResult Details(int? id)
        {
            //throw new Exception("Error in Details View");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            EmplyeeDetailsViewModel emplyeeDetailsViewModel = new EmplyeeDetailsViewModel
            {
                Employee = employee,
                PageTitle = "Employee Details."
            };
            return View(emplyeeDetailsViewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel employeeCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(employeeCreateViewModel);
                Employee newEmployee = new Employee
                {
                    Name = employeeCreateViewModel.Name,
                    Email = employeeCreateViewModel.Email,
                    Department = employeeCreateViewModel.Department,
                    PhotoPath = uniqueFileName
                };
                //_employeeRepository.Add(employee);
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employeeEditViewModel)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(employeeEditViewModel.Id);
                employee.Name = employeeEditViewModel.Name;
                employee.Email = employeeEditViewModel.Email;
                employee.Department = employeeEditViewModel.Department;
                if (employeeEditViewModel.Photo != null)
                {
                    if (employeeEditViewModel.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", employeeEditViewModel.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadFile(employeeEditViewModel);
                }
                //_employeeRepository.Add(employee);
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadFile(EmployeeCreateViewModel employeeCreateViewModel)
        {
            string uniqueFileName = null;
            if (employeeCreateViewModel.Photo != null)
            {
                string uploadsFolder = System.IO.Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "" + employeeCreateViewModel.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                employeeCreateViewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }
    }
}