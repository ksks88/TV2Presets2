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
using TV2Presets2.Models;

namespace TV2Presets2.Controllers
{
    public class SteerableAntennasController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/SteerableAntennas
        public IQueryable<SteerableAntenna> GetSteerableAntennas()
        {
            return db.SteerableAntennas;
        }

        // GET: api/SteerableAntennas/5
        [ResponseType(typeof(SteerableAntenna))]
        public async Task<IHttpActionResult> GetSteerableAntenna(int id)
        {
            SteerableAntenna steerableAntenna = await db.SteerableAntennas.FindAsync(id);
            if (steerableAntenna == null)
            {
                return NotFound();
            }

            return Ok(steerableAntenna);
        }

        // PUT: api/SteerableAntennas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSteerableAntenna(int id, SteerableAntenna steerableAntenna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != steerableAntenna.Id)
            {
                return BadRequest();
            }

            db.Entry(steerableAntenna).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SteerableAntennaExists(id))
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

        // POST: api/SteerableAntennas
        [ResponseType(typeof(SteerableAntenna))]
        public async Task<IHttpActionResult> PostSteerableAntenna(SteerableAntenna steerableAntenna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SteerableAntennas.Add(steerableAntenna);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = steerableAntenna.Id }, steerableAntenna);
        }

        // DELETE: api/SteerableAntennas/5
        [ResponseType(typeof(SteerableAntenna))]
        public async Task<IHttpActionResult> DeleteSteerableAntenna(int id)
        {
            SteerableAntenna steerableAntenna = await db.SteerableAntennas.FindAsync(id);
            if (steerableAntenna == null)
            {
                return NotFound();
            }

            db.SteerableAntennas.Remove(steerableAntenna);
            await db.SaveChangesAsync();

            return Ok(steerableAntenna);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SteerableAntennaExists(int id)
        {
            return db.SteerableAntennas.Count(e => e.Id == id) > 0;
        }
    }
}