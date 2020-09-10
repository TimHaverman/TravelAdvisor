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
    public class FoodOrderLineItemsController : Controller
    {
        private TravelAdvisorEntities1 db = new TravelAdvisorEntities1();

        // GET: FoodOrderLineItems
        public ActionResult Index()
        {
            var foodOrderLineItems = db.FoodOrderLineItems.Include(f => f.FoodOrder1);
            return View(foodOrderLineItems.ToList());
        }

        // GET: FoodOrderLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderLineItem foodOrderLineItem = db.FoodOrderLineItems.Find(id);
            if (foodOrderLineItem == null)
            {
                return HttpNotFound();
            }
            return View(foodOrderLineItem);
        }

        // GET: FoodOrderLineItems/Create
        public ActionResult Create()
        {
            ViewBag.FoodOrder = new SelectList(db.FoodOrders, "Id", "Name");
            return View();
        }

        // POST: FoodOrderLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,FoodOrder,Ammount")] FoodOrderLineItem foodOrderLineItem)
        {
            if (ModelState.IsValid)
            {
                db.FoodOrderLineItems.Add(foodOrderLineItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodOrder = new SelectList(db.FoodOrders, "Id", "Name", foodOrderLineItem.FoodOrder);
            return View(foodOrderLineItem);
        }

        // GET: FoodOrderLineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderLineItem foodOrderLineItem = db.FoodOrderLineItems.Find(id);
            if (foodOrderLineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodOrder = new SelectList(db.FoodOrders, "Id", "Name", foodOrderLineItem.FoodOrder);
            return View(foodOrderLineItem);
        }

        // POST: FoodOrderLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,FoodOrder,Ammount")] FoodOrderLineItem foodOrderLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOrderLineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodOrder = new SelectList(db.FoodOrders, "Id", "Name", foodOrderLineItem.FoodOrder);
            return View(foodOrderLineItem);
        }

        // GET: FoodOrderLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrderLineItem foodOrderLineItem = db.FoodOrderLineItems.Find(id);
            if (foodOrderLineItem == null)
            {
                return HttpNotFound();
            }
            return View(foodOrderLineItem);
        }

        // POST: FoodOrderLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodOrderLineItem foodOrderLineItem = db.FoodOrderLineItems.Find(id);
            db.FoodOrderLineItems.Remove(foodOrderLineItem);
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
