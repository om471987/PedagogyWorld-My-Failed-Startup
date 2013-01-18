using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld;

namespace PedagogyWorld.Areas.Admin.Controllers
{   
    public class StrandDomainController : Controller
    {
        private Context context = new Context();

        //
        // GET: /StrandDomain/

        public ViewResult Index()
        {
            return View(context.StrandDomains.Include(stranddomain => stranddomain.Headers).ToList());
        }

        //
        // GET: /StrandDomain/Details/5

        public ViewResult Details(int id)
        {
            StrandDomain stranddomain = context.StrandDomains.Single(x => x.Id == id);
            return View(stranddomain);
        }

        //
        // GET: /StrandDomain/Create

        public ActionResult Create()
        {
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleSubjects = context.Subjects;
            return View();
        } 

        //
        // POST: /StrandDomain/Create

        [HttpPost]
        public ActionResult Create(StrandDomain stranddomain)
        {
            if (ModelState.IsValid)
            {
                context.StrandDomains.Add(stranddomain);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(stranddomain);
        }
        
        //
        // GET: /StrandDomain/Edit/5
 
        public ActionResult Edit(int id)
        {
            StrandDomain stranddomain = context.StrandDomains.Single(x => x.Id == id);
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(stranddomain);
        }

        //
        // POST: /StrandDomain/Edit/5

        [HttpPost]
        public ActionResult Edit(StrandDomain stranddomain)
        {
            if (ModelState.IsValid)
            {
                context.Entry(stranddomain).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleGrades = context.Grades;
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleSubjects = context.Subjects;
            return View(stranddomain);
        }

        //
        // GET: /StrandDomain/Delete/5
 
        public ActionResult Delete(int id)
        {
            StrandDomain stranddomain = context.StrandDomains.Single(x => x.Id == id);
            return View(stranddomain);
        }

        //
        // POST: /StrandDomain/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StrandDomain stranddomain = context.StrandDomains.Single(x => x.Id == id);
            context.StrandDomains.Remove(stranddomain);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}