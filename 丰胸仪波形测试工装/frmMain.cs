using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 丰胸仪波形测试工装
{
    public partial class frmMain : Form
    {
        #region 字段
        private double[] m_dataScaled;
        private AppConfig appConfig;
        private MarkTimeHelper MarkTimeAi;
        private OutputFile OutputFileAi;
        private Queue<double> DataQueue;
        private decimal DataCount = 0;
        private string[] StrChannelMath;
        #endregion

        #region 初始化
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DataQueue = new Queue<double>();
            OutputFileAi = new OutputFile();
            StrChannelMath = new string[] { "c", "c", "c", "c", "c", "c", "c", "c" };

            initConfig();

            initListViewAi(listViewAi);
            initWaveformAiCtrlUsb4704();
        }

        private void initConfig()
        {
            appConfig = new AppConfig();
            //采样方式配置
            numFrequency.Value = Convert.ToDecimal(appConfig.GetConfig("numFrequency", "1000"));
            switch (Convert.ToInt32(appConfig.GetConfig("radioConnectionType","0")))
            {
                case 1:
                    radioDifferential.Checked = true;
                    break;
                case 0:
                default:
                    radioSingleEnded.Checked = true;
                    break;
            }

            //数据输出配置
            txtFilePath.Text = appConfig.GetConfig("txtFilePath", "");

            //文件拆分配置
            OutputFileAi.FileSplitSize = Convert.ToDecimal(appConfig.GetConfig("FileSplitSize", OutputFileAi.FileSplitSize.ToString()));
            OutputFileAi.FileSplitNumber = Convert.ToDecimal(appConfig.GetConfig("FileSplitNumber", OutputFileAi.FileSplitNumber.ToString()));
            switch (Convert.ToInt32(appConfig.GetConfig("radioFileSplit", "0")))
            {
                case 1:
                    radioFileSplit1.Checked = true;
                    break;
                case 2:
                    radioFileSplit2.Checked = true;
                    break;
                case 0:
                default:
                    radioFileSplit0.Checked = true;
                    break;
            }

            //通道运算配置
            comboChannelMath.SelectedIndex = Convert.ToInt32(appConfig.GetConfig("comboChannelMath", "0"));
            for (int i = 0; i < StrChannelMath.Length; i++)
            {
                StrChannelMath[i] = appConfig.GetConfig("StrChannelMath" + i.ToString(), StrChannelMath[i]);
            }
            txtChannelMath.Text = comboChannelMath.SelectedIndex > 0 ? StrChannelMath[comboChannelMath.SelectedIndex - 1] : "";

            //周期判定配置
            comboCycleChannelSelect.SelectedIndex = Convert.ToInt32(appConfig.GetConfig("comboCycleChannelSelect", "0"));
        }

        private void initError(string message, string title)
        {
            if (message.Contains("The device is not available")) //该设备不可用
            {
                MessageBox.Show("该设备不可用，请检查设备是否连接，或是否被其他程序占用。", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }
        #endregion

        #region 列表操作
        private void initListViewAi(ListView listView)
        {
            listView.Clear();
            DataCount = 0;

            //基本属性设置
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            //创建列表头
            listView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            listView.Columns.Add("通道0", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道1", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道2", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道3", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道4", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道5", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道6", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道7", 80, HorizontalAlignment.Center);

            //初始化列表数据
            clearListViewAi(listView);
        }

        private void clearListViewAi(ListView listView)
        {
            //清空列表内容
            listView.Items.Clear();
            //顶部数据数据
            setListViewItem0("实时", listViewAi, new string[] { "0", "0", "0", "0", "0", "0", "0", "0" });
        }

        private void setListViewItem0(string name, ListView listView, params string[] arrData)
        {
            if (listView.Items.Count > 0)
            {
                editListViewItems(listView, 0, arrData);
            }
            else
            {
                addListViewItems(listView, arrData);
            }
            listViewAi.Items[0].Text = name;
        }

        private void addListViewItems(ListView listView, params string[] arrData)
        {
            if (listView.Items.Count > 100000)
            {
                clearListViewAi(listView);
            }
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = (DataCount++).ToString();//listView.Items.Count.ToString();
            foreach (string item in arrData)
            {
                listViewItem.SubItems.Add(item);
            }
            listView.Items.Add(listViewItem);
        }

        private void editListViewItems(ListView listView, int index, params string[] arrData)
        {
            if (listView.Items.Count <= index)
            {
                return;
            }

            if (listView.Items[index].SubItems.Count - 1 != arrData.Length)
            {
                return;
            }

            for (int i = 0; i < arrData.Length; i++)
            {
                if (listView.Items[index].SubItems[i + 1].Text != arrData[i])
                {
                    listView.Items[index].SubItems[i + 1].Text = arrData[i];
                }
            }
        }
        #endregion

        #region USB4704
        /// <summary>
        /// 初始化设备驱动
        /// </summary>
        /// <param name="deviceNumber">驱动号</param>
        private void initWaveformAiCtrlUsb4704(int deviceNumber = 1)
        {
            try
            {
                waveformAiCtrlUsb4704.SelectedDevice = new DeviceInformation("USB-4704,BID#0");
                SetWaveformAiCtrlUsb4704();
            }
            catch (Exception error)
            {
                initError("WaveformAiCtrl:" + error.Message, "Usb4704初始化失败");
                Application.Exit();
                return;
            }

            if (!waveformAiCtrlUsb4704.Initialized)
            {
                MessageBox.Show("Usb4704驱动不存在！", "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
        }

        /// <summary>
        /// 设置模拟输入配置
        /// </summary>
        /// <param name="clock"></param>
        /// <param name="bufferLength"></param>
        /// <param name="isDifferential"></param>
        private void SetWaveformAiCtrlUsb4704(int clock = 5000, bool isDifferential = false)
        {
            //约束参数
            if (clock < 50)
            {
                clock = 50;
            }
            if (clock > 5000)
            {
                clock = 5000;
            }

            //写入配置
            waveformAiCtrlUsb4704.Conversion.ClockRate = clock;  //AD转换频率（32-47619）
            waveformAiCtrlUsb4704.Record.SectionLength = 8;     //采样缓存区大小（保证通信间隔大于100us）
            waveformAiCtrlUsb4704.Conversion.ChannelStart = 0;  //采样起始通道
            waveformAiCtrlUsb4704.Conversion.ChannelCount = 8;  //采样通道数
            foreach (AiChannel item in waveformAiCtrlUsb4704.Channels)
            {
                item.SignalType = isDifferential ? AiSignalType.Differential : AiSignalType.SingleEnded;
            }

            Console.WriteLine("设备号：" + waveformAiCtrlUsb4704.SelectedDevice.DeviceNumber);
            Console.WriteLine("设备描述：" + waveformAiCtrlUsb4704.SelectedDevice.Description);
            Console.WriteLine("设备模式：" + waveformAiCtrlUsb4704.SelectedDevice.DeviceMode);
            Console.WriteLine("设备采样频率：" + waveformAiCtrlUsb4704.Conversion.ClockRate);
            Console.WriteLine("设备通道数：" + waveformAiCtrlUsb4704.Conversion.ChannelCount);

            //初始化缓存空间
            int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
            int sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
            m_dataScaled = new double[chanCount * sectionLength];
        }

        /// <summary>
        /// 模拟数据接收处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveformAiCtrlUsb4704_DataReady(object sender, Automation.BDaq.BfdAiEventArgs e)
        {
            Console.WriteLine(MarkTimeAi.MarkName + " = " + MarkTimeAi.StrMarkTime());
            try
            {
                if (waveformAiCtrlUsb4704.State == ControlState.Idle)
                {
                    return;
                }
                //Console.WriteLine("接收数据：" + e.Count.ToString());
                if (m_dataScaled.Length < e.Count)
                {
                    m_dataScaled = new double[e.Count];
                    //Console.WriteLine("接收数据：" + e.Count.ToString());
                }

                int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
                int sectionLength = e.Count / chanCount; //sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
                ErrorCode err = waveformAiCtrlUsb4704.GetData(e.Count, m_dataScaled);
                if (err != ErrorCode.Success && err != ErrorCode.WarningRecordEnd)
                {
                    Console.WriteLine("错误：" + err);
                    return;
                }
                
                Console.WriteLine("接收数据：" + e.Count.ToString() + " chanCount:" + chanCount.ToString() + " sectionLength" + sectionLength.ToString());

                //刷新列表显示
                this.Invoke(new Action(() =>
                {
                    double[] arrSumData = new double[chanCount];
                    string[] outputData = new string[sectionLength];
                    listViewAi.BeginUpdate();
                    for (int i = 0; i < sectionLength; i++)
                    {
                        string[] arrData = new string[chanCount];
                        for (int j = 0; j < chanCount; j++)
                        {
                            int cnt = i * chanCount + j;
                            double value = AiDataMathProcess(j, m_dataScaled[cnt]);
                            arrData[j] = value.ToString("f4");
                            arrSumData[j] += value;
                        }
                        addListViewItems(listViewAi, arrData);
                        outputData[i] = string.Join(",", arrData);
                    }
                    OutputFileAi.AddWriteLine(outputData);
                    string[] arrAvgData = new string[arrSumData.Length];
                    for (int i = 0; i < arrSumData.Length; i++)
                    {
                        arrAvgData[i] = (arrSumData[i] / sectionLength).ToString("f4");
                    }
                    setListViewItem0("实时", listViewAi, arrAvgData);
                    listViewAi.EndUpdate();
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine("DataReady错误：" + error.Message);
            }
        }

        /// <summary>
        /// 接收数据运算
        /// </summary>
        /// <param name="arrData">基本数据数组</param>
        private void AiDataMathProcess(ref double[] arrData)
        {
            if (comboChannelMath.SelectedIndex > 0 && arrData.Length <= StrChannelMath.Length)
            {
                for (int i = 0; i < arrData.Length; i++)
                {
                    arrData[i] = AiDataMathProcess(i, arrData[i]);
                }
            }
        }

        private double AiDataMathProcess(int ch, double valData)
        {
            if (comboChannelMath.SelectedIndex > 0 && ch <= StrChannelMath.Length)
            {
                try
                {
                    string strMath = StrChannelMath[ch].Replace("c", valData.ToString());
                    string strResult = new DataTable().Compute(strMath, null).ToString();
                    return Convert.ToDouble(strResult);
                }
                catch (Exception err)
                {
                    Console.WriteLine("通道" + ch.ToString() + "运算错误：" + err.Message);
                    return 0;
                }
            }
            else
            {
                return valData;
            }
        }

        private void btnAiClear_Click(object sender, EventArgs e)
        {
            if (btnAiStart.Enabled)
            {
                initListViewAi(listViewAi);
            }
            //GC.Collect(); //强制清理内存
        }

        private void btnAiStart_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text != "")
            {
                Regex regex = new Regex(@"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
                Match m = regex.Match(txtFilePath.Text);
                if (!m.Success)
                {
                    MessageBox.Show("非法的文件保存路径，请重新选择！", "文件路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    OpenOutputFileAi();
                }
            }
            else
            {
                MessageBox.Show("请先选择输出文件。", "启动失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.CallOnClick(btnSelectFile);
                return;
            }

            //写入新配置参数
            SetWaveformAiCtrlUsb4704(Convert.ToInt32(numFrequency.Value), Convert.ToInt32(txtChannalCount.Text) == 4);
            ErrorCode err = waveformAiCtrlUsb4704.Prepare();
            if (err == ErrorCode.Success)
            {
                //二次写入配置参数
                SetWaveformAiCtrlUsb4704(Convert.ToInt32(numFrequency.Value), Convert.ToInt32(txtChannalCount.Text) == 4);
                MarkTimeAi = new MarkTimeHelper("采样");
                //启动采样
                err = waveformAiCtrlUsb4704.Start();
            }

            if (err != ErrorCode.Success)
            {
                MessageBox.Show("错误：" + err);
                return;
            }

            btnAiStart.Enabled = false;
            btnAiStop.Enabled = true;
            Common.GroupEnable(groupFunction, false);

            appConfig.SetConfig("numFrequency", numFrequency.Value.ToString());
            appConfig.SetConfig("FileSplitNumber", OutputFileAi.FileSplitNumber.ToString());
            appConfig.SetConfig("FileSplitSize", OutputFileAi.FileSplitSize.ToString());
        }

        private void OpenOutputFileAi()
        {
            OutputFileAi.FilePath = txtFilePath.Text.Trim();
            if (radioFileSplit0.Checked)
            {
                OutputFileAi.FileSplit = EnumFileSplit.None;
            }
            else if (radioFileSplit1.Checked)
            {
                OutputFileAi.FileSplit = EnumFileSplit.Size;
                OutputFileAi.FileSplitSize = numFileSplitValue.Value;
            }
            else
            {
                OutputFileAi.FileSplit = EnumFileSplit.Number;
                OutputFileAi.FileSplitNumber = numFileSplitValue.Value;
            }
            OutputFileAi.Enable = true;
        }

        private void btnAiStop_Click(object sender, EventArgs e)
        {
            ErrorCode err = waveformAiCtrlUsb4704.Stop();
            if (err != ErrorCode.Success)
            {
                MessageBox.Show("错误：" + err);
                return;
            }
            waveformAiCtrlUsb4704.Release();
            btnAiStart.Enabled = true;
            btnAiStop.Enabled = false;
            Common.GroupEnable(groupFunction, true);
        }
        #endregion

        #region UI控制
        private void listViewAi_Resize(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.Width > 200)
            {
                foreach (ColumnHeader item in listView.Columns)
                {
                    switch (item.Text)
                    {
                        case "序号":
                            item.Width = 60;
                            break;
                        default:
                            item.Width = (listView.Width - 85) / 8;
                            break;
                    }
                }
            }
        }

        private void radioFileSplit_EnabledChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                //Console.WriteLine("单选框：" + radioButton.Tag);
                appConfig.SetConfig("radioFileSplit", radioButton.Tag.ToString());
                switch (radioButton.Tag.ToString())
                {
                    case "0":
                        panelFileSplit.Visible = false;
                        break;
                    case "1":
                        panelFileSplit.Visible = true;
                        labFileSplitUnit.Text = "KB";
                        numFileSplitValue.Value = OutputFileAi.FileSplitSize;
                        break;
                    case "2":
                        panelFileSplit.Visible = true;
                        labFileSplitUnit.Text = "个";
                        numFileSplitValue.Value = OutputFileAi.FileSplitNumber;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "数据输出";
            sfd.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            sfd.Filter = "表格文件|*.csv|所有文件|*.*";
            sfd.OverwritePrompt = false;
            sfd.ShowDialog();

            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }

            txtFilePath.Text = path;
            appConfig.SetConfig("txtFilePath", path);
        }

        /// <summary>
        /// 周期判定选择框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboCycleChannelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelCycle.Visible = (comboCycleChannelSelect.SelectedIndex > 0);
        }

        /// <summary>
        /// 采样方式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioConnectionType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                appConfig.SetConfig("radioConnectionType", radioButton.Tag.ToString());
                if (radioButton.Tag.ToString() == "0")
                {
                    txtChannalCount.Text = "8";
                }
                else
                {
                    txtChannalCount.Text = "4";
                }
            }
        }

        /// <summary>
        /// 通道运算选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboChannelMath_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            panelChannelMath.Visible = (comboBox.SelectedIndex > 0);
            appConfig.SetConfig("comboChannelMath", comboBox.SelectedIndex.ToString());
            int ch = comboBox.SelectedIndex - 1;
            if (ch >= 0 && ch <= 7)
            {
                setListViewItem0("表达式", listViewAi, StrChannelMath);
                txtChannelMath.Text = StrChannelMath[ch];
            }
            else
            {
                setListViewItem0("实时", listViewAi, new string[] { "0", "0", "0", "0", "0", "0", "0", "0" });
            }
        }

        /// <summary>
        /// 保存通道运算表达式按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChannelMathSave_Click(object sender, EventArgs e)
        {
            int ch = comboChannelMath.SelectedIndex - 1;
            string strMath = txtChannelMath.Text;
            if (ch >= 0 && ch <= 7)
            {
                string tmp = strMath.Replace("c", "1.23");
                try
                {
                    string vMath = new DataTable().Compute(tmp, null).ToString();
                    if (!double.TryParse(vMath, out double x))
                    {
                        throw new Exception("运算结果”" + vMath + "“不是一个数值。");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    MessageBox.Show(err.Message, "表达式不合法", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                StrChannelMath[ch] = strMath;
                setListViewItem0("表达式", listViewAi, StrChannelMath);
                appConfig.SetConfig("StrChannelMath" + ch.ToString(), strMath);
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(txtFilePath.Text))
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + txtFilePath.Text);
            }
        }
        #endregion

    }
}
