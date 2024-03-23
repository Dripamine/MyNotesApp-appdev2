using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Notesapp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult AddOrEdit(int id=0)
        {
            User userModel = new User();
            return View(userModel);
        }
        [HttpPost]
        public ActionResult AddOrEdit(User userModel) {
            using (appdev2Entities dbcontext = new appdev2Entities())
            { 
                if(dbcontext.Users.Any(x=>x.Username==userModel.Username)) {
                    ViewBag.DuplicateMessage = "username already exist.";
                    return View("AddOrEdit", userModel);
                }
                dbcontext.Users.Add(userModel);
            dbcontext.SaveChanges(); }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new User());
        }
    }
}