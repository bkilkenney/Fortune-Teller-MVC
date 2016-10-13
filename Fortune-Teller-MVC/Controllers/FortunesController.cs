using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fortune_Teller_MVC.Models;

namespace Fortune_Teller_MVC.Controllers
{
    public class FortunesController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Fortunes
        public ActionResult Index()
        {
            var fortunes = db.Fortunes.Include(f => f.Customer);
            return View(fortunes.ToList());
        }

        // GET: Fortunes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fortune fortune = db.Fortunes.Find(id);
            if (fortune == null)
            {
                return HttpNotFound();
            }
            return View(fortune);
        }

        // GET: Fortunes/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: Fortunes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FortuneID,CustomerID,YearsUntilRetirement,MoneyInBank,VacationHome,TransportationMode")] Fortune fortune)
        {
            if (ModelState.IsValid)
            {
                db.Fortunes.Add(fortune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", fortune.CustomerID);
            return View(fortune);
        }

        // GET: Fortunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fortune fortune = db.Fortunes.Find(id);
            if (fortune == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", fortune.CustomerID);
            return View(fortune);
        }

        // POST: Fortunes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FortuneID,CustomerID,YearsUntilRetirement,MoneyInBank,VacationHome,TransportationMode")] Fortune fortune)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fortune).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", fortune.CustomerID);
            return View(fortune);
        }

        // GET: Fortunes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fortune fortune = db.Fortunes.Find(id);
            if (fortune == null)
            {
                return HttpNotFound();
            }
            return View(fortune);
        }

        // POST: Fortunes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fortune fortune = db.Fortunes.Find(id);
            db.Fortunes.Remove(fortune);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
