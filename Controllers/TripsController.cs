using Microsoft.AspNet.Identity;
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
    public class TripsController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: Trips
        public ActionResult Index(bool? completedtrips) //TRH added completedtrips and all of if statement
        {
            DateTime nowtime = DateTime.Now;         
            var userId = User.Identity.GetUserId();
            IEnumerable<Trip> trips = db.Customers.Find(userId).Trips;
            if(completedtrips.HasValue)
            {
                if (completedtrips.Value)
                {
                    trips = trips.Where(t => t.CompletedDateTime.HasValue && t.CompletedDateTime.Value <= DateTime.Now);
                }
                else
                {
                    trips = trips.Where(t => !t.CompletedDateTime.HasValue || t.CompletedDateTime.Value >= DateTime.Now);
                }
            }           
            trips = trips.OrderBy(t => t.CompletedDateTime.HasValue).ThenByDescending(t => t.CompletedDateTime);

            if (completedtrips.HasValue)
            {
                return PartialView("IndexPartial", trips.ToList());
            }
            return View(trips.ToList());
        }

        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            var chart = trip.GetChartDescription();

            if(chart != null)
            {
                var chartjason = JsonConvert.SerializeObject(chart);
                ViewBag.chart = "https://quickchart.io/chart?c=" + HttpUtility.UrlEncode(chartjason);
            }

            var chartTotal = trip.GetTotalChartDescription();

            if (chartTotal != null)
            {
                var chartTotaljason = JsonConvert.SerializeObject(chartTotal);
                ViewBag.chartTotal = "https://quickchart.io/chart?c=" + HttpUtility.UrlEncode(chartTotaljason);
            }

            return View(trip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            var trip = new TripCreateVM()
            {
                CustId = User.Identity.GetUserId()
            };
            return View(trip);
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripCreateVM tripVm)
        {
            if (ModelState.IsValid)
            {
                var trip = tripVm.convertToTrip();
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Create","Budgets", new { id = trip.Id });
            }

            ViewBag.CustId = new SelectList(db.Customers, "CustId", "First_Name", tripVm.CustId);
            return View(tripVm);
        }

        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustId = new SelectList(db.Customers, "CustId", "First_Name", trip.CustId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CustId,Description,TypeOfTrip,CompletedDateTime,Suggested")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustId = new SelectList(db.Customers, "CustId", "Name", trip.CustId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);

            trip.deleteTrip(db);
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
