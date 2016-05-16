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
    public class FixedAntennasController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/FixedAntennas
        public IQueryable<FixedAntenna> GetFixedAntennas()
        {
            return db.FixedAntennas;
        }

        // GET: api/FixedAntennas/5
        [ResponseType(typeof(FixedAntenna))]
        public async Task<IHttpActionResult> GetFixedAntenna(int id)
        {
            FixedAntenna fixedAntenna = await db.FixedAntennas.FindAsync(id);
            if (fixedAntenna == null)
            {
                return NotFound();
            }

            return Ok(fixedAntenna);
        }
        

        // PUT: api/FixedAntennas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFixedAntenna(int id, FixedAntenna fixedAntenna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fixedAntenna.Id)
            {
                return BadRequest();
            }

            // must manually set navigation property if i change position, or else it will throw exception
            if (fixedAntenna.SatellitePosition.Id != fixedAntenna.SatellitePositionId)
                fixedAntenna.SatellitePosition = db.SatellitePositions.FirstOrDefault(x => x.Id == fixedAntenna.SatellitePositionId);

            

            try
            {
                bool isok = await SetMatrixInputnames(fixedAntenna);
                if (!isok)
                {
                    return InternalServerError(new Exception("Cannot apply IRD to Visionic"));
                }
                db.Entry(fixedAntenna).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixedAntennaExists(id))
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

        private Task<bool> SetMatrixInputnames(FixedAntenna fixedAntenna)
        {
            return Task.Factory.StartNew(()=>
            {
                try
                {
                    Type UniCommandType = Type.GetTypeFromProgID("UniCommand.CSetBoundData");
                    dynamic UniCommand = Activator.CreateInstance(UniCommandType);
                    if (UniCommand == null)
                        return false;
                    Type ddrteType = Type.GetTypeFromProgID("DDRTE.Mgmt");
                    dynamic ddrte = Activator.CreateInstance(ddrteType);
                    if (ddrte == null)
                        return false;

                    UniCommand.ServerAddress = "127.0.0.1";
                    UniCommand.ServerPort = 6062;
                    UniCommand.MaxCommandRetries = 3;
                    UniCommand.EnableUserError = false;
                    UniCommand.EnableProgressDialog = false;
                    UniCommand.SchemaDatabase = ddrte.ProjectDatabase;

                    if (fixedAntenna.XHighInput != null && fixedAntenna.XHighFreq != null)
                    {
                        SetVisionicVariable("INP" + fixedAntenna.XHighInput, "Caption", fixedAntenna.Name + " X-High", UniCommand);
                        SetNameOnMatrix("S", (int)fixedAntenna.XHighInput, fixedAntenna.Name + " X-High", ddrte);
                    }
                    if (fixedAntenna.XLowInput != null && fixedAntenna.XLowFreq != null)
                    {
                        SetVisionicVariable("INP" + fixedAntenna.XLowInput, "Caption", fixedAntenna.Name + " X-Low", UniCommand);
                        SetNameOnMatrix("S", (int)fixedAntenna.XLowInput, fixedAntenna.Name + " X-Low", ddrte);
                    }
                    if (fixedAntenna.YHighInput != null && fixedAntenna.YHighFreq != null)
                    {
                        SetVisionicVariable("INP" + fixedAntenna.YHighInput, "Caption", fixedAntenna.Name + " Y-High", UniCommand);
                        SetNameOnMatrix("S", (int)fixedAntenna.YHighInput, fixedAntenna.Name + " Y-High", ddrte);
                    }
                    if (fixedAntenna.YLowInput != null && fixedAntenna.YLowFreq != null)
                    {
                        SetVisionicVariable("INP" + fixedAntenna.YLowInput, "Caption", fixedAntenna.Name + " Y-Low", UniCommand);
                        SetNameOnMatrix("S", (int)fixedAntenna.YLowInput, fixedAntenna.Name + " Y-Low", ddrte);
                    }

                    
                }
                catch (Exception EX_NAME)
                {
                    return false;
                }
                return true;
            });
        }

        // POST: api/FixedAntennas
        [ResponseType(typeof(FixedAntenna))]
        public async Task<IHttpActionResult> PostFixedAntenna(FixedAntenna fixedAntenna)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isok = await SetMatrixInputnames(fixedAntenna);
            if (!isok)
            {
                return InternalServerError(new Exception("Cannot apply IRD to Visionic"));
            }

            db.FixedAntennas.Add(fixedAntenna);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fixedAntenna.Id }, fixedAntenna);
        }

        // DELETE: api/FixedAntennas/5
        [ResponseType(typeof(FixedAntenna))]
        public async Task<IHttpActionResult> DeleteFixedAntenna(int id)
        {
            FixedAntenna fixedAntenna = await db.FixedAntennas.FindAsync(id);
            if (fixedAntenna == null)
            {
                return NotFound();
            }

            db.FixedAntennas.Remove(fixedAntenna);
            await db.SaveChangesAsync();

            return Ok(fixedAntenna);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FixedAntennaExists(int id)
        {
            return db.FixedAntennas.Count(e => e.Id == id) > 0;
        }

        private static void SetVisionicVariable(string DeviceName, string ParameterName, string newValue, dynamic pCmd)
        {
            pCmd.ResetVariables();
            pCmd.AddVariable(string.Format("{0}=>P=>{1}", DeviceName, ParameterName), newValue);
            pCmd.SetData();
        }
        private void SetNameOnMatrix(string inputType, int inIndex, string newname, dynamic ddrte)
        {

            if (!ddrte.IsDemoMode)
            {
                int instance = ddrte.FindInstance("Matrix");
                dynamic matrixdriver = ddrte.GetRunningInstance(instance);
                matrixdriver.SetAlias(0, inputType, inIndex, newname);

                matrixdriver.ReadNamesFromDevice(0);
            }
        }
    }
}