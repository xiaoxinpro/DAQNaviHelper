namespace 丰胸仪波形测试工装
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAiClear = new System.Windows.Forms.Button();
            this.btnAiStop = new System.Windows.Forms.Button();
            this.btnAiStart = new System.Windows.Forms.Button();
            this.listViewAi = new System.Windows.Forms.ListView();
            this.waveformAiCtrlUsb4704 = new Automation.BDaq.WaveformAiCtrl(this.components);
            this.groupFunction = new System.Windows.Forms.GroupBox();
            this.panelChannelMath = new System.Windows.Forms.Panel();
            this.btnChannelMathReset = new System.Windows.Forms.Button();
            this.btnChannelMathSave = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtChannelMath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.numFrequency = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtChannalCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.radioDifferential = new System.Windows.Forms.RadioButton();
            this.radioSingleEnded = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelCycle = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.numCycleVoltage = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numCycleLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panelFileSplit = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numFileSplitValue = new System.Windows.Forms.NumericUpDown();
            this.labFileSplitUnit = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioFileSplit0 = new System.Windows.Forms.RadioButton();
            this.radioFileSplit1 = new System.Windows.Forms.RadioButton();
            this.radioFileSplit2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboChannelMath = new System.Windows.Forms.ComboBox();
            this.comboCycleChannelSelect = new System.Windows.Forms.ComboBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupFunction.SuspendLayout();
            this.panelChannelMath.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
            this.panelCycle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleLength)).BeginInit();
            this.panelFileSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFileSplitValue)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnAiClear);
            this.groupBox1.Controls.Add(this.btnAiStop);
            this.groupBox1.Controls.Add(this.btnAiStart);
            this.groupBox1.Controls.Add(this.listViewAi);
            this.groupBox1.Location = new System.Drawing.Point(12, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 348);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据显示";
            // 
            // btnAiClear
            // 
            this.btnAiClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAiClear.Location = new System.Drawing.Point(6, 318);
            this.btnAiClear.Name = "btnAiClear";
            this.btnAiClear.Size = new System.Drawing.Size(75, 23);
            this.btnAiClear.TabIndex = 5;
            this.btnAiClear.Text = "清空列表";
            this.btnAiClear.UseVisualStyleBackColor = true;
            this.btnAiClear.Click += new System.EventHandler(this.btnAiClear_Click);
            // 
            // btnAiStop
            // 
            this.btnAiStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiStop.Enabled = false;
            this.btnAiStop.Location = new System.Drawing.Point(656, 318);
            this.btnAiStop.Name = "btnAiStop";
            this.btnAiStop.Size = new System.Drawing.Size(75, 23);
            this.btnAiStop.TabIndex = 2;
            this.btnAiStop.Text = "停止";
            this.btnAiStop.UseVisualStyleBackColor = true;
            this.btnAiStop.Click += new System.EventHandler(this.btnAiStop_Click);
            // 
            // btnAiStart
            // 
            this.btnAiStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAiStart.Location = new System.Drawing.Point(575, 318);
            this.btnAiStart.Name = "btnAiStart";
            this.btnAiStart.Size = new System.Drawing.Size(75, 23);
            this.btnAiStart.TabIndex = 1;
            this.btnAiStart.Text = "启动";
            this.btnAiStart.UseVisualStyleBackColor = true;
            this.btnAiStart.Click += new System.EventHandler(this.btnAiStart_Click);
            // 
            // listViewAi
            // 
            this.listViewAi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAi.Location = new System.Drawing.Point(6, 20);
            this.listViewAi.Name = "listViewAi";
            this.listViewAi.Size = new System.Drawing.Size(725, 292);
            this.listViewAi.TabIndex = 3;
            this.listViewAi.UseCompatibleStateImageBehavior = false;
            this.listViewAi.Resize += new System.EventHandler(this.listViewAi_Resize);
            // 
            // waveformAiCtrlUsb4704
            // 
            this.waveformAiCtrlUsb4704._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("waveformAiCtrlUsb4704._StateStream")));
            this.waveformAiCtrlUsb4704.DataReady += new System.EventHandler<Automation.BDaq.BfdAiEventArgs>(this.waveformAiCtrlUsb4704_DataReady);
            // 
            // groupFunction
            // 
            this.groupFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFunction.Controls.Add(this.panelChannelMath);
            this.groupFunction.Controls.Add(this.label13);
            this.groupFunction.Controls.Add(this.panel2);
            this.groupFunction.Controls.Add(this.label9);
            this.groupFunction.Controls.Add(this.label4);
            this.groupFunction.Controls.Add(this.panelCycle);
            this.groupFunction.Controls.Add(this.panelFileSplit);
            this.groupFunction.Controls.Add(this.panel1);
            this.groupFunction.Controls.Add(this.label2);
            this.groupFunction.Controls.Add(this.comboChannelMath);
            this.groupFunction.Controls.Add(this.comboCycleChannelSelect);
            this.groupFunction.Controls.Add(this.btnSelectFile);
            this.groupFunction.Controls.Add(this.txtFilePath);
            this.groupFunction.Controls.Add(this.label1);
            this.groupFunction.Location = new System.Drawing.Point(12, 12);
            this.groupFunction.Name = "groupFunction";
            this.groupFunction.Size = new System.Drawing.Size(740, 157);
            this.groupFunction.TabIndex = 5;
            this.groupFunction.TabStop = false;
            this.groupFunction.Text = "功能更设置";
            // 
            // panelChannelMath
            // 
            this.panelChannelMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelChannelMath.Controls.Add(this.btnChannelMathReset);
            this.panelChannelMath.Controls.Add(this.btnChannelMathSave);
            this.panelChannelMath.Controls.Add(this.label15);
            this.panelChannelMath.Controls.Add(this.txtChannelMath);
            this.panelChannelMath.Controls.Add(this.label14);
            this.panelChannelMath.Location = new System.Drawing.Point(140, 121);
            this.panelChannelMath.Name = "panelChannelMath";
            this.panelChannelMath.Size = new System.Drawing.Size(590, 32);
            this.panelChannelMath.TabIndex = 17;
            this.panelChannelMath.Visible = false;
            // 
            // btnChannelMathReset
            // 
            this.btnChannelMathReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelMathReset.Location = new System.Drawing.Point(532, 5);
            this.btnChannelMathReset.Name = "btnChannelMathReset";
            this.btnChannelMathReset.Size = new System.Drawing.Size(55, 23);
            this.btnChannelMathReset.TabIndex = 3;
            this.btnChannelMathReset.Text = "重置";
            this.btnChannelMathReset.UseVisualStyleBackColor = true;
            this.btnChannelMathReset.Click += new System.EventHandler(this.btnChannelMathReset_Click);
            // 
            // btnChannelMathSave
            // 
            this.btnChannelMathSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChannelMathSave.Location = new System.Drawing.Point(471, 5);
            this.btnChannelMathSave.Name = "btnChannelMathSave";
            this.btnChannelMathSave.Size = new System.Drawing.Size(55, 23);
            this.btnChannelMathSave.TabIndex = 3;
            this.btnChannelMathSave.Text = "保存";
            this.btnChannelMathSave.UseVisualStyleBackColor = true;
            this.btnChannelMathSave.Click += new System.EventHandler(this.btnChannelMathSave_Click);
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(311, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(142, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "使用c表示当前通道参数";
            // 
            // txtChannelMath
            // 
            this.txtChannelMath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChannelMath.Location = new System.Drawing.Point(82, 7);
            this.txtChannelMath.Name = "txtChannelMath";
            this.txtChannelMath.Size = new System.Drawing.Size(223, 21);
            this.txtChannelMath.TabIndex = 1;
            this.txtChannelMath.Text = "c";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "运算表达式：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 131);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "通道运算：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.numFrequency);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtChannalCount);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.radioDifferential);
            this.panel2.Controls.Add(this.radioSingleEnded);
            this.panel2.Location = new System.Drawing.Point(75, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(656, 32);
            this.panel2.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(429, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "Hz";
            // 
            // numFrequency
            // 
            this.numFrequency.Location = new System.Drawing.Point(366, 4);
            this.numFrequency.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numFrequency.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFrequency.Name = "numFrequency";
            this.numFrequency.Size = new System.Drawing.Size(57, 21);
            this.numFrequency.TabIndex = 5;
            this.numFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFrequency.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(305, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "采样频率：";
            // 
            // txtChannalCount
            // 
            this.txtChannalCount.Location = new System.Drawing.Point(255, 3);
            this.txtChannalCount.Name = "txtChannalCount";
            this.txtChannalCount.ReadOnly = true;
            this.txtChannalCount.Size = new System.Drawing.Size(34, 21);
            this.txtChannalCount.TabIndex = 3;
            this.txtChannalCount.Text = "8";
            this.txtChannalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(180, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "采样通道数：";
            // 
            // radioDifferential
            // 
            this.radioDifferential.AutoSize = true;
            this.radioDifferential.Location = new System.Drawing.Point(93, 4);
            this.radioDifferential.Name = "radioDifferential";
            this.radioDifferential.Size = new System.Drawing.Size(71, 16);
            this.radioDifferential.TabIndex = 1;
            this.radioDifferential.Tag = "1";
            this.radioDifferential.Text = "差分采样";
            this.radioDifferential.UseVisualStyleBackColor = true;
            this.radioDifferential.CheckedChanged += new System.EventHandler(this.radioConnectionType_CheckedChanged);
            // 
            // radioSingleEnded
            // 
            this.radioSingleEnded.AutoSize = true;
            this.radioSingleEnded.Checked = true;
            this.radioSingleEnded.Location = new System.Drawing.Point(4, 4);
            this.radioSingleEnded.Name = "radioSingleEnded";
            this.radioSingleEnded.Size = new System.Drawing.Size(83, 16);
            this.radioSingleEnded.TabIndex = 0;
            this.radioSingleEnded.TabStop = true;
            this.radioSingleEnded.Tag = "0";
            this.radioSingleEnded.Text = "单通道采样";
            this.radioSingleEnded.UseVisualStyleBackColor = true;
            this.radioSingleEnded.CheckedChanged += new System.EventHandler(this.radioConnectionType_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "采样方式：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "周期判定：";
            this.label4.Visible = false;
            // 
            // panelCycle
            // 
            this.panelCycle.Controls.Add(this.label7);
            this.panelCycle.Controls.Add(this.numCycleVoltage);
            this.panelCycle.Controls.Add(this.label6);
            this.panelCycle.Controls.Add(this.numericUpDown1);
            this.panelCycle.Controls.Add(this.label8);
            this.panelCycle.Controls.Add(this.numCycleLength);
            this.panelCycle.Controls.Add(this.label5);
            this.panelCycle.Location = new System.Drawing.Point(140, 219);
            this.panelCycle.Name = "panelCycle";
            this.panelCycle.Size = new System.Drawing.Size(591, 32);
            this.panelCycle.TabIndex = 12;
            this.panelCycle.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(347, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "V";
            // 
            // numCycleVoltage
            // 
            this.numCycleVoltage.DecimalPlaces = 3;
            this.numCycleVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numCycleVoltage.Location = new System.Drawing.Point(271, 4);
            this.numCycleVoltage.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCycleVoltage.Name = "numCycleVoltage";
            this.numCycleVoltage.Size = new System.Drawing.Size(70, 21);
            this.numCycleVoltage.TabIndex = 4;
            this.numCycleVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCycleVoltage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "判定阈值电压：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(435, 4);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(70, 21);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(377, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "阈值长度：";
            // 
            // numCycleLength
            // 
            this.numCycleLength.Location = new System.Drawing.Point(95, 4);
            this.numCycleLength.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCycleLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCycleLength.Name = "numCycleLength";
            this.numCycleLength.Size = new System.Drawing.Size(70, 21);
            this.numCycleLength.TabIndex = 2;
            this.numCycleLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCycleLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "最大周期长度：";
            // 
            // panelFileSplit
            // 
            this.panelFileSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFileSplit.Controls.Add(this.label3);
            this.panelFileSplit.Controls.Add(this.numFileSplitValue);
            this.panelFileSplit.Controls.Add(this.labFileSplitUnit);
            this.panelFileSplit.Location = new System.Drawing.Point(465, 85);
            this.panelFileSplit.Name = "panelFileSplit";
            this.panelFileSplit.Size = new System.Drawing.Size(266, 32);
            this.panelFileSplit.TabIndex = 11;
            this.panelFileSplit.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "拆分参数：";
            // 
            // numFileSplitValue
            // 
            this.numFileSplitValue.Location = new System.Drawing.Point(117, 9);
            this.numFileSplitValue.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFileSplitValue.Name = "numFileSplitValue";
            this.numFileSplitValue.Size = new System.Drawing.Size(120, 21);
            this.numFileSplitValue.TabIndex = 8;
            this.numFileSplitValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFileSplitValue.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // labFileSplitUnit
            // 
            this.labFileSplitUnit.AutoSize = true;
            this.labFileSplitUnit.Location = new System.Drawing.Point(244, 11);
            this.labFileSplitUnit.Name = "labFileSplitUnit";
            this.labFileSplitUnit.Size = new System.Drawing.Size(17, 12);
            this.labFileSplitUnit.TabIndex = 9;
            this.labFileSplitUnit.Text = "KB";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioFileSplit0);
            this.panel1.Controls.Add(this.radioFileSplit1);
            this.panel1.Controls.Add(this.radioFileSplit2);
            this.panel1.Location = new System.Drawing.Point(75, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 32);
            this.panel1.TabIndex = 10;
            // 
            // radioFileSplit0
            // 
            this.radioFileSplit0.AutoSize = true;
            this.radioFileSplit0.Checked = true;
            this.radioFileSplit0.Location = new System.Drawing.Point(4, 9);
            this.radioFileSplit0.Name = "radioFileSplit0";
            this.radioFileSplit0.Size = new System.Drawing.Size(59, 16);
            this.radioFileSplit0.TabIndex = 4;
            this.radioFileSplit0.TabStop = true;
            this.radioFileSplit0.Tag = "0";
            this.radioFileSplit0.Text = "不拆分";
            this.radioFileSplit0.UseVisualStyleBackColor = true;
            this.radioFileSplit0.CheckedChanged += new System.EventHandler(this.radioFileSplit_EnabledChanged);
            // 
            // radioFileSplit1
            // 
            this.radioFileSplit1.AutoSize = true;
            this.radioFileSplit1.Location = new System.Drawing.Point(69, 9);
            this.radioFileSplit1.Name = "radioFileSplit1";
            this.radioFileSplit1.Size = new System.Drawing.Size(107, 16);
            this.radioFileSplit1.TabIndex = 5;
            this.radioFileSplit1.Tag = "1";
            this.radioFileSplit1.Text = "按文件大小拆分";
            this.radioFileSplit1.UseVisualStyleBackColor = true;
            this.radioFileSplit1.CheckedChanged += new System.EventHandler(this.radioFileSplit_EnabledChanged);
            // 
            // radioFileSplit2
            // 
            this.radioFileSplit2.AutoSize = true;
            this.radioFileSplit2.Location = new System.Drawing.Point(182, 9);
            this.radioFileSplit2.Name = "radioFileSplit2";
            this.radioFileSplit2.Size = new System.Drawing.Size(107, 16);
            this.radioFileSplit2.TabIndex = 6;
            this.radioFileSplit2.Tag = "2";
            this.radioFileSplit2.Text = "按数据长度拆分";
            this.radioFileSplit2.UseVisualStyleBackColor = true;
            this.radioFileSplit2.CheckedChanged += new System.EventHandler(this.radioFileSplit_EnabledChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件拆分：";
            // 
            // comboChannelMath
            // 
            this.comboChannelMath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChannelMath.FormattingEnabled = true;
            this.comboChannelMath.Items.AddRange(new object[] {
            "关闭",
            "通道0",
            "通道1",
            "通道2",
            "通道3",
            "通道4",
            "通道5",
            "通道6",
            "通道7"});
            this.comboChannelMath.Location = new System.Drawing.Point(75, 128);
            this.comboChannelMath.Name = "comboChannelMath";
            this.comboChannelMath.Size = new System.Drawing.Size(59, 20);
            this.comboChannelMath.TabIndex = 0;
            this.comboChannelMath.SelectedIndexChanged += new System.EventHandler(this.comboChannelMath_SelectedIndexChanged);
            // 
            // comboCycleChannelSelect
            // 
            this.comboCycleChannelSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCycleChannelSelect.Enabled = false;
            this.comboCycleChannelSelect.FormattingEnabled = true;
            this.comboCycleChannelSelect.Items.AddRange(new object[] {
            "关闭",
            "通道0",
            "通道1",
            "通道2",
            "通道3",
            "通道4",
            "通道5",
            "通道6",
            "通道7"});
            this.comboCycleChannelSelect.Location = new System.Drawing.Point(75, 222);
            this.comboCycleChannelSelect.Name = "comboCycleChannelSelect";
            this.comboCycleChannelSelect.Size = new System.Drawing.Size(59, 20);
            this.comboCycleChannelSelect.TabIndex = 0;
            this.comboCycleChannelSelect.Visible = false;
            this.comboCycleChannelSelect.SelectedIndexChanged += new System.EventHandler(this.comboCycleChannelSelect_SelectedIndexChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFile.Location = new System.Drawing.Point(656, 56);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "选择";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(75, 58);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(575, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据输出：";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 542);
            this.Controls.Add(this.groupFunction);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(780, 380);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "丰胸仪波形采集工装";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupFunction.ResumeLayout(false);
            this.groupFunction.PerformLayout();
            this.panelChannelMath.ResumeLayout(false);
            this.panelChannelMath.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
            this.panelCycle.ResumeLayout(false);
            this.panelCycle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleLength)).EndInit();
            this.panelFileSplit.ResumeLayout(false);
            this.panelFileSplit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFileSplitValue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAiClear;
        private System.Windows.Forms.Button btnAiStop;
        private System.Windows.Forms.Button btnAiStart;
        private System.Windows.Forms.ListView listViewAi;
        private Automation.BDaq.WaveformAiCtrl waveformAiCtrlUsb4704;
        private System.Windows.Forms.GroupBox groupFunction;
        private System.Windows.Forms.Label labFileSplitUnit;
        private System.Windows.Forms.NumericUpDown numFileSplitValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioFileSplit2;
        private System.Windows.Forms.RadioButton radioFileSplit1;
        private System.Windows.Forms.RadioButton radioFileSplit0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelFileSplit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelCycle;
        private System.Windows.Forms.ComboBox comboCycleChannelSelect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numCycleVoltage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numCycleLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numFrequency;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtChannalCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioDifferential;
        private System.Windows.Forms.RadioButton radioSingleEnded;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panelChannelMath;
        private System.Windows.Forms.Button btnChannelMathReset;
        private System.Windows.Forms.Button btnChannelMathSave;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtChannelMath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboChannelMath;
    }
}

