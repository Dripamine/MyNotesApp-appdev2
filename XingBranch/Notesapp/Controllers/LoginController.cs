using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notesapp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(User userModel)
        {
            using(appdev2Entities dbcontext = new appdev2Entities())
            {
                var userDetails=dbcontext.Users.Where(u=>u.Username == userModel.Username && u.Password==userModel.Password).FirstOrDefault();
                if (userDetails==null)
                {
                    userModel.errormessage = "wrong username or password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userId"]=userDetails.UserId;
                    Session["username"]=userDetails.Username;
                    return RedirectToAction("Index","Home");

                }
            }
            
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}