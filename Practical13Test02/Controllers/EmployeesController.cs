using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practical13Test02.Data;
using Practical13Test02.Models;

namespace Practical13Test02.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationContext _context = new ApplicationContext();

        [NonAction]
        private IEnumerable<SelectListItem> GetCategory()
        {
            IEnumerable<SelectListItem> EmpDesignation = _context.EmployeeDesignations.Select(u=>new SelectListItem
            {
                Text = u.Designation,
                Value = u.Id.ToString()
            });
            return EmpDesignation;
        }
        // GET: Employees
        public ActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.EmployeeDesignation);
            return View(employees.ToList());
        }
        public ActionResult Create(int? id)
        {
            ViewBag.EmployeeDesignationId = GetCategory();
            if (id == null)
            {
                ViewBag.btnval = "Create";
                return View();
            }
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.btnval = "Update";
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if(DateTime.Now.Year-employee.DOB.Year<18)
            {
                ModelState.AddModelError("Invalid age.", "Employee age must be greater than 18.");
            }
            if (ModelState.IsValid)
            {
                if(employee.Id==0)
                {
                    _context.Employees.Add(employee);
                }
                if(employee.Id!=0)
                {
                    _context.Entry(employee).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeDesignationId = GetCategory();
            return View(employee);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Empcount()
        {  
            List<DesingationCount> employeeCounts = _context.Employees
                                                    .Include(x => x.EmployeeDesignation)
                                                    .GroupBy(x => x.EmployeeDesignation.Designation)
                                                    .Select(g => new DesingationCount { DesignationName = g.Key, Count = g.Count() })
                                                    .ToList();
            return View(employeeCounts);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
