using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedagogyWorld.Data;
using PedagogyWorld.Models;

namespace PedagogyWorld.Areas.Admin.Controllers
{
    [Authorize]
    public class DistrictsController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Districts/

        public ViewResult Index()
        {
            return View(context.Districts.Include(district => district.Schools).ToList());
        }

        //
        // GET: /Districts/Details/5

        public ViewResult Details(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // GET: /Districts/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStates = context.States;
            return View();
        } 

        //
        // POST: /Districts/Create

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
        // GET: /Districts/Edit/5
 
        public ActionResult Edit(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            ViewBag.PossibleStates = context.States;
            return View(district);
        }

        //
        // POST: /Districts/Edit/5

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
        // GET: /Districts/Delete/5
 
        public ActionResult Delete(int id)
        {
            District district = context.Districts.Single(x => x.Id == id);
            return View(district);
        }

        //
        // POST: /Districts/Delete/5

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