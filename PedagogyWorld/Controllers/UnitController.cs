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
    public class UnitController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Unit/

        public ViewResult Index()
        {
            return View(context.Units.Include(unit => unit.OutcomeUnits).Include(unit => unit.UnitFiles).Include(unit => unit.UnitStandards).Include(unit => unit.UserProfileUnits).ToList());
        }

        //
        // GET: /Unit/Details/5

        public ViewResult Details(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;
            return View();
        } 

        //
        // POST: /Unit/Create

        [HttpPost]
        public ActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                unit.Id = Guid.NewGuid();
                context.Units.Add(unit);

                context.UserProfileUnits.Add(new UserProfileUnit
                    {
                        Unit_Id = unit.Id ,
                        UserProfile_Id = context.UserProfiles.FirstOrDefault(t=>t.UserName == User.Identity.Name).UserId
                    });
                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(unit);
        }
        
        //
        // GET: /Unit/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                context.Entry(unit).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(unit);
        }

        //
        // GET: /Unit/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            context.Units.Remove(unit);
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