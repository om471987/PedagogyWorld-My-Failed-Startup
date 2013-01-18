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
    public class OutcomeController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Outcome/

        public ViewResult Index()
        {
            return View(context.Outcomes.Include(outcome => outcome.OutcomeUnits).ToList());
        }

        //
        // GET: /Outcome/Details/5

        public ViewResult Details(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
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
                context.Outcomes.Add(outcome);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(outcome);
        }
        
        //
        // GET: /Outcome/Edit/5
 
        public ActionResult Edit(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcome/Edit/5

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
        // GET: /Outcome/Delete/5
 
        public ActionResult Delete(int id)
        {
            Outcome outcome = context.Outcomes.Single(x => x.Id == id);
            return View(outcome);
        }

        //
        // POST: /Outcome/Delete/5

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