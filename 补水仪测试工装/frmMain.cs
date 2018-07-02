using DAQNavi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 补水仪测试工装
{
    public partial class frmMain : Form
    {
        #region 常量
        private static string[] STR_TEST_STATUS = { "待测", "运行", "暂停", "成功", "失败" };
        private static string[] STR_TEST_NAME = {
            "充电4V测试", "充电3.7V测试", "充电3.1V测试", "充电4.4V测试", "充电1A测试", "充电短路测试",
            "放电3.7V测试", "放电1A测试", "喷雾测试", "USB电平检查"
        };
        private static string[] STR_TEST_MARK = {
            "充电检查红灯亮",   //充电4V测试
            "检查输出手机充电5V电压",     //充电3.7V测试
            "检查输出手机充电0V电压",     //充电3.1V测试
            "充满检查绿灯亮",     //充电4.4V测试
            "输出1A检查电池3.7V电压",       //充电1A测试
            "模拟电池对地短路，测试电池保护",       //充电短路测试
            "检查输出手机充电空载5V电压",     //放电3.7V测试
            "检查手机放电1A电流是对应的输出5V电压",       //放电1A测试
            "检查喷雾峰值电压和雾化片工作频率",     //喷雾测试
            "检查USB分压电阻电平"      //USB电平检查
        };

        //输出IO
        private const int DO_K1 = 0;
        private const int DO_K2 = 1;
        private const int DO_K3 = 2;
        private const int DO_K4 = 3;
        private const int DO_K5 = 4;
        private const int DO_K6 = 5;
        private const int DO_K7 = 6;
        private const int DO_K8 = 7;

        //输入IO
        private const int DI_P2 = 0;

        //输入Cnt
        private const int CNT_P1 = 0;

        //输入AI
        private const int AI_AD1 = 0;
        private const int AI_AD2 = 1;
        private const int AI_AD3 = 2;
        private const int AI_AD4 = 3;
        private const int AI_AD5 = 4;
        private const int AI_AD6 = 5;
        private const int AI_AD7 = 6;
        private const int AI_AD8 = 7;
        #endregion

        #region 字段
        private int nowTestItem = 0;
        private bool isTestRun = false;
        private DAQNaviHelper USB4704;
        private AiModeType AdData;
        private int CntTimes = 0;
        #endregion

        #region 初始化

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            InitLabelStatus(labelStatus);
            InitProgressStatus(progressBarStatus);
            InitListViewStatus(listViewStatus);
            InitUsb4704();
        }

        #endregion

        #region 状态栏
        /// <summary>
        /// 初始化状态栏
        /// </summary>
        /// <param name="label">状态栏控件</param>
        private void InitLabelStatus(Label label)
        {
            label.BackColor = Color.White;
            label.ForeColor = Color.Black;
            label.Text = "待开启测试";
        }

        /// <summary>
        /// 设置状态栏
        /// </summary>
        /// <param name="label">状态栏控件</param>
        /// <param name="txt">文本</param>
        /// <param name="e">样式</param>
        private void SetLabelStatus(Label label, string txt, enumTestStatus e = enumTestStatus.Wait)
        {
            label.Text = txt.Trim();
            switch (e)
            {
                case enumTestStatus.Run:
                    label.BackColor = Color.White;
                    label.ForeColor = Color.Blue;
                    break;
                case enumTestStatus.Success:
                    label.BackColor = Color.Green;
                    label.ForeColor = Color.White;
                    break;
                case enumTestStatus.Fail:
                    label.BackColor = Color.Red;
                    label.ForeColor = Color.White;
                    break;
                case enumTestStatus.Wait:
                case enumTestStatus.Pause:
                default:
                    label.BackColor = Color.White;
                    label.ForeColor = Color.Black;
                    break;
            }
        }

        #endregion

        #region 状态进度条
        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="progress">进度条控件</param>
        private void InitProgressStatus(ProgressBar progress)
        {
            progress.Maximum = STR_TEST_NAME.Length * 10;
            progress.Minimum = 0;
            progress.Value = progress.Minimum;
        }

        /// <summary>
        /// 设置进度条数值
        /// </summary>
        /// <param name="progress">进度条控件</param>
        /// <param name="value">数值</param>
        private void SetProgressStatus(ProgressBar progress, int value)
        {
            if (value <= progress.Maximum)
            {
                progress.Value = value;
            }
        }

        /// <summary>
        /// 设置进度条数值
        /// </summary>
        private void SetProgressStatus()
        {
            SetProgressStatus(progressBarStatus, nowTestItem * 10);
        }
        #endregion

        #region 状态列表
        /// <summary>
        /// 初始化状态列表
        /// </summary>
        /// <param name="listView">列表控件</param>
        private void InitListViewStatus(ListView listView)
        {
            //基本属性设置
            listView.Clear();
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;
            listView.SmallImageList = imageListStatus;

            //创建列表头
            listView.Columns.Add("状态", 75, HorizontalAlignment.Center);
            listView.Columns.Add("测试内容", 150, HorizontalAlignment.Center);
            listView.Columns.Add("备注", 375, HorizontalAlignment.Left);

            //添加数据
            listView.BeginUpdate();
            for (int i = 0; i < STR_TEST_NAME.Length; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                SetListViewItemStatus(listViewItem, enumTestStatus.Wait);
                listViewItem.SubItems.Add(STR_TEST_NAME[i]);
                listViewItem.SubItems.Add(STR_TEST_MARK[i]);
                listView.Items.Add(listViewItem);
            }
            listView.EndUpdate();
        }

        /// <summary>
        /// 设置列表状态
        /// </summary>
        /// <param name="listViewItem">子列表控件</param>
        /// <param name="e">状态</param>
        private void SetListViewItemStatus(ListViewItem listViewItem, enumTestStatus e)
        {
            listViewItem.ImageIndex = Convert.ToInt32(e);
            listViewItem.Text = " " + STR_TEST_STATUS[listViewItem.ImageIndex];
        }

        /// <summary>
        /// 列表控件
        /// </summary>
        /// <param name="listView">列表控件</param>
        /// <param name="item">行数</param>
        /// <param name="e">状态</param>
        private void SetListViewItemStatus(ListView listView, int item, enumTestStatus e)
        {
            if (listView.Items.Count > item)
            {
                SetListViewItemStatus(listView.Items[item], e);
            }
        }

        /// <summary>
        /// 获取状态列表中的某项状态
        /// </summary>
        /// <param name="listView">状态列表控件</param>
        /// <param name="item">行号</param>
        /// <returns>状态枚举</returns>
        private enumTestStatus GetListViewItemStatus(ListView listView, int item)
        {
            if (listView.Items.Count > item)
            {
                return (enumTestStatus)listView.Items[item].ImageIndex;
            }
            return enumTestStatus.Fail;
        }
        #endregion

        #region 状态公共函数
        /// <summary>
        /// 设置初始化状态
        /// </summary>
        /// <param name="num">测试号</param>
        private void SetInitStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                SetLabelStatus(labelStatus, "初始化【" + STR_TEST_NAME[num] + "】测试", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Run);
                SetProgressStatus(progressBarStatus, num * 10);
            }
        }

        private void SetRunStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试中", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Run);
                if (progressBarStatus.Value < ((num + 1) * 10))
                {
                    SetProgressStatus(progressBarStatus, progressBarStatus.Value + 1);
                }
            }
        }

        private void SetSuccessStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试通过", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Success);
                SetProgressStatus(progressBarStatus, (num + 1) * 10);
            }
        }

        private void SetFailStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试失败", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Fail);
                SetProgressStatus(progressBarStatus, (num + 1) * 10);
            }
        }
        #endregion

        #region USB4704
        /// <summary>
        /// 初始化USB-4704
        /// </summary>
        private void InitUsb4704()
        {
            //初始化设备
            USB4704 = new DAQNaviHelper("USB-4704,BID#0");
            if (!USB4704.InitDevice(out string message))
            {
                initError(message, "USB-4704初始化失败");
            }

            //绑定异常发生事件
            USB4704.BindErrorEvent(new DAQNaviHelper.DelegateErrorEvent(USB4704_EventError));

            //初始化功能模块
            InitFunc();

        }

        /// <summary>
        /// 初始化错误处理
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        private void initError(string message, string title)
        {
            if (message.Contains("The device is not available")) //该设备不可用
            {
                MessageBox.Show("该设备不可用，请检查测试工装是否连接，或是否被其他程序占用。", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }

        /// <summary>
        /// USB4704错误事件
        /// </summary>
        /// <param name="message">信息</param>
        private void USB4704_EventError(string message)
        {
            if (message.Contains("ErrorDeviceNotExist") || message.Contains("ErrorFuncNotInited") || message.Contains("ErrorUndefined")) //该设备不可用
            {
                timerTest.Enabled = false;
                USB4704.IDevice.CloseDevice();
                MessageBox.Show("测试设备无法访问，请检查测试工装是否断开连接，或是否被其他程序占用。", message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
        #endregion

        #region 功能模块
        /// <summary>
        /// 初始化功能模块
        /// </summary>
        private void InitFunc()
        {
            nowTestItem = 0;
            isTestRun = false;
            CntTimes = 0;

            //初始化数值输出
            byte[] portData = new byte[8];
            portData[DO_K1] = 1;
            portData[DO_K2] = 1;
            portData[DO_K3] = 1;
            portData[DO_K4] = 1;
            portData[DO_K5] = 1;
            portData[DO_K6] = 1;
            portData[DO_K7] = 1;
            portData[DO_K8] = 0;
            USB4704.IDevice.SetDoMode(portData);

            //初始化模拟输入
            //USB4704.IDevice.StartAiMode(USB4704_AiEvent, 0.3, true);

            //初始化数值输入
            USB4704.IDevice.StartDiMode(USB4704_DiChangeEvent);

            //初始化脉冲计数器
            //USB4704.IDevice.StartCntMode(USB4704_CntEvent, 1);
        }


        /// <summary>
        /// 数字输入改变事件
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="data"></param>
        private void USB4704_DiChangeEvent(int bit, byte data)
        {
            if (DI_P2 == bit)
            {
                this.Invoke(new Action(() =>
                {
                    Console.WriteLine("DI_P2 = " + Convert.ToBoolean(data));
                    if (!Convert.ToBoolean(data) && isTestRun == false)
                    {
                        InitTest();
                    }
                }));
            }
        }

        /// <summary>
        /// 模拟输入获取事件
        /// </summary>
        /// <param name="aiModeData"></param>
        private void USB4704_AiEvent(AiModeType aiModeData)
        {
            AdData = aiModeData;
        }

        /// <summary>
        /// 脉冲计数器事件
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="freq"></param>
        private void USB4704_CntEvent(int channel, int freq)
        {
            if (channel == 0)
            {
                Console.WriteLine("脉冲频率 = " + freq);
            }
        }

        /// <summary>
        /// 选择电源（充电控制，放电控制）
        /// </summary>
        /// <param name="e"></param>
        private void SelectPower(enumTestPower e)
        {
            USB4704.IDevice.SetDoModeBit(DO_K8, Convert.ToByte(e));
        }

        /// <summary>
        /// 选择电压
        /// </summary>
        /// <param name="e"></param>
        private void SelectBatteryVoltage(enumTestBatteryVoltage e)
        {
            switch (e)
            {
                case enumTestBatteryVoltage.Vol4_0:
                    USB4704.IDevice.SetDoModeBit(DO_K2, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K3, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K1, 0);
                    break;
                case enumTestBatteryVoltage.Vol3_7:
                    USB4704.IDevice.SetDoModeBit(DO_K1, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K3, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K2, 0);
                    break;
                case enumTestBatteryVoltage.Vol3_1:
                    USB4704.IDevice.SetDoModeBit(DO_K1, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K2, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K3, 0);
                    break;
                case enumTestBatteryVoltage.Vol4_4:
                    USB4704.IDevice.SetDoModeBit(DO_K1, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K2, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K3, 1);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 选择充电电流
        /// </summary>
        /// <param name="e"></param>
        private void SelectChargingCurrent(enumTestChargingCurrent e)
        {
            switch (e)
            {
                case enumTestChargingCurrent.Cur_0A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 1);
                    break;
                case enumTestChargingCurrent.Cur_1A:
                    USB4704.IDevice.SetDoModeBit(DO_K5, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K4, 0);
                    break;
                case enumTestChargingCurrent.Cur_4A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 0);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 选择放电电流
        /// </summary>
        /// <param name="e"></param>
        private void SelectDichargingCurrent(enumTestDischargingCurrent e)
        {
            USB4704.IDevice.SetDoModeBit(DO_K7, Convert.ToByte(e));
        }

        /// <summary>
        /// 选择喷雾
        /// </summary>
        /// <param name="isOpen">是否开启喷雾</param>
        private void SelectSpray(bool isOpen)
        {
            USB4704.IDevice.SetDoModeBit(DO_K6, Convert.ToByte(!isOpen));
        }
        #endregion

        #region 测试流程
        private void InitTest()
        {
            nowTestItem = 0;
            CntTimes = 0;
            timerTest.Interval = 300;
            StartTest();
        }

        private void StartTest()
        {
            isTestRun = true;
            timerTest.Enabled = true;
        }

        private void StopTest()
        {
            isTestRun = false;
            timerTest.Enabled = false;
        }

        private void NextTest()
        {
            nowTestItem++;
            CntTimes = 0;
            timerTest.Interval = 400;
        }

        private void timerTest_Tick(object sender, EventArgs e)
        {
            MarkTimeHelper.MarkTime(MarkTimeStatus.End, "测试定时");
            MarkTimeHelper.MarkTime(MarkTimeStatus.Start, "测试定时");
            switch (nowTestItem)
            {
                case 0:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_0);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckRedLight, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 1:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckOut5V, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 2:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_1);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckOut0V, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 3:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_4);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckGreenLight, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 4:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_1A);
                        USB4704.IDevice.StartAiMode(TestCheck1ABatteryVoltage, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 5:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Charging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_4A);
                        USB4704.IDevice.StartAiMode(TestCheck4AOutVoltage, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 6:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheck0AOutVoltage, 0.3, true);
                    }
                    else if (CntTimes > 10)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    if (TestCheckUSBVoltage)
                    {

                    }
                    else
                    {

                    }
                    break;
                default:
                    StopTest();
                    break;
            }
        }

        private void TestCheckRedLight(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD1];
            Console.WriteLine("红灯电压AD1 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 2.5 &&　vol > 1.2)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private void TestCheckOut5V(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            Console.WriteLine("手机充电电压AD3 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 5.25 && vol > 4.75)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private void TestCheckOut0V(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            Console.WriteLine("手机充电电压AD3 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 0.75)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private void TestCheckGreenLight(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD1];
            Console.WriteLine("绿灯电压AD1 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 3.5 && vol > 2.2)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private void TestCheck1ABatteryVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD7];
            Console.WriteLine("电池电压AD7 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 3.8 && vol > 3.5)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private void TestCheck4AOutVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            Console.WriteLine("5V输出电压AD3 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 0.75)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        private bool TestCheckUSBVoltage = false;
        private void TestCheck0AOutVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            double vol2 = aiModeData.Avg[AI_AD4];
            double vol3 = aiModeData.Avg[AI_AD5];
            Console.WriteLine("5V输出电压AD3 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 5.25 && vol > 4.75)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                        if (vol2 < 2.75 && vol2 > 2.55 && vol3 < 2.05 && vol3 > 1.85)
                        {
                            TestCheckUSBVoltage = true;
                        }
                        else
                        {
                            TestCheckUSBVoltage = false;
                        }
                    }
                }
            }));
        }

        private void TestCheck1AOutVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            Console.WriteLine("5V输出电压AD3 = " + vol);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 5.25 && vol > 4.75)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        NextTest();
                    }
                }
            }));
        }

        #endregion

        #region 按钮事件

        /// <summary>
        /// 启动按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitSwitch_Click(object sender, EventArgs e)
        {
            InitTest();
            btnInitSwitch.Text = "重启";
            btnRunSwitch.Text = "暂停";
            btnRunSwitch.Enabled = true;
        }

        /// <summary>
        /// 暂停继续按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunSwitch_Click(object sender, EventArgs e)
        {
            if (btnRunSwitch.Text == "暂停")
            {
                btnRunSwitch.Text = "继续";
                StopTest();
            }
            else
            {
                btnRunSwitch.Text = "暂停";
                StartTest();
            }
        }

        #endregion
    }

    #region 枚举类型
    /// <summary>
    /// 测试状态枚举
    /// </summary>
    public enum enumTestStatus
    {
        Wait = 0,
        Run = 1,
        Pause = 2,
        Success = 3,
        Fail = 4
    }

    /// <summary>
    /// 测试充放电枚举
    /// </summary>
    public enum enumTestPower
    {
        Charging = 0,
        Discharging = 1
    }

    /// <summary>
    /// 测试电池电压枚举
    /// </summary>
    public enum enumTestBatteryVoltage
    {
        Vol4_0,
        Vol3_7,
        Vol3_1,
        Vol4_4
    }

    /// <summary>
    /// 测试充电电流枚举
    /// </summary>
    public enum enumTestChargingCurrent
    {
        Cur_0A,
        Cur_1A,
        Cur_4A
    }

    /// <summary>
    /// 测试放电电流枚举
    /// </summary>
    public enum enumTestDischargingCurrent
    {
        Cur_1A = 0,
        Cur_0A = 1
    }
    #endregion
}
