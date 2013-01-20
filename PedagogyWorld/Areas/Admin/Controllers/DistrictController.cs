using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{   
    public class DistrictController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /District/

        public ViewResult Index()
        {
            return View(_context.Districts.Include(district => district.Schools).ToList());
        }

        //
        // GET: /District/Details/5

        public ViewResult Details(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // GET: /District/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStates = _context.States;
            return View();
        } 

        //
        // POST: /District/Create

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
        
        //
        // GET: /District/Edit/5
 
        public ActionResult Edit(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            ViewBag.PossibleStates = _context.States;
            return View(district);
        }

        //
        // POST: /District/Edit/5

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

        //
        // GET: /District/Delete/5
 
        public ActionResult Delete(int id)
        {
            District district = _context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // POST: /District/Delete/5

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