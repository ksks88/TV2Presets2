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
    public class BISSCodesController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/BISSCodes
        public IQueryable<BISSCode> GetBISSCodes()
        {
            return db.BISSCodes;
        }

        // GET: api/BISSCodes/5
        [ResponseType(typeof(BISSCode))]
        public async Task<IHttpActionResult> GetBISSCode(int id)
        {
            BISSCode bISSCode = await db.BISSCodes.FindAsync(id);
            if (bISSCode == null)
            {
                return NotFound();
            }

            return Ok(bISSCode);
        }

        // PUT: api/BISSCodes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBISSCode(int id, BISSCode bISSCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bISSCode.Id)
            {
                return BadRequest();
            }

            db.Entry(bISSCode).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BISSCodeExists(id))
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

        // POST: api/BISSCodes
        [ResponseType(typeof(BISSCode))]
        public async Task<IHttpActionResult> PostBISSCode(BISSCode bISSCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BISSCodes.Add(bISSCode);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bISSCode.Id }, bISSCode);
        }

        // DELETE: api/BISSCodes/5
        [ResponseType(typeof(BISSCode))]
        public async Task<IHttpActionResult> DeleteBISSCode(int id)
        {
            BISSCode bISSCode = await db.BISSCodes.FindAsync(id);
            if (bISSCode == null)
            {
                return NotFound();
            }

            db.BISSCodes.Remove(bISSCode);
            await db.SaveChangesAsync();

            return Ok(bISSCode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BISSCodeExists(int id)
        {
            return db.BISSCodes.Count(e => e.Id == id) > 0;
        }
    }
}