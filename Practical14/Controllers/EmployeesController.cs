using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Practical14;

namespace Practical14.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeManagementP13Test01Entities db = new EmployeeManagementP13Test01Entities();

        // GET: Employees
        public ActionResult Index(string search,int? page)
        {
            if(string.IsNullOrEmpty(search)==false)
            {
                List<Employee> emp = db.Employees.Where(model => model.Name.StartsWith(search)).ToList();
                return PartialView("_SearchData", emp);
            }
            return View(db.Employees.ToList().ToPagedList(page ?? 1, 1));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.Action = "Submit";
                return View();
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
               HttpNotFound();
            }
            ViewBag.Action = "Update";
            return View(employee);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if(DateTime.Now.Year - employee.DateofBirth.Year<18)
            {
                ModelState.AddModelError("ageerror", "Invalid age.");
            }
            if (ModelState.IsValid)
            {
                employee.Age = DateTime.Now.Year - employee.DateofBirth.Year;
                if(employee.Id==0)
                {
                    db.Employees.Add(employee);
                }
                else if(employee.Id!=0)
                {
                    db.Entry(employee).State = EntityState.Modified;
                }
                db.SaveChanges();
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
            Employee employee = db.Employees.Find(id);
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
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
