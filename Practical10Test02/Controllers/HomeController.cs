using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Practical10Test02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ContentResult Content()
        {
            return Content("<h3>Content result returns different content's format to view. MVC returns different format using content return like HTML format, Java Script format and any other format.</h3>", "text/html", System.Text.Encoding.UTF8);
        }
        public FileResult FileResult() 
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Web.config"));
            return File(fileBytes, "text/plain");
        }
        public JavaScriptResult Javascriptresult()
        {
            var msg = "alert('Welcome to JavascriptResult View.');";
            return JavaScript(msg);
        }
        public JsonResult Jsonresult() 
        {
            var data = new List<string> { "apple", "banana", "orange" };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public EmptyResult emptyResult() 
        {
            return new EmptyResult();
        }
        public PartialViewResult Partialviewresult()
        {
            return PartialView("_PartialView");
        }
        public ActionResult Redirecttoaction()
        {
            return RedirectToAction("Index");
        }
        public RedirectResult Redirectresult()
        {
            string externalUrl = "https://www.simform.com/";
            return Redirect(externalUrl);
        }
    }
}