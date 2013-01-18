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
    public class HeaderController : Controller
    {
        private Context context = new Context();

        //
        // GET: /Header/

        public ViewResult Index()
        {
            return View(context.Headers.Include(header => header.Standards).ToList());
        }

        //
        // GET: /Header/Details/5

        public ViewResult Details(int id)
        {
            Header header = context.Headers.Single(x => x.Id == id);
            return View(header);
        }

        //
        // GET: /Header/Create

        public ActionResult Create()
        {
            ViewBag.PossibleStrandDomains = context.StrandDomains;
            return View();
        } 

        //
        // POST: /Header/Create

        [HttpPost]
        public ActionResult Create(Header header)
        {
            if (ModelState.IsValid)
            {
                context.Headers.Add(header);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.PossibleStrandDomains = context.StrandDomains;
            return View(header);
        }
        
        //
        // GET: /Header/Edit/5
 
        public ActionResult Edit(int id)
        {
            Header header = context.Headers.Single(x => x.Id == id);
            ViewBag.PossibleStrandDomains = context.StrandDomains;
            return View(header);
        }

        //
        // POST: /Header/Edit/5

        [HttpPost]
        public ActionResult Edit(Header header)
        {
            if (ModelState.IsValid)
            {
                context.Entry(header).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PossibleStrandDomains = context.StrandDomains;
            return View(header);
        }

        //
        // GET: /Header/Delete/5
 
        public ActionResult Delete(int id)
        {
            Header header = context.Headers.Single(x => x.Id == id);
            return View(header);
        }

        //
        // POST: /Header/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Header header = context.Headers.Single(x => x.Id == id);
            context.Headers.Remove(header);
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