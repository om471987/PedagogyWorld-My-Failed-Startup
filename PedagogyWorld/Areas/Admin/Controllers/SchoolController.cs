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
    public class SchoolController : Controller
    {
        private Context context = new Context();

        //
        // GET: /School/

        public ViewResult Index()
        {
            return View(context.Schools.Include(school => school.UserProfileSchools).ToList());
        }

        //
        // GET: /School/Details/5

        public ViewResult Details(int id)
        {
            School school = context.Schools.Single(x => x.Id == id);
            return View(school);
        }

        //
        // GET: /School/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleDistricts = context.Districts;
            return View();
        } 

        //
        // POST: /School/Create

        [HttpPost]
        public ActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                context.Schools.Add(school);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleDistricts = context.Districts;
            return View(school);
        }
        
        //
        // GET: /School/Edit/5
 
        public ActionResult Edit(int id)
        {
            School school = context.Schools.Single(x => x.Id == id);
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleDistricts = context.Districts;
            return View(school);
        }

        //
        // POST: /School/Edit/5

        [HttpPost]
        public ActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                context.Entry(school).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStates = context.States;
            ViewBag.PossibleDistricts = context.Districts;
            return View(school);
        }

        //
        // GET: /School/Delete/5
 
        public ActionResult Delete(int id)
        {
            School school = context.Schools.Single(x => x.Id == id);
            return View(school);
        }

        //
        // POST: /School/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = context.Schools.Single(x => x.Id == id);
            context.Schools.Remove(school);
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