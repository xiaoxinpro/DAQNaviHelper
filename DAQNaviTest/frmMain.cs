using Automation.BDaq;
using DAQNaviHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace DAQNaviHelper
{
    public partial class frmMain : Form
    {
        #region 字段
        private double[] m_dataScaled;
        private System.Windows.Forms.Timer timerTest;
        private System.Timers.Timer timerAi;
        private System.Timers.Timer timerAo;
        private System.Timers.Timer timerCnt;
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            initListViewAi(listViewAi);
            initWaveformAiCtrlUsb4704();

            initListViewDi(listViewDi);
            initInstantDiCtrlUsb4704();

            initListViewDo(listViewDo);
            initInstantDoCtrlUsb4704();

            initEventCounterCtrlUsb4704();

            timerTest = new System.Windows.Forms.Timer();
            timerTest.Tick += new EventHandler(TimerTest_Elapsed);
            timerTest.Interval = 10;
            timerTest.Enabled = true;

            //DAQNaviHelper dAQNaviHelper = new DAQNaviHelper("USB-4704,BID#0");
            //if(!dAQNaviHelper.InitDevice(out string message))
            //{
            //    initError(message, "USB-4704初始化失败");
            //}
            
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

        private void TimerTest_Elapsed(object sender, EventArgs e)
        {
            byte bitData = Convert.ToByte(!Convert.ToBoolean(listViewDo.Items[6].SubItems[1].Text));
            instantDoCtrlUsb4704.WriteBit(0, 6, bitData);
        }

        #region 表格操作
        private void initListViewAi(ListView listView)
        {
            //基本属性设置
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            //创建列表头
            listView.Columns.Add("序号", 50, HorizontalAlignment.Center);
            listView.Columns.Add("通道0", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道1", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道2", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道3", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道4", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道5", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道6", 80, HorizontalAlignment.Center);
            listView.Columns.Add("通道7", 80, HorizontalAlignment.Center);

            //添加数据
            addListViewItems(listViewAi, new string[] { "0", "0", "0", "0", "0", "0", "0", "0" });
            listViewAi.Items[0].Text = "实时";

        }

        private void initListViewDi(ListView listView)
        {
            //基本属性设置
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            //创建列表头
            listView.Columns.Add("通道", 40, HorizontalAlignment.Center);
            listView.Columns.Add("状态", 60, HorizontalAlignment.Center);

            //添加数据
            for (int i = 0; i < 8; i++)
            {
                addListViewItems(listView, "None");
            }
        }

        private void initListViewDo(ListView listView)
        {
            //基本属性设置
            listView.CheckBoxes = true;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            //创建列表头
            listView.Columns.Add("通道", 40, HorizontalAlignment.Center);
            listView.Columns.Add("状态", 60, HorizontalAlignment.Center);

            //添加数据
            for (int i = 0; i < 8; i++)
            {
                addListViewItems(listView, "True");
            }
        }

        private void initListViewData(ListView listView,params string[] arrData)
        {
            listView.Clear();
            foreach (string item in arrData)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = item;
                listView.Items.Add(listViewItem);
            }
        }

        private void addListViewItems(ListView listView, params string[] arrData)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = listView.Items.Count.ToString();
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

        #region USB - 4704 模拟输入
        private void initWaveformAiCtrlUsb4704(int deviceNumber = 1)
        {
            try
            {
                waveformAiCtrlUsb4704.SelectedDevice = new DeviceInformation("USB-4704,BID#0");
                waveformAiCtrlUsb4704.Conversion.ClockRate = 4096;  //AD转换频率（32-47619）
                waveformAiCtrlUsb4704.Record.SectionLength = 8;     //采样缓存区大小（保证通信间隔大于100us）
                waveformAiCtrlUsb4704.Conversion.ChannelStart = 0;  //采样起始通道
                waveformAiCtrlUsb4704.Conversion.ChannelCount = 8;  //采样通道数
            }
            catch (Exception error)
            {
                initError("WaveformAiCtrl:" + error.Message, "Usb4704初始化失败");
                Application.Exit();
                return;
            }

            Console.WriteLine("设备号：" + waveformAiCtrlUsb4704.SelectedDevice.DeviceNumber);
            Console.WriteLine("设备描述：" + waveformAiCtrlUsb4704.SelectedDevice.Description);
            Console.WriteLine("设备模式：" + waveformAiCtrlUsb4704.SelectedDevice.DeviceMode);
            Console.WriteLine("设备采样频率：" + waveformAiCtrlUsb4704.Conversion.ClockRate);
            Console.WriteLine("设备通道数：" + waveformAiCtrlUsb4704.Conversion.ChannelCount);

            if (!waveformAiCtrlUsb4704.Initialized)
            {
                MessageBox.Show("Usb4704驱动不存在！", "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            //初始化缓存空间
            int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
            int sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
            m_dataScaled = new double[chanCount * sectionLength];
        }

        private void waveformAiCtrlUsb4704_DataReady(object sender, Automation.BDaq.BfdAiEventArgs e)
        {
            MarkTimeHelper.MarkTime(MarkTimeStatus.End, "采样");
            MarkTimeHelper.MarkTime(MarkTimeStatus.Start, "采样");
            try
            {
                if (waveformAiCtrlUsb4704.State == ControlState.Idle)
                {
                    return;
                }

                if (m_dataScaled.Length < e.Count)
                {
                    m_dataScaled = new double[e.Count];
                    //Console.WriteLine("接收数据：" + e.Count.ToString());
                }

                int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
                int sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
                ErrorCode err = waveformAiCtrlUsb4704.GetData(e.Count, m_dataScaled);
                if (err != ErrorCode.Success && err != ErrorCode.WarningRecordEnd)
                {
                    Console.WriteLine("错误：" + err);
                    return;
                }

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
                    editListViewItems(listViewAi, 0, arrAvgData);
                    listViewAi.EndUpdate();
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine("DataReady错误：" + error.Message);
            }
        }

        private void btnAiStart_Click(object sender, EventArgs e)
        {
            ErrorCode err = waveformAiCtrlUsb4704.Prepare();
            if (err == ErrorCode.Success)
            {
                err = waveformAiCtrlUsb4704.Start();
            }

            if (err != ErrorCode.Success)
            {
                Console.WriteLine("错误：" + err);
                return;
            }

            btnAiStart.Enabled = false;
            btnAiStop.Enabled = true;
        }

        private void btnAiStop_Click(object sender, EventArgs e)
        {
            ErrorCode err = waveformAiCtrlUsb4704.Stop();
            if (err != ErrorCode.Success)
            {
                Console.WriteLine("错误：" + err);
                return;
            }
            btnAiStart.Enabled = true;
            btnAiStop.Enabled = false;
        }

        #endregion

        #region USB - 4704 数字输入
        private void initInstantDiCtrlUsb4704()
        {
            try
            {
                instantDiCtrlUsb4704.SelectedDevice = new DeviceInformation("USB-4704,BID#0");
            }
            catch (Exception error)
            {
                initError("InstantDiCtrl:" + error.Message, "Usb4704初始化失败");
                Application.Exit();
                return;
            }

            timerAi = new System.Timers.Timer(100);
            timerAi.Elapsed += new ElapsedEventHandler(TimerAi_Elapsed);
            timerAi.AutoReset = true;
            timerAi.Enabled = true;
        }

        private void TimerAi_Elapsed(object sender, ElapsedEventArgs e)
        {
            byte portData = 0;
            ErrorCode err = instantDiCtrlUsb4704.Read(0,out portData);
            //Console.WriteLine("端口：" + portData.ToString("x2"));
            try
            {
                this.Invoke(new Action(() =>
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Boolean bitStatus = (portData & (0x01 << i)) == (0x01 << i);
                        editListViewItems(listViewDi, i, bitStatus.ToString());
                    }
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }
        #endregion

        #region USB - 4704 数字输出
        private void initInstantDoCtrlUsb4704()
        {
            try
            {
                instantDoCtrlUsb4704.SelectedDevice = new DeviceInformation("USB-4704,BID#0");
            }
            catch (Exception error)
            {
                initError("InstantDoCtrl:" + error.Message, "Usb4704初始化失败");
                Application.Exit();
                return;
            }

            for (int i = 0; i < listViewDo.Items.Count; i++)
            {
                byte bitData = Convert.ToByte(listViewDo.Items[i].Checked);
                instantDoCtrlUsb4704.WriteBit(0, i, bitData);
            }

            timerAo = new System.Timers.Timer(100);
            timerAo.Elapsed += new ElapsedEventHandler(TimerAo_Elapsed);
            timerAo.AutoReset = true;
            timerAo.Enabled = true;
        }

        private void listViewDo_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            instantDoCtrlUsb4704.WriteBit(0, e.Item.Index, Convert.ToByte(e.Item.Checked));
            //instantDoCtrlUsb4704.ReadBit(0, e.Item.Index, out byte bitData);
            //Console.WriteLine("设置端口" + e.Item.Index + ":" + bitData);
        }

        private void TimerAo_Elapsed(object sender, ElapsedEventArgs e)
        {
            byte portData = 0;
            ErrorCode err = instantDoCtrlUsb4704.Read(0, out portData);
            //Console.WriteLine("DO端口：" + portData.ToString("x2"));
            try
            {
                this.Invoke(new Action(() =>
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Boolean bitStatus = (portData & (0x01 << i)) == (0x01 << i);
                        editListViewItems(listViewDo, i, bitStatus.ToString());
                    }
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        #endregion

        #region USB - 4704 脉冲计数
        private void initEventCounterCtrlUsb4704()
        {
            try
            {
                eventCounterCtrlUsb4704.SelectedDevice = new DeviceInformation("USB-4704,BID#0");
                eventCounterCtrlUsb4704.ChannelStart = 0;
                eventCounterCtrlUsb4704.ChannelCount = 1;
                eventCounterCtrlUsb4704.Enabled = false;
            }
            catch (Exception error)
            {
                initError("WaveformAiCtrl:" + error.Message, "Usb4704初始化失败");
                Application.Exit();
                return;
            }

            timerCnt = new System.Timers.Timer(100);
            timerCnt.Elapsed += new ElapsedEventHandler(TimerCnt_Elapsed);
            timerCnt.AutoReset = true;
            timerCnt.Enabled = false;
        }

        private void TimerCnt_Elapsed(object sender, ElapsedEventArgs e)
        {
            //eventCounterCtrlUsb4704.Enabled = false;
            ErrorCode err = eventCounterCtrlUsb4704.Read(out int cntData);
            //eventCounterCtrlUsb4704.Enabled = true;
            TimeSpan timeSpan = MarkTimeHelper.MarkTime(MarkTimeStatus.End, err + "计数定时器", cntData.ToString());
            MarkTimeHelper.MarkTime(MarkTimeStatus.Start, "计数定时器");
            if (!checkCnt.Checked)
            {
                eventCounterCtrlUsb4704.Enabled = false;
                eventCounterCtrlUsb4704.Enabled = true;
                cntData = Convert.ToInt32(cntData / timeSpan.TotalSeconds);
            }
            try
            {
                this.Invoke(new Action(() =>
                {
                    labelCnt.Text = cntData.ToString();
                }));
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

        }

        private void btnCntStart_Click(object sender, EventArgs e)
        {
            timerCnt.Interval = Convert.ToInt32(txtCnt.Text);
            timerCnt.Enabled = true;
            eventCounterCtrlUsb4704.Enabled = true;
            btnCntStop.Enabled = true;
            btnCntStart.Enabled = false;
            txtCnt.Enabled = false;
        }

        private void btnCntStop_Click(object sender, EventArgs e)
        {
            timerCnt.Enabled = false;
            eventCounterCtrlUsb4704.Enabled = false;
            btnCntStop.Enabled = false;
            btnCntStart.Enabled = true;
            txtCnt.Enabled = true;
        }

        #endregion
    }
}
