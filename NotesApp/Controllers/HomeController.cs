using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesApp.Controllers
{
    public class HomeController : Controller
    {
        NotesDBContext _context = new NotesDBContext();
        public ActionResult Index()
        {
            var listofData = _context.Notes.ToList();
            return View(listofData);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Note model)
        {
            _context.Notes.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.Notes.Where(x => x.NoteId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Note Model)
        {
            var data = _context.Notes.Where(x => x.NoteId == Model.NoteId).FirstOrDefault();
            if (data != null)
            {
                data.Title = Model.Title;
                data.Content = Model.Content;
                data.Date = Model.Date;
                _context.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult Detail(int id)
        {
            var data = _context.Notes.Where(x => x.NoteId == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            var data = _context.Notes.Where(x => x.NoteId == id).FirstOrDefault();
            _context.Notes.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("index");
        }
    }
}