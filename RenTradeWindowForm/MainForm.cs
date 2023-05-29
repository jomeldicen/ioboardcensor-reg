using Microsoft.Extensions.Options;
using RenTradeWindowService;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RenTradeWindowForm.Helper;
using System.Text.RegularExpressions;
using RenTradeWindowForm.Properties;
using System.Linq;
using FoxLearn.License;
using Microsoft.VisualBasic;

namespace RenTradeWindowForm
{
    public partial class MainForm : Form
    {
        private readonly IOptions<ServiceConfiguration> _options;
        private RegistryDriver registry;
        private LearJob learjob;
        private readonly int _threadDelay;

        private readonly string _machineName;
        private readonly string _machineType;
        private readonly string _machineClass;
        private readonly string _environmentMode;
        private readonly string _testPcsMsg;
        private readonly string _prodPcsMsg;
        private readonly string _quotaPcsMsg;
        private readonly string _endJobMsg;

        private readonly int _firstPcsInitCount;
        private readonly int _firstPcsInitCount1;
        private readonly int _firstPcsInitCount2;
        private readonly int _midPcsInitCount;
        private readonly int _midPcsInitCount1;
        private readonly int _midPcsInitCount2;
        private readonly int _lastPcsInitCount;
        private readonly int _lastPcsInitCount1;
        private readonly int _lastPcsInitCount2;
        private readonly int _quotaInitCount;
        private readonly int _wireTwistInitCount;

        private readonly bool _isLicense = false;

        private string Mode;

        ManualResetEvent _suspendEvent = new ManualResetEvent(true);

        public MainForm(IOptions<ServiceConfiguration> options)
        {
            InitializeComponent();

            //add this:
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            this.Text = null;

            _options = options;

            // License Check if valid or not
            this._isLicense = false;
            string prodID = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(prodID);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                this._isLicense = true;
            }

            // Registry init/configuration
            registry = new RegistryDriver(_options);

            // Delay sleep interval
            _threadDelay = _options.Value.ThreadDelay;

            // Confirmation Message
            _testPcsMsg = _options.Value.TestPcsMsg;
            _prodPcsMsg = _options.Value.ProdPcsMsg;
            _quotaPcsMsg = _options.Value.QuotaPcsMsg;
            _endJobMsg = _options.Value.EndJobMsg;
            _machineName = String.IsNullOrEmpty(_options.Value.MachineName) ? Environment.MachineName : _options.Value.MachineName;
            _machineType = String.IsNullOrEmpty(_options.Value.MachineType) ? "RG" : _options.Value.MachineType;
            _machineClass = String.IsNullOrEmpty(_options.Value.MachineClass) ? "" : _options.Value.MachineClass;
            _environmentMode = _options.Value.EnvironmentMode;

            // Count for Pull Test and Caliper
            _firstPcsInitCount = _options.Value.FirstPcsInitCount;
            _firstPcsInitCount1 = _options.Value.FirstPcsInitCount1;
            _firstPcsInitCount2 = _options.Value.FirstPcsInitCount2;
            _midPcsInitCount = _options.Value.MidPcsInitCount;
            _midPcsInitCount1 = _options.Value.MidPcsInitCount1;
            _midPcsInitCount2 = _options.Value.MidPcsInitCount2;
            _lastPcsInitCount = _options.Value.LastPcsInitCount;
            _lastPcsInitCount1 = _options.Value.LastPcsInitCount1;
            _lastPcsInitCount2 = _options.Value.LastPcsInitCount2;
            _quotaInitCount = _options.Value.QuotaInitCount;
            _wireTwistInitCount = _options.Value.WireTwistInitCount;

            // Windows Form Start Position
            this.StartPosition = FormStartPosition.Manual;
            foreach (var scrn in Screen.AllScreens)
            {
                if (scrn.Bounds.Contains(this.Location))
                {
                    this.Location = new Point(scrn.Bounds.Right - this.Width, scrn.Bounds.Top);
                    return;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            groupBox3.Text = (_machineType == "RG") ? "Counter" : "Cycle Count";
            groupBox4.Text = (_machineType == "RG") ? "Mid-Piece Count" : "Counter";
            groupBox3.Visible = (_machineType == "RG") ? true : false;
            groupBox4.Visible = (_machineType == "RG") ? true : false;
            lblRemarks.Visible = (_machineType == "RG") ? true : false;

            this.TopMost = true;
            this.TopLevel = true;

            backgroundWorker.RunWorkerAsync();
        }

        //private bool GetLearInformation()
        //{
        //    learjob = new LearJob(_options);

        //    lblLeadSet.Text = learjob.LeadSet;
        //    lblOrderNos.Text = learjob.OrderNumber;
        //    lblDailyTarget.Text = learjob.JobQty.ToString();
        //    lblBatchTarget.Text = learjob.BundleSize.ToString();
        //    lblMachineName.Text = _machineName;

        //    // Clear and Disable controls if no active job loaded
        //    if (String.IsNullOrEmpty(learjob.OrderNumber))
        //    {
        //        lblLeadSet.Text = "n/a";
        //        lblOrderNos.Text = "n/a";

        //        btnStart.Enabled = false;
        //        btnTerminate.Enabled = false;

        //        stStripMenuItem.Enabled = false;
        //        tmStripMenuItem.Enabled = false;
        //        ptStripMenuItem.Enabled = false;
        //        cmStripMenuItem.Enabled = false;

        //        lblStatus.Text = "No Active JO";
        //        lblRemarks.Text = "Ongoing checking...";

        //        return false;
        //    }

        //    return true;
        //}

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // get Job Information
            learjob = new LearJob(_options);

            lblLeadSet.Text = learjob.LeadSet;
            lblOrderNos.Text = learjob.OrderNumber;
            lblBatchTarget.Text = learjob.BundleSize.ToString();
            lblMachineName.Text = _machineName;

            // if Machine Class is MT
            if (_machineClass == "MT")
            {
                if (String.IsNullOrEmpty(registry.MaterialNos))
                    lblDailyTarget.Text = "...";
                else
                    lblDailyTarget.Text = (learjob.JobQty * Convert.ToInt16(registry.MaterialCount)).ToString();

                lblRefNos.Text = registry.MaterialNos;
                lblRefCount.Text = registry.MaterialCount;
            }
            else
            {
                lblDailyTarget.Text = learjob.JobQty.ToString();
                lblRefNos.Text = "n/a";
                lblRefCount.Text = "n/a";
            }

            // Clear and Disable controls if no active job loaded
            if (String.IsNullOrEmpty(learjob.OrderNumber))
            {
                lblLeadSet.Text = "n/a";
                lblOrderNos.Text = "n/a";

                btnStart.Enabled = false;
                btnTerminate.Enabled = false;

                stStripMenuItem.Enabled = false;
                tmStripMenuItem.Enabled = false;
                ptStripMenuItem.Enabled = false;
                cmStripMenuItem.Enabled = false;

                lblStatus.Text = "No Active JO";
                lblRemarks.Text = "Ongoing checking...";

                goto proceed;
            }

            Mode = "X0";

            // X0 - means device is working as expected
            if (registry.IOBoardStatus == Mode)
            {
                // means license is valid
                if(this._isLicense)
                {

                    // reel section
                    if (!registry.ReelStatus)
                    {
                        menuStrip.Enabled = false;

                        btnStart.Enabled = false;
                        btnTerminate.Enabled = false;

                        lblStatus.Text = (registry.IsProd) ? "Production" : "Test Mode";
                        lblRemarks.Text = "Reel has been deactivated! For BOM scanning";

                        grpInput.Enabled = true;
                        grpInput.Location = new Point(12, 238);
                        grpInput.Text = "BOM Scanning";
                        lblInputLabel.Text = "Please enter/scan BOM Value";
                        txtInput.Focus();

                        goto proceed;
                    }
                    //groupBox3.Text = (_machineType == "RG") ? "Counter" : "Cycle Count";

                    // Test Mode
                    if (!registry.IsProd)
                    {
                        menuStrip.Enabled = true;

                        // Waiting to abort job on the DIIT Software
                        if (registry.ProcessStage == "F2")
                        {
                            lblRemarks.Text = "Waiting to abort JO on DIIT";
                            goto proceed;
                        }

                        lblStatus.Text = (_machineType == "RG")? "Test Mode" : "Production";

                        // Intial First Step
                        if (registry.ProcessStage == "A1")
                        {
                            lblRemarks.Text = (registry.PedalStatus) ? "For item pc(s) execution: " + registry.ProcessCounter.ToString() + " out of " + ((_machineType == "RG")? _firstPcsInitCount.ToString() : lblDailyTarget.Text): "Click 'Start' button to proceed";

                            grpWireTwist.Enabled = false;
                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // validation if achieve test qty
                        if (registry.ProcessStage == "A2" && !registry.PedalStatus)
                        {
                            lblRemarks.Text = "For quantity validation";
                            lblQuota.Text = (_machineType == "WT") ? (Convert.ToInt16(lblQuota.Text) + 1).ToString() : lblQuota.Text;

                            DialogResult result = MessageBox.Show(this, _testPcsMsg, "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                if (_machineType == "RG")
                                    registry.WriteRegistry("processStage", "A4B");
                                else
                                {
                                    registry.WriteRegistry("cycleCounter", "0");
                                    registry.WriteRegistry("processStage", "A6B");

                                    grpWireTwist.Location = new Point(12, 238);
                                    grpWireTwist.Enabled = true;
                                    txtPitchDim.Text = "0";
                                    txtLeftDim.Text = "0";
                                    txtRightDim.Text = "0";
                                    lblWTRemarks.Text = "";
                                }
                            }
                            else
                            {     
                                registry.WriteRegistry("processStage", "A2C");
                                txtInput.Text = "0";
                                lblInputLabel.Text = "Please input additional quantity";
                                grpInput.Text = "Additional Piece";
                                lblInputRemarks.Text = "";
                            }

                            goto proceed;
                        }

                        // show input panel
                        if (registry.ProcessStage == "A2C" && !registry.PedalStatus)
                        {
                            lblRemarks.Text = "For additional quantity execution";

                            grpInput.Location = new Point(12, 238);
                            lblInputLabel.Text = "Please input additional quantity";
                            grpInput.Text = "Additional Piece";
                            grpInput.Enabled = true;

                            goto proceed;
                        }

                        if (registry.ProcessStage == "A2D" && !registry.PedalStatus)
                        {
                            lblRemarks.Text = "For additional quantity execution";

                            registry.WriteRegistry("pedalStatus", "True");
                            registry.WriteRegistry("processStage", "A1");

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Additional pc(s) for Test
                        if (registry.ProcessStage == "A3")
                        {
                            lblRemarks.Text = "For item pc(s) execution: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount.ToString();

                            goto proceed;
                        }

                        // Caliper Test message confirmation
                        // <-- disregard -->
                        if (registry.ProcessStage == "A4A" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount1.ToString();
                            DialogResult result = MessageBox.Show(this, "Please perform (" + _firstPcsInitCount.ToString() + ") CALIPER/PULL TEST input", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.OK)
                            {
                                registry.WriteRegistry("processStage", "A4B");
                            }

                            goto proceed;
                        }

                        // Caliper Test execution
                        if (registry.ProcessStage == "A4B" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount1.ToString();

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Caliper Test validation if no data within specific timeframe
                        if (registry.ProcessStage == "A4C" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount1.ToString();
                            DialogResult result = MessageBox.Show(this, "Do you want to continue CALIPER execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                                registry.WriteRegistry("processStage", "A4B");
                            else
                                MessageBox.Show(this, "Please continue CALIPER execution.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            goto proceed;
                        }

                        // Pull Test message confirmation
                        if (registry.ProcessStage == "A5A" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount2.ToString();
                            registry.WriteRegistry("processStage", "A5B");

                            goto proceed;
                        }

                        // Pull Test execution
                        if (registry.ProcessStage == "A5B" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount2.ToString();

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Pull Test validation if no data within specific timeframe
                        if (registry.ProcessStage == "A5C" && _machineType == "RG")
                        {
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount2.ToString();
                            DialogResult result = MessageBox.Show(this, "Do you want to continue PULL TEST execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                registry.WriteRegistry("processStage", "A5B");
                            }
                            else
                            {
                                MessageBox.Show(this, "Please continue PULL TEST execution", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            goto proceed;
                        }

                        // Wire Twist message confirmation
                        if (registry.ProcessStage == "A6A" && _machineType == "WT")
                        {
                            lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                            registry.WriteRegistry("processStage", "A6B");
                            grpWireTwist.Location = new Point(12, 238);

                            goto proceed;
                        } 

                        // Wire Twist execution
                        if (registry.ProcessStage == "A6B" && _machineType == "WT")
                        {
                            lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                            grpWireTwist.Enabled = true;
                            grpWireTwist.Location = new Point(12, 238);

                            goto proceed;
                        }

                        // Message Confirmation to back to execution
                        if (registry.ProcessStage == "A8")
                        {
                            lblRemarks.Text = "For " + ((_machineType == "RG") ? "PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _firstPcsInitCount.ToString() : "WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString());
                            //MessageBox.Show(this, "Production execution activated.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            registry.WriteRegistry("isProd", "True");
                            registry.WriteRegistry("processStage", "B1");
                            registry.WriteRegistry("testCounter", "0");

                            if (_machineType == "RG")
                                registry.WriteRegistry("processCounter", "0");
                            else
                                // in Wire Twist, continuation of counting
                                registry.WriteRegistry("processCounter", _firstPcsInitCount.ToString());

                            registry.WriteRegistry("pedalStatus", "True");

                            goto proceed;
                        }
                    }
                    else // Production
                    {
                        menuStrip.Enabled = true;

                        // Waiting to end job on the DIIT Software
                        if (registry.ProcessStage == "F1")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "Waiting to finish JO on DIIT: " + registry.ProcessCounter.ToString() + " out of " + learjob.JobQty.ToString();
                            goto proceed;
                        }

                        // Waiting to abort job on the DIIT Software
                        if (registry.ProcessStage == "F2")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "Waiting to abort JO on DIIT";
                            goto proceed;
                        }

                        //if ((registry.ProcessStage == "C6B" || registry.ProcessStage == "B6B") && _machineType == "WT")
                        //{
                        //    grpWireTwist.Enabled = false;
                        //    txtPitchDim.Text = "0";
                        //    txtLeftDim.Text = "0";
                        //    txtRightDim.Text = "0";
                        //    resetGroupInput();
                        //}

                        //*********************** Start Daily Quota Section ***********************//
                        // validation if achieve test qty
                        if(_midPcsInitCount != 0)
                        {
                            if (registry.ProcessStage == "C2" && !registry.PedalStatus)
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For mid pc(s) validation";

                                DialogResult result = MessageBox.Show(this, _quotaPcsMsg, "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                if (result == DialogResult.OK)
                                {
                                    lblRemarks.Text = "Execute additional (" + _midPcsInitCount.ToString() + ") item(s)";
                                    // activate pedal to continue
                                    registry.WriteRegistry("pedalStatus", "True");
                                    registry.WriteRegistry("processStage", "C3");
                                }

                                goto proceed;
                            }

                            // Additional pc(s) for Test
                            if (registry.ProcessStage == "C3")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For additional pc(s) execution: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount.ToString();

                                grpInput.Enabled = false;
                                resetGroupInput();

                                goto proceed;
                            }

                            // validation if achieve test qty
                            if (registry.ProcessStage == "C3B")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For additional pc(s) execution: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount.ToString();

                                DialogResult result = MessageBox.Show(this, _testPcsMsg, "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                {
                                    registry.WriteRegistry("pedalStatus", "False");

                                    if (_machineType == "RG")
                                        registry.WriteRegistry("processStage", "C4B");
                                    else
                                    {
                                        registry.WriteRegistry("processStage", "C6B");

                                        grpWireTwist.Location = new Point(12, 238);
                                        grpWireTwist.Enabled = true;
                                        txtPitchDim.Text = "0";
                                        txtLeftDim.Text = "0";
                                        txtRightDim.Text = "0";
                                        lblWTRemarks.Text = "";
                                    }

                                    registry.WriteRegistry("testCounter", "0");
                                }
                                else
                                {
                                    registry.WriteRegistry("processStage", "C3C");
                                    txtInput.Text = "0";
                                    lblInputRemarks.Text = "";
                                    lblInputLabel.Text = "Please input additional quantity";
                                    grpInput.Text = "Additional Piece";
                                    txtInput.Focus();
                                }

                                goto proceed;
                            }

                            // show input panel
                            if (registry.ProcessStage == "C3C" && !registry.PedalStatus)
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For additional quantity execution";

                                grpInput.Location = new Point(12, 238);
                                lblInputLabel.Text = "Please input additional quantity";
                                grpInput.Text = "Additional Piece";
                                grpInput.Enabled = true;

                                goto proceed;
                            }

                            if (registry.ProcessStage == "C3D" && !registry.PedalStatus)
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For additional quantity execution";

                                registry.WriteRegistry("pedalStatus", "True");
                                registry.WriteRegistry("processStage", "C3");

                                grpInput.Enabled = false;
                                resetGroupInput();

                                goto proceed;
                            }

                            // Caliper Test message confirmation
                            // <-- disregard -->
                            if (registry.ProcessStage == "C4A" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount1.ToString();

                                DialogResult result = MessageBox.Show(this, "Please perform (" + _midPcsInitCount.ToString() + ") CALIPER/PULL TEST input", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                if (result == DialogResult.OK)
                                {
                                    registry.WriteRegistry("processStage", "C4B");
                                }

                                goto proceed;
                            }

                            // Caliper Test execution
                            if (registry.ProcessStage == "C4B" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount1.ToString();

                                grpInput.Enabled = false;
                                resetGroupInput();

                                goto proceed;
                            }

                            // Caliper Test validation if no data within specific timeframe
                            if (registry.ProcessStage == "C4C" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount1.ToString();

                                DialogResult result = MessageBox.Show(this, "Do you want to continue CALIPER execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                    registry.WriteRegistry("processStage", "C4B");
                                else
                                    MessageBox.Show(this, "Please continue CALIPER execution", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                goto proceed;
                            }

                            // Pull Test message confirmation
                            if (registry.ProcessStage == "C5A" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount2.ToString();
                                registry.WriteRegistry("processStage", "C5B");

                                goto proceed;
                            }

                            // Pull Test execution
                            if (registry.ProcessStage == "C5B" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount2.ToString();

                                grpInput.Enabled = false;
                                resetGroupInput();

                                goto proceed;
                            }

                            // Pull Test validation if no data within specific timeframe
                            if (registry.ProcessStage == "C5C" && _machineType == "RG")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount2.ToString();

                                DialogResult result = MessageBox.Show(this, "Do you want to continue PULL TEST execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                    registry.WriteRegistry("processStage", "C5B");
                                else
                                    MessageBox.Show(this, "Please continue PULL TEST execution", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                goto proceed;
                            }

                            // Wire Twist message confirmation
                            if (registry.ProcessStage == "C6A" && _machineType == "WT")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                                registry.WriteRegistry("processStage", "C6B");
                                grpWireTwist.Location = new Point(12, 238);

                                goto proceed;
                            }

                            // Wire Twist execution
                            if (registry.ProcessStage == "C6B" && _machineType == "WT")
                            {
                                lblStatus.Text = "Mid Piece";
                                lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                                grpWireTwist.Enabled = true;
                                grpWireTwist.Location = new Point(12, 238);

                                goto proceed;
                            }

                            // Message Confirmation to back to execution
                            if (registry.ProcessStage == "C8")
                            {
                                lblStatus.Text = "Mid Piece"; 
                                lblRemarks.Text = "For " + ((_machineType == "RG") ? "PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _midPcsInitCount.ToString() : "WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString());

                                grpWireTwist.Enabled = false;
                                txtPitchDim.Text = "0";
                                txtLeftDim.Text = "0";
                                txtRightDim.Text = "0";
                                resetGroupInput();
                                //DialogResult result = MessageBox.Show(this, "Please continue production execution.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                //if (result == DialogResult.OK)
                                //{
                                registry.WriteRegistry("processStage", "B1");
                                registry.WriteRegistry("quotaCounter", "0");
                                registry.WriteRegistry("testCounter", "0");
                                //}

                                goto proceed;
                            }
                        }
                        //*********************** End Daily Quota Section ***********************//

                        //*********************** Start Production Section ***********************//
                        // Start Production
                        if (registry.ProcessStage == "B1" && !registry.PedalStatus)
                        {
                            // activate pedal to continue
                            lblStatus.Text = "Production";
                            if(_machineType == "RG")
                                lblRemarks.Text = "Pedal is in action: " + registry.ProcessCounter.ToString() + " out of " + lblDailyTarget.Text;
                            else
                            {
                                int i = _wireTwistInitCount * registry.ProcessCounter;
                                string counter = Convert.ToInt16(lblDailyTarget.Text) <= i ? lblDailyTarget.Text : i.ToString();
                                lblRemarks.Text = "Pedal is in action: " + counter + " out of " + lblDailyTarget.Text;
                            }

                            registry.WriteRegistry("pedalStatus", "True");

                            goto proceed;
                        }

                        if (registry.ProcessStage == "B1" && registry.PedalStatus)
                        {
                            // activate pedal to continue
                            lblStatus.Text = "Production";
                            if(_machineType == "RG")
                                lblRemarks.Text = "Pedal is in action: " + registry.ProcessCounter.ToString() + " out of " + lblDailyTarget.Text;
                            else
                            {
                                int i = _wireTwistInitCount * registry.ProcessCounter;
                                string counter = Convert.ToInt16(lblDailyTarget.Text) <= i ? lblDailyTarget.Text : i.ToString();
                                lblRemarks.Text = "Pedal is in action: " + counter + " out of " + lblDailyTarget.Text;
                            }


                            grpWireTwist.Enabled = false;
                            grpInput.Enabled = false;
                            txtPitchDim.Text = "0";
                            txtLeftDim.Text = "0";
                            txtRightDim.Text = "0";
                            txtInput.Text = "0";
                            lblWTRemarks.Text = "";
                            lblInputRemarks.Text = "";
                            resetGroupInput();

                            goto proceed;
                        }

                        // validation if achieve prod qty
                        if (registry.ProcessStage == "B2" && !registry.PedalStatus)
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For quantity validation";

                            if(_machineType == "RG")
                            {
                                DialogResult result = MessageBox.Show(this, _prodPcsMsg, "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                if (result == DialogResult.Yes)
                                {
                                    lblRemarks.Text = "Execute additional (" + _lastPcsInitCount.ToString() + ") item(s)";
                                    // activate pedal to continue
                                    registry.WriteRegistry("pedalStatus", "True");
                                    registry.WriteRegistry("processStage", "B3");
                                }
                                else
                                {
                                    registry.WriteRegistry("processStage", "B2C");
                                    lblInputRemarks.Text = "";
                                    lblInputLabel.Text = "Please input additional quantity";
                                    txtInput.Text = "0";
                                }
                            } 
                            else
                            {
                                int i = _wireTwistInitCount * registry.ProcessCounter;
                                string counter = Convert.ToInt16(lblDailyTarget.Text) <= i ? lblDailyTarget.Text : i.ToString();
                                lblRemarks.Text = "For last pc(s) execution: " + counter + " out of " + lblDailyTarget.Text;

                                // activate pedal to continue
                                registry.WriteRegistry("pedalStatus", "True");
                                registry.WriteRegistry("processStage", "B3");
                            }

                            goto proceed;
                        }

                        // show input panel
                        if (registry.ProcessStage == "B2C" && !registry.PedalStatus)
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For additional quantity execution";

                            grpInput.Enabled = true;
                            grpInput.Location = new Point(12, 238);
                            lblInputLabel.Text = "Please input additional quantity";
                            txtInput.Focus();

                            goto proceed;
                        }

                        if (registry.ProcessStage == "B2D" && !registry.PedalStatus)
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For additional quantity execution";

                            registry.WriteRegistry("pedalStatus", "True");
                            registry.WriteRegistry("processStage", "B1");
                            txtInput.Text = "0";
                            grpInput.Enabled = false;

                            goto proceed;
                        }

                        // Additional pc(s) for Test
                        if (registry.ProcessStage == "B3")
                        {
                            lblStatus.Text = "Production";

                            if(_machineType == "RG")
                                lblRemarks.Text = "For additional pc(s) execution: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount.ToString();
                            else
                            {
                                int i = _wireTwistInitCount * registry.ProcessCounter;
                                string counter = Convert.ToInt16(lblDailyTarget.Text) <= i ? lblDailyTarget.Text : i.ToString();
                                lblRemarks.Text = "For last pc(s) execution: " + counter + " out of " + lblDailyTarget.Text;
                                //lblQuota.Text = (Convert.ToInt16(lblQuota.Text) + 1).ToString();
                            }
                                

                            goto proceed;
                        }

                        // validation if achieve test qty
                        if (registry.ProcessStage == "B3B")
                        {
                            lblStatus.Text = "Production";
                            if (_machineType == "RG")
                                lblRemarks.Text = "For additional pc(s) execution: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount.ToString();
                            else
                            {
                                int i = _wireTwistInitCount * registry.ProcessCounter;
                                string counter = Convert.ToInt16(lblDailyTarget.Text) <= i ? lblDailyTarget.Text : i.ToString();
                                lblRemarks.Text = "For last pc(s) execution: " + counter + " out of " + lblDailyTarget.Text;
                            }

                            DialogResult result = MessageBox.Show(this, _testPcsMsg, "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                registry.WriteRegistry("pedalStatus", "False");

                                if (_machineType == "RG")
                                    registry.WriteRegistry("processStage", "B4B");
                                else
                                {
                                    registry.WriteRegistry("processStage", "B6B");

                                    grpWireTwist.Location = new Point(304, 238);
                                    grpWireTwist.Enabled = true;
                                    txtPitchDim.Text = "0";
                                    txtLeftDim.Text = "0";
                                    txtRightDim.Text = "0";
                                    lblWTRemarks.Text = "";
                                }

                                registry.WriteRegistry("testCounter", "0");
                            }
                            else
                            {
                                registry.WriteRegistry("processStage", "B3C");
                                lblInputRemarks.Text = "";
                                lblInputLabel.Text = "Please input additional quantity";
                                grpInput.Text = "Additional Piece";
                                txtInput.Text = "0";
                            }

                            goto proceed;
                        }

                        // show input panel
                        if (registry.ProcessStage == "B3C" && !registry.PedalStatus)
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For additional quantity execution";

                            grpInput.Location = new Point(12, 238);
                            lblInputLabel.Text = "Please input additional quantity";
                            grpInput.Text = "Additional Piece";
                            grpInput.Enabled = true;

                            goto proceed;
                        }

                        if (registry.ProcessStage == "B3D" && !registry.PedalStatus)
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For additional quantity execution";

                            registry.WriteRegistry("pedalStatus", "True");
                            registry.WriteRegistry("processStage", "B3");

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Caliper Test message confirmation
                        // <-- disregard -->
                        if (registry.ProcessStage == "B4A" && _machineType == "RG")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount1.ToString();
                            
                            DialogResult result = MessageBox.Show(this, "Please perform (" + _lastPcsInitCount.ToString() + ") CALIPER/PULL TEST input", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.OK)
                            {
                                registry.WriteRegistry("processStage", "B4B");
                            }

                            goto proceed;
                        }

                        // Caliper Test execution
                        if (registry.ProcessStage == "B4B" && _machineType == "RG")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount1.ToString();

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Caliper Test validation if no data within specific timeframe
                        if (registry.ProcessStage == "B4C" && _machineType == "RG")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For CALIPER input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount1.ToString();

                            DialogResult result = MessageBox.Show(this, "Do you want to continue CALIPER execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                                registry.WriteRegistry("processStage", "B4B");
                            else
                                MessageBox.Show(this, "Please continue CALIPER execution", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            goto proceed;
                        }

                        // Pull Test message confirmation
                        if (registry.ProcessStage == "B5A" && _machineType == "RG")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount2.ToString();

                            registry.WriteRegistry("processStage", "B5B");

                            goto proceed;
                        }

                        // Pull Test execution
                        if (registry.ProcessStage == "B5B" && _machineType == "RG")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount2.ToString();

                            grpInput.Enabled = false;
                            resetGroupInput();

                            goto proceed;
                        }

                        // Pull Test validation if no data within specific timeframe
                        if (registry.ProcessStage == "B5C")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount2.ToString();

                            DialogResult result = MessageBox.Show(this, "Do you want to continue PULL TEST execution?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                                registry.WriteRegistry("processStage", "B5B");
                            else
                                MessageBox.Show(this, "Please continue PULL TEST execution", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            goto proceed;
                        }

                        // Wire Twist message confirmation
                        if (registry.ProcessStage == "B6A" && _machineType == "WT")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                            registry.WriteRegistry("processStage", "B6B");
                            grpWireTwist.Location = new Point(12, 238);

                            goto proceed;
                        }

                        // Wire Twist execution
                        if (registry.ProcessStage == "B6B" && _machineType == "WT")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString();

                            grpWireTwist.Enabled = true;
                            grpWireTwist.Location = new Point(12, 238);

                            goto proceed;
                        }

                        // Finished Job Message
                        if (registry.ProcessStage == "B8")
                        {
                            lblStatus.Text = "Production";
                            lblRemarks.Text = "For " + ((_machineType == "RG") ? "PULL TEST input: " + registry.TestCounter.ToString() + " out of " + _lastPcsInitCount.ToString() : "WIRE TWIST input: " + registry.TestCounter.ToString() + " out of " + _wireTwistInitCount.ToString());

                            btnStart.Enabled = false;
                            btnTerminate.Enabled = false;

                            grpInput.Enabled = false;
                            grpWireTwist.Enabled = false;
                            txtPitchDim.Text = "0";
                            txtLeftDim.Text = "0";
                            txtRightDim.Text = "0";
                            txtInput.Text = "0";
                            resetGroupInput();

                        InputEndJob:
                            DialogResult result = MessageBox.Show(this, _endJobMsg, "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.Yes)
                            {
                                registry.WriteRegistry("processStage", "F1");

                                menuStrip.Enabled = true;

                                btnStart.Enabled = true;
                                btnTerminate.Enabled = false;

                                stStripMenuItem.Enabled = true;
                                tmStripMenuItem.Enabled = false;
                                ptStripMenuItem.Enabled = false;
                                cmStripMenuItem.Enabled = false;
                            }
                            else
                                goto InputEndJob;
                        }
                        //*********************** End Production Section ***********************//
                    }
                } 
                else
                {
                    lblRemarks.Text = "A valid software license could not be found";

                    btnStart.Enabled = false;
                    btnTerminate.Enabled = false;
                    menuStrip.Enabled = true;

                    stStripMenuItem.Enabled = false;
                    tmStripMenuItem.Enabled = false;
                    ptStripMenuItem.Enabled = false;
                    cmStripMenuItem.Enabled = false;
                    lcStripMenuItem.Enabled = true;
                    abStripMenuItem.Enabled = true;
                    rsStripMenuItem.Enabled = false;    
                }
            } 
            else
            {
                switch (registry.IOBoardStatus)
                {
                    case "X1":
                        MessageBox.Show(this, "IO Device is not properly set", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "X2":
                        MessageBox.Show(this, "Com Port is not properly set", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "X3":
                        MessageBox.Show(this, "IO Device error upon Initialize", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "X4":
                        MessageBox.Show(this, "IO Device port is closed", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "X5":
                        MessageBox.Show(this, "IO Device error upon Closing", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        break;
                }

                btnStart.Enabled = false;
                btnTerminate.Enabled = false;
                menuStrip.Enabled = false;
            }

        proceed:
            Thread.Sleep(_threadDelay);
            backgroundWorker.ReportProgress(0);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            registry.ReadRegistry();

            // X0 - meand device is working as expected
            if (registry.IOBoardStatus == Mode)
            {
                // valid license
                if(this._isLicense)
                {
                    if (String.IsNullOrEmpty(learjob.OrderNumber))
                    {
                        btnStart.Enabled = false;
                        btnTerminate.Enabled = false;

                        stStripMenuItem.Enabled = false;
                        tmStripMenuItem.Enabled = false;
                        ptStripMenuItem.Enabled = false;
                        cmStripMenuItem.Enabled = false;

                        tsStatusLabel.Text = "Looking for active JO";
                        tsStatusLabel.ToolTipText = "No Active JO";
                        tsStatusLabel.Image = (Image)(Resources.ResourceManager.GetObject("red"));
                    }
                    else
                    {
                        stStripMenuItem.Enabled = false;
                        tmStripMenuItem.Enabled = true;
                        ptStripMenuItem.Enabled = false;
                        cmStripMenuItem.Enabled = false;

                        if (!registry.PedalStatus && !registry.IsProd && registry.ProcessStage == "A1" && registry.ProcessCounter == 0)
                        {
                            btnStart.Enabled = true;
                            btnTerminate.Enabled = false;
                        }
                        else
                        {
                            btnStart.Enabled = false;
                            btnTerminate.Enabled = true;
                        }

                        if (registry.ProcessStage == "A1")
                        {
                            stStripMenuItem.Enabled = btnStart.Enabled;
                            tmStripMenuItem.Enabled = !btnStart.Enabled;
                            ptStripMenuItem.Enabled = false;
                            cmStripMenuItem.Enabled = false;
                        }

                        // 2 - validation if achieve test qty, 3 - Pull Test message confirmation, 5 - Pull Test message confirmation
                        string[] stageArray1 = { "A2", "A3", "A5A", "A8",
                                             "B1", "B2", "B3", "B5A", "B8",
                                             "C2", "C3", "C5A", "C8" };
                        if (stageArray1.Contains(registry.ProcessStage))
                        {
                            stStripMenuItem.Enabled = false;
                            tmStripMenuItem.Enabled = true;
                            ptStripMenuItem.Enabled = false;
                            cmStripMenuItem.Enabled = false;
                        }

                        // 4 - Caliper Test
                        string[] stageArray2 = { "A4A", "B4A", "C4A" };
                        if (stageArray2.Contains(registry.ProcessStage))
                        {
                            stStripMenuItem.Enabled = false;
                            tmStripMenuItem.Enabled = true;
                            ptStripMenuItem.Enabled = true;
                            cmStripMenuItem.Enabled = true;
                        }

                        // 4 - Caliper Test Execution
                        string[] stageArray4 = { "A4B", "B4B", "C4B", "A4C", "B4C", "C4C" };
                        if (stageArray4.Contains(registry.ProcessStage))
                        {
                            stStripMenuItem.Enabled = false;
                            tmStripMenuItem.Enabled = true;
                            ptStripMenuItem.Enabled = true;
                            cmStripMenuItem.Enabled = false;
                        }

                        // 6 - Pull Test execution, 7 - Pull Test validation if no data within specific timeframe, Message Confirmation to back to execution
                        string[] stageArray3 = { "A5B", "A5C",
                                             "B5B", "B5C",
                                             "C5B", "C5C" };
                        if (stageArray3.Contains(registry.ProcessStage))
                        {
                            stStripMenuItem.Enabled = false;
                            tmStripMenuItem.Enabled = true;
                            ptStripMenuItem.Enabled = true;
                            cmStripMenuItem.Enabled = false;
                        }

                        tsStatusLabel.Text = (registry.PedalStatus) ? "Pedal is active" : "Pedal is disabled";
                        tsStatusLabel.ToolTipText = "Last Activity";
                        tsStatusLabel.Image = (Image)(Resources.ResourceManager.GetObject((registry.PedalStatus) ? "green" : "red"));

                        // Change the color code of the label and remarks
                        if (registry.ReelStatus)
                        {
                            if (registry.IsProd)
                            {
                                if(registry.ProcessStage[0] != 'C')
                                {
                                    lblStatus.ForeColor = Color.ForestGreen;
                                    lblRemarks.ForeColor = Color.ForestGreen;
                                } else
                                {
                                    lblStatus.ForeColor = Color.OrangeRed;
                                    lblRemarks.ForeColor = Color.OrangeRed;
                                }
                            }
                            else
                            {
                                lblStatus.ForeColor = (_machineType == "RG")? Color.Black : Color.ForestGreen;
                                lblRemarks.ForeColor = (_machineType == "RG")? Color.Black : Color.ForestGreen;
                            }
                        } else
                        {
                            lblStatus.ForeColor = Color.Red;
                            lblRemarks.ForeColor = Color.Red;
                        }
                    }                    
                } 
                else
                {
                    tsStatusLabel.Text = "Unregistered Software";
                    tsStatusLabel.ToolTipText = "Invalid License";
                    tsStatusLabel.Image = (Image)(Resources.ResourceManager.GetObject("red"));
                }
            } 
            else
            {
                if(String.IsNullOrEmpty(learjob.OrderNumber) && registry.IOBoardStatus == "X0")
                {
                    tsStatusLabel.Text = "Looking for active JO";
                    tsStatusLabel.ToolTipText = "No Active JO";
                } 
                else
                {
                    tsStatusLabel.Text = "IO Device error";
                    tsStatusLabel.ToolTipText = "Last Activity";
                }

                tsStatusLabel.Image = (Image)(Resources.ResourceManager.GetObject("red"));
            }

            lblCounter.Text = (registry.IsProd) ? registry.ProcessCounter.ToString() : "0";
            if (_machineType == "RG")
                lblQuota.Text = (_midPcsInitCount != 0) ? registry.QuotaCounter.ToString() : "N/A";
            else
                lblQuota.Text = registry.CycleCounter.ToString();

            tsDateTimeLabel.Text = System.DateTime.Today.ToLongDateString();
            tsDateTimeLabel.ToolTipText = "Last Activity";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void resetGroupInput()
        {
            // Wire Twist Group
            lblWTRemarks.Text = "";
            grpWireTwist.Location = new Point(330, 279);
            //txtPitchDim.Text = "0";
            //txtLeftDim.Text = "0";
            //txtRightDim.Text = "0";

            // Input Group
            lblInputRemarks.Text = "";
            grpInput.Location = new Point(330, 136);
            lblInputLabel.Text = "Please input additional quantity";
            grpInput.Text = "Additional Piece";
            //txtInput.Text = "0";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_machineClass == "MT")
            {
                grpRef.Location = new Point(12, 435);
                grpRef.Enabled = true;
            }

            grpInput.Enabled = false;
            grpWireTwist.Enabled = false;
            resetGroupInput();

            btnStart.Enabled = false;
            btnTerminate.Enabled = true;

            stStripMenuItem.Enabled = false;
            tmStripMenuItem.Enabled = true;
            ptStripMenuItem.Enabled = false;
            cmStripMenuItem.Enabled = false;

            if(_machineClass == "MT")
            {
            } 
            else
            {
                registry.WriteRegistry("pedalStatus", "True");

            }
            //if(this._machineType == "WT")
            //{
            //    registry.WriteRegistry("processStage", "B1");
            //    registry.WriteRegistry("isProd", "True");
            //}
        }

        private void btnTerminate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to terminate this job?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                grpInput.Enabled = false;
                grpWireTwist.Enabled = false;
                resetGroupInput();

                stStripMenuItem.Enabled = true;
                tmStripMenuItem.Enabled = false;
                ptStripMenuItem.Enabled = false;
                cmStripMenuItem.Enabled = false;

                btnStart.Enabled = true;
                btnTerminate.Enabled = false;

                registry.WriteRegistry("pedalStatus", "False");
                registry.WriteRegistry("processStage", "F2");
            } 
        }

        private void stStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStart_Click(sender, e);
        }

        private void tmStripMenuItem_Click(object sender, EventArgs e)
        {
            btnTerminate_Click(sender, e);
        }

        private void cmStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = "";
            DialogBox.ShowInputDialogBox(ref input, "Please input caliper results.", "Message Confirmation", 300, 110);

            Regex regex = new Regex(@"[^0-9^.^\;^\s*]");
            MatchCollection matches = regex.Matches(input);

            if (!String.IsNullOrEmpty(input) && matches.Count > 0)
            {
                switch (registry.ProcessStage)
                {
                    case "A4A":
                        registry.WriteRegistry("processStage", "A5A");
                        break;
                    case "B4A":
                        registry.WriteRegistry("processStage", "B5A");
                        break;
                    case "C4A":
                        registry.WriteRegistry("processStage", "C5A");
                        break;
                    default:
                        break;
                }

                // log results
                registry.TextLogger(learjob.OrderNumber, DateTimeOffset.Now + " - [" + learjob.OrderNumber + ":" + learjob.LeadSet + "] - Caliper Dimension: " + input);
            }
            else
            {
                MessageBox.Show("Please input a valid value", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ptStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip.Enabled = false;

            DialogResult result = MessageBox.Show("Add additional test pc(s)?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                string input = "0";
                DialogBox.ShowInputDialogBox(ref input, "Please input additional quantity.", "Message Confirmation", 300, 110);

                if (int.TryParse(input, out int value) && !String.IsNullOrEmpty(input))
                {
                    // not greater than 9
                    if (input.Length > 1)
                    {
                        MessageBox.Show("Maximum quantity is not greater than 9.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // not less than or equal to 0
                    if (input.Length <= 0)
                    {
                        MessageBox.Show("Quantity should not be less than or equal to 0.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if(value == 0)
                    {
                        MessageBox.Show("Quantity should not be equal to 0.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Test Mode
                    if (!registry.IsProd)
                    {
                        if (value <= registry.ProcessCounter)
                        {
                            lblRemarks.Text = "Pedal is in action";
                            menuStrip.Enabled = true;

                            var counter1 = registry.ProcessCounter - value;
                            registry.WriteRegistry("processCounter", counter1.ToString());

                            //if (value >= registry.TestCounter)
                            //    registry.WriteRegistry("testCounter", "0");
                            //else
                            //{
                            //    counter1 = registry.TestCounter - value;
                            //    registry.WriteRegistry("testCounter", counter1.ToString());
                            //}

                            registry.WriteRegistry("testCounter", "0");
                            // activate pedal to continue
                            registry.WriteRegistry("pedalStatus", "True");

                            string[] stageArray1 = { "A4A", "A4B", "A4C", "A5A", "A5B", "A5C", "A6A", "A6B", "A8" };
                            if (stageArray1.Contains(registry.ProcessStage))
                            {
                                registry.WriteRegistry("processStage", "A1");
                            }

                            string[] stageArray2 = { "B4A", "B4B", "B4C", "B5A", "B5B", "B5C", "B6A", "B6B", "B8" };
                            if (stageArray2.Contains(registry.ProcessStage))
                            {
                                registry.WriteRegistry("processStage", "B1");
                            }

                            string[] stageArray3 = { "C4A", "C4B", "C4C", "C5A", "C5B", "C5C", "C6A", "C6B", "C8" };
                            if (stageArray3.Contains(registry.ProcessStage))
                            {
                                registry.WriteRegistry("processStage", "C2");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Quantity should not greater than the accumulated count", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        string[] stageArray0 = { "B4B", "B5B", "B6B", "C4B", "C5B", "C6B" };
                        string[] stageArray1 = { "B4B", "B5B", "B6B" };
                        string[] stageArray2 = { "C4B", "C5B", "C6B" };

                        if (stageArray0.Contains(registry.ProcessStage))
                        {
                            int initCount = 0;

                            if (stageArray1.Contains(registry.ProcessStage))
                                initCount = _lastPcsInitCount;

                            if (stageArray2.Contains(registry.ProcessStage))
                                initCount = _midPcsInitCount;

                            if (value <= initCount)
                            {
                                lblRemarks.Text = "Pedal is in action";
                                menuStrip.Enabled = true;

                                var counter1 = initCount - value;
                                registry.WriteRegistry("testCounter", counter1.ToString());

                                // activate pedal to continue
                                registry.WriteRegistry("pedalStatus", "True");

                                if (stageArray1.Contains(registry.ProcessStage))
                                    registry.WriteRegistry("processStage", "B3");

                                if (stageArray2.Contains(registry.ProcessStage))
                                    registry.WriteRegistry("processStage", "C3");
                            }
                            else
                            {
                                MessageBox.Show("Quantity should not greater than the accumulated count", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        } 
                    }
                }
                else
                {
                    MessageBox.Show("Please input a valid value", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void rsStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Restarting PAO Interface I/O Board.", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            registry.WriteRegistry("ioBoardStatus", "R0");
        }

        private void abStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void lcStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseRegForm licRegForm = new LicenseRegForm();
            licRegForm.ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Reel Status
            if(!registry.ReelStatus)
            {
                string input = txtInput.Text;

                if (!String.IsNullOrEmpty(input) && learjob.IsBOMExist(input))
                {
                    registry.WriteRegistry("reelStatus", "True");
                    registry.WriteRegistry("pedalStatus", "True");

                    grpInput.Enabled = false;
                    lblInputRemarks.Text = "";

                    // log results
                    registry.TextLogger(learjob.OrderNumber, DateTimeOffset.Now + " - Reel Sensor Serial: " + input);
                    resetGroupInput();
                }
                else
                {
                    lblInputRemarks.Text = "BOM value does not exist!";
                    txtInput.Text = "";
                    return;
                }
            } 
            else
            {
                grpInput.Text = "Additional Piece";

                // First Piece
                if (registry.ProcessStage == "A2C")
                {
                    lblInputLabel.Text = "Please input additional quantity";
                    string input = txtInput.Text;

                    if (int.TryParse(input, out int value) && !String.IsNullOrEmpty(input))
                    {
                        // not greater than 1
                        if (value > 1)
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than 1.";
                            return;
                        }

                        // not less than or equal to 0
                        if (input.Length <= 0)
                        {
                            lblInputRemarks.Text = "Quantity should not be less than or equal to 0.";
                            return;
                        }

                        if (value <= registry.ProcessCounter && value != 0)
                        {
                            var counter1 = registry.ProcessCounter - value;
                            registry.WriteRegistry("processCounter", counter1.ToString());
                            registry.WriteRegistry("cycleCounter", "0");

                            // activate pedal to continue
                            txtInput.Text = "0";
                            registry.WriteRegistry("processStage", "A2D");
                        }
                        else
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than the accumulated count";
                            return;
                        }
                    }
                    else
                    {
                        lblInputRemarks.Text = "Please input a valid value";
                        return;
                    }
                }

                // Last Piece
                if (registry.ProcessStage == "B2C" && _machineType == "RG")
                {
                    lblInputLabel.Text = "Please input additional quantity";
                    string input = txtInput.Text;

                    if (int.TryParse(input, out int value) && !String.IsNullOrEmpty(input))
                    {
                        // not greater than 1
                        if (value > 1)
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than 1.";
                            return;
                        }

                        // not less than or equal to 0
                        if (value <= 0)
                        {
                            lblInputRemarks.Text = "Quantity should not be less than or equal to 0.";
                            return;
                        }

                        if (value <= registry.ProcessCounter && value != 0)
                        {
                            var counter1 = registry.ProcessCounter - value;
                            registry.WriteRegistry("processCounter", counter1.ToString());
                            registry.WriteRegistry("cycleCounter", "0");

                            if (registry.QuotaCounter >= value)
                            {
                                counter1 = registry.QuotaCounter - value;
                                registry.WriteRegistry("quotaCounter", counter1.ToString());
                            }
                            // activate pedal to continue
                            txtInput.Text = "0";
                            registry.WriteRegistry("processStage", "B2D");
                            return;
                        }
                        else
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than the accumulated count";
                            return;
                        }                            
                    }
                    else
                    {
                        lblInputRemarks.Text = "Please input a valid value";
                        return;
                    }
                }

                if (registry.ProcessStage == "B3C")
                {
                    lblInputLabel.Text = "Please input additional quantity";
                    string input = txtInput.Text;

                    if (int.TryParse(input, out int value) && !String.IsNullOrEmpty(input))
                    {
                        // not greater than 1
                        if (value > 1)
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than 1.";
                            return;
                        }

                        // not less than or equal to 0
                        if (input.Length <= 0)
                        {
                            lblInputRemarks.Text = "Quantity should not be less than or equal to 0.";
                            return;
                        }

                        // if machine type is regular then perform normal process
                        if(_machineType == "RG")
                        {
                            if (value <= registry.TestCounter && value != 0)
                            {
                                var counter1 = registry.TestCounter - value;
                                registry.WriteRegistry("testCounter", counter1.ToString());
                            registry.WriteRegistry("cycleCounter", "0");

                                // activate pedal to continue
                                txtInput.Text = "0";
                                registry.WriteRegistry("processStage", "B3D");
                            }
                            else
                            {
                                lblInputRemarks.Text = "Quantity should not be greater than the accumulated count";
                                return;
                            }
                        } 
                        else
                        {
                            if (value <= registry.ProcessCounter && value != 0)
                            {
                                var counter1 = registry.ProcessCounter - value;
                                registry.WriteRegistry("processCounter", counter1.ToString());
                                registry.WriteRegistry("cycleCounter", "0");

                                if (registry.QuotaCounter >= value)
                                {
                                    counter1 = registry.QuotaCounter - value;
                                    registry.WriteRegistry("quotaCounter", counter1.ToString());
                                }

                                if (value <= registry.TestCounter && value != 0)
                                {
                                    var counter0 = registry.TestCounter - value;
                                    registry.WriteRegistry("testCounter", counter0.ToString());
                                }
                                else
                                    registry.WriteRegistry("testCounter", "0");

                                // activate pedal to continue
                                txtInput.Text = "0";
                                registry.WriteRegistry("processStage", "B2D");
                                return;
                            }
                            else
                            {
                                lblInputRemarks.Text = "Quantity should not be greater than the accumulated count";
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblInputRemarks.Text = "Please input a valid value";
                        return;
                    }
                }

                // Mid Piece
                if (registry.ProcessStage == "C3C")
                {
                    lblInputLabel.Text = "Please input additional quantity";
                    string input = txtInput.Text;

                    if (int.TryParse(input, out int value) && !String.IsNullOrEmpty(input))
                    {
                        // not greater than 1
                        if (value > 1)
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than 1.";
                            return;
                        }

                        // not less than or equal to 0
                        if (input.Length <= 0)
                        {
                            lblInputRemarks.Text = "Quantity should not be less than or equal to 0.";
                            return;
                        }

                        if (value <= registry.TestCounter && value != 0)
                        {
                            var counter1 = registry.TestCounter - value;
                            registry.WriteRegistry("testCounter", counter1.ToString());
                            registry.WriteRegistry("cycleCounter", "0");

                            // activate pedal to continue
                            txtInput.Text = "0";
                            registry.WriteRegistry("processStage", "C3D");
                        }
                        else
                        {
                            lblInputRemarks.Text = "Quantity should not be greater than the accumulated count";
                            return;
                        }
                    }
                    else
                    {
                        lblInputRemarks.Text = "Please input a valid value";
                        return;
                    }
                }
            }            
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, new EventArgs());
            }
        }

        private void btnWireTwist_Click(object sender, EventArgs e)
        {
            // Wire Twist Input
            string[] stageArray1 = { "A6B", "B6B", "C6B" };

            if (stageArray1.Contains(registry.ProcessStage) && _machineType == "WT")
            {
                string input1 = txtPitchDim.Text;
                string input2 = txtLeftDim.Text;
                string input3 = txtRightDim.Text;

                //if (!String.IsNullOrEmpty(txtInput.Text) && matches.Count > 0)
                if ((decimal.TryParse(input1, out decimal p) && !String.IsNullOrEmpty(input1)) &&
                    (decimal.TryParse(input2, out decimal l) && !String.IsNullOrEmpty(input2)) &&
                    (decimal.TryParse(input3, out decimal r) && !String.IsNullOrEmpty(input3)) )
                {
                    if(p == 0 || l == 0 || r == 0)
                    {
                        lblWTRemarks.Text = "Value(s) should not be less than or equal to 0.";
                        return;
                    }

                    int value = _wireTwistInitCount;

                    var counter1 = registry.TestCounter + 1;
                    if(counter1 <= value && (p != 0 || l != 0 || r!=0))
                    {
                        registry.WriteRegistry("testCounter", counter1.ToString());

                        // log results
                        //grpWireTwist.Enabled = false; 
                        txtPitchDim.Text = "0";
                        txtLeftDim.Text = "0";
                        txtRightDim.Text = "0";
                        //resetGroupInput();
                        registry.TextLogger(learjob.OrderNumber, DateTimeOffset.Now + " - [" + p + ":::" + l + ":::" + r + ":::" + learjob.OrderNumber + ":::" + learjob.LeadSet + "]");
                    }
                    else
                    {
                        lblWTRemarks.Text = "Quantity should not be greater than the accumulated count";
                        return;
                    }
                }
                else
                {
                    lblWTRemarks.Text = "Please input a valid decimal value";
                    return;
                }
            }
        }

        private void txtPitchDim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnWireTwist_Click(this, new EventArgs());
            }
        }

        private void txtLeftDim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnWireTwist_Click(this, new EventArgs());
            }
        }

        private void txtRightDim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnWireTwist_Click(this, new EventArgs());
            }
        }

        private void btnRefOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtRef.Text))
            {
                stStripMenuItem.Enabled = true;
                tmStripMenuItem.Enabled = false;
                ptStripMenuItem.Enabled = false;
                cmStripMenuItem.Enabled = false;

                btnStart.Enabled = true;
                btnTerminate.Enabled = false;

                // Reference Group
                lblRefRemarks.Text = "";
                txtRef.Text = "";
                grpRef.Location = new Point(330, 15);
                registry.WriteRegistry("pedalStatus", "False");
                registry.WriteRegistry("processStage", "A1");

                MessageBox.Show("Please input valid material number!", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else 
            {
                DialogResult result = MessageBox.Show(this, "You are entering: " + txtRef.Text + ". Do you want to proceed?", "Message Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    int sBOMCount = learjob.BOMCount(txtRef.Text);

                    if (sBOMCount == 0)
                    {
                        MessageBox.Show("Material number not found!", "Message Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        registry.WriteRegistry("materialNos", txtRef.Text);
                        registry.WriteRegistry("materialCount", sBOMCount.ToString());

                        lblRefRemarks.Text = "";
                        txtRef.Text = "";
                        grpRef.Location = new Point(330, 15);

                        resetGroupInput();
                        registry.WriteRegistry("pedalStatus", "True");
                        registry.ReadRegistry();
                        btnStart.Enabled = false;
                        btnTerminate.Enabled = true;

                        stStripMenuItem.Enabled = false;
                        tmStripMenuItem.Enabled = true;
                        ptStripMenuItem.Enabled = false;
                        cmStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    // Reference Group
                    lblRefRemarks.Text = "";
                    txtRef.Text = "";
                    grpRef.Location = new Point(330, 15);
                    registry.WriteRegistry("pedalStatus", "False");
                }
            }            
        }

        private void btnRefCancel_Click(object sender, EventArgs e)
        {
            // Reference Group
            lblRefRemarks.Text = "";
            txtRef.Text = "";
            grpRef.Location = new Point(330, 15);
            registry.WriteRegistry("pedalStatus", "False");
            registry.WriteRegistry("processStage", "A1");
        }
    }
}
