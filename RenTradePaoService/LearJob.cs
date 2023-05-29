using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenTradeWindowService
{
    public class LearJob
    {
        private LearCebuPAO_API.LearCebuPAO_API OLearCebuPAOapi = new LearCebuPAO_API.LearCebuPAO_API();
        private RegistryDriver registry;

        public string JobInfo { get; private set; }
        private int BOMcount { get; set; }
        public string OrderNumber { get; private set; }
        public string LeadSet { get; private set; }
        public int JobQty { get; private set; }
        public int BundleSize { get; private set; }

        private readonly string _environmentMode;
        private readonly string _machineName;
        private readonly string _testJobNos;

        public LearJob(IOptions<ServiceConfiguration> options)
        {
            // Registry init/configuration
            registry = new RegistryDriver(options);

            _environmentMode = options.Value.EnvironmentMode;
            _machineName = String.IsNullOrEmpty(options.Value.MachineName) ? Environment.MachineName : options.Value.MachineName;
            _testJobNos = options.Value.TestJobNos;

            GetJobStarted();
        }

        public void GetJobStarted()
        {
            try
            {
                JobInfo = _testJobNos;

                // Production Mode
                if (_environmentMode == "PRD")
                {
                    JobInfo = OLearCebuPAOapi.GetJobStarted(_machineName);
                    //BOMcount = OLearCebuPAOapi.GetBOMcount()
                }

                string[] stringSeparators = new string[] { ":::" };
                if (!String.IsNullOrEmpty(JobInfo) && JobInfo != "ErrorDB")
                {
                    string[] tokens = JobInfo.Split(stringSeparators, StringSplitOptions.None);

                    OrderNumber = tokens[0].Trim().ToString();
                    LeadSet = tokens[1].Trim().ToString();
                    JobQty = Convert.ToInt16(tokens[2]);
                    BundleSize = Convert.ToInt16(tokens[3]);

                    registry.WriteRegistry("jobInfo", JobInfo);

                    // if current order nos is different on old order nos
                    registry.ReadRegistry();
                    if (!String.IsNullOrWhiteSpace(registry.OldOrderNos))
                    {
                        if (!OrderNumber.Equals(registry.OldOrderNos) && (registry.ProcessStage == "B8" || registry.ProcessStage == "A1" || registry.ProcessStage == "F1"))
                        {
                            registry.WriteRegistry("quotaCounter", "0");
                            registry.ReadRegistry();
                        }
                    }
                } 
                else if (JobInfo == "ErrorDB")
                {
                    JobInfo = registry.JobInfo;

                    string[] tokens = JobInfo.Split(stringSeparators, StringSplitOptions.None);

                    OrderNumber = tokens[0].Trim().ToString();
                    LeadSet = tokens[1].Trim().ToString();
                    JobQty = Convert.ToInt16(tokens[2]);
                    BundleSize = Convert.ToInt16(tokens[3]);

                    registry.ReadRegistry();
                }
                else
                {
                    OrderNumber = "";
                    LeadSet = "";
                    JobQty = 0;
                    BundleSize = 0;

                    registry.ResetRegistry();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger("Get JobStarted Function: " + ex.Message);
            }
        }

        public int BOMCount(string BOMValue)
        {
            string BOMValueTrimP = BOMValue;
            string sBOMitems = "";
            int sBOMCount = 0;

            // Production Environment Mode
            if (_environmentMode == "PRD")
            {
                if (BOMValue.Length > 0)
                    BOMValueTrimP = (BOMValue[0] == 'P') ? BOMValue.Remove(0, 1) : BOMValue;

                sBOMCount = OLearCebuPAOapi.GetBOMcount(LeadSet, BOMValueTrimP, ref sBOMitems);
            }

            return sBOMCount;
        }

        public bool IsBOMExist(string BOMValue)
        {
            string BOMValueTrimP = BOMValue;
            string sBOMitems = "";
            bool isBOMexist = false;
            string[] stringSeparators = new string[] { ":::" };

            // Production Environment Mode
            if (_environmentMode == "PRD")
            {
                if (BOMValue.Length > 0)
                    BOMValueTrimP = (BOMValue[0] == 'P') ? BOMValue.Remove(0, 1) : BOMValue;
                isBOMexist = OLearCebuPAOapi.DoesBOMexist(LeadSet, BOMValueTrimP, ref sBOMitems);
            }

            registry.TextLogger(this.OrderNumber, DateTimeOffset.Now + " - BOMValue: " + BOMValue + ", BOMExist: " + isBOMexist + ", sBOMItems: " + sBOMitems + " EnvironmentMode: " + _environmentMode);
            string[] tokens = sBOMitems.Split(stringSeparators, StringSplitOptions.None);

            if (isBOMexist || tokens.Contains(BOMValueTrimP))
                return true;

            return false;
        }

        public void JobFinished()
        {
            bool isPaoJobFinished = true;

            // Production Mode
            if (_environmentMode == "PRD")
            {
                isPaoJobFinished = OLearCebuPAOapi.IsPAOJobFinished(OrderNumber, _machineName);
                registry.WriteRegistry("oldOrderNos", OrderNumber.ToString());
            }

            if (isPaoJobFinished)
            {
                registry.ReadRegistry();
                //registry.ErrorLogger(_environmentMode + "= Machine: " + _machineName + " Order Nos: " + OrderNumber.ToString() + " IsPaoJobFinish: " + isPaoJobFinished.ToString());

                registry.ResetRegistry();
            }
        }
    }
}
