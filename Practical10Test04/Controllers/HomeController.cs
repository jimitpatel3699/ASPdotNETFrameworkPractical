using Practical10Test04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical10Test04.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "ErrorView")]
        public ActionResult Index(Calsi calsi)
        {
            if (calsi.Number2 != 0)
            {
                var result = calsi.Number1 / calsi.Number2;
                ViewBag.result = "<script>alert(" + result.ToString() + ")</script>";
                return View();
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
    }
}