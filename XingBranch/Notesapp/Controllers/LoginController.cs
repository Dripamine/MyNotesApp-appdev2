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
            using (appdev2Entities dbcontext = new appdev2Entities())
            {
                var userDetails = dbcontext.Users.FirstOrDefault(u => u.Username == userModel.Username);
                if (userDetails != null && BCrypt.Net.BCrypt.Verify(userModel.Password, userDetails.Password))
                {
                    Session["userId"] = userDetails.UserId;
                    Session["username"] = userDetails.Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    userModel.errormessage = "Wrong username or password";
                    return View("Index", userModel);
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