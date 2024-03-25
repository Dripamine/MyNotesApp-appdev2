using System.Linq;
using System.Web.Mvc;
using BCrypt.Net;

namespace Notesapp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {
            if (ModelState.IsValid)
            {
                if (userModel.Password != userModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View("AddOrEdit", userModel);
                }

                using (appdev2Entities dbcontext = new appdev2Entities())
                {
                    if (dbcontext.Users.Any(x => x.Username == userModel.Username))
                    {
                        ViewBag.DuplicateMessage = "Username already exists.";
                        return View("AddOrEdit", userModel);
                    }
                    userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
                    dbcontext.Users.Add(userModel);
                    dbcontext.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.SuccessMessage = "Registration Successful";
                return RedirectToAction("Index", "Login");
            }
            return View("AddOrEdit", userModel);
        }
    }
}