using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;
using PedagogyWorld.Models;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize]
    public class FileTypesController : Controller
    {
        private Context context = new Context();

        //
        // GET: /FileTypes/

        public ViewResult Index()
        {
            return View(context.FileTypes.Include(filetype => filetype.Files).ToList());
        }

        //
        // GET: /FileTypes/Details/5

        public ViewResult Details(int id)
        {
            FileType filetype = context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // GET: /FileTypes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FileTypes/Create

        [HttpPost]
        public ActionResult Create(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                context.FileTypes.Add(filetype);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(filetype);
        }
        
        //
        // GET: /FileTypes/Edit/5
 
        public ActionResult Edit(int id)
        {
            FileType filetype = context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // POST: /FileTypes/Edit/5

        [HttpPost]
        public ActionResult Edit(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                context.Entry(filetype).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filetype);
        }

        //
        // GET: /FileTypes/Delete/5
 
        public ActionResult Delete(int id)
        {
            FileType filetype = context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // POST: /FileTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            FileType filetype = context.FileTypes.Single(x => x.Id == id);
            context.FileTypes.Remove(filetype);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}