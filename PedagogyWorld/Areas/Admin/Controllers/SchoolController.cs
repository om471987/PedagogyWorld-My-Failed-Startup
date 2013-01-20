using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SchoolController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /School/

        public ViewResult Index()
        {
            return View(_context.Schools.Include(school => school.UserProfileSchools).ToList());
        }

        //
        // GET: /School/Details/5

        public ViewResult Details(int id)
        {
            School school = _context.Schools.Single(x => x.Id == id);
            return View(school);
        }

        //
        // GET: /School/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStates = _context.States;
            ViewBag.PossibleDistricts = _context.Districts;
            return View();
        } 

        //
        // POST: /School/Create

        [HttpPost]
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                _context.Schools.Add(school);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStates = _context.States;
            ViewBag.PossibleDistricts = _context.Districts;
            return View(school);
        }
        
        //
        // GET: /School/Edit/5
 
        public ActionResult Edit(int id)
        {
            School school = _context.Schools.Single(x => x.Id == id);
            ViewBag.PossibleStates = _context.States;
            ViewBag.PossibleDistricts = _context.Districts;
            return View(school);
        }

        //
        // POST: /School/Edit/5

        [HttpPost]
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(school).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStates = _context.States;
            ViewBag.PossibleDistricts = _context.Districts;
            return View(school);
        }

        //
        // GET: /School/Delete/5
 
        public ActionResult Delete(int id)
        {
            School school = _context.Schools.Single(x => x.Id == id);
            return View(school);
        }

        //
        // POST: /School/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = _context.Schools.Single(x => x.Id == id);
            _context.Schools.Remove(school);
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