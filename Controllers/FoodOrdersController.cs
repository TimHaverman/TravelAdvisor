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
    public class FoodOrdersController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: FoodOrders
        public ActionResult Index(int? id, int? dayid, bool? asPartial) // edited by TRH
        {
            IEnumerable<FoodOrder> foodOrders = db.Trips.Find(id).FoodOrders;
            if (dayid.HasValue)
            {
                foodOrders = foodOrders.Where(f => f.DayId == dayid);
            }
            
            foodOrders = foodOrders.OrderBy(f => f.Day.DayNumber); // Ask about Completed datetime or by Day
            if(asPartial.HasValue && asPartial.Value) // edited by TRH
            {
                return PartialView(foodOrders.ToList());
            }
            return View(foodOrders.ToList());
        }
        
        // GET: FoodOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }

        // GET: FoodOrders/Create
        public ActionResult Create()
        {
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id");
            return View();
        }

        // POST: FoodOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CompletedDateTime,Total,BudgetId,NumberAdults,NumberChildren,TripId,DayId")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.FoodOrders.Add(foodOrder);
                db.SaveChanges();
                return RedirectToAction("CreateExpenseVM", "Days", new { id = foodOrder.DayId });  // Edited by TRH changed params
            }

            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", foodOrder.BudgetId);
            return View(foodOrder);
        }

        // GET: FoodOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", foodOrder.BudgetId);
            return View(foodOrder);
        }

        // POST: FoodOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CompletedDateTime,Total,BudgetId,NumberAdults,NumberChildren,TripId,DayId")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Days", new { id = foodOrder.DayId });
            }
            ViewBag.BudgetId = new SelectList(db.Budgets, "Id", "Id", foodOrder.BudgetId);
            return View(foodOrder);
        }

        // GET: FoodOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }

        // POST: FoodOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            db.FoodOrders.Remove(foodOrder);
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
