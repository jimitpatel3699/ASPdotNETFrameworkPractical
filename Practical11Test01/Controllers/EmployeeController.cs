using Microsoft.Ajax.Utilities;
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
            if(employee.Dob.Date== DateTime.Now.Date)
            {
                ModelState.AddModelError("Invaliddate", "Invalid date.");
            }
           else if(DateTime.Now.Year-employee.Dob.Year<18) 
            {
                ModelState.AddModelError("Invalidage", "Employee age must be greater than 18.");
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
        public ActionResult  Edit(Guid id)
        {
            Employee emp = Employees.Find(x=>x.Id==id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (employee.Dob.Date == DateTime.Now.Date)
            {
                ModelState.AddModelError("Invaliddate", "Invalid date.");
            }
            else if (DateTime.Now.Year - employee.Dob.Year < 18)
            {
                ModelState.AddModelError("Invalidage", "Employee age must be greater than 18.");
            }
            if (ModelState.IsValid)
            {
                TempData["status"] = "Employee updated.";
                Employee emp = Employees.Find(x => x.Id == employee.Id);
                emp.Name = employee.Name;
                emp.Dob = employee.Dob;
                emp.Mobile = employee.Mobile;
                emp.Address = employee.Address;
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(Guid id) 
        {
            Employee emp = Employees.Find(x => x.Id == id);
            Employees.Remove(emp);
            TempData["status"] = "Employee Deleted.";
            return RedirectToAction("Index");
        }
    }
}