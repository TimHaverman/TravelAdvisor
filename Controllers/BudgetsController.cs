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
    public class BudgetsController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Budgets
        public ActionResult Index(int? id, bool? asPartial)
        {
            var budgets = db.Trips.Find(id).Budgets.OrderBy(b => b.Department.Name);
            if (asPartial.HasValue && asPartial.Value)
            {
                return PartialView(budgets.ToList());
            }
            return View(budgets.ToList());
        }


        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create(int? id)
        {
            var budget = new Budget()
            {
                TripId = id.Value,
                Trip = db.Trips.Find(id)
            };

            var allDepartmentIds = db.Departments.Select(d => d.Id);
            var usedDepartmentIds = budget.Trip.Budgets.Select(b => b.DeparmentID);
            var remainIds = allDepartmentIds.Except(usedDepartmentIds).ToList();
            var departments = db.Departments.Join(remainIds, d => d.Id, r => r, (d, r) => d).ToList();

            ViewBag.DeparmentID = new SelectList(departments, "Id", "Name");
           
            if (!departments.Any())
            {
                return RedirectToAction("Details","Trips", new { id = budget.TripId });
            }

            return View(budget);
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Create", new { id = budget.TripId });
            }

            budget.Trip = db.Trips.Find(budget.TripId);

            var allDepartmentIds = db.Departments.Select(d => d.Id);
            var usedDepartmentIds = budget.Trip.Budgets.Select(b => b.DeparmentID);
            var remainIds = allDepartmentIds.Except(usedDepartmentIds).ToList();
            var departments = db.Departments.Join(remainIds, d => d.Id, r => r, (d, r) => d).ToList();

            ViewBag.DeparmentID = new SelectList(departments, "Id", "Name");
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Trips, "Id", "Name", budget.TripId);
            ViewBag.DeparmentID = new SelectList(db.Departments, "Id", "Name", budget.DeparmentID);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DeparmentID,Amount,TripId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Trips", new { id = budget.TripId });
            }
            ViewBag.DayId = new SelectList(db.Trips, "Id", "Name", budget.TripId);
            ViewBag.DeparmentID = new SelectList(db.Departments, "Id", "Name", budget.DeparmentID);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
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
