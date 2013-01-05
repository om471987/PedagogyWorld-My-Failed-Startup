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
    public class StatesController : Controller
    {
        private Context context = new Context();


        public ViewResult Main()
        {
            return View();
        }
        //
        // GET: /States/

        public ViewResult Index()
        {
            return View(context.States.Include(state => state.Districts).ToList());
        }

        //
        // GET: /States/Details/5

        public ViewResult Details(int id)
        {
            State state = context.States.Single(x => x.Id == id);
            return View(state);
        }

        //
        // GET: /States/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /States/Create

        [HttpPost]
        public ActionResult Create(State state)
        {
            if (ModelState.IsValid)
            {
                context.States.Add(state);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(state);
        }
        
        //
        // GET: /States/Edit/5
 
        public ActionResult Edit(int id)
        {
            State state = context.States.Single(x => x.Id == id);
            return View(state);
        }

        //
        // POST: /States/Edit/5

        [HttpPost]
        public ActionResult Edit(State state)
        {
            if (ModelState.IsValid)
            {
                context.Entry(state).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(state);
        }

        //
        // GET: /States/Delete/5
 
        public ActionResult Delete(int id)
        {
            State state = context.States.Single(x => x.Id == id);
            return View(state);
        }

        //
        // POST: /States/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            State state = context.States.Single(x => x.Id == id);
            context.States.Remove(state);
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