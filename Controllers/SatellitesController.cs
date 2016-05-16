using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.ModelBinding;
using TV2Presets2.Models;

namespace TV2Presets2.Controllers
{
    public class SatellitesController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/Satellites
        public IQueryable<Satellite> GetSatellites()
        {
            return db.Satellites;
        }

        // GET: api/Satellites/5
        [ResponseType(typeof(Satellite))]
        public async Task<IHttpActionResult> GetSatellite(int id)
        {
            Satellite satellite = await db.Satellites.FindAsync(id);
            if (satellite == null)
            {
                return NotFound();
            }

            return Ok(satellite);
        }

        // PUT: api/Satellites/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSatellite(int id, Satellite satellite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != satellite.Id)
            {
                return BadRequest();
            }

            if (satellite.SatellitePosition.Id != satellite.SatellitePositionId)
                satellite.SatellitePosition = db.SatellitePositions.FirstOrDefault(x => x.Id == satellite.SatellitePositionId);


            db.Entry(satellite).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SatelliteExists(id))
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

        // POST: api/Satellites
        [ResponseType(typeof(Satellite))]
        public async Task<IHttpActionResult> PostSatellite(Satellite satellite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Satellites.Add(satellite);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            

            
            return CreatedAtRoute("DefaultApi", new { id = satellite.Id }, satellite);
        }

        // DELETE: api/Satellites/5
        [ResponseType(typeof(Satellite))]
        public async Task<IHttpActionResult> DeleteSatellite(int id)
        {
            Satellite satellite = await db.Satellites.FindAsync(id);
            if (satellite == null)
            {
                return NotFound();
            }

            db.Satellites.Remove(satellite);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            

            return Ok(satellite);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SatelliteExists(int id)
        {
            return db.Satellites.Count(e => e.Id == id) > 0;
        }
    }
}