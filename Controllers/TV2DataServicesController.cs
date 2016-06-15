using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using TV2Presets2.Models;
using UniCommand;

namespace TV2Presets2.Controllers
{
    public class TV2DataServicesController : ApiController
    {
        private TV2PresetsModelContainer db = new TV2PresetsModelContainer();

        public async Task<IHttpActionResult> PostApplyDownlinkPreset(dynamic data)
        {
            try
            {
                string retstr = await ApplyDownlinkPreset(data);
                if (retstr == "")
                    return Ok("Preset applyed succesfully");
                return InternalServerError(new Exception(retstr));
            }
            catch (Exception EX_NAME)
            {
                return InternalServerError(EX_NAME);
            }
            
        }


        // my time consuming task
        // resharper made it static, why, i do not know...
        private Task<string> ApplyDownlinkPreset(dynamic data)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    int channelid = data.ChannelId;
                    DownlinkChannel channel = db.DownlinkChannels.Find(channelid);
                    if (channel == null)
                        return "Chsnnel not found";
                    int irdid = data.irdId;
                    IRD ird = db.IRDs.Find(irdid);
                    if (ird == null)
                        return "IRD not found";
                    string antennaType = data.antennaType;
                    int antennaid = data.antennaid;


                    int? MtxInput;
                    double LNBLoFrequency;
                    int InputNumber = 0;

                    dynamic selectedAntenna;

                    if (antennaType == "Fixed")
                    {
                        selectedAntenna = db.FixedAntennas.Find(antennaid);  
                    }
                    else if (antennaType == "Steerable")
                    {
                        selectedAntenna = db.SteerableAntennas.Find(antennaid);
                    }
                    else
                    {
                        return "Antenna type not selected";
                    }

                    if (selectedAntenna == null)
                        return "Antenna not found";

                    if (channel.Polarisation == PolarisationEnum.Horizontal && channel.Frequency >= 11700)
                    { // x-high
                        MtxInput = selectedAntenna.YHighInput;
                        LNBLoFrequency = selectedAntenna.XHighFreq;
                    }
                    else if (channel.Polarisation == PolarisationEnum.Horizontal && channel.Frequency < 11700)
                    {
                        MtxInput = selectedAntenna.XLowInput;
                        LNBLoFrequency = selectedAntenna.XLowFreq;
                    }
                    else if (channel.Polarisation == PolarisationEnum.Vertical && channel.Frequency >= 11700)
                    {
                        MtxInput = selectedAntenna.YHighInput;
                        LNBLoFrequency = selectedAntenna.YHighFreq;
                    }
                    else
                    {
                        MtxInput = selectedAntenna.YLowInput;
                        LNBLoFrequency = selectedAntenna.YLowFreq;
                    }


                    Type UniCommandType = Type.GetTypeFromProgID("UniCommand.CSetBoundData",true);
                    dynamic UniCommand = Activator.CreateInstance(UniCommandType);
                    //CSetBoundData UniCommand = new CSetBoundData();
                    if (UniCommand == null)
                        return "Cannot create UniCommand.CSetBoundData";

                   
                    
                    dynamic ddrte = Marshal.GetActiveObject("DDRTE.Mgmt");

                    if (ddrte == null)
                        return "Cannot find Visionic server";

                    UniCommand.ServerAddress = "127.0.0.1";
                    UniCommand.ServerPort = 6062;
                    UniCommand.MaxCommandRetries = 3;
                    UniCommand.EnableUserError = false;
                    UniCommand.EnableProgressDialog = false;
                    UniCommand.SchemaDatabase = ddrte.ProjectDatabase;

                    //object[] paramseters = { 0, Convert.ToInt32(ird.MatrixOutput) , MtxInput };
                    // convert frequency from MHz to kHz, and Symbol Rate from kS/s to S/s
                    string strFrequency = (channel.Frequency * 1000).ToString(CultureInfo.InvariantCulture);
                    string strSymbolRate = (channel.SymbolRate * 1000).ToString(CultureInfo.InvariantCulture);
                    string strLOFrequency = (LNBLoFrequency * 1000).ToString(CultureInfo.InvariantCulture);

                    // Route matrix
                    int matrixinstance = Convert.ToInt32(ddrte.FindInstance("Matrix"));

                    //bool r = UniCommand.CallServerMethodByInstance(matrixinstance, "SetOutput", paramseters);
                    dynamic matrixdriver = ddrte.GetRunningInstance(matrixinstance);
                    if (MtxInput != null)
                        matrixdriver.SetOutput(0, ird.MatrixOutput, MtxInput);

                    if (ird.IRDType == IRDTypes.RX8200 || ird.IRDType == IRDTypes.RX8200S2ip)  // 8200
                    {
                        InputNumber = Convert.ToInt32(GetVisionicValue(ird.Name, "IRD InputNum Select", ddrte));
                        SetVisionicVariable(ird.Name, "Polarization_" + (InputNumber + 1), (channel.Polarisation == PolarisationEnum.Horizontal ? 1 : 0).ToString(), UniCommand);
                        SetVisionicVariable(ird.Name, "LNB Frequency_" + (InputNumber + 1), strLOFrequency, UniCommand);
                        SetVisionicVariable(ird.Name, "Input Frequency_" + (InputNumber + 1), strFrequency, UniCommand);
                        SetVisionicVariable(ird.Name, "Symbol rate_" + (InputNumber + 1), strSymbolRate, UniCommand);
                        SetVisionicVariable(ird.Name, "RollOffSet_" + (InputNumber + 1), ((int)channel.RollOff).ToString() , UniCommand);
                        SetVisionicVariable(ird.Name, "VModulationModeSet_" + (InputNumber + 1), ((int)channel.Modulation).ToString(), UniCommand);
                        int bissId;

                        if (Int32.TryParse((string)data.bissId, out bissId))
                        {
                            BISSCode biss = db.BISSCodes.Find(bissId);
                            if (biss == null)
                                return "Cannot find BissCode";

                            if (biss.BISSType == BISSTypeEnum.RAS)
                            {
                                SetVisionicVariable(ird.Name, "RASKey", biss.BISSKey, UniCommand);
                            }
                            else
                            {
                                SetVisionicVariable(ird.Name, "BISS Mode", ((int) biss.BISSType).ToString(), UniCommand);
                                SetVisionicVariable(ird.Name, "BISS code", biss.BISSKey, UniCommand);
                            }
                            SetVisionicVariable(ird.Name, "BISSCodeName", biss.Name, UniCommand);
                        }
                       

                    }
                    else // 1290/1260
                    {
                        InputNumber = Convert.ToInt32(GetVisionicValue(ird.Name, "InputSelRaw", ddrte));
                        InputNumber -= 2;

                        SetVisionicVariable(ird.Name, "FECSet_" + (InputNumber + 1), ((int)channel.FEC).ToString(), UniCommand);
                        SetVisionicVariable(ird.Name, "Polarization_" + (InputNumber + 1), (channel.Polarisation == PolarisationEnum.Horizontal ? 2 : 1).ToString(), UniCommand);
                        SetVisionicVariable(ird.Name, "LNB Frequency_" + (InputNumber + 1), strLOFrequency, UniCommand);
                        SetVisionicVariable(ird.Name, "Input Frequency_" + (InputNumber + 1), strFrequency, UniCommand);
                        SetVisionicVariable(ird.Name, "Symbol rate_" + (InputNumber + 1), strSymbolRate, UniCommand);
                        SetVisionicVariable(ird.Name, "RollOffSet_" + (InputNumber + 1), ((int)channel.RollOff + 1).ToString(), UniCommand);
                        SetVisionicVariable(ird.Name, "VModulationModeSet_" + (InputNumber + 1), ((int)channel.Modulation == 0 ? 1 : 2).ToString(), UniCommand);
                        int bissId;
                        if (Int32.TryParse((string)data.bissId,out bissId))
                        {
                            BISSCode biss = db.BISSCodes.Find(bissId);
                            if (biss == null)
                                return "Cannot find BissCode";

                            if (biss.BISSType == BISSTypeEnum.RAS)
                            {
                                SetVisionicVariable(ird.Name, "RASKey", biss.BISSKey, UniCommand);
                            }
                            else
                            {
                                // here to implement custom invocation of snmpset.exe because setting of this parmeter doesn't work from visionic!
                                SetVisionicVariable(ird.Name, "BISS Mode", ((int) biss.BISSType + 1).ToString(),
                                    UniCommand); // biss mode should work fine

                                SetBissMode1290(ird.IPAddress, biss.BISSKey);
                                SetVisionicVariable(ird.Name, "currentBiss", biss.BISSKey, UniCommand);
                            }
                            SetVisionicVariable(ird.Name, "BISSCodeName", biss.Name, UniCommand);
                        }
                   
                    }

                    SetVisionicVariable("preset" + Convert.ToInt32(ird.Name.Split(' ')[1]), "Caption", channel.Name, UniCommand);

                    int extid;

                    if (Int32.TryParse((string)data.extId, out extid))
                    {
                        EXTCard extcard = db.EXTCards.Find(extid);
                        if (extcard == null)
                            return "Cannot find Ext card";
                        routeMCRMatrix(ird.MatrixOutput, extcard.MatrixOutput);
                    }
                    


                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            });
        }

        private void routeMCRMatrix(int input, int output)
        {
            List<byte> ByteList = new List<byte>();
            ByteList.Add(0x10);
            ByteList.Add(0x02);
            ByteList.Add(0x02);  // command
            ByteList.Add(0x00);  // matrix number is 0 and leyer is 0
            // now
            byte b = 0x00;
            b = (byte)(b | (byte)(output / 128));
            b = (byte)(b & 0x07);  // thats source DIV 128

            b = (byte)(b | ((byte)(input / 128) << 4));
            b = (byte)(b & 0x7F);

            ByteList.Add(b);
            // ======
            ByteList.Add(Convert.ToByte(output % 128));
            ByteList.Add(Convert.ToByte(input % 128));
            ByteList.Add(0x05);

            byte checksum = 0x00;
            checksum = (byte)(((ByteList[2] ^ 0xFF) + 1) + ((ByteList[3] ^ 0xFF) + 1) +
                ((ByteList[4] ^ 0xFF) + 1) +
                ((ByteList[5] ^ 0xFF) + 1) +
                ((ByteList[6] ^ 0xFF) + 1) +
                ((ByteList[7] ^ 0xFF) + 1));



            ByteList.Add(checksum);
            ByteList.Add(0x10);
            ByteList.Add(0x03);

            for (int j = 2; j < ByteList.Count - 2; j++)
            {
                if (ByteList[j] == 0x10)
                {
                    ByteList.Insert(j + 1, 0x10);
                    j++;
                }
            }

            TcpClient tcpcli = new TcpClient();
            //try ToString connect ToString primary
            try
            {
                tcpcli.Connect("10.225.15.11", 9100);
            }
            catch (Exception)
            {
                try
                {
                    tcpcli.Connect("10.225.15.12", 9100);
                }
                catch (Exception)
                {
                    return;
                }
            }

            NetworkStream stm = tcpcli.GetStream();

            stm.Write(ByteList.ToArray(), 0, ByteList.ToArray().Length);


            byte[] recbytes = new byte[100];
            int i = stm.Read(recbytes, 0, 100);
            byte[] y = { 0x10, 0x06 };
            if ((from int a in Enumerable.Range(0, 1 + recbytes.Length - y.Length) where recbytes.Skip(a).Take(y.Length).SequenceEqual(y) select (int?)a).Any())
            {
                Console.WriteLine("ACK nfrom server");
                stm.Write(y, 0, 2);
            }



            tcpcli.Close();
        }

        private void SetBissMode1290(string Address, string BissCode)
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Program Files (x86)\\Intorel\\Visionic\\snmpset.exe";
            startInfo.Arguments = "-c private -v1 " + Address + " .1.3.6.1.4.1.1773.1.3.200.3.7.2.5.1.0 x " + BissCode;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo = startInfo;
            proc.Start();
        }

        private static void SetVisionicVariable(string DeviceName, string ParameterName, string newValue, dynamic pCmd)
        {
            pCmd.ResetVariables();
            pCmd.AddVariable(string.Format("{0}=>P=>{1}", DeviceName, ParameterName), newValue);
            pCmd.SetData();
        }

        private string GetVisionicValue(string DeviceName, string ParameterName, dynamic ddrte)
        {

            string ret = "";
            int deviceinstance = ddrte.FindInstance(DeviceName);
            dynamic device = ddrte.GetRunningInstance(deviceinstance);

            string notifXml = device.NotifData;
            XElement notifelement = XElement.Parse(notifXml);
            try
            {
                ret = (from XElement xel in notifelement.Descendants("D") where xel.Attribute("I").Value == "0" select xel).FirstOrDefault().Descendants("P").Where(x => x.Attribute("N").Value == ParameterName).FirstOrDefault().Value;
            }
            catch (Exception)
            {
                ret = "Error";
            }
            
            return ret;
        }
    }
}
