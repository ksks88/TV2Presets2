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
    public class DownlinkChannelsController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/DownlinkChannels
        public IQueryable<DownlinkChannel> GetDownlinkChannels()
        {
            return db.DownlinkChannels;
        }

        // GET: api/DownlinkChannels/5
        [ResponseType(typeof(DownlinkChannel))]
        public async Task<IHttpActionResult> GetDownlinkChannel(int id)
        {
            DownlinkChannel downlinkChannel = await db.DownlinkChannels.FindAsync(id);
            if (downlinkChannel == null)
            {
                return NotFound();
            }

            return Ok(downlinkChannel);
        }

        // PUT: api/DownlinkChannels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDownlinkChannel(int id, DownlinkChannel downlinkChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != downlinkChannel.Id)
            {
                return BadRequest();
            }

            if (downlinkChannel.Satellite.Id != downlinkChannel.SatelliteId)
                downlinkChannel.Satellite = db.Satellites.FirstOrDefault(x => x.Id == downlinkChannel.SatelliteId);

            db.Entry(downlinkChannel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownlinkChannelExists(id))
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

        // POST: api/DownlinkChannels
        [ResponseType(typeof(DownlinkChannel))]
        public async Task<IHttpActionResult> PostDownlinkChannel(DownlinkChannel downlinkChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DownlinkChannels.Add(downlinkChannel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = downlinkChannel.Id }, downlinkChannel);
        }

        // DELETE: api/DownlinkChannels/5
        [ResponseType(typeof(DownlinkChannel))]
        public async Task<IHttpActionResult> DeleteDownlinkChannel(int id)
        {
            DownlinkChannel downlinkChannel = await db.DownlinkChannels.FindAsync(id);
            if (downlinkChannel == null)
            {
                return NotFound();
            }

            db.DownlinkChannels.Remove(downlinkChannel);
            await db.SaveChangesAsync();

            return Ok(downlinkChannel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DownlinkChannelExists(int id)
        {
            return db.DownlinkChannels.Count(e => e.Id == id) > 0;
        }
    }
}