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
    public class GradesController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Grades/

        public ViewResult Index()
        {
            return View(context.Grades.Include(grade => grade.Units).ToList());
        }

        //
        // GET: /Grades/Details/5

        public ViewResult Details(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // GET: /Grades/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Grades/Create

        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                context.Grades.Add(grade);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(grade);
        }
        
        //
        // GET: /Grades/Edit/5
 
        public ActionResult Edit(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grades/Edit/5

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                context.Entry(grade).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        //
        // GET: /Grades/Delete/5
 
        public ActionResult Delete(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grades/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            context.Grades.Remove(grade);
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