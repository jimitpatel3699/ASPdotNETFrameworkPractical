using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical10Test03.Controllers
{
    public class HomeController : Controller
    { 
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(Duration = 300)]
        public ActionResult CachedDateTime() 
        {
            string dateTime = DateTime.Now.ToString();
            ViewBag.dateTime = dateTime;
            return Content(dateTime);
        }
    }
}