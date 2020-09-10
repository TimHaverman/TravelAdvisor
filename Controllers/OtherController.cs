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
    public class OtherController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Other
        public ActionResult Index(int? id, int? dayid, bool? asPartial)
        {
            var other = db.Other.Include(o => o.Budget);
            other = other.Where(o => o.Budget.TripId == id);
            if (dayid.HasValue)
            {
                other = other.Where(o => o.DayId == dayid);
            }
            
            other = other.OrderBy(o => o.Day.DayNumber);
            if (asPartial.HasValue && asPartial.Value)
            {
                return PartialView(other.ToList());
            }

            return View(other.ToList());
        }

        // GET: Other/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Other other = db.Other.Find(id);
            if (other == null)
            {
                return HttpNotFound();
            }
            return View(other);
        }

        // GET: Other/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id");
            return View();
        }

        // POST: Other/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Other other)
        {
            if (ModelState.IsValid)
            {
                db.Other.Add(other);
                db.SaveChanges();
                return RedirectToAction("CreateExpenseVM","Days", new {id = other.DayId});
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", other.BudgetId);
            return View(other);
        }

        // GET: Other/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Other other = db.Other.Find(id);
            if (other == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", other.BudgetId);
            return View(other);
        }

        // POST: Other/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Amount,BudgetId,DayId")] Other other)
        {
            if (ModelState.IsValid)
            {
                db.Entry(other).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Days", new { id = other.DayId });
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", other.BudgetId);
            return View(other);
        }

        // GET: Other/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Other other = db.Other.Find(id);
            if (other == null)
            {
                return HttpNotFound();
            }
            return View(other);
        }

        // POST: Other/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Other other = db.Other.Find(id);
            db.Other.Remove(other);
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
