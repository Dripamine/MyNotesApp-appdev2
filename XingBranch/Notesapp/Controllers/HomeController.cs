using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notesapp.Controllers
{
    public class HomeController : Controller
    {
        appdev2Entities _context = new appdev2Entities();
        public ActionResult Index()
        {
            int userId = (int)Session["userId"];
            var listofData = _context.Notes.Where(note => note.UserId == userId).ToList();
            return View(listofData);
        }
        [HttpGet]
        public ActionResult Create() {
        
            return View();
        }
        [HttpPost]
        public ActionResult Create(Note model) {
            model.Date = DateTime.Now;
            model.UserId = (int)Session["userId"];
            _context.Notes.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data insert seccessfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id) {
        var data=_context.Notes.Where(x=>x.NoteId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Note model)
        {
            var data = _context.Notes.Where(x => x.NoteId == model.NoteId).FirstOrDefault();
            if (data != null)
            {
                data.NoteId = model.NoteId;
                data.Title = model.Title;
                data.Content = model.Content;
                data.Date = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int? id)
        {
            var data = _context.Notes.Where(x => x.NoteId == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _context.Notes.Where(x => x.NoteId == id).FirstOrDefault();
            _context.Notes.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Search(string searchString)
        {
            int userId = (int)Session["userId"];
            var listOfData = _context.Notes
                                    .Where(n => n.UserId == userId &&
                                                n.Title.Contains(searchString))
                                    .ToList();
            ViewBag.SearchString = searchString;
            return View(listOfData);
        }
    }
}