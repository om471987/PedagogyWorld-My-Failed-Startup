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
    public class DistrictController : Controller
    {
        private Context context = new Context();

        //
        // GET: /District/

        public ViewResult Index()
        {
            return View(context.Districts.Include(district => district.Schools).ToList());
        }

        //
        // GET: /District/Details/5

        public ViewResult Details(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // GET: /District/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStates = context.States;
            return View();
        } 

        //
        // POST: /District/Create

        [HttpPost]
        public ActionResult Create(District district)
        {
            if (ModelState.IsValid)
            {
                context.Districts.Add(district);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStates = context.States;
            return View(district);
        }
        
        //
        // GET: /District/Edit/5
 
        public ActionResult Edit(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            ViewBag.PossibleStates = context.States;
            return View(district);
        }

        //
        // POST: /District/Edit/5

        [HttpPost]
        public ActionResult Edit(District district)
        {
            if (ModelState.IsValid)
            {
                context.Entry(district).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStates = context.States;
            return View(district);
        }

        //
        // GET: /District/Delete/5
 
        public ActionResult Delete(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // POST: /District/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            context.Districts.Remove(district);
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