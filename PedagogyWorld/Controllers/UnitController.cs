using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PedagogyWorld.Models;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class UnitController : Controller
    {
        private readonly Context db = new Context();

        public ViewResult Index()
        {
            var userId = (int) Membership.GetUser().ProviderUserKey;
            return View(db.Units.Where(t => t.UserProfile_Id == userId).Include(unit => unit.OutcomeUnits).Include(unit => unit.UnitFiles).Include(unit => unit.UnitStandards).Include(unit => unit.UserProfile).ToList());
        }

        public ViewResult Details(Guid id)
        {
            var unit = db.Units.Single(x => x.Id == id);
            return View(unit);
        }

        public ActionResult Create()
        {

            var model = new UnitModel();

            var result = new List<SelectListItem>();
            foreach (var t in db.Outcomes)
            {
                result.Add(new SelectListItem
                {
                    Text = t.OutcomeName,
                    Value = t.Id.ToString()
                });
            }
            model.OutcomeTypes = result.ToList();
            
            ViewBag.PossibleGrades = db.Grades;
            ViewBag.PossibleSubjects = db.Subjects;
            return View(model);
        } 

        [HttpPost]
        public ActionResult Create(UnitModel unitModel)
        {
            if (ModelState.IsValid)
            {
                unitModel.Unit.Id = Guid.NewGuid();
                db.Units.Add(unitModel.Unit);
                unitModel.Unit.UserProfile_Id = (int) Membership.GetUser().ProviderUserKey;

                foreach (var t in unitModel.OutcomeIds)
                {
                    var type = new OutcomeUnit
                    {
                        Unit_Id = unitModel.Unit.Id,
                        Outcome_Id = t
                    };
                    db.OutcomeUnits.Add(type);
                }

                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleGrades = db.Grades;
            ViewBag.PossibleSubjects = db.Subjects;
            var model = new UnitModel();
            var result = new List<SelectListItem>();
            foreach (var t in db.Outcomes)
            {
                result.Add(new SelectListItem
                {
                    Text = t.OutcomeName,
                    Value = t.Id.ToString()
                });
            }
            model.OutcomeTypes = result.ToList();

            return View(model);
        }
 
        public ActionResult Edit(Guid id)
        {
            var model = new UnitModel();
            var unit = db.Units.Single(x => x.Id == id);

            model.Unit = unit;
            ViewBag.PossibleGrades = db.Grades;
            ViewBag.PossibleSubjects = db.Subjects;

            var result = new List<SelectListItem>();
            foreach (var t in db.Outcomes)
            {
                result.Add(new SelectListItem
                {
                    Text = t.OutcomeName,
                    Value = t.Id.ToString()
                });
            }
            model.OutcomeTypes = result.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleGrades = db.Grades;
            ViewBag.PossibleSubjects = db.Subjects;
            return View(unit);
        }

        public ActionResult AllignStandard(Guid id)
        {
            ViewBag.Domains = db.StrandDomains;
            ViewBag.Headers = db.Headers;
            ViewBag.Standards = db.Standards;

            var model = new StandardModel();

            var result = new List<SelectListItem>();
            foreach (var t in db.Standards)
            {
                result.Add(new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            }
            model.Standards = result.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AllignStandard(StandardModel returnModel)
        {
            if (ModelState.IsValid)
            {
                foreach (var t in returnModel.StandardIds)
                {
                    var us = new UnitStandard
                        {
                            Unit_Id = returnModel.Id,
                            Standard_Id = t
                        };
                    db.UnitStandards.Add(us);
                }
                db.SaveChanges();
                return RedirectToAction("Details", new { id = returnModel .Id});
            }
            ViewBag.Domains = db.StrandDomains;
            ViewBag.Headers = db.Headers;
            ViewBag.Standards = db.Standards;
            var model = new StandardModel();

            var result = new List<SelectListItem>();
            foreach (var t in db.Standards)
            {
                result.Add(new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            }
            model.Standards = result.ToList();
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var unit = db.Units.Single(x => x.Id == id);
            return View(unit);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var unit = db.Units.Single(x => x.Id == id);
            db.Units.Remove(unit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}