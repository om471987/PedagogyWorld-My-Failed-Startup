using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StandardController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /Standard/

        public ViewResult Index()
        {
            return View(_context.Standards.ToList());
        }

        //
        // GET: /Standard/Details/5

        public ViewResult Details(int id)
        {
            Standard standard = _context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // GET: /Standard/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Standard/Create

        [HttpPost]
        public ActionResult Create(Standard standard)
        {
            if (ModelState.IsValid)
            {
                _context.Standards.Add(standard);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(standard);
        }
        
        //
        // GET: /Standard/Edit/5
 
        public ActionResult Edit(int id)
        {
            Standard standard = _context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // POST: /Standard/Edit/5

        [HttpPost]
        public ActionResult Edit(Standard standard)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(standard).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standard);
        }

        //
        // GET: /Standard/Delete/5
 
        public ActionResult Delete(int id)
        {
            Standard standard = _context.Standards.Single(x => x.Id == id);
            return View(standard);
        }

        //
        // POST: /Standard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Standard standard = _context.Standards.Single(x => x.Id == id);
            _context.Standards.Remove(standard);
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