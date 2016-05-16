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
    public class IRDsController : ApiController
    {
        private readonly TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        // GET: api/IRDs
        public IQueryable<IRD> GetIRDs()
        {
            return db.IRDs;
        }

        // GET: api/IRDs/5
        [ResponseType(typeof(IRD))]
        public async Task<IHttpActionResult> GetIRD(int id)
        {
            IRD iRD = await db.IRDs.FindAsync(id);
            if (iRD == null)
            {
                return NotFound();
            }

            return Ok(iRD);
        }

        // PUT: api/IRDs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIRD(int id, IRD iRD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iRD.Id)
            {
                return BadRequest();
            }

            try
            {
                bool isok = await SetIRDOnVisionic(iRD);
                if (!isok)
                {
                    return InternalServerError(new Exception("Cannot apply IRD to Visionic"));
                }

                db.Entry(iRD).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IRDExists(id))
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

        // POST: api/IRDs
        [ResponseType(typeof(IRD))]
        public async Task<IHttpActionResult> PostIRD(IRD iRD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isok = await SetIRDOnVisionic(iRD);
            if (!isok)
            {
                return InternalServerError(new Exception("Cannot apply IRD to Visionic"));
            }

            db.IRDs.Add(iRD);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = iRD.Id }, iRD);
        }

        // DELETE: api/IRDs/5
        [ResponseType(typeof(IRD))]
        public async Task<IHttpActionResult> DeleteIRD(int id)
        {
            IRD iRD = await db.IRDs.FindAsync(id);
            if (iRD == null)
            {
                return NotFound();
            }

            db.IRDs.Remove(iRD);
            await db.SaveChangesAsync();

            return Ok(iRD);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IRDExists(int id)
        {
            return db.IRDs.Count(e => e.Id == id) > 0;
        }


        private Task<bool> SetIRDOnVisionic(IRD ird)
        {
            return Task.Factory.StartNew(() =>
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

                    string irdtag = "";
                    string template = "";
                    string irdcaption = "";
                    switch (ird.IRDType)
                    {
                        case IRDTypes.RX8200:
                            irdtag = "IRD_8200";
                            template = "Tandberg RX8200";
                            irdcaption = "RX 8200";
                            break;
                        case IRDTypes.TX1260:
                            irdtag = "IRD_1290";
                            template = "Tandberg RX1290";
                            irdcaption = "RX 1260";
                            break;
                        case IRDTypes.TX1290:
                            irdtag = "IRD_1290";
                            template = "Tandberg RX1290";
                            irdcaption = "RX 1290";
                            break;
                        case IRDTypes.RX8200S2ip:
                            irdtag = "IRD_8200-S2ip";
                            template = "RX8200-S2ip";
                            irdcaption = "RX 8200-S2ip";
                            break;
                        default:
                            break;
                    }

                    SetVisionicVariable(ird.Name, "IRDType", irdtag, UniCommand); // to je za custom dialog
                    SetVisionicVariable(ird.Name + " type", "Caption", irdcaption, UniCommand);



                    object[] parameters = { 0, ird.IPAddress + ";public;10;" + template + ";3;3;1000;1;private;" };
                    int instance = ddrte.FindInstance(ird.Name);
                    dynamic irddriver = ddrte.GetRunningInstance(instance);
                    irddriver.SetMgmtData(0, ird.IPAddress + ";public;10;" + template + ";3;3;1000;1;private;");

                    SetVisionicVariable("OUTP" + ird.MatrixOutput, "Caption", ird.Name, UniCommand);
                    return true;
                }
                catch (Exception EX_NAME)
                {
                    //Console.WriteLine(EX_NAME);
                    return false;
                }

            });
        }

        private static void SetVisionicVariable(string DeviceName, string ParameterName, string newValue, dynamic pCmd)
        {
            pCmd.ResetVariables();
            pCmd.AddVariable(string.Format("{0}=>P=>{1}", DeviceName, ParameterName), newValue);
            pCmd.SetData();
        }
    }
}