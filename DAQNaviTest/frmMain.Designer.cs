namespace DAQNavi
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.waveformAiCtrlUsb4704 = new Automation.BDaq.WaveformAiCtrl(this.components);
            this.btnAiStart = new System.Windows.Forms.Button();
            this.btnAiStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewAi = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewDi = new System.Windows.Forms.ListView();
            this.instantDiCtrlUsb4704 = new Automation.BDaq.InstantDiCtrl(this.components);
            this.instantDoCtrlUsb4704 = new Automation.BDaq.InstantDoCtrl(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewDo = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkCnt = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.btnCntStop = new System.Windows.Forms.Button();
            this.btnCntStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCnt = new System.Windows.Forms.Label();
            this.eventCounterCtrlUsb4704 = new Automation.BDaq.EventCounterCtrl(this.components);
            this.instantAoCtrlUsb4704 = new Automation.BDaq.InstantAoCtrl(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAoSite = new System.Windows.Forms.Button();
            this.numVol0 = new System.Windows.Forms.NumericUpDown();
            this.numVol1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVol0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVol1)).BeginInit();
            this.SuspendLayout();
            // 
            // waveformAiCtrlUsb4704
            // 
            this.waveformAiCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("waveformAiCtrlUsb4704._StateStream")));
            this.waveformAiCtrlUsb4704.DataReady += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.waveformAiCtrlUsb4704_DataReady);
            // 
            // btnAiStart
            // 
            this.btnAiStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiStart.Location = new System.Drawing.Point(593, 253);
            this.btnAiStart.Name = "btnAiStart";
            this.btnAiStart.Size = new System.Drawing.Size(75, 23);
            this.btnAiStart.TabIndex = 1;
            this.btnAiStart.Text = "启动";
            this.btnAiStart.UseVisualStyleBackColor = true;
            this.btnAiStart.Click += new System.EventHandler(this.btnAiStart_Click);
            // 
            // btnAiStop
            // 
            this.btnAiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiStop.Enabled = false;
            this.btnAiStop.Location = new System.Drawing.Point(674, 253);
            this.btnAiStop.Name = "btnAiStop";
            this.btnAiStop.Size = new System.Drawing.Size(75, 23);
            this.btnAiStop.TabIndex = 2;
            this.btnAiStop.Text = "停止";
            this.btnAiStop.UseVisualStyleBackColor = true;
            this.btnAiStop.Click += new System.EventHandler(this.btnAiStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnAiStop);
            this.groupBox1.Controls.Add(this.btnAiStart);
            this.groupBox1.Controls.Add(this.listViewAi);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 283);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模拟输入";
            // 
            // listViewAi
            // 
            this.listViewAi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAi.Location = new System.Drawing.Point(6, 20);
            this.listViewAi.Name = "listViewAi";
            this.listViewAi.Size = new System.Drawing.Size(743, 227);
            this.listViewAi.TabIndex = 3;
            this.listViewAi.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewDi);
            this.groupBox2.Location = new System.Drawing.Point(12, 301);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 250);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数字输入";
            // 
            // listViewDi
            // 
            this.listViewDi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDi.Location = new System.Drawing.Point(11, 21);
            this.listViewDi.Name = "listViewDi";
            this.listViewDi.Size = new System.Drawing.Size(174, 223);
            this.listViewDi.TabIndex = 0;
            this.listViewDi.UseCompatibleStateImageBehavior = false;
            // 
            // instantDiCtrlUsb4704
            // 
            this.instantDiCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantDiCtrlUsb4704._StateStream")));
            // 
            // instantDoCtrlUsb4704
            // 
            this.instantDoCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantDoCtrlUsb4704._StateStream")));
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listViewDo);
            this.groupBox3.Location = new System.Drawing.Point(218, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 250);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数字输出";
            // 
            // listViewDo
            // 
            this.listViewDo.Location = new System.Drawing.Point(6, 21);
            this.listViewDo.Name = "listViewDo";
            this.listViewDo.Size = new System.Drawing.Size(188, 223);
            this.listViewDo.TabIndex = 0;
            this.listViewDo.UseCompatibleStateImageBehavior = false;
            this.listViewDo.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewDo_ItemChecked);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkCnt);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtCnt);
            this.groupBox4.Controls.Add(this.btnCntStop);
            this.groupBox4.Controls.Add(this.btnCntStart);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.labelCnt);
            this.groupBox4.Location = new System.Drawing.Point(424, 301);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 129);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "计时器";
            // 
            // checkCnt
            // 
            this.checkCnt.AutoSize = true;
            this.checkCnt.Location = new System.Drawing.Point(146, 29);
            this.checkCnt.Name = "checkCnt";
            this.checkCnt.Size = new System.Drawing.Size(48, 16);
            this.checkCnt.TabIndex = 7;
            this.checkCnt.Text = "累计";
            this.checkCnt.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "采样：";
            // 
            // txtCnt
            // 
            this.txtCnt.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtCnt.Location = new System.Drawing.Point(72, 21);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.Size = new System.Drawing.Size(68, 26);
            this.txtCnt.TabIndex = 5;
            this.txtCnt.Text = "100";
            // 
            // btnCntStop
            // 
            this.btnCntStop.Enabled = false;
            this.btnCntStop.Location = new System.Drawing.Point(140, 75);
            this.btnCntStop.Name = "btnCntStop";
            this.btnCntStop.Size = new System.Drawing.Size(54, 23);
            this.btnCntStop.TabIndex = 4;
            this.btnCntStop.Text = "停止";
            this.btnCntStop.UseVisualStyleBackColor = true;
            this.btnCntStop.Click += new System.EventHandler(this.btnCntStop_Click);
            // 
            // btnCntStart
            // 
            this.btnCntStart.Location = new System.Drawing.Point(72, 75);
            this.btnCntStart.Name = "btnCntStart";
            this.btnCntStart.Size = new System.Drawing.Size(54, 23);
            this.btnCntStart.TabIndex = 3;
            this.btnCntStart.Text = "启动";
            this.btnCntStart.UseVisualStyleBackColor = true;
            this.btnCntStart.Click += new System.EventHandler(this.btnCntStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "计数器：";
            // 
            // labelCnt
            // 
            this.labelCnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCnt.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCnt.Location = new System.Drawing.Point(72, 50);
            this.labelCnt.Name = "labelCnt";
            this.labelCnt.Size = new System.Drawing.Size(122, 22);
            this.labelCnt.TabIndex = 0;
            this.labelCnt.Text = "0";
            this.labelCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eventCounterCtrlUsb4704
            // 
            this.eventCounterCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("eventCounterCtrlUsb4704._StateStream")));
            // 
            // instantAoCtrlUsb4704
            // 
            this.instantAoCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantAoCtrlUsb4704._StateStream")));
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numVol1);
            this.groupBox5.Controls.Add(this.numVol0);
            this.groupBox5.Controls.Add(this.btnAoSite);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(425, 437);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(199, 114);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "模拟输出";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "通道0：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "通道1：";
            // 
            // btnAoSite
            // 
            this.btnAoSite.Location = new System.Drawing.Point(139, 85);
            this.btnAoSite.Name = "btnAoSite";
            this.btnAoSite.Size = new System.Drawing.Size(54, 23);
            this.btnAoSite.TabIndex = 1;
            this.btnAoSite.Text = "设置";
            this.btnAoSite.UseVisualStyleBackColor = true;
            this.btnAoSite.Click += new System.EventHandler(this.btnAoSite_Click);
            // 
            // numVol0
            // 
            this.numVol0.DecimalPlaces = 3;
            this.numVol0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numVol0.Location = new System.Drawing.Point(65, 30);
            this.numVol0.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numVol0.Name = "numVol0";
            this.numVol0.Size = new System.Drawing.Size(120, 21);
            this.numVol0.TabIndex = 2;
            // 
            // numVol1
            // 
            this.numVol1.DecimalPlaces = 3;
            this.numVol1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numVol1.Location = new System.Drawing.Point(65, 53);
            this.numVol1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numVol1.Name = "numVol1";
            this.numVol1.Size = new System.Drawing.Size(120, 21);
            this.numVol1.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 590);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "研华USB-4704测试工具";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVol0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVol1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Automation.BDaq.WaveformAiCtrl waveformAiCtrlUsb4704;
        private System.Windows.Forms.Button btnAiStart;
        private System.Windows.Forms.Button btnAiStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewAi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listViewDi;
        private Automation.BDaq.InstantDiCtrl instantDiCtrlUsb4704;
        private Automation.BDaq.InstantDoCtrl instantDoCtrlUsb4704;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listViewDo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelCnt;
        private Automation.BDaq.EventCounterCtrl eventCounterCtrlUsb4704;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCntStop;
        private System.Windows.Forms.Button btnCntStart;
        private System.Windows.Forms.TextBox txtCnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkCnt;
        private Automation.BDaq.InstantAoCtrl instantAoCtrlUsb4704;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numVol1;
        private System.Windows.Forms.NumericUpDown numVol0;
        private System.Windows.Forms.Button btnAoSite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

