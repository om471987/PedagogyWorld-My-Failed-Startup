using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{   
    [Authorize(Roles = "Admin")]
    public class DistrictController : Controller
    {
        private readonly Context _context = new Context();

        public ViewResult Index(int start = 0)
        {
            ViewBag.Next = start + 20;
            return View(_context.Districts.Include(district => district.Schools).OrderBy(t=>t.Id).Skip(start).Take(20).ToList());
        }

        public ViewResult Details(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        public ActionResult Create()
        {
            ViewBag.PossibleStates = _context.States;
            return View();
        } 

        [HttpPost]
        public ActionResult Create(District district)
        {
            if (ModelState.IsValid)
            {
                _context.Districts.Add(district);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStates = _context.States;
            return View(district);
        }

        public ActionResult Edit(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            ViewBag.PossibleStates = _context.States;
            return View(district);
        }

        [HttpPost]
        public ActionResult Edit(District district)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(district).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStates = _context.States;
            return View(district);
        }

        public ActionResult Delete(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            _context.Districts.Remove(district);
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