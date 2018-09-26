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
            this.btnInitSwitch = new System.Windows.Forms.Button();
            this.labelTestNumber = new System.Windows.Forms.Label();
            this.btnTestStop = new System.Windows.Forms.Button();
            this.listViewStatus = new System.Windows.Forms.ListView();
            this.progressBarStatus = new System.Windows.Forms.ProgressBar();
            this.labelStatus = new System.Windows.Forms.Label();
            this.gbBleConfig = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboSerial = new System.Windows.Forms.ComboBox();
            this.btnOpenSerial = new System.Windows.Forms.Button();
            this.labelBleStatus = new System.Windows.Forms.Label();
            this.gbBleConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInitSwitch
            // 
            this.btnInitSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitSwitch.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnInitSwitch.Location = new System.Drawing.Point(408, 12);
            this.btnInitSwitch.Name = "btnInitSwitch";
            this.btnInitSwitch.Size = new System.Drawing.Size(150, 65);
            this.btnInitSwitch.TabIndex = 6;
            this.btnInitSwitch.Text = "开启";
            this.btnInitSwitch.UseVisualStyleBackColor = true;
            // 
            // labelTestNumber
            // 
            this.labelTestNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTestNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTestNumber.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.labelTestNumber.ForeColor = System.Drawing.Color.Blue;
            this.labelTestNumber.Location = new System.Drawing.Point(13, 506);
            this.labelTestNumber.Name = "labelTestNumber";
            this.labelTestNumber.Size = new System.Drawing.Size(701, 25);
            this.labelTestNumber.TabIndex = 11;
            this.labelTestNumber.Text = "测试编码";
            this.labelTestNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTestStop
            // 
            this.btnTestStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestStop.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnTestStop.Location = new System.Drawing.Point(564, 12);
            this.btnTestStop.Name = "btnTestStop";
            this.btnTestStop.Size = new System.Drawing.Size(150, 65);
            this.btnTestStop.TabIndex = 10;
            this.btnTestStop.Text = "强制停止";
            this.btnTestStop.UseVisualStyleBackColor = true;
            // 
            // listViewStatus
            // 
            this.listViewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStatus.Location = new System.Drawing.Point(13, 180);
            this.listViewStatus.Name = "listViewStatus";
            this.listViewStatus.Size = new System.Drawing.Size(701, 323);
            this.listViewStatus.TabIndex = 9;
            this.listViewStatus.UseCompatibleStateImageBehavior = false;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatus.Location = new System.Drawing.Point(13, 150);
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
            this.labelStatus.Location = new System.Drawing.Point(13, 80);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(701, 66);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "测试状态";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbBleConfig
            // 
            this.gbBleConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBleConfig.Controls.Add(this.labelBleStatus);
            this.gbBleConfig.Controls.Add(this.btnOpenSerial);
            this.gbBleConfig.Controls.Add(this.comboSerial);
            this.gbBleConfig.Controls.Add(this.label1);
            this.gbBleConfig.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbBleConfig.Location = new System.Drawing.Point(13, 12);
            this.gbBleConfig.Name = "gbBleConfig";
            this.gbBleConfig.Size = new System.Drawing.Size(389, 65);
            this.gbBleConfig.TabIndex = 12;
            this.gbBleConfig.TabStop = false;
            this.gbBleConfig.Text = "蓝牙配置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // comboSerial
            // 
            this.comboSerial.FormattingEnabled = true;
            this.comboSerial.Location = new System.Drawing.Point(72, 27);
            this.comboSerial.Name = "comboSerial";
            this.comboSerial.Size = new System.Drawing.Size(93, 29);
            this.comboSerial.TabIndex = 1;
            // 
            // btnOpenSerial
            // 
            this.btnOpenSerial.Location = new System.Drawing.Point(171, 27);
            this.btnOpenSerial.Name = "btnOpenSerial";
            this.btnOpenSerial.Size = new System.Drawing.Size(66, 29);
            this.btnOpenSerial.TabIndex = 2;
            this.btnOpenSerial.Text = "打开";
            this.btnOpenSerial.UseVisualStyleBackColor = true;
            // 
            // labelBleStatus
            // 
            this.labelBleStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBleStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBleStatus.Location = new System.Drawing.Point(243, 27);
            this.labelBleStatus.Name = "labelBleStatus";
            this.labelBleStatus.Size = new System.Drawing.Size(140, 29);
            this.labelBleStatus.TabIndex = 3;
            this.labelBleStatus.Text = "蓝牙状态";
            this.labelBleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 543);
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
            this.gbBleConfig.ResumeLayout(false);
            this.gbBleConfig.PerformLayout();
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
        private System.Windows.Forms.Button btnOpenSerial;
        private System.Windows.Forms.ComboBox comboSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBleStatus;
    }
}

