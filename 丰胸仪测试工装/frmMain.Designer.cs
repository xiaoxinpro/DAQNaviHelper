namespace 丰胸仪测试工装
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
            this.btnInitSwitch = new System.Windows.Forms.Button();
            this.labelTestNumber = new System.Windows.Forms.Label();
            this.btnTestStop = new System.Windows.Forms.Button();
            this.listViewStatus = new System.Windows.Forms.ListView();
            this.progressBarStatus = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.gbBleConfig = new System.Windows.Forms.GroupBox();
            this.labelBleStatus = new System.Windows.Forms.Label();
            this.btnSerialPortSwitch = new System.Windows.Forms.Button();
            this.comboSerial = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageListStatus = new System.Windows.Forms.ImageList(this.components);
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.toolBleWrite = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelBle = new System.Windows.Forms.ToolStripLabel();
            this.toolBtnBleInit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorBle1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnBleFind = new System.Windows.Forms.ToolStripButton();
            this.toolComboBle = new System.Windows.Forms.ToolStripComboBox();
            this.toolBtnBleLink = new System.Windows.Forms.ToolStripButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.gbBleConfig.SuspendLayout();
            this.toolBleWrite.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitSwitch
            // 
            this.btnInitSwitch.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnInitSwitch.Location = new System.Drawing.Point(13, 108);
            this.btnInitSwitch.Name = "btnInitSwitch";
            this.btnInitSwitch.Size = new System.Drawing.Size(165, 57);
            this.btnInitSwitch.TabIndex = 6;
            this.btnInitSwitch.Text = "开启";
            this.btnInitSwitch.UseVisualStyleBackColor = true;
            this.btnInitSwitch.Click += new System.EventHandler(this.btnInitSwitch_Click);
            // 
            // labelTestNumber
            // 
            this.labelTestNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTestNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTestNumber.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.labelTestNumber.ForeColor = System.Drawing.Color.Blue;
            this.labelTestNumber.Location = new System.Drawing.Point(13, 525);
            this.labelTestNumber.Name = "labelTestNumber";
            this.labelTestNumber.Size = new System.Drawing.Size(701, 25);
            this.labelTestNumber.TabIndex = 11;
            this.labelTestNumber.Text = "测试编码";
            this.labelTestNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTestNumber.DoubleClick += new System.EventHandler(this.labelTestNumber_DoubleClick);
            // 
            // btnTestStop
            // 
            this.btnTestStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestStop.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnTestStop.Location = new System.Drawing.Point(184, 108);
            this.btnTestStop.Name = "btnTestStop";
            this.btnTestStop.Size = new System.Drawing.Size(530, 57);
            this.btnTestStop.TabIndex = 10;
            this.btnTestStop.Text = "强制停止";
            this.btnTestStop.UseVisualStyleBackColor = true;
            this.btnTestStop.Click += new System.EventHandler(this.btnTestStop_Click);
            // 
            // listViewStatus
            // 
            this.listViewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStatus.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.listViewStatus.Location = new System.Drawing.Point(13, 260);
            this.listViewStatus.Name = "listViewStatus";
            this.listViewStatus.Size = new System.Drawing.Size(701, 262);
            this.listViewStatus.TabIndex = 9;
            this.listViewStatus.UseCompatibleStateImageBehavior = false;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatus.Location = new System.Drawing.Point(13, 231);
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(701, 23);
            this.progressBarStatus.TabIndex = 8;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatus.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStatus.Location = new System.Drawing.Point(13, 168);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(701, 60);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "测试状态";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbBleConfig
            // 
            this.gbBleConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBleConfig.Controls.Add(this.checkBox1);
            this.gbBleConfig.Controls.Add(this.toolBleWrite);
            this.gbBleConfig.Controls.Add(this.labelBleStatus);
            this.gbBleConfig.Controls.Add(this.btnSerialPortSwitch);
            this.gbBleConfig.Controls.Add(this.comboSerial);
            this.gbBleConfig.Controls.Add(this.label1);
            this.gbBleConfig.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.gbBleConfig.Location = new System.Drawing.Point(13, 5);
            this.gbBleConfig.Name = "gbBleConfig";
            this.gbBleConfig.Size = new System.Drawing.Size(701, 97);
            this.gbBleConfig.TabIndex = 12;
            this.gbBleConfig.TabStop = false;
            this.gbBleConfig.Text = "蓝牙配置";
            // 
            // labelBleStatus
            // 
            this.labelBleStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBleStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBleStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBleStatus.ForeColor = System.Drawing.Color.Blue;
            this.labelBleStatus.Location = new System.Drawing.Point(243, 27);
            this.labelBleStatus.Name = "labelBleStatus";
            this.labelBleStatus.Size = new System.Drawing.Size(452, 29);
            this.labelBleStatus.TabIndex = 3;
            this.labelBleStatus.Text = "蓝牙状态";
            this.labelBleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSerialPortSwitch
            // 
            this.btnSerialPortSwitch.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSerialPortSwitch.Location = new System.Drawing.Point(171, 27);
            this.btnSerialPortSwitch.Name = "btnSerialPortSwitch";
            this.btnSerialPortSwitch.Size = new System.Drawing.Size(66, 29);
            this.btnSerialPortSwitch.TabIndex = 2;
            this.btnSerialPortSwitch.Text = "打开";
            this.btnSerialPortSwitch.UseVisualStyleBackColor = true;
            this.btnSerialPortSwitch.Click += new System.EventHandler(this.btnSerialPortSwitch_Click);
            // 
            // comboSerial
            // 
            this.comboSerial.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboSerial.FormattingEnabled = true;
            this.comboSerial.Location = new System.Drawing.Point(72, 27);
            this.comboSerial.Name = "comboSerial";
            this.comboSerial.Size = new System.Drawing.Size(93, 28);
            this.comboSerial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // imageListStatus
            // 
            this.imageListStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatus.ImageStream")));
            this.imageListStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatus.Images.SetKeyName(0, "TestTool_Status_0.png");
            this.imageListStatus.Images.SetKeyName(1, "TestTool_Status_1.png");
            this.imageListStatus.Images.SetKeyName(2, "TestTool_Status_2.png");
            this.imageListStatus.Images.SetKeyName(3, "TestTool_Status_3.png");
            this.imageListStatus.Images.SetKeyName(4, "TestTool_Status_4.png");
            // 
            // timerTest
            // 
            this.timerTest.Tick += new System.EventHandler(this.timerTest_Tick);
            // 
            // toolBleWrite
            // 
            this.toolBleWrite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolBleWrite.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolBleWrite.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBleWrite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelBle,
            this.toolBtnBleInit,
            this.toolStripSeparatorBle1,
            this.toolBtnBleFind,
            this.toolComboBle,
            this.toolBtnBleLink});
            this.toolBleWrite.Location = new System.Drawing.Point(3, 67);
            this.toolBleWrite.Name = "toolBleWrite";
            this.toolBleWrite.Size = new System.Drawing.Size(695, 27);
            this.toolBleWrite.TabIndex = 10;
            this.toolBleWrite.Text = "toolStrip1";
            this.toolBleWrite.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolBleWrite_ItemClicked);
            // 
            // toolStripLabelBle
            // 
            this.toolStripLabelBle.Name = "toolStripLabelBle";
            this.toolStripLabelBle.Size = new System.Drawing.Size(83, 24);
            this.toolStripLabelBle.Text = " 蓝牙操作：";
            // 
            // toolBtnBleInit
            // 
            this.toolBtnBleInit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnBleInit.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBleInit.Image")));
            this.toolBtnBleInit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBleInit.Name = "toolBtnBleInit";
            this.toolBtnBleInit.Size = new System.Drawing.Size(69, 24);
            this.toolBtnBleInit.Tag = "0";
            this.toolBtnBleInit.Text = "初始命令";
            // 
            // toolStripSeparatorBle1
            // 
            this.toolStripSeparatorBle1.Name = "toolStripSeparatorBle1";
            this.toolStripSeparatorBle1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolBtnBleFind
            // 
            this.toolBtnBleFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnBleFind.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBleFind.Image")));
            this.toolBtnBleFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBleFind.Name = "toolBtnBleFind";
            this.toolBtnBleFind.Size = new System.Drawing.Size(69, 24);
            this.toolBtnBleFind.Tag = "1";
            this.toolBtnBleFind.Text = "搜索蓝牙";
            // 
            // toolComboBle
            // 
            this.toolComboBle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolComboBle.Name = "toolComboBle";
            this.toolComboBle.Size = new System.Drawing.Size(230, 27);
            // 
            // toolBtnBleLink
            // 
            this.toolBtnBleLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnBleLink.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBleLink.Image")));
            this.toolBtnBleLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBleLink.Name = "toolBtnBleLink";
            this.toolBtnBleLink.Size = new System.Drawing.Size(41, 24);
            this.toolBtnBleLink.Tag = "2";
            this.toolBtnBleLink.Text = "连接";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.checkBox1.Location = new System.Drawing.Point(611, 69);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 24);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "自动连接";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 562);
            this.Controls.Add(this.gbBleConfig);
            this.Controls.Add(this.btnInitSwitch);
            this.Controls.Add(this.labelTestNumber);
            this.Controls.Add(this.btnTestStop);
            this.Controls.Add(this.listViewStatus);
            this.Controls.Add(this.progressBarStatus);
            this.Controls.Add(this.labelStatus);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "丰胸仪测试工装";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbBleConfig.ResumeLayout(false);
            this.gbBleConfig.PerformLayout();
            this.toolBleWrite.ResumeLayout(false);
            this.toolBleWrite.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInitSwitch;
        private System.Windows.Forms.Label labelTestNumber;
        private System.Windows.Forms.Button btnTestStop;
        private System.Windows.Forms.ListView listViewStatus;
        private System.Windows.Forms.ProgressBar progressBarStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox gbBleConfig;
        private System.Windows.Forms.Button btnSerialPortSwitch;
        private System.Windows.Forms.ComboBox comboSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBleStatus;
        private System.Windows.Forms.ImageList imageListStatus;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.ToolStrip toolBleWrite;
        private System.Windows.Forms.ToolStripLabel toolStripLabelBle;
        private System.Windows.Forms.ToolStripButton toolBtnBleInit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorBle1;
        private System.Windows.Forms.ToolStripButton toolBtnBleFind;
        private System.Windows.Forms.ToolStripComboBox toolComboBle;
        private System.Windows.Forms.ToolStripButton toolBtnBleLink;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

