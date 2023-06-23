using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical10Test01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Route("Employee/{name:alpha}")]
        public ActionResult Employee(string name) 
        {
            ViewBag.Employeename = name;
            return View();
        }
    }
}