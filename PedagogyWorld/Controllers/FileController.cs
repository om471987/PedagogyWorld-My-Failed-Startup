using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld;

namespace PedagogyWorld.Controllers
{   
    public class FileController : Controller
    {
        private Context context = new Context();

        //
        // GET: /File/

        public ViewResult Index()
        {
            return View(context.Files.Include(file => file.FileFileTypes).Include(file => file.TeachingDates).Include(file => file.UnitFiles).ToList());
        }

        //
        // GET: /File/Details/5

        public ViewResult Details(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(File file)
        {
            if (ModelState.IsValid)
            {
                file.Id = Guid.NewGuid();
                context.Files.Add(file);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(file);
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(File file)
        {
            if (ModelState.IsValid)
            {
                context.Entry(file).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            return View(file);
        }

        //
        // POST: /File/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            File file = context.Files.Single(x => x.Id == id);
            context.Files.Remove(file);
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