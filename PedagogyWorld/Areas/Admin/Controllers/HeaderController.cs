using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HeaderController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /Header/

        public ViewResult Index()
        {
            return View(_context.Headers.Include(header => header.Standards).ToList());
        }

        //
        // GET: /Header/Details/5

        public ViewResult Details(int id)
        {
            var header = _context.Headers.Single(x => x.Id == id);
            return View(header);
        }

        //
        // GET: /Header/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStrandDomains = _context.StrandDomains;
            return View();
        } 

        //
        // POST: /Header/Create

        [HttpPost]
        public ActionResult Create(Header header)
        {
            if (ModelState.IsValid)
            {
                _context.Headers.Add(header);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStrandDomains = _context.StrandDomains;
            return View(header);
        }
        
        //
        // GET: /Header/Edit/5
 
        public ActionResult Edit(int id)
        {
            Header header = _context.Headers.Single(x => x.Id == id);
            ViewBag.PossibleStrandDomains = _context.StrandDomains;
            return View(header);
        }

        //
        // POST: /Header/Edit/5

        [HttpPost]
        public ActionResult Edit(Header header)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(header).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStrandDomains = _context.StrandDomains;
            return View(header);
        }

        //
        // GET: /Header/Delete/5
 
        public ActionResult Delete(int id)
        {
            Header header = _context.Headers.Single(x => x.Id == id);
            return View(header);
        }

        //
        // POST: /Header/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Header header = _context.Headers.Single(x => x.Id == id);
            _context.Headers.Remove(header);
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