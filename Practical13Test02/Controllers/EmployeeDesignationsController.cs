using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practical13Test02.Data;
using Practical13Test02.Models;

namespace Practical13Test02.Controllers
{
    public class EmployeeDesignationsController : Controller
    {
        private ApplicationContext _context = new ApplicationContext();

        public ActionResult Index()
        {
            return View(_context.EmployeeDesignations.ToList());
        }
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.btnval = "Create";
                return View();
            }
            EmployeeDesignation employeeDesignation = _context.EmployeeDesignations.Find(id);
            if (employeeDesignation == null)
            {
                return HttpNotFound();
            }
            ViewBag.btnval = "Edit";
            return View(employeeDesignation);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDesignation employeeDesignation)
        {
            if(_context.EmployeeDesignations.Any(x=>x.Designation==employeeDesignation.Designation && x.Id!=employeeDesignation.Id))
            {
                ModelState.AddModelError("designationerror", "Designation already Exists.");
            }
            if (ModelState.IsValid)
            {
                if(employeeDesignation.Id==0)
                {
                    _context.EmployeeDesignations.Add(employeeDesignation);
                }
                if(employeeDesignation.Id != 0)
                {
                    _context.Entry(employeeDesignation).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(employeeDesignation);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDesignation employeeDesignation = _context.EmployeeDesignations.Find(id);
            if (employeeDesignation == null)
            {
                return HttpNotFound();
            }
            return View(employeeDesignation);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDesignation employeeDesignation = _context.EmployeeDesignations.Find(id);
            if (employeeDesignation == null)
            {
                return HttpNotFound();
            }
            _context.EmployeeDesignations.Remove(employeeDesignation);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
