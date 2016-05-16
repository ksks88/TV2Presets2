using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TV2Presets2.Models;
// ReSharper disable All

namespace TV2Presets2.Controllers
{
    public class SatellitePositionsController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/SatellitePositions
        public IQueryable<SatellitePosition> GetSatellitePositions()
        {
            return db.SatellitePositions;
        }

        // GET: api/SatellitePositions/5
        [ResponseType(typeof(SatellitePosition))]
        public async Task<IHttpActionResult> GetSatellitePosition(int id)
        {
            SatellitePosition satellitePosition = await db.SatellitePositions.FindAsync(id);
            if (satellitePosition == null)
            {
                return NotFound();
            }

            return Ok(satellitePosition);
        }

        // PUT: api/SatellitePositions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSatellitePosition(int id, SatellitePosition satellitePosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != satellitePosition.Id)
            {
                return BadRequest();
            }

            db.Entry(satellitePosition).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SatellitePositionExists(id))
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

        // POST: api/SatellitePositions
        [ResponseType(typeof(SatellitePosition))]
        public async Task<IHttpActionResult> PostSatellitePosition(SatellitePosition satellitePosition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SatellitePositions.Add(satellitePosition);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = satellitePosition.Id }, satellitePosition);
        }

        // DELETE: api/SatellitePositions/5
        [ResponseType(typeof(SatellitePosition))]
        public async Task<IHttpActionResult> DeleteSatellitePosition(int id)
        {
            SatellitePosition satellitePosition = await db.SatellitePositions.FindAsync(id);
            if (satellitePosition == null)
            {
                return NotFound();
            }

            db.SatellitePositions.Remove(satellitePosition);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;
                if (sqlException != null)
                {
                    if (sqlException.Errors.Count > 0)
                    {
                        switch (sqlException.Errors[0].Number)
                        {
                            case 547: // Foreign Key violation
                                return BadRequest("Position : " + satellitePosition.Name + " could not be deleted, because it is in use!");
                            default:
                                throw;
                        }
                    }
                }
                else
                {
                    throw;
                } 

                
            }
            

            return Ok(satellitePosition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SatellitePositionExists(int id)
        {
            return db.SatellitePositions.Count(e => e.Id == id) > 0;
        }
    }
}