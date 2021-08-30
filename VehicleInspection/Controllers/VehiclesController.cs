using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleInspection.Models;

namespace VehicleInspection.Controllers
{
    [Authorize(Roles = "Manager")]
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            foreach (Vehicle v in db.Vehicles)
            {
                if (db.Inspections.Where(i => i.VehicleId == v.Id).Count() > 0)
                    v.LastInspect = db.Inspections.Where(i => i.VehicleId == v.Id).Max(t => t.Timestamp);
                else
                    v.LastInspect = null;
            }
            db.SaveChanges();
            return View(db.Vehicles.OrderBy(i => i.UnitNumber).ToList());
        }
                
        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UnitNumber,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (db.Vehicles.Any(v => v.UnitNumber == vehicle.UnitNumber))
                {
                    ModelState.AddModelError("UnitNumber", "That vehicle already exists");
                }
                else {
                    vehicle.LastInspect = null;
                    db.Vehicles.Add(vehicle);                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type",vehicle.VehicleTypeId);
            return View(vehicle);
        }

        //POST: Vehicles/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UnitNumber,LastInspect,VehicleTypeId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                if (db.Vehicles.Any(v => v.UnitNumber == vehicle.UnitNumber) && vehicle.UnitNumber!=db.Vehicles.AsNoTracking().FirstOrDefault(v => v.Id == vehicle.Id).UnitNumber)
                {
                    ModelState.AddModelError("UnitNumber", "That vehicle already exists");
                }
                else
                {
                    db.Entry(vehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);

        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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
