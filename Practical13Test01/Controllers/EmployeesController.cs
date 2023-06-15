using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practical13Test01.Data;
using Practical13Test01.Models;

namespace Practical13Test01.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationContext _context = new ApplicationContext();
        public ActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        public ActionResult Details(int? id)
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
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if(employee.DateofBirth.Date==DateTime.Now.Date)
            {
                ModelState.AddModelError("invalidDob", "Invalid Date of Birth.");
            }
            else if(DateTime.Now.Year - employee.DateofBirth.Year < 18 )
            {
                ModelState.AddModelError("invalidage", "Employee age must be greater than 18.");
            }
            if (ModelState.IsValid)
            {
                employee.Age=DateTime.Now.Year- employee.DateofBirth.Year;
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }
        public ActionResult Edit(int? id)
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
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (employee.DateofBirth.Date == DateTime.Now.Date)
            {
                ModelState.AddModelError("invalidDob", "Invalid Date of Birth.");
            }
            else if (DateTime.Now.Year - employee.DateofBirth.Year < 18)
            {
                ModelState.AddModelError("invalidage", "Employee age must be greater than 18.");
            }
            if (ModelState.IsValid)
            {
                employee.Age = DateTime.Now.Year - employee.DateofBirth.Year;
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
