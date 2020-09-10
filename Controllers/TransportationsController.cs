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
    public class TransportationsController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Transportations
        public ActionResult Index(int? id, int? dayid, bool? asPartial)
        {
            var transportations = db.Transportations.Include(t => t.Budget);
            transportations = transportations.Where(t => t.Budget.TripId == id);
            if (dayid.HasValue)
            {
                transportations = transportations.Where(t => t.DayId == dayid);
            }

            transportations = transportations.OrderBy(t => t.Day.DayNumber);
            if (asPartial.HasValue && asPartial.Value)
            {
                return PartialView(transportations.ToList());
            }
            return View(transportations.ToList());
        }

        // GET: Transportations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // GET: Transportations/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id");
            return View();
        }

        // POST: Transportations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,BudgetId,Amount,DayId")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Transportations.Add(transportation);
                db.SaveChanges();
                return RedirectToAction("CreateExpenseVM", "Days", new { id = transportation.DayId }); // Edited by TRH changed params
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", transportation.BudgetId);
            return View(transportation);
        }

        // GET: Transportations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", transportation.BudgetId);
            return View(transportation);
        }

        // POST: Transportations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,BudgetId,DayId")] Transportation transportation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Days", new { id = transportation.DayId });
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", transportation.BudgetId);
            return View(transportation);
        }

        // GET: Transportations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportation transportation = db.Transportations.Find(id);
            if (transportation == null)
            {
                return HttpNotFound();
            }
            return View(transportation);
        }

        // POST: Transportations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportation transportation = db.Transportations.Find(id);
            db.Transportations.Remove(transportation);
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
