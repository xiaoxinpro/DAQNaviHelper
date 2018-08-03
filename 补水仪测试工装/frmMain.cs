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
            "放电短路测试", "空载USB测试", "正常工作USB测试", "低电压USB测试", "充电中测试", "充电完成测试",
            "喷雾测试"
        };
        private static string[] STR_TEST_MARK = {
            "选择电池4V，放电电流4A，模拟电池短路，检查短路USB电压0V。",   //放电短路测试
            "选择电池3.7V，检查空载USB电压5V。",     //空载USB测试
            "选择电压3.7V，电流1A，检查正常Usb工作电压5V。",     //正常工作USB测试
            "选择电池2.3V，检查低电压USB输出电压。",     //低电压USB测试
            "选择电池3.7V，充电电流1A，检查红灯电压2.2V与电池电压3.7V。",       //充电中测试
            "选择电池4.4V，充满电池，检查绿灯电压3.12V。",       //充电完成测试
            "喷雾检查，喷雾峰值电压与喷雾频率。"     //喷雾测试
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
        private const int AI_AD0 = 0;
        private const int AI_AD1 = 1;
        private const int AI_AD2 = 2;
        private const int AI_AD3 = 3;
        private const int AI_AD4 = 4;
        private const int AI_AD5 = 5;
        private const int AI_AD6 = 6;
        private const int AI_AD7 = 7;
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

        /// <summary>
        /// 检测完成并输出状态栏显示
        /// </summary>
        /// <param name="label">状态栏控件</param>
        /// <param name="listView">测试列表控件</param>
        /// <returns></returns>
        private bool DetectDoneLabelStatus(Label label, ListView listView)
        {
            bool isSuccess = true;
            string message = "测试结果：";
            for (int i = 0; i < listView.Items.Count; i++)
            {
                if (listView.Items[i].ImageIndex == (int)enumTestStatus.Fail)
                {
                    isSuccess = false;
                    message += listView.Items[i].SubItems[1].Text + "【不合格】，";
                }
            }

            if (isSuccess)
            {
                SetLabelStatus(labelStatus, "PASS", enumTestStatus.Success);
                LogHelper.LogInfo(message + "合格");
            }
            else
            {
                SetLabelStatus(labelStatus, "NG", enumTestStatus.Fail);
                LogHelper.LogInfo(message);
            }
            return isSuccess;
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
            int index = Convert.ToInt32(e);
            if (listViewItem.ImageIndex != index)
            {
                listViewItem.ImageIndex = Convert.ToInt32(e);
                listViewItem.Text = " " + STR_TEST_STATUS[index];
            }
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
        /// 复位列表状态
        /// </summary>
        /// <param name="listView">列表控件</param>
        private void ResetListViewItemStatus(ListView listView)
        {
            listView.BeginUpdate();
            foreach (ListViewItem item in listView.Items)
            {
                SetListViewItemStatus(item, enumTestStatus.Wait);
            }
            listView.EndUpdate();
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
            LogHelper.LogError(title + message);

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
                LogHelper.LogError("设备出错\t" + message);
                try
                {
                    timerTest.Enabled = false;
                    USB4704.IDevice.CloseDevice();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                MessageBox.Show("测试设备无法访问，请检查测试工装是否断开连接，或是否被其他程序占用。", message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                LogHelper.LogWarn("设备异常\t" + message);
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
            InitIO();

            //初始化模拟输入
            //USB4704.IDevice.StartAiMode(USB4704_AiEvent, 0.3, true);

            //初始化数值输入
            USB4704.IDevice.StartDiMode(USB4704_DiChangeEvent);

            //初始化脉冲计数器
            //USB4704.IDevice.StartCntMode(USB4704_CntEvent, 1);

            LogHelper.LogInfo("功能模块初始化完成");
        }

        /// <summary>
        /// 初始化Io口参数
        /// </summary>
        private void InitIO()
        {
            byte[] portData = new byte[8];
            portData[DO_K1] = 1;
            portData[DO_K2] = 0;
            portData[DO_K3] = 1;
            portData[DO_K4] = 0;
            portData[DO_K5] = 0;
            portData[DO_K6] = 1;
            portData[DO_K7] = 1;
            portData[DO_K8] = 0;
            USB4704.IDevice.SetDoMode(portData);
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
                        LogHelper.LogInfo("外部按钮触发");
                        Common.CallOnClick(btnInitSwitch);
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
            USB4704.IDevice.SetDoModeBit(DO_K7, Convert.ToByte(e));
        }

        /// <summary>
        /// 选择放电电流
        /// </summary>
        /// <param name="e"></param>
        private void SelectDichargingCurrent(enumTestDischargingCurrent e)
        {
            USB4704.IDevice.SetDoModeBit(DO_K4, Convert.ToByte(e));
            switch (e)
            {
                case enumTestDischargingCurrent.Cur_0A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 0);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 0);
                    break;
                case enumTestDischargingCurrent.Cur_1A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 0);
                    break;
                case enumTestDischargingCurrent.Cur_3A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 0);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 1);
                    break;
                case enumTestDischargingCurrent.Cur_4A:
                    USB4704.IDevice.SetDoModeBit(DO_K4, 1);
                    USB4704.IDevice.SetDoModeBit(DO_K5, 1);
                    break;
                default:
                    break;
            }
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
        /// <summary>
        /// 初始化测试
        /// </summary>
        private void InitTest()
        {
            nowTestItem = 0;
            CntTimes = 0;
            timerTest.Interval = 300;
            ResetListViewItemStatus(listViewStatus);
            StartTest();
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void StartTest()
        {
            isTestRun = true;
            timerTest.Enabled = true;
            LogHelper.LogInfo("测试开始...");
        }

        /// <summary>
        /// 暂停/停止测试
        /// </summary>
        private void StopTest()
        {
            isTestRun = false;
            timerTest.Enabled = false;
            LogHelper.LogInfo("停止测试\r\n");
        }

        /// <summary>
        /// 测试下一项
        /// </summary>
        private void NextTest()
        {
            nowTestItem++;
            CntTimes = 0;
            timerTest.Interval = 400;
        }

        /// <summary>
        /// 测试定时器中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTest_Tick(object sender, EventArgs e)
        {
            switch (nowTestItem)
            {
                case 0:
                    if (CntTimes++ == 0)
                    {
                        timerTest.Interval = 350;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_0);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_4A);
                        USB4704.IDevice.StartAiMode(TestCheckShortUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试1\t选择电池4V，放电电流4A，模拟电池短路，检查短路USB电压0V。");
                    }
                    else if (CntTimes > 10)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试1\t测试超时。");
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
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckVoidUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试2\t选择电池3.7V，检查空载USB电压5V。");
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试2\t测试超时。");
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
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_1A);
                        USB4704.IDevice.StartAiMode(TestCheck1AUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试3\t选择电压3.7V，电流1A，检查正常Usb工作电压5V。");
                    }
                    else if (CntTimes > 10)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试3\t测试超时。");
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
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_1);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckLowUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试4\t选择电池2.3V，检查低电压USB输出电压。");
                    }
                    else if (CntTimes > 10)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试4\t测试超时。");
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
                        LogHelper.LogInfo("开始测试5\t选择电池3.7V，充电电流1A，检查红灯电压2.2V与电池电压3.7V。");
                    }
                    else if (CntTimes > 10)
                    {
                        //SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试5\t测试超时。");
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
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_4);
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_1A);
                        LogHelper.LogInfo("开始测试6\t选择电池4.4V，充满电池，检查绿灯电压3.12V。");
                    }
                    else if (CntTimes == 2)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StartAiMode(TestCheckGreenLignt, 0.3, true);
                        SetRunStatus(nowTestItem);
                    }
                    else if (CntTimes > 10)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试6\t测试超时。");
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
                        timerTest.Interval = 550;
                        SetInitStatus(nowTestItem);
                        SelectPower(enumTestPower.Discharging);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol3_7);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        SelectSpray(true);
                        isTestCheckSprayVoltage = false;
                        isTestCheckSprayCoun = false;
                        USB4704.IDevice.StartCntMode(TestCheckSprayCount, 2);
                        USB4704.IDevice.StartAiMode(TestCheckSprayVoltage, 1, true);
                        LogHelper.LogInfo("开始测试7\t喷雾检查，喷雾峰值电压与喷雾频率。");
                    }
                    else if (isTestCheckSprayVoltage && isTestCheckSprayCoun)
                    {
                        SelectSpray(false);
                        USB4704.IDevice.StopCntMode();
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试7\t通过。");
                        NextTest();
                    }
                    else if (CntTimes > 10)
                    {
                        SelectSpray(false);
                        USB4704.IDevice.StopCntMode();
                        USB4704.IDevice.StopAiMode();
                        SetFailStatus(nowTestItem);
                        LogHelper.LogWarn("结束测试7\t测试超时。");
                        NextTest();
                    }
                    else
                    {
                        SetRunStatus(nowTestItem);
                    }
                    break;
                default:
                    DetectDoneLabelStatus(labelStatus, listViewStatus);
                    StopTest();
                    break;
            }
        }

        /// <summary>
        /// 选择电池4V，放电电流4A，模拟电池短路，检查短路USB电压0V。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheckShortUsbVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            string log = string.Format("Usb输出电压AD3 = {0:0.000}", vol);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 3.0)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" +(index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 选择电池3.7V，检查空载USB电压5V。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheckVoidUsbVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            double vol1 = aiModeData.Avg[AI_AD4];
            double vol2 = aiModeData.Avg[AI_AD5];
            string log = string.Format("Usb输出电压AD3、AD4、AD5 = {0:0.000}\t{1:0.000}\t{2:0.000}", vol, vol1, vol2);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if ((vol > 4.9 && vol < 5.2) && (vol1 > 2.1 && vol1 < 2.3) && (vol2 > 1.5 && vol2 < 1.7)) 
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 选择电压3.7V，电流1A，检查正常Usb工作电压5V。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheck1AUsbVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            string log = string.Format("Usb输出电压AD3 = {0:0.000}", vol);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol > 4.8 && vol < 5.2) 
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 选择电池2.3V，检查低电压USB输出电压。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheckLowUsbVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD3];
            string log = string.Format("Usb输出电压AD3 = {0:0.000}", vol);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (vol < 3.0)
                    {
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 选择电池3.7V，充电电流1A，检查红灯电压2.2V与电池电压3.7V。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheck1ABatteryVoltage(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD0] - aiModeData.Avg[AI_AD7];
            double red = aiModeData.Avg[AI_AD1];
            string log = string.Format("电池电压AD0-AD7 = {0:0.000}\t红灯电压AD1 = {1:0.000}", vol, red);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if ((vol < 4.2 && vol > 3.7) && (red > 2.1 && red < 2.3)) 
                    {
                        //SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 选择电池4.4V，充满电池，检查绿灯电压3.12V。
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheckGreenLignt(AiModeType aiModeData)
        {
            double vol = aiModeData.Avg[AI_AD0] - aiModeData.Avg[AI_AD7];
            double green = aiModeData.Avg[AI_AD1];
            string log = string.Format("电池电压AD0-AD7 = {0:0.000}\t绿灯电压AD1 = {1:0.000}", vol, green);
            Console.WriteLine(log);
            LogHelper.LogInfo("\t\t" + log);
            int index = nowTestItem;
            this.Invoke(new Action(() =>
            {
                if (GetListViewItemStatus(listViewStatus, index) == enumTestStatus.Run)
                {
                    if (green > 3.0 && green < 3.3)
                    {
                        SelectChargingCurrent(enumTestChargingCurrent.Cur_0A);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
                        NextTest();
                    }
                }
            }));
        }

        /// <summary>
        /// 判断喷雾峰值电压是否合格
        /// </summary>
        private bool isTestCheckSprayVoltage = false;

        /// <summary>
        /// 检测喷雾峰值电压（AD6）
        /// </summary>
        /// <param name="aiModeData"></param>
        private void TestCheckSprayVoltage(AiModeType aiModeData)
        {
            if (!isTestCheckSprayVoltage)
            {
                double vol = aiModeData.Max[AI_AD6] * 11;
                double blue = aiModeData.Avg[AI_AD2];
                string log = string.Format("喷雾峰值电压AD6*11 = {0:0.000}\t蓝灯电压AD2 = {1:0.000}", vol, blue);
                Console.WriteLine(log);
                LogHelper.LogInfo("\t\t" + log);
                if ((vol < 40 && vol > 30) && (blue < 2.3 && blue > 2.0)) 
                {
                    isTestCheckSprayVoltage = true;
                }
            }
        }

        /// <summary>
        /// 判断喷雾频率是否合格
        /// </summary>
        private bool isTestCheckSprayCoun = false;

        /// <summary>
        /// 检测喷雾频率（P1）
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="freq"></param>
        private void TestCheckSprayCount(int channel, int freq)
        {
            if (channel == 0)
            {
                Console.WriteLine("喷雾频率P1 = " + freq);
                LogHelper.LogInfo("\t\t喷雾频率P1 = " + freq);
                if (freq < 120000 && freq > 100000)
                {
                    isTestCheckSprayCoun = true;
                    LogHelper.LogInfo("喷雾频率检测合格：" + freq);
                }
            }
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
        Charging = 1,
        Discharging = 0
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
        Cur_0A = 1,
        Cur_1A = 0
    }

    /// <summary>
    /// 测试放电电流枚举
    /// </summary>
    public enum enumTestDischargingCurrent
    {
        Cur_0A,
        Cur_1A,
        Cur_3A,
        Cur_4A
    }
    #endregion
}
