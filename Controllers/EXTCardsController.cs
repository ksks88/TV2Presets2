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
    public class EXTCardsController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/EXTCards
        public IQueryable<EXTCard> GetEXTCards()
        {
            return db.EXTCards;
        }

        // GET: api/EXTCards/5
        [ResponseType(typeof(EXTCard))]
        public async Task<IHttpActionResult> GetEXTCard(int id)
        {
            EXTCard eXTCard = await db.EXTCards.FindAsync(id);
            if (eXTCard == null)
            {
                return NotFound();
            }

            return Ok(eXTCard);
        }

        // PUT: api/EXTCards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEXTCard(int id, EXTCard eXTCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eXTCard.Id)
            {
                return BadRequest();
            }

            db.Entry(eXTCard).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EXTCardExists(id))
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

        // POST: api/EXTCards
        [ResponseType(typeof(EXTCard))]
        public async Task<IHttpActionResult> PostEXTCard(EXTCard eXTCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EXTCards.Add(eXTCard);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = eXTCard.Id }, eXTCard);
        }

        // DELETE: api/EXTCards/5
        [ResponseType(typeof(EXTCard))]
        public async Task<IHttpActionResult> DeleteEXTCard(int id)
        {
            EXTCard eXTCard = await db.EXTCards.FindAsync(id);
            if (eXTCard == null)
            {
                return NotFound();
            }

            db.EXTCards.Remove(eXTCard);
            await db.SaveChangesAsync();

            return Ok(eXTCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EXTCardExists(int id)
        {
            return db.EXTCards.Count(e => e.Id == id) > 0;
        }
    }
}