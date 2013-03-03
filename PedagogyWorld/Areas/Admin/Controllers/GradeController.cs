using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GradeController : Controller
    {
        private readonly Context _context = new Context();

        //
        // GET: /Grade/

        public ViewResult Index()
        {

            return View(_context.Grades.ToList());
        }

        //
        // GET: /Grade/Details/5

        public ViewResult Details(int id)
        {
            Grade grade = _context.Grades.Include(t=>t.UnitGrades).Single(x => x.Id == id);
            return View(grade);
        }

        //
        // GET: /Grade/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Grade/Create

        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Grades.Add(grade);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(grade);
        }
        
        //
        // GET: /Grade/Edit/5
 
        public ActionResult Edit(int id)
        {
            Grade grade = _context.Grades.Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(grade).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grade);
        }

        //
        // GET: /Grade/Delete/5
 
        public ActionResult Delete(int id)
        {
            Grade grade = _context.Grades.Include(t => t.UnitGrades).Single(x => x.Id == id);
            return View(grade);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = _context.Grades.Include(t => t.UnitGrades).Single(x => x.Id == id);
            _context.Grades.Remove(grade);
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