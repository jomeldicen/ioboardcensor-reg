
namespace RenTradeWindowForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.actionStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ptStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.abStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lcStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDateTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBatchTarget = new System.Windows.Forms.Label();
            this.lblDailyTarget = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOrderNos = new System.Windows.Forms.Label();
            this.lblLeadSet = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblCounter = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblQuota = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblInputRemarks = new System.Windows.Forms.Label();
            this.lblInputLabel = new System.Windows.Forms.Label();
            this.grpWireTwist = new System.Windows.Forms.GroupBox();
            this.txtRightDim = new System.Windows.Forms.TextBox();
            this.txtLeftDim = new System.Windows.Forms.TextBox();
            this.txtPitchDim = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnWireTwist = new System.Windows.Forms.Button();
            this.lblWTRemarks = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTerminate = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.grpWireTwist.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionStripMenuItem,
            this.helpStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(312, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // actionStripMenuItem
            // 
            this.actionStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStripMenuItem,
            this.tmStripMenuItem,
            this.toolStripSeparator,
            this.ptStripMenuItem,
            this.cmStripMenuItem});
            this.actionStripMenuItem.Name = "actionStripMenuItem";
            this.actionStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionStripMenuItem.Text = "Action";
            // 
            // stStripMenuItem
            // 
            this.stStripMenuItem.Name = "stStripMenuItem";
            this.stStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.stStripMenuItem.Text = "Start";
            this.stStripMenuItem.Click += new System.EventHandler(this.stStripMenuItem_Click);
            // 
            // tmStripMenuItem
            // 
            this.tmStripMenuItem.Enabled = false;
            this.tmStripMenuItem.Name = "tmStripMenuItem";
            this.tmStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tmStripMenuItem.Text = "Terminate";
            this.tmStripMenuItem.Click += new System.EventHandler(this.tmStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(138, 6);
            // 
            // ptStripMenuItem
            // 
            this.ptStripMenuItem.Enabled = false;
            this.ptStripMenuItem.Name = "ptStripMenuItem";
            this.ptStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ptStripMenuItem.Text = "Add Test Pcs";
            this.ptStripMenuItem.Click += new System.EventHandler(this.ptStripMenuItem_Click);
            // 
            // cmStripMenuItem
            // 
            this.cmStripMenuItem.Enabled = false;
            this.cmStripMenuItem.Name = "cmStripMenuItem";
            this.cmStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cmStripMenuItem.Text = "Caliper Entry";
            this.cmStripMenuItem.Visible = false;
            this.cmStripMenuItem.Click += new System.EventHandler(this.cmStripMenuItem_Click);
            // 
            // helpStripMenuItem
            // 
            this.helpStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rsStripMenuItem,
            this.toolStripSeparator2,
            this.abStripMenuItem,
            this.lcStripMenuItem});
            this.helpStripMenuItem.Name = "helpStripMenuItem";
            this.helpStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpStripMenuItem.Text = "Help";
            // 
            // rsStripMenuItem
            // 
            this.rsStripMenuItem.Name = "rsStripMenuItem";
            this.rsStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.rsStripMenuItem.Text = "Reset IO";
            this.rsStripMenuItem.Click += new System.EventHandler(this.rsStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // abStripMenuItem
            // 
            this.abStripMenuItem.Name = "abStripMenuItem";
            this.abStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.abStripMenuItem.Text = "About PAO Interface";
            this.abStripMenuItem.Click += new System.EventHandler(this.abStripMenuItem_Click);
            // 
            // lcStripMenuItem
            // 
            this.lcStripMenuItem.Name = "lcStripMenuItem";
            this.lcStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.lcStripMenuItem.Text = "License Registration";
            this.lcStripMenuItem.Click += new System.EventHandler(this.lcStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel,
            this.tsDateTimeLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(312, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tsStatusLabel.Image = global::RenTradeWindowForm.Properties.Resources.green;
            this.tsStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(16, 17);
            this.tsStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsDateTimeLabel
            // 
            this.tsDateTimeLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tsDateTimeLabel.Name = "tsDateTimeLabel";
            this.tsDateTimeLabel.Size = new System.Drawing.Size(281, 17);
            this.tsDateTimeLabel.Spring = true;
            this.tsDateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsDateTimeLabel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBatchTarget);
            this.groupBox1.Controls.Add(this.lblDailyTarget);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblOrderNos);
            this.groupBox1.Controls.Add(this.lblLeadSet);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Job Information";
            // 
            // lblBatchTarget
            // 
            this.lblBatchTarget.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBatchTarget.Location = new System.Drawing.Point(229, 44);
            this.lblBatchTarget.Name = "lblBatchTarget";
            this.lblBatchTarget.Size = new System.Drawing.Size(40, 13);
            this.lblBatchTarget.TabIndex = 7;
            this.lblBatchTarget.Text = "0";
            // 
            // lblDailyTarget
            // 
            this.lblDailyTarget.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDailyTarget.Location = new System.Drawing.Point(229, 23);
            this.lblDailyTarget.Name = "lblDailyTarget";
            this.lblDailyTarget.Size = new System.Drawing.Size(40, 13);
            this.lblDailyTarget.TabIndex = 6;
            this.lblDailyTarget.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(145, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Bundle Size:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(145, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Order Quantity:";
            // 
            // lblOrderNos
            // 
            this.lblOrderNos.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOrderNos.Location = new System.Drawing.Point(68, 44);
            this.lblOrderNos.Name = "lblOrderNos";
            this.lblOrderNos.Size = new System.Drawing.Size(75, 13);
            this.lblOrderNos.TabIndex = 3;
            this.lblOrderNos.Text = "n/a";
            // 
            // lblLeadSet
            // 
            this.lblLeadSet.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLeadSet.Location = new System.Drawing.Point(68, 23);
            this.lblLeadSet.Name = "lblLeadSet";
            this.lblLeadSet.Size = new System.Drawing.Size(75, 13);
            this.lblLeadSet.TabIndex = 2;
            this.lblLeadSet.Text = "n/a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Order Nos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Lead Set:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRemarks);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Location = new System.Drawing.Point(12, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 96);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Status";
            // 
            // lblRemarks
            // 
            this.lblRemarks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblRemarks.Location = new System.Drawing.Point(4, 56);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(263, 24);
            this.lblRemarks.TabIndex = 10;
            this.lblRemarks.Text = "Pedal is enabled";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(6, 19);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(263, 42);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Test Mode";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblCounter);
            this.groupBox3.Location = new System.Drawing.Point(12, 238);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(134, 66);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Counter";
            // 
            // lblCounter
            // 
            this.lblCounter.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCounter.Location = new System.Drawing.Point(6, 18);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(122, 37);
            this.lblCounter.TabIndex = 10;
            this.lblCounter.Text = "0";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblQuota);
            this.groupBox4.Location = new System.Drawing.Point(154, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(134, 66);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mid-Piece Count";
            // 
            // lblQuota
            // 
            this.lblQuota.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblQuota.Location = new System.Drawing.Point(5, 18);
            this.lblQuota.Name = "lblQuota";
            this.lblQuota.Size = new System.Drawing.Size(122, 37);
            this.lblQuota.TabIndex = 11;
            this.lblQuota.Text = "0";
            this.lblQuota.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.grpInput);
            this.panel1.Controls.Add(this.grpWireTwist);
            this.panel1.Controls.Add(this.btnTerminate);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblMachineName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 477);
            this.panel1.TabIndex = 5;
            // 
            // grpInput
            // 
            this.grpInput.BackColor = System.Drawing.SystemColors.Control;
            this.grpInput.Controls.Add(this.txtInput);
            this.grpInput.Controls.Add(this.btnOk);
            this.grpInput.Controls.Add(this.lblInputRemarks);
            this.grpInput.Controls.Add(this.lblInputLabel);
            this.grpInput.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpInput.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpInput.Location = new System.Drawing.Point(330, 136);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(276, 137);
            this.grpInput.TabIndex = 14;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Additional Piece";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtInput.Location = new System.Drawing.Point(6, 54);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(210, 29);
            this.txtInput.TabIndex = 16;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(222, 54);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(44, 29);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblInputRemarks
            // 
            this.lblInputRemarks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInputRemarks.Location = new System.Drawing.Point(4, 86);
            this.lblInputRemarks.Name = "lblInputRemarks";
            this.lblInputRemarks.Size = new System.Drawing.Size(260, 24);
            this.lblInputRemarks.TabIndex = 11;
            this.lblInputRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInputLabel
            // 
            this.lblInputLabel.AutoSize = true;
            this.lblInputLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInputLabel.Location = new System.Drawing.Point(6, 38);
            this.lblInputLabel.Name = "lblInputLabel";
            this.lblInputLabel.Size = new System.Drawing.Size(172, 13);
            this.lblInputLabel.TabIndex = 14;
            this.lblInputLabel.Text = "Please input additional quantity";
            // 
            // grpWireTwist
            // 
            this.grpWireTwist.BackColor = System.Drawing.SystemColors.Control;
            this.grpWireTwist.Controls.Add(this.txtRightDim);
            this.grpWireTwist.Controls.Add(this.txtLeftDim);
            this.grpWireTwist.Controls.Add(this.txtPitchDim);
            this.grpWireTwist.Controls.Add(this.label9);
            this.grpWireTwist.Controls.Add(this.label8);
            this.grpWireTwist.Controls.Add(this.btnWireTwist);
            this.grpWireTwist.Controls.Add(this.lblWTRemarks);
            this.grpWireTwist.Controls.Add(this.label5);
            this.grpWireTwist.Enabled = false;
            this.grpWireTwist.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpWireTwist.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpWireTwist.Location = new System.Drawing.Point(330, 279);
            this.grpWireTwist.Name = "grpWireTwist";
            this.grpWireTwist.Size = new System.Drawing.Size(276, 137);
            this.grpWireTwist.TabIndex = 15;
            this.grpWireTwist.TabStop = false;
            this.grpWireTwist.Text = "Wire Twist Dimension";
            // 
            // txtRightDim
            // 
            this.txtRightDim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRightDim.Location = new System.Drawing.Point(148, 52);
            this.txtRightDim.Name = "txtRightDim";
            this.txtRightDim.Size = new System.Drawing.Size(65, 29);
            this.txtRightDim.TabIndex = 20;
            this.txtRightDim.Text = "0";
            this.txtRightDim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRightDim_KeyDown);
            // 
            // txtLeftDim
            // 
            this.txtLeftDim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLeftDim.Location = new System.Drawing.Point(77, 52);
            this.txtLeftDim.Name = "txtLeftDim";
            this.txtLeftDim.Size = new System.Drawing.Size(65, 29);
            this.txtLeftDim.TabIndex = 18;
            this.txtLeftDim.Text = "0";
            this.txtLeftDim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLeftDim_KeyDown);
            // 
            // txtPitchDim
            // 
            this.txtPitchDim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPitchDim.Location = new System.Drawing.Point(6, 52);
            this.txtPitchDim.Name = "txtPitchDim";
            this.txtPitchDim.Size = new System.Drawing.Size(65, 29);
            this.txtPitchDim.TabIndex = 16;
            this.txtPitchDim.Text = "0";
            this.txtPitchDim.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPitchDim_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(148, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Right";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(77, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Left";
            // 
            // btnWireTwist
            // 
            this.btnWireTwist.BackColor = System.Drawing.SystemColors.Control;
            this.btnWireTwist.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnWireTwist.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnWireTwist.ForeColor = System.Drawing.Color.Black;
            this.btnWireTwist.Location = new System.Drawing.Point(222, 52);
            this.btnWireTwist.Name = "btnWireTwist";
            this.btnWireTwist.Size = new System.Drawing.Size(44, 29);
            this.btnWireTwist.TabIndex = 13;
            this.btnWireTwist.Text = "OK";
            this.btnWireTwist.UseVisualStyleBackColor = false;
            this.btnWireTwist.Click += new System.EventHandler(this.btnWireTwist_Click);
            // 
            // lblWTRemarks
            // 
            this.lblWTRemarks.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWTRemarks.Location = new System.Drawing.Point(4, 84);
            this.lblWTRemarks.Name = "lblWTRemarks";
            this.lblWTRemarks.Size = new System.Drawing.Size(260, 24);
            this.lblWTRemarks.TabIndex = 11;
            this.lblWTRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(6, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Pitch";
            // 
            // btnTerminate
            // 
            this.btnTerminate.BackColor = System.Drawing.SystemColors.Control;
            this.btnTerminate.Enabled = false;
            this.btnTerminate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnTerminate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTerminate.ForeColor = System.Drawing.Color.Black;
            this.btnTerminate.Location = new System.Drawing.Point(154, 323);
            this.btnTerminate.Name = "btnTerminate";
            this.btnTerminate.Size = new System.Drawing.Size(134, 48);
            this.btnTerminate.TabIndex = 12;
            this.btnTerminate.Text = "Terminate";
            this.btnTerminate.UseVisualStyleBackColor = false;
            this.btnTerminate.Click += new System.EventHandler(this.btnTerminate_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.Control;
            this.btnStart.Enabled = false;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(12, 323);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(134, 48);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 36);
            this.label1.TabIndex = 11;
            this.label1.Text = "Machine Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMachineName
            // 
            this.lblMachineName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMachineName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMachineName.Location = new System.Drawing.Point(59, 10);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(229, 42);
            this.lblMachineName.TabIndex = 10;
            this.lblMachineName.Text = "XXX-00-0000";
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 523);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "RenTrade Services";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.grpWireTwist.ResumeLayout(false);
            this.grpWireTwist.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem actionStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ptStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBatchTarget;
        private System.Windows.Forms.Label lblDailyTarget;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOrderNos;
        private System.Windows.Forms.Label lblLeadSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblQuota;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.ToolStripStatusLabel tsDateTimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.ToolStripMenuItem stStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnTerminate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem rsStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem abStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lcStripMenuItem;
        private System.Windows.Forms.Label lblInputLabel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblInputRemarks;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.GroupBox grpWireTwist;
        private System.Windows.Forms.TextBox txtPitchDim;
        private System.Windows.Forms.Button btnWireTwist;
        private System.Windows.Forms.Label lblWTRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRightDim;
        private System.Windows.Forms.TextBox txtLeftDim;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}

