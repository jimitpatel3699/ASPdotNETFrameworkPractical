using Practical15Test02.Data;
using Practical15Test02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace Practical15Test02.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationContext _context = new ApplicationContext();
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
          var result =  _context.UserMasters.Where(x=>x.UserId==user.UserName&& x.Password==user.Password).ToList();
            if(result.Count==0) 
            {
                ModelState.AddModelError("invalid user", "Invalid Useid/Password");
            }
            if(ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Home");
            }            
            return View();
        }
        public ActionResult Logout() 
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}