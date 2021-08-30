using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleInspection.DTOs;
using VehicleInspection.Models;

namespace VehicleInspection.Controllers.Api
{
    public class InspectionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Inspections
        public IEnumerable<InspectionDto> GetInspections()
        {
            return db.Inspections
                .Include(i => i.Vehicle)
                .ToList()
                .Select(Mapper.Map<Inspection, InspectionDto>);
        }

        // GET: api/Inspections/5
        public IEnumerable<InspectionDto> GetInspection(int id)
        {
            return db.Inspections
                .Include(i => i.Vehicle)
                .Where(i => i.VehicleId == id)
                .ToList()
                .Select(Mapper.Map<Inspection, InspectionDto>);
        }

        // PUT: api/Inspections/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInspection(int id, Inspection inspection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inspection.InspectionID)
            {
                return BadRequest();
            }

            db.Entry(inspection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Inspections
        [ResponseType(typeof(Inspection))]
        public IHttpActionResult PostInspection(Inspection inspection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Inspections.Add(inspection);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = inspection.InspectionID }, inspection);
        }

        // DELETE: api/Inspections/5
        [HttpDelete]
        public IHttpActionResult DeleteInspection(int id)
        {
            var inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return NotFound();
            }

            if (inspection.HasImages)
            {
                IEnumerable<Image> images = db.Images.Where(i => i.InspectionID == id).ToList();
                foreach (Image i in images)
                {
                    string file = HttpContext.Current.Server.MapPath(i.ImagePath);
                    File.Delete(file);
                }
            }

            db.Inspections.Remove(inspection);
            db.SaveChanges();

            return Ok(inspection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InspectionExists(int id)
        {
            return db.Inspections.Count(e => e.InspectionID == id) > 0;
        }
    }
}