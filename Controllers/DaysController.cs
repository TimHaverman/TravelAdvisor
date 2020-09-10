using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelAdvisor.Models;
using TravelAdvisor.Models.ViewModels;

namespace TravelAdvisor.Controllers
{
    [Authorize]
    public class DaysController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Days
        public ActionResult Index(int? id, bool? asPartial)
        {
            var days = db.Trips.Find(id).Days.OrderBy(d => d.DayNumber);
            if (asPartial.HasValue && asPartial.Value)
            {
                return PartialView(days.ToList());
            }
            return View(days.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);            
            if (day == null)
            {
                return HttpNotFound();
            }
            
            var chartTotal = day.GetTotalChartDescription();

            if (chartTotal != null)
            {
                var chartTotaljason = JsonConvert.SerializeObject(chartTotal);
                ViewBag.chartDayTotal = "https://quickchart.io/chart?c=" + HttpUtility.UrlEncode(chartTotaljason);
            }

            var chartDoughnut = day.GetDoughnutChartDescription();

            if (chartTotal != null)
            {
                var chartTotaljason = JsonConvert.SerializeObject(chartDoughnut);
                ViewBag.chartDayDoughnut = "https://quickchart.io/chart?c=" + HttpUtility.UrlEncode(chartTotaljason);
            }
            var chartpolarArea = day.GetpolarAreaChartDescription();

            if (chartTotal != null)
            {
                var chartTotaljason = JsonConvert.SerializeObject(chartpolarArea);
                ViewBag.chartDayPolarArea = "https://quickchart.io/chart?c=" + HttpUtility.UrlEncode(chartTotaljason);
            }

            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create(int? id)
        {
            var trip = db.Trips.Find(id);
            var day = new Day()
            {
                TripID = id.Value,
                DayNumber = trip.Days.Count() + 1,
                Trip = trip

            };
            return View(day);
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TripID,Description,DayNumber")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(day);
                db.SaveChanges();
                return RedirectToAction("Create","Budgets", new { id = day.Id});
            }

            day.Trip = db.Trips.Find(day.TripID);
            return View(day);
        }
        public ActionResult CreateExpenseVM( int? id )
        {
            var day = db.Days.Find(id); 
            var ExpenseVM = new ExpenseVM() { DayId = id, day = day, TripId = day.TripID };
            ViewBag.budgets = new SelectList(day.Trip.Budgets,"Id","Department.Name");
            return View(ExpenseVM);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripID = new SelectList(db.Trips, "Id", "Name", day.TripID);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TripID,Description,DayNumber")] Day day)
        {
            if (ModelState.IsValid)
            {
                db.Entry(day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Trips", new { id = day.TripID });
            }
            ViewBag.TripID = new SelectList(db.Trips, "Id", "Name", day.TripID);
            return View(day);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
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
