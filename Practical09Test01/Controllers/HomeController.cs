using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical09Test01.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route()]
        public ActionResult Index()
        {
            return View();
        }
        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "application description page.";
            return View();
        }
        [Route("Show")]
        public string Show()
        {
            return "Show method of Home Controller.";
        }
        [Route("Data/{id:int}")]
        public string Data(int id)
        {
            return $"Data method of Home Controller.\nid:{id}";
            
        }
    }
}