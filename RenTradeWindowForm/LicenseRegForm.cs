using System;
using FoxLearn.License;
using System.Windows.Forms;
using System.Diagnostics;

namespace RenTradeWindowForm
{
    public partial class LicenseRegForm : Form
    {
        const int ProductCode = 130;

        public LicenseRegForm()
        {
            InitializeComponent();
        }

        private void LicenseRegForm_Load(object sender, EventArgs e)
        {
            txtProdKey.ReadOnly = false;
            btnOk.Enabled = true;

            txtProdId.Text = ComputerInfo.GetComputerId();

            KeyManager km = new KeyManager(txtProdId.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                txtProdKey.ReadOnly = true;
                btnOk.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProdId.Text);
            string productKey = txtProdKey.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Lear Corporation";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }
                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    MessageBox.Show("You have been successfully registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (Process clsProcess in Process.GetProcesses())
                    {
                        //now we're going to see if any of the running processes
                        //match the currently running processes. Be sure to not add the .exe to the name you provide, 
                        //Remember, if you have the process running more than once,  say IE open 4 times the loop thr way it is now will close all 4,
                        //if you want it to just close the first one it finds then add a return; after the Kill
                        if (clsProcess.ProcessName.Contains("RenTradeWindowForm"))
                            clsProcess.Kill();
                    }

                    this.Close();
                }
            }
            else
                MessageBox.Show("Your product key is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
