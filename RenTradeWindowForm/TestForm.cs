using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RenTradeWindowForm
{
    public partial class TestForm : Form
    {
        private int _counter = 0;
        private bool _isVisible = false;

        public TestForm()
        {
            InitializeComponent();
        }

        ManualResetEvent _suspendEvent = new ManualResetEvent(true);


        private void TestForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //_counter++;
            //if(_counter % 10 == 0)
            //{
            //    _suspendEvent.WaitOne();
            //    this._isVisible = true;

            //    if (panel1.InvokeRequired)
            //    {
            //        panel1.Invoke(new MethodInvoker(delegate { panel1.Visible = true; }));
            //    }
            //}

            //if (!this._isVisible)
            //{
                Thread.Sleep(100);
                backgroundWorker.ReportProgress(0);
            //}
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblCounter.Text = _counter.ToString();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            //MessageBox.Show("Task Completed");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _counter = 0;
            _isVisible = false;
            // panel1.Visible = _isVisible;

            Regex regex = new Regex(@"[^0-9^.^\;^\s*]");
            MatchCollection matches = regex.Matches(textBox1.Text);

            if (!String.IsNullOrEmpty(textBox1.Text) && matches.Count > 0)
            {
                var counter1 = _counter + 1;
                lblCounter.Text = counter1.ToString();

                // log results
                //registry.TextLogger(learjob.OrderNumber, DateTimeOffset.Now + " - [" + learjob.OrderNumber + ":" + learjob.LeadSet + "] - Caliper Dimension: " + input);
            }
            else
                label1.Text = "Please input a valid value";
        }



        private void textbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show(textBox1.Text);
                //btnOk_Click(this, new EventArgs());
            }
        }
    }
}
