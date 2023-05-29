using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenTradeWindowService
{
    public class ServiceConfiguration
    {
        public string EnvironmentMode { get; set; }
        public string MachineName { get; set; }
        public string MachineType { get; set; }
        public string MachineClass { get; set; }
        public string LogPath { get; set; }
        public string DashboardFormPath { get; set; }
        public string MessageQueueName { get; set; }
        public int ThreadDelay { get; set; }
        public int MsgQueueWaitingTime { get; set; }
        public string TestPcsMsg { get; set; }
        public string ProdPcsMsg { get; set; }
        public string QuotaPcsMsg { get; set; }
        public string EndJobMsg { get; set; }
        public string ComPort { get; set; }
        public int BaudRate { get; set; }
        public string Parity { get; set; }
        public int DataBits { get; set; }
        public string StopBits { get; set; }
        public int FirstPcsInitCount { get; set; } // test pcs count
        public int FirstPcsInitCount1 { get; set; } // caliper count
        public int FirstPcsInitCount2 { get; set; } // pull test count
        public int MidPcsInitCount { get; set; }
        public int MidPcsInitCount1 { get; set; }
        public int MidPcsInitCount2 { get; set; }
        public int LastPcsInitCount { get; set; }
        public int LastPcsInitCount1 { get; set; }
        public int LastPcsInitCount2 { get; set; }
        public int QuotaInitCount { get; set; }
        public int WireTwistInitCount { get; set; }
        public int WireTwistCycleCount { get; set; }
        public string TestJobNos { get; set; }
    }
}
