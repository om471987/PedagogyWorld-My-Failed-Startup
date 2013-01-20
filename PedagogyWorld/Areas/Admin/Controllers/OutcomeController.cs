using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OutcomeController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /Outcome/

        public ViewResult Index()
        {
            return View(_context.Outcomes.Include(outcome => outcome.OutcomeUnits).ToList());
        }

        //
        // GET: /Outcome/Details/5

        public ViewResult Details(int id)
        {
            Outcome outcome = _context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // GET: /Outcome/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Outcome/Create

        [HttpPost]
        public ActionResult Create(Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                _context.Outcomes.Add(outcome);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(outcome);
        }
        
        //
        // GET: /Outcome/Edit/5
 
        public ActionResult Edit(int id)
        {
            Outcome outcome = _context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcome/Edit/5

        [HttpPost]
        public ActionResult Edit(Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(outcome).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outcome);
        }

        //
        // GET: /Outcome/Delete/5
 
        public ActionResult Delete(int id)
        {
            Outcome outcome = _context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcome/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Outcome outcome = _context.Outcomes.Single(x => x.Id == id);
            _context.Outcomes.Remove(outcome);
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