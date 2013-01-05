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
    public class OutcomesController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Outcomes/

        public ViewResult Index()
        {
            return View(context.Outcomes.Include(outcome => outcome.Units).ToList());
        }

        //
        // GET: /Outcomes/Details/5

        public ViewResult Details(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // GET: /Outcomes/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Outcomes/Create

        [HttpPost]
        public ActionResult Create(Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                context.Outcomes.Add(outcome);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(outcome);
        }
        
        //
        // GET: /Outcomes/Edit/5
 
        public ActionResult Edit(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcomes/Edit/5

        [HttpPost]
        public ActionResult Edit(Outcome outcome)
        {
            if (ModelState.IsValid)
            {
                context.Entry(outcome).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outcome);
        }

        //
        // GET: /Outcomes/Delete/5
 
        public ActionResult Delete(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcomes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            context.Outcomes.Remove(outcome);
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