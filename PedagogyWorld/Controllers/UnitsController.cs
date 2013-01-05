using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PedagogyWorld.Data;
using PedagogyWorld.Models;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class UnitsController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Units/

        public ViewResult Index()
        {
            return View(context.Units.Include(unit => unit.Outcomes).Include(unit => unit.Files).ToList());
        }

        //
        // GET: /Units/Details/5

        public ViewResult Details(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // GET: /Units/Create

        public ActionResult Create()
        {
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;

            ViewBag.PossibleOutcomes = context.Outcomes;


            return View(new UnitModel());
        } 

        //
        // POST: /Units/Create

        [HttpPost]
        public ActionResult Create(UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {
                var unit = new Unit
                    {
                        Id = Guid.NewGuid(),
                        Name = unitModel.Name,
                        Description = unitModel.Description,
                        GradeId = unitModel.GradeId,
                        SubjectId = unitModel.SubjectId
                    };
                foreach (var t in unitModel.SelectedOutcomes)
                {
                    var outcome = context.Outcomes.FirstOrDefault(c => c.Id == t);
                    unit.Outcomes = new List<Outcome> {outcome};
                    outcome.Units = new List<Unit> { unit };
                }
                context.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View(unitModel);
        }
        
        //
        // GET: /Units/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(unit);
        }

        //
        // POST: /Units/Edit/5

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
        // GET: /Units/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            Unit unit = context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // POST: /Units/Delete/5

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