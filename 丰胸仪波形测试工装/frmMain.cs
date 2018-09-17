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
        //private System.Timers.Timer timerAi;
        private MarkTimeHelper MarkTimeAi;
        private OutputFile OutputFileAi;
        private Queue<double> DataQueue;
        private decimal DataCount = 0;
        private string[] StrChannelMath;
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DataQueue = new Queue<double>();
            OutputFileAi = new OutputFile();
            StrChannelMath = new string[] { "c", "c", "c", "c", "c", "c", "c", "c" };

            comboChannelMath.SelectedIndex = 0;
            comboCycleChannelSelect.SelectedIndex = 0;

            initListViewAi(listViewAi);
            initWaveformAiCtrlUsb4704();
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
                    listViewAi.BeginUpdate();
                    for (int i = 0; i < sectionLength; i++)
                    {
                        string[] arrData = new string[chanCount];
                        for (int j = 0; j < chanCount; j++)
                        {
                            int cnt = i * chanCount + j;
                            arrData[j] = m_dataScaled[cnt].ToString("f4");
                            arrSumData[j] += m_dataScaled[cnt];
                        }
                        addListViewItems(listViewAi, arrData);
                    }
                    string[] arrAvgData = new string[arrSumData.Length];
                    for (int i = 0; i < arrSumData.Length; i++)
                    {
                        arrAvgData[i] = (arrSumData[i] / sectionLength).ToString("f4");
                    }
                    setListViewItem0("实时", listViewAi, arrAvgData);
                    listViewAi.EndUpdate();
                }));

                //添加到输出与数据处理
                string[] outputData = new string[sectionLength];
                for (int i = 0; i < sectionLength; i++)
                {
                    int cnt = i * chanCount + 0; //定位到指定通道
                    //DataQueue.Enqueue(m_dataScaled[cnt]);
                    string[] arrData = new string[chanCount];
                    for (int j = 0; j < chanCount; j++)
                    {
                        cnt = i * chanCount + j;
                        arrData[j] = m_dataScaled[cnt].ToString("f4");
                    }
                    outputData[i] = string.Join(",", arrData);
                }
                OutputFileAi.AddWriteLine(outputData);
            }
            catch (Exception error)
            {
                Console.WriteLine("DataReady错误：" + error.Message);
            }
        }

        /// <summary>
        /// 接收数据分解
        /// </summary>
        /// <param name="arrData">接收数据数组</param>
        private void DataProcess(double[] arrData)
        {

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
                Console.WriteLine("错误：" + err);
                return;
            }

            btnAiStart.Enabled = false;
            btnAiStop.Enabled = true;
            Common.GroupEnable(groupFunction, false);
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
                Console.WriteLine("错误：" + err);
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

        #endregion

        /// <summary>
        /// 通道运算选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboChannelMath_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            panelChannelMath.Visible = (comboBox.SelectedIndex > 0);
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
                    if (!double.TryParse(vMath,out double x))
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
            }
        }
    }
}
