using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelAdvisor.Models;

namespace TravelAdvisor.Controllers
{
    public class AccommodationsController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Accommodations
        public ActionResult Index(int? id, int? dayid,bool? asPartial)
        {

            //var tripAccommodations = db.Trips.Find(id).Budgets.SelectMany(b => b.Accommodations); Better way


            var accommodations = db.Accommodations.Include(a => a.Budget); //Edited by TRH 
            accommodations = accommodations.Where(a => a.Budget.TripId == id);          
            if (dayid.HasValue)
            {
                accommodations = accommodations.Where(f => f.DayId == dayid);
            }

            accommodations = accommodations.OrderBy(a => a.Day.DayNumber);
           
            if (asPartial.HasValue && asPartial.Value)
            {               
                return PartialView(accommodations.ToList());
            }
            return View(accommodations.ToList());
        }

        // GET: Accommodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // GET: Accommodations/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id");
            return View();
        }

        // POST: Accommodations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Amount,BudgetId,DayId")] Accommodation accommodation) // Edit by TRH added DayId
        {
            if (ModelState.IsValid)
            {
                db.Accommodations.Add(accommodation);
                db.SaveChanges();
                return RedirectToAction("CreateExpenseVM", "Days", new { id = accommodation.DayId }); // Edit by TRH added diff redirect with new params
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", accommodation.BudgetId); 
            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", accommodation.BudgetId);
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,BudgetId,DayId")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accommodation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Days", new { id = accommodation.DayId });
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", accommodation.BudgetId);
            return View(accommodation);
        }

        // GET: Accommodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            db.Accommodations.Remove(accommodation);
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
