using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld;

namespace PedagogyWorld.Areas.Admin.Controllers
{   
    public class GradeController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Grade/

        public ViewResult Index()
        {
            return View(context.Grades.Include(grade => grade.Units).Include(grade => grade.UnitStandards).ToList());
        }

        //
        // GET: /Grade/Details/5

        public ViewResult Details(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Grade/Create

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
        // GET: /Grade/Edit/5
 
        public ActionResult Edit(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grade/Edit/5

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
        // GET: /Grade/Delete/5
 
        public ActionResult Delete(int id)
        {
            Grade grade = context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grade/Delete/5

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