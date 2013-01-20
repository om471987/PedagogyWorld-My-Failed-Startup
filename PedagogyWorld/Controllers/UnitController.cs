using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PedagogyWorld.Controllers
{
    [Authorize]
    public class UnitController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /Unit/

        public ViewResult Index()
        {
            return View(_context.Units.Include(unit => unit.OutcomeUnits).Include(unit => unit.UnitFiles).Include(unit => unit.UnitStandards).Include(unit => unit.UserProfile).ToList());
        }

        //
        // GET: /Unit/Details/5

        public ViewResult Details(Guid id)
        {
            var unit = _context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // GET: /Unit/Create

        public ActionResult Create()
        {
            ViewBag.PossibleGrades = _context.Grades;
            ViewBag.PossibleSubjects = _context.Subjects;
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
                _context.Units.Add(unit);
                unit.UserProfile_Id = (int)Membership.GetUser().ProviderUserKey;
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleGrades = _context.Grades;
            ViewBag.PossibleSubjects = _context.Subjects;
            return View(unit);
        }
        
        //
        // GET: /Unit/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            var unit = _context.Units.Single(x => x.Id == id);
            ViewBag.PossibleGrades = _context.Grades;
            ViewBag.PossibleSubjects = _context.Subjects;
            return View(unit);
        }

        //
        // POST: /Unit/Edit/5

        [HttpPost]
        public ActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(unit).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleGrades = _context.Grades;
            ViewBag.PossibleSubjects = _context.Subjects;
            return View(unit);
        }

        //
        // GET: /Unit/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            var unit = _context.Units.Single(x => x.Id == id);
            return View(unit);
        }

        //
        // POST: /Unit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var unit = _context.Units.Single(x => x.Id == id);
            _context.Units.Remove(unit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}