using System;
using FoxLearn.License;
using System.Windows.Forms;

namespace RenTradeWindowForm
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            lblProdId.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProdId.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    lblProdKey.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                        lblLicenseType.Text = string.Format("{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                    else
                        lblLicenseType.Text = "Full";
                }
            }
            else
            {
                lblProdId.Text = "?";
                lblProdKey.Text = "?";
                lblLicenseType.Text = "?";
            }
        }
    }
}
