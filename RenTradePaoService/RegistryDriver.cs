using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenTradeWindowService
{
    public class RegistryDriver
    {
        private RegistryKey key;
        private string[] logs;

        public int PedalFlag { get; private set; }
        public bool PedalStatus { get; private set; }
        public bool PedalIsActivated { get; private set; }
        public int ProcessCounter { get; set; }
        public bool IsProd { get; private set; }
        public bool IsPullTest { get; private set; }
        public int TestCounter { get; set; }
        public int QuotaCounter { get; set; }
        public int CycleCounter { get; set; }
        public string ProcessStage { get; private set; }
        public bool ReelStatus { get; private set; }

        public string IOBoardStatus { get; private set; }
        public string OldOrderNos { get; private set; }
        public string JobInfo { get; private set; }
        public string MaterialNos { get; private set; }
        public string MaterialCount { get; private set; }

        private readonly string _logPath;

        public RegistryDriver(IOptions<ServiceConfiguration> options)
        {
            //InitializeRegistry();

            _logPath = options.Value.LogPath;
        }

        private void InitializeRegistry()
        {
            ////opening the subkey  
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\RenTradeSettings");

            //if it does exist, set key values
            if (key == null)
            {
                //accessing the LocalMachine root element  
                //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
                this.key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RenTradeSettings");

                //storing the values  
                key.SetValue("pedalFlag", "0");                 // 1 means hit, 0 means loose
                key.SetValue("pedalStatus", "False");           // true means pedal is activated by default
                key.SetValue("processCounter", "0");            // number of process production item per batches
                key.SetValue("isProd", "False");                // true means reel material is detected

                key.SetValue("testCounter", "0");               // number of pull test item on first Pcs
                key.SetValue("quotaCounter", "0");              // number of cummulative production count
                key.SetValue("cycleCounter", "0");              // number of cycle count

                key.SetValue("processStage", "A1");              // A1-validation if achieve test qty, A2-1st pull test, A3-1st calipher test, 
                                                                 // B1-validation if achieve prod qty, B2-2nd pull test, B3-2nd calipher test, B4-
                key.SetValue("reelStatus", "True");             // true means reel material is detected

                key.SetValue("ioBoardStatus", "");              // IO Board Status
                key.SetValue("materialNos", "");                // Material Nos
                key.SetValue("materialCount", "0");              // Material Count

                key.Close();
            }
        }

        public void ReadRegistry()
        {
            try
            {
                //opening the subkey  
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\RenTradeSettings");

                //if it does exist, retrieve the stored values  
                if (key != null)
                {
                    this.PedalFlag = Convert.ToInt16(key.GetValue("pedalFlag"));                            // 1 means hit, 0 means loose
                    this.PedalStatus = Convert.ToBoolean(key.GetValue("pedalStatus"));                      // true means pedal is activated by default
                    this.ProcessCounter = Convert.ToInt16(key.GetValue("processCounter"));                  // number of process production item per batches
                    this.IsProd = Convert.ToBoolean(key.GetValue("isProd"));                                // number of process production item per batches
                    this.IsPullTest = Convert.ToBoolean(key.GetValue("isPullTest"));                        // true means reel material is detected

                    this.TestCounter = Convert.ToInt16(key.GetValue("testCounter"));                        // number of pull test item on first Pcs
                    this.QuotaCounter = Convert.ToInt16(key.GetValue("quotaCounter"));                      // number of cummulative production count    
                    this.CycleCounter = Convert.ToInt16(key.GetValue("cycleCounter"));                      // number of cycle wt count                    
                    this.ProcessStage = key.GetValue("processStage").ToString();                            // get the current stage

                    this.ReelStatus = Convert.ToBoolean(key.GetValue("reelStatus"));                        // Check Reel Status

                    this.IOBoardStatus = key.GetValue("ioBoardStatus").ToString();
                    this.OldOrderNos = key.GetValue("oldOrderNos").ToString();
                    this.JobInfo = key.GetValue("jobInfo").ToString();
                    this.MaterialNos = key.GetValue("materialNos").ToString();
                    this.MaterialCount = key.GetValue("materialCount").ToString();

                    key.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void WriteRegistry(string settingName, string settingVal)
        {
            try
            {
                //accessing the LocalMachine root element and adding "RenTradeSettings" subkey to the "SOFTWARE" subkey  
                this.key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\RenTradeSettings");

                //storing the values  
                key.SetValue(settingName, settingVal);
                key.Close();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
        }

        public void ResetRegistry()
        {
            this.WriteRegistry("jobInfo", "");
            this.WriteRegistry("materialNos", "");
            this.WriteRegistry("materialCount", "0");
            this.WriteRegistry("pedalFlag", "0");
            this.WriteRegistry("isProd", "False");
            this.WriteRegistry("pedalStatus", "False");
            this.WriteRegistry("processCounter", "0");
            this.WriteRegistry("testCounter", "0");
            this.WriteRegistry("cycleCounter", "0");
            this.WriteRegistry("processStage", "A1");

            this.WriteRegistry("reelStatus", "True");
            this.WriteRegistry("ioBoardStatus", "X0");
        }

        public void ErrorLogger(string message)
        {
            string errorFolder = "error";
            string subFolderPath = this.@_logPath + errorFolder + "\\";

            if (!Directory.Exists(subFolderPath))
                Directory.CreateDirectory(subFolderPath);

            this.logs = new string[] { DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + " - ERROR: " + message };
            File.AppendAllLines(subFolderPath + "ErrorLogs.txt", this.logs);
        }

        public void TextLogger(string filename, string message)
        {
            string dateFolder = DateTime.Now.ToString("yyyyMMdd");
            string subFolderPath = this.@_logPath + "trace\\" + dateFolder + "\\";

            if (!Directory.Exists(subFolderPath))
                Directory.CreateDirectory(subFolderPath);

            string[] lines = new string[] { message.ToString() };
            File.AppendAllLines(subFolderPath + "datalogs-" + filename + ".txt", lines);
        }
    }
}
