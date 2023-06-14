using Practical11Test01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical11Test01.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> Employees = new List<Employee>();
        // GET: Employee
        public ActionResult Index()
        {
            return View(Employees);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if(employee.Dob.Date==DateTime.Now.Date) 
            {
                ModelState.AddModelError("Invaliddate", "Invalid Dateof Birth");
            }
            if(ModelState.IsValid) 
            {
                Guid empid = Guid.NewGuid();
                TempData["status"] = "Employee added.";
                employee.Id = empid;
                Employees.Add(employee);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}