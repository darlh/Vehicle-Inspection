using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VehicleInspection.Models;

namespace VehicleInspection.Controllers
{
    public class InspectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inspections
        public ActionResult Index(int? id)
        {
            var returnView = "Index";
            if (User.IsInRole("Manager")) { returnView = "ManagerIndex"; }

            //if (id != null) {
            //    return View(returnView, db.Inspections.Where(i => i.VehicleId == id).ToList());
            //}
            //else { return View(returnView, db.Inspections.ToList()); }            

            ViewData["vId"] = id;

            return View(returnView);

        }

        // GET: All Inspections
        [Authorize(Roles = "Manager")]
        public ActionResult IndexAll()
        {
            return View();
        }

        //GET: Inspections/ViewImages/5
        public ActionResult ViewImages(int id)
        {
            IEnumerable<Image> images = db.Images.Where(i => i.InspectionID == id).ToList();
            return View(images);
        }


        // GET: Inspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // GET: Inspections/Create
        public ActionResult Create()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles.OrderBy(i => i.UnitNumber), "Id", "UnitNumber");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InspectionID,VehicleId,Name,Timestamp,Mileage,Tires,Brakes,Fluids,Lights,Windshield,Exterior,Interior,Supplies,Notes,Issues")] Inspection inspection, Image imageModel)
        {
            if (ModelState.IsValid)
            {
                //count files in file base                
                int imageCount = 0;
                foreach (HttpPostedFileBase file in imageModel.ImageFiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        imageCount++;
                    }
                }

                //check for non images
                bool hasNonImages = false;
                if (imageCount > 0)
                {
                    if (imageModel.ImageFiles.Any(i => i.ContentType.Contains("image") == false))
                        hasNonImages = true;
                }

                if (hasNonImages)
                    ModelState.AddModelError("Images", "Only image files may be uploaded");

                else if (imageCount > 10)
                    ModelState.AddModelError("Images", "Only 10 images may be uploaded with the inspection");

                else
                {
                    foreach (HttpPostedFileBase file in imageModel.ImageFiles)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            inspection.HasImages = true;

                            Image i = new Image
                            {
                                InspectionID = imageModel.InspectionID
                            };

                            string filename = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                            i.ImagePath = "~/Inspection_Images/" + filename;
                            filename = Path.Combine(Server.MapPath("~/Inspection_Images/"), filename);
                            file.SaveAs(filename);
                            db.Images.Add(i);
                        }

                    }

                    //update the most recent inspection for the vehicle
                    var vehicleToUpdate = db.Vehicles.Find(inspection.VehicleId);
                    vehicleToUpdate.LastInspect = DateTime.Now;

                    db.Inspections.Add(inspection);
                    db.SaveChanges();

                    //send email if there are issues
                    if (inspection.Issues != null)
                    {
                        SendIssueAlertEmail(inspection);
                    }


                    ModelState.Clear();

                    return RedirectToAction("Index", new { id = inspection.VehicleId });
                }

            }

            ViewBag.VehicleId = new SelectList(db.Vehicles.OrderBy(i => i.UnitNumber), "Id", "UnitNumber", inspection.VehicleId);

            return View(inspection);
        }

        public void SendIssueAlertEmail(Inspection inspection)
        {
            var unitNumber = db.Vehicles.FirstOrDefault(i => i.Id == inspection.VehicleId).UnitNumber;
            var body =
                "<h3>Vehicle Daily Inspection App: Issue Alert</h3>" +
                "<br />" +
                "<p><b>{0}</b></p>" +
                "<p><b>Van: </b>{1}</p>" +
                "<p><b>Driver: </b>{2}</p>" +
                "<p><b>Issue: </b>{3}</p>" +
                "<br />" +
                //"<p><b>Click <a href='http://localhost:52886/Inspections/Details/{4}'>here</a> to view inspection details.</b></p>" +
                "<p><b>Click <a href='http://QTS.VehicleDailyInspection.com/Inspections/Details/{4}'>here</a> to view inspection details.</b></p>" +
                "<br />" +
                "<hr />" +
                //"<p><i>You have recieved this email becuase you have subscribed to Vehicle Issue Alerts. Manage your subscriptions <a href='http://localhost:52886/manage'>here</a>.</i></p>";
                "<p><i>You have recieved this email becuase you have subscribed to Vehicle Issue Alerts. Manage your subscriptions <a href='http://QTS.VehicleDailyInspection.com/Manage'>here</a>.</i></p>";
            MailMessage email = new MailMessage();
            foreach (ApplicationUser manager in db.Users.Where(u => u.IsManager == true).ToList())
            {
                if (manager.IsSubscribedIssues==true)
                email.Bcc.Add(manager.Email);
            }
            var userId = User.Identity.GetUserId();
            var driver = db.Users.FirstOrDefault(u => u.Id == userId);
            var isSubscribed = driver.IsSubscribedIssues;
            if (isSubscribed)
            { email.CC.Add(driver.Email); }
            email.Subject = "Issue Reoprted for " + unitNumber;
            email.Body = string.Format(body, inspection.Timestamp, unitNumber, inspection.Name, inspection.Issues, inspection.InspectionID);
            email.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(email);
            }
        }

        // GET: Inspections/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles.OrderBy(i => i.UnitNumber), "Id", "UnitNumber", inspection.VehicleId);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InspectionID,VehicleId,Name,Timestamp,Mileage,Tires,Brakes,Fluids,Lights,Windshield,Exterior,Interior,Supplies,Notes,Issues,HasImages")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = inspection.VehicleId });
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles.OrderBy(i => i.UnitNumber), "Id", "UnitNumber", inspection.VehicleId);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspection inspection = db.Inspections.Find(id);
            db.Inspections.Remove(inspection);
            db.SaveChanges();

            return RedirectToAction("Index", new { id = inspection.VehicleId });
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