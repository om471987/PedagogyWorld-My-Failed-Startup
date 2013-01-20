using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileTypeController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /FileType/

        public ViewResult Index()
        {
            return View(_context.FileTypes.Include(filetype => filetype.FileFileTypes).ToList());
        }

        //
        // GET: /FileType/Details/5

        public ViewResult Details(int id)
        {
            FileType filetype = _context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // GET: /FileType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FileType/Create

        [HttpPost]
        public ActionResult Create(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                _context.FileTypes.Add(filetype);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(filetype);
        }
        
        //
        // GET: /FileType/Edit/5
 
        public ActionResult Edit(int id)
        {
            FileType filetype = _context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // POST: /FileType/Edit/5

        [HttpPost]
        public ActionResult Edit(FileType filetype)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(filetype).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filetype);
        }

        //
        // GET: /FileType/Delete/5
 
        public ActionResult Delete(int id)
        {
            FileType filetype = _context.FileTypes.Single(x => x.Id == id);
            return View(filetype);
        }

        //
        // POST: /FileType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            FileType filetype = _context.FileTypes.Single(x => x.Id == id);
            _context.FileTypes.Remove(filetype);
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