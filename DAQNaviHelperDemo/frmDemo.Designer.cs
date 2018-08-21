namespace DAQNaviHelperDemo
{
    partial class frmDemo
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
            this.btnTest = new System.Windows.Forms.Button();
            this.btnGetAiData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAiStop = new System.Windows.Forms.Button();
            this.txtAiInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAiData = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDiList = new System.Windows.Forms.CheckedListBox();
            this.btnDiSwitch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkDoList = new System.Windows.Forms.CheckedListBox();
            this.btnDoSwitch = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCnt = new System.Windows.Forms.TextBox();
            this.btnCntStop = new System.Windows.Forms.Button();
            this.btnCntStart = new System.Windows.Forms.Button();
            this.labelCnt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numAoVol0 = new System.Windows.Forms.NumericUpDown();
            this.numAoVol1 = new System.Windows.Forms.NumericUpDown();
            this.btnAoSite = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAiInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAoVol0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAoVol1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnGetAiData
            // 
            this.btnGetAiData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetAiData.Location = new System.Drawing.Point(261, 19);
            this.btnGetAiData.Name = "btnGetAiData";
            this.btnGetAiData.Size = new System.Drawing.Size(77, 23);
            this.btnGetAiData.TabIndex = 1;
            this.btnGetAiData.Text = "获取Ai数据";
            this.btnGetAiData.UseVisualStyleBackColor = true;
            this.btnGetAiData.Click += new System.EventHandler(this.btnGetAiData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAiStop);
            this.groupBox1.Controls.Add(this.txtAiInterval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAiData);
            this.groupBox1.Controls.Add(this.btnGetAiData);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 168);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模拟输入";
            // 
            // btnAiStop
            // 
            this.btnAiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiStop.Location = new System.Drawing.Point(344, 19);
            this.btnAiStop.Name = "btnAiStop";
            this.btnAiStop.Size = new System.Drawing.Size(44, 23);
            this.btnAiStop.TabIndex = 5;
            this.btnAiStop.Text = "停止";
            this.btnAiStop.UseVisualStyleBackColor = true;
            this.btnAiStop.Click += new System.EventHandler(this.btnAiStop_Click);
            // 
            // txtAiInterval
            // 
            this.txtAiInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAiInterval.DecimalPlaces = 1;
            this.txtAiInterval.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.txtAiInterval.Location = new System.Drawing.Point(111, 20);
            this.txtAiInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.txtAiInterval.Name = "txtAiInterval";
            this.txtAiInterval.Size = new System.Drawing.Size(136, 21);
            this.txtAiInterval.TabIndex = 4;
            this.txtAiInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "采样时间（秒）：";
            // 
            // txtAiData
            // 
            this.txtAiData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAiData.Location = new System.Drawing.Point(6, 48);
            this.txtAiData.Multiline = true;
            this.txtAiData.Name = "txtAiData";
            this.txtAiData.Size = new System.Drawing.Size(382, 108);
            this.txtAiData.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDiList);
            this.groupBox2.Controls.Add(this.btnDiSwitch);
            this.groupBox2.Location = new System.Drawing.Point(12, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 209);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数字输入";
            // 
            // chkDiList
            // 
            this.chkDiList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDiList.Enabled = false;
            this.chkDiList.FormattingEnabled = true;
            this.chkDiList.Location = new System.Drawing.Point(7, 48);
            this.chkDiList.Name = "chkDiList";
            this.chkDiList.Size = new System.Drawing.Size(92, 148);
            this.chkDiList.TabIndex = 1;
            // 
            // btnDiSwitch
            // 
            this.btnDiSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDiSwitch.Location = new System.Drawing.Point(6, 18);
            this.btnDiSwitch.Name = "btnDiSwitch";
            this.btnDiSwitch.Size = new System.Drawing.Size(93, 23);
            this.btnDiSwitch.TabIndex = 0;
            this.btnDiSwitch.Text = "开启";
            this.btnDiSwitch.UseVisualStyleBackColor = true;
            this.btnDiSwitch.Click += new System.EventHandler(this.btnDiSwitch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkDoList);
            this.groupBox3.Controls.Add(this.btnDoSwitch);
            this.groupBox3.Location = new System.Drawing.Point(123, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 209);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数字输出";
            // 
            // chkDoList
            // 
            this.chkDoList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDoList.Enabled = false;
            this.chkDoList.FormattingEnabled = true;
            this.chkDoList.Location = new System.Drawing.Point(7, 48);
            this.chkDoList.Name = "chkDoList";
            this.chkDoList.Size = new System.Drawing.Size(92, 148);
            this.chkDoList.TabIndex = 1;
            this.chkDoList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkDoList_ItemCheck);
            // 
            // btnDoSwitch
            // 
            this.btnDoSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoSwitch.Location = new System.Drawing.Point(6, 18);
            this.btnDoSwitch.Name = "btnDoSwitch";
            this.btnDoSwitch.Size = new System.Drawing.Size(93, 23);
            this.btnDoSwitch.TabIndex = 0;
            this.btnDoSwitch.Text = "开启";
            this.btnDoSwitch.UseVisualStyleBackColor = true;
            this.btnDoSwitch.Click += new System.EventHandler(this.btnDoSwitch_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtCnt);
            this.groupBox4.Controls.Add(this.btnCntStop);
            this.groupBox4.Controls.Add(this.btnCntStart);
            this.groupBox4.Controls.Add(this.labelCnt);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(234, 215);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(172, 117);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "计时器";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "秒";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "采样：";
            // 
            // txtCnt
            // 
            this.txtCnt.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txtCnt.Location = new System.Drawing.Point(56, 20);
            this.txtCnt.Name = "txtCnt";
            this.txtCnt.Size = new System.Drawing.Size(76, 26);
            this.txtCnt.TabIndex = 5;
            this.txtCnt.Text = "1";
            this.txtCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCntStop
            // 
            this.btnCntStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCntStop.Enabled = false;
            this.btnCntStop.Location = new System.Drawing.Point(91, 84);
            this.btnCntStop.Name = "btnCntStop";
            this.btnCntStop.Size = new System.Drawing.Size(54, 23);
            this.btnCntStop.TabIndex = 4;
            this.btnCntStop.Text = "停止";
            this.btnCntStop.UseVisualStyleBackColor = true;
            this.btnCntStop.Click += new System.EventHandler(this.btnCntStop_Click);
            // 
            // btnCntStart
            // 
            this.btnCntStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCntStart.Location = new System.Drawing.Point(23, 84);
            this.btnCntStart.Name = "btnCntStart";
            this.btnCntStart.Size = new System.Drawing.Size(54, 23);
            this.btnCntStart.TabIndex = 3;
            this.btnCntStart.Text = "启动";
            this.btnCntStart.UseVisualStyleBackColor = true;
            this.btnCntStart.Click += new System.EventHandler(this.btnCntStart_Click);
            // 
            // labelCnt
            // 
            this.labelCnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCnt.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCnt.Location = new System.Drawing.Point(56, 51);
            this.labelCnt.Name = "labelCnt";
            this.labelCnt.Size = new System.Drawing.Size(76, 22);
            this.labelCnt.TabIndex = 0;
            this.labelCnt.Text = "0";
            this.labelCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "频率：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnAoSite);
            this.groupBox5.Controls.Add(this.numAoVol1);
            this.groupBox5.Controls.Add(this.numAoVol0);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Location = new System.Drawing.Point(235, 339);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(171, 85);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "模拟输出";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "通道1：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "通道0：";
            // 
            // numAoVol0
            // 
            this.numAoVol0.DecimalPlaces = 3;
            this.numAoVol0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numAoVol0.Location = new System.Drawing.Point(55, 25);
            this.numAoVol0.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAoVol0.Name = "numAoVol0";
            this.numAoVol0.Size = new System.Drawing.Size(60, 21);
            this.numAoVol0.TabIndex = 7;
            this.numAoVol0.Tag = "0";
            this.numAoVol0.ValueChanged += new System.EventHandler(this.numAoVol_ValueChanged);
            // 
            // numAoVol1
            // 
            this.numAoVol1.DecimalPlaces = 3;
            this.numAoVol1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numAoVol1.Location = new System.Drawing.Point(55, 52);
            this.numAoVol1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAoVol1.Name = "numAoVol1";
            this.numAoVol1.Size = new System.Drawing.Size(60, 21);
            this.numAoVol1.TabIndex = 7;
            this.numAoVol1.Tag = "1";
            this.numAoVol1.ValueChanged += new System.EventHandler(this.numAoVol_ValueChanged);
            // 
            // btnAoSite
            // 
            this.btnAoSite.Location = new System.Drawing.Point(121, 22);
            this.btnAoSite.Name = "btnAoSite";
            this.btnAoSite.Size = new System.Drawing.Size(44, 50);
            this.btnAoSite.TabIndex = 8;
            this.btnAoSite.Text = "设置";
            this.btnAoSite.UseVisualStyleBackColor = true;
            this.btnAoSite.Click += new System.EventHandler(this.btnAoSite_Click);
            // 
            // frmDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 450);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTest);
            this.Name = "frmDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DAQNavi Helper Demo";
            this.Load += new System.EventHandler(this.frmDemo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAiInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAoVol0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAoVol1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnGetAiData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAiData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtAiInterval;
        private System.Windows.Forms.Button btnAiStop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chkDiList;
        private System.Windows.Forms.Button btnDiSwitch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox chkDoList;
        private System.Windows.Forms.Button btnDoSwitch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCnt;
        private System.Windows.Forms.Button btnCntStop;
        private System.Windows.Forms.Button btnCntStart;
        private System.Windows.Forms.Label labelCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAoSite;
        private System.Windows.Forms.NumericUpDown numAoVol1;
        private System.Windows.Forms.NumericUpDown numAoVol0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

