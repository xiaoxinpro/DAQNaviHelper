namespace 补水仪测试工装
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
            this.btnRunSwitch = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBarStatus = new System.Windows.Forms.ProgressBar();
            this.listViewStatus = new System.Windows.Forms.ListView();
            this.imageListStatus = new System.Windows.Forms.ImageList(this.components);
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.btnTestStop = new System.Windows.Forms.Button();
            this.labelTestNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnInitSwitch
            // 
            this.btnInitSwitch.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnInitSwitch.Location = new System.Drawing.Point(108, 550);
            this.btnInitSwitch.Name = "btnInitSwitch";
            this.btnInitSwitch.Size = new System.Drawing.Size(134, 56);
            this.btnInitSwitch.TabIndex = 0;
            this.btnInitSwitch.Text = "开启";
            this.btnInitSwitch.UseVisualStyleBackColor = true;
            this.btnInitSwitch.Click += new System.EventHandler(this.btnInitSwitch_Click);
            // 
            // btnRunSwitch
            // 
            this.btnRunSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunSwitch.Enabled = false;
            this.btnRunSwitch.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnRunSwitch.Location = new System.Drawing.Point(399, 550);
            this.btnRunSwitch.Name = "btnRunSwitch";
            this.btnRunSwitch.Size = new System.Drawing.Size(134, 56);
            this.btnRunSwitch.TabIndex = 0;
            this.btnRunSwitch.Text = "暂停";
            this.btnRunSwitch.UseVisualStyleBackColor = true;
            this.btnRunSwitch.Click += new System.EventHandler(this.btnRunSwitch_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatus.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStatus.Location = new System.Drawing.Point(13, 80);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(635, 66);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "测试状态";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarStatus.Location = new System.Drawing.Point(13, 150);
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(635, 23);
            this.progressBarStatus.TabIndex = 2;
            // 
            // listViewStatus
            // 
            this.listViewStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStatus.Location = new System.Drawing.Point(13, 180);
            this.listViewStatus.Name = "listViewStatus";
            this.listViewStatus.Size = new System.Drawing.Size(635, 323);
            this.listViewStatus.TabIndex = 3;
            this.listViewStatus.UseCompatibleStateImageBehavior = false;
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
            // btnTestStop
            // 
            this.btnTestStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestStop.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnTestStop.Location = new System.Drawing.Point(13, 12);
            this.btnTestStop.Name = "btnTestStop";
            this.btnTestStop.Size = new System.Drawing.Size(635, 65);
            this.btnTestStop.TabIndex = 4;
            this.btnTestStop.Text = "强制停止";
            this.btnTestStop.UseVisualStyleBackColor = true;
            this.btnTestStop.Click += new System.EventHandler(this.btnTestStop_Click);
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
            this.labelTestNumber.Size = new System.Drawing.Size(635, 25);
            this.labelTestNumber.TabIndex = 5;
            this.labelTestNumber.Text = "测试编码";
            this.labelTestNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelTestNumber.DoubleClick += new System.EventHandler(this.labelTestNumber_DoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 543);
            this.Controls.Add(this.labelTestNumber);
            this.Controls.Add(this.btnTestStop);
            this.Controls.Add(this.listViewStatus);
            this.Controls.Add(this.progressBarStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.btnRunSwitch);
            this.Controls.Add(this.btnInitSwitch);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "补水仪测试工装";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInitSwitch;
        private System.Windows.Forms.Button btnRunSwitch;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBarStatus;
        private System.Windows.Forms.ListView listViewStatus;
        private System.Windows.Forms.ImageList imageListStatus;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.Button btnTestStop;
        private System.Windows.Forms.Label labelTestNumber;
    }
}

