using DAQNavi;
using SerialPortHelperLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 丰胸仪测试工装
{
    public partial class frmMain : Form
    {
        #region 常量
        private static string[] STR_TEST_STATUS = { "待测", "运行", "暂停", "成功", "失败" };
        private static string[] STR_TEST_NAME = {
            "丰胸仪测试1", "丰胸仪测试2", "丰胸仪测试3", "丰胸仪测试4", "丰胸仪测试5", "丰胸仪测试6",
            "丰胸仪测试7"
        };
        private static string[] STR_TEST_MARK = {
            "丰胸仪测试内容...",   //丰胸仪测试
            "丰胸仪测试内容...",     //丰胸仪测试
            "丰胸仪测试内容...",     //丰胸仪测试
            "丰胸仪测试内容...",     //丰胸仪测试
            "丰胸仪测试内容...",       //丰胸仪测试
            "丰胸仪测试内容...",       //丰胸仪测试
            "丰胸仪测试内容..."     //丰胸仪测试
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

        //输出AO
        private const int AO_DA0 = 0;
        private const int AO_DA1 = 1;
        #endregion

        #region 字段
        private int nowTestItem = 0;
        private bool isTestRun = false;
        private DAQNaviHelper USB4704;
        private AiModeType AdData;
        private int CntTimes = 0;

        //定义AppConfig类
        private AppConfig appConfig;

        //定义ConfigCom类
        private ConfigCom configCom;

        //定义串口蓝牙类
        private SerialBle serialBle;

        //定义SerialPortHelper类
        private SerialPortHelper serialPortHelper;

        //定义丰胸仪蓝牙类
        private BreastBle breastBle;
        #endregion

        #region 初始化
        public frmMain()
        {
            InitializeComponent();

            //版本号显示
            this.Text += @" V" + Application.ProductVersion.ToString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //初始化配置
            appConfig = new AppConfig();

            //初始化界面
            InitLabelStatus(labelStatus);
            InitProgressStatus(progressBarStatus);
            InitListViewStatus(listViewStatus);
            InitToolTipMain(toolTipMain);

            //初始化串口配置控件
            InitSerialBle();

            //初始化串口助手
            InitSerialPortHelper();

            //初始化丰胸仪蓝牙模块
            InitBreastBle();

            //初始化硬件驱动
            InitUsb4704();
        }

        /// <summary>
        /// 初始化浮动文字框
        /// </summary>
        private void InitToolTipMain(ToolTip toolTip)
        {
            toolTip.SetToolTip(checkAutoLinkBle, "搜索到可用蓝牙后自动连接");
        }

        /// <summary>
        /// 初始化串口助手
        /// </summary>
        private void InitSerialPortHelper()
        {
            //实例化串口配置
            configCom = new ConfigCom(comboSerial);
            configCom.PortName = comboSerial.Text;
            configCom.BaudRate = 115200;
            configCom.DataBits = 8;
            configCom.StopBits = StopBits.One;
            configCom.Parity = Parity.None;

            //设置串口搜索默认值
            configCom.SetSerialPortDefaultInfo("USB 串行设备"); //TI CC2540 USB CDC Serial Port
            configCom.AddSerialPortDefaultInfo("TI CC2540 USB CDC Serial Port");

            //实例化串口助手
            serialPortHelper = new SerialPortHelper(configCom.GetConfigComData());
            serialPortHelper.BindSerialPortDataReceivedProcessEvent(new SerialPortHelper.DelegateSerialPortDataReceivedProcessEvent(SerialPortDataReceivedProcess));
            serialPortHelper.BindSerialPortErrorEvent(new SerialPortHelper.DelegateSerialPortErrorEvent(SerialPortErrorProcess));
            serialPortHelper.SerialReceviedTimeInterval = 40;
            serialPortHelper.SerialWriteTimeInterval = 200;
            serialPortHelper.SerialReceviedLengthMax = 1024;
        }

        /// <summary>
        /// 初始化串口配置控件
        /// </summary>
        private void InitSerialBle()
        {
            serialBle = new SerialBle(toolComboBle, new SerialBle.DelegateBleSerialWrite(AddSerialWrite));
            serialBle.EventBleLog += OutputBleLog;
            serialBle.IsAutoLink = checkAutoLinkBle.Checked;
        }

        /// <summary>
        /// 初始化丰胸仪蓝牙类
        /// </summary>
        private void InitBreastBle()
        {
            breastBle = new BreastBle(listViewReceiveData);
            breastBle.EventAddCmdWrite += BreastBle_EventAddCmdWrite;
            breastBle.EventChangeReceivedData += BreastBle_EventChangeReceivedData;
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
            label.Text = "准备就绪";
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

        /// <summary>
        /// 设置进度条是否完成
        /// </summary>
        /// <param name="isDone"></param>
        private void SetProgressStatus(bool isDone)
        {
            progressBarStatus.Value = isDone ? progressBarStatus.Maximum : progressBarStatus.Minimum;
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
            listView.Columns.Add("状态     ", 75, HorizontalAlignment.Center);
            listView.Columns.Add("测试项目", 150, HorizontalAlignment.Center);
            listView.Columns.Add("测试内容", 375, HorizontalAlignment.Left);

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

            //自适应宽度
            foreach (ColumnHeader ch in listView.Columns) { ch.Width = -2; }
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

        /// <summary>
        /// 双击列表单独测试子项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewStatus_DoubleClick(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItem listViewItem = listView.SelectedItems[0];
            if (listViewItem.Index >= 0)
            {
                Console.WriteLine("触发单独测试项目：" + listViewItem.SubItems[1].Text);
                StopTest();
                InitIO();
                SetTest(listViewItem.Index);
                StartTest();
            }
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
                SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试中", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Run);
                SetProgressStatus(progressBarStatus, num * 10);
            }
        }

        /// <summary>
        /// 设置运行状态
        /// </summary>
        /// <param name="num">测试号</param>
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

        /// <summary>
        /// 设置成功状态
        /// </summary>
        /// <param name="num">测试号</param>
        private void SetSuccessStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                //SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试通过", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Success);
                SetProgressStatus(progressBarStatus, (num + 1) * 10);
            }
        }

        /// <summary>
        /// 设置失败状态
        /// </summary>
        /// <param name="num">测试号</param>
        private void SetFailStatus(int num)
        {
            if (STR_TEST_NAME.Length > num)
            {
                //SetLabelStatus(labelStatus, "项目【" + STR_TEST_NAME[num] + "】测试失败", enumTestStatus.Run);
                SetListViewItemStatus(listViewStatus, num, enumTestStatus.Fail);
                SetProgressStatus(progressBarStatus, (num + 1) * 10);
            }
        }
        #endregion

        #region 串口事件
        /// <summary>
        /// 串口错误事件
        /// </summary>
        /// <param name="enumError"></param>
        /// <param name="strError"></param>
        private void SerialPortErrorProcess(enumSerialError enumError, string strError)
        {
            switch (enumError)
            {
                case enumSerialError.LinkError:
                    serialPortHelper.CloseCom(out string str);
                    Console.WriteLine("串口错误：" + strError);
                    break;
                case enumSerialError.WriteError:
                    Console.WriteLine("发送错误：" + strError);
                    break;
                case enumSerialError.ReceivedError:
                    Console.WriteLine("接收错误：" + strError);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 串口接收数据事件
        /// </summary>
        /// <param name="arrDataReceived"></param>
        private void SerialPortDataReceivedProcess(byte[] arrData)
        {
            this.Invoke(new Action(() =>
            {
                if (!SerialData.IsBytesToString(arrData))
                {
                    Console.WriteLine("接收数据：" + SerialData.ToHexString(arrData));
                    breastBle.BytesReceviedDataProcess(arrData);
                }
                else
                {
                    string strData = SerialData.ToString(arrData);
                    Console.WriteLine("接收数据：" + strData);
                    if (strData.IndexOf("OK") >= 0)
                    {
                        serialBle.ReceiveSerialBle(strData);
                    }
                }
            }));
        }
        #endregion

        #region 串口方法

        /// <summary>
        /// 添加发送数据
        /// </summary>
        /// <param name="arrData">byte数组数据</param>
        private void AddSerialWrite(byte[] arrData)
        {
            try
            {
                if (arrData.Length < 1 || !serialPortHelper.IsOpen)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

            serialPortHelper.Write(arrData);
        }

        /// <summary>
        /// 添加发送数据
        /// </summary>
        /// <param name="strData">字符串数据</param>
        private void AddSerialWrite(string strData)
        {
            if (strData.Length > 0)
            {
                AddSerialWrite(SerialData.ToByteArray(strData));
            }
        }

        /// <summary>
        /// 添加发送数据
        /// </summary>
        /// <param name="arrData">数组数据</param>
        private void AddSerialWrite(string[] arrData)
        {
            foreach (string item in arrData)
            {
                AddSerialWrite(item);
            }
        }

        /// <summary>
        /// 蓝牙输出时间
        /// </summary>
        /// <param name="strLog"></param>
        private void OutputBleLog(string strLog)
        {
            Console.WriteLine("蓝牙日志：" + strLog);
            if (labelBleStatus.Text != strLog)
            {
                labelBleStatus.Text = strLog;
            }
        }
        #endregion

        #region 丰胸仪蓝牙函数
        /// <summary>
        /// 丰胸仪蓝牙接收数据发生改变
        /// </summary>
        /// <param name="arrData"></param>
        private void BreastBle_EventChangeReceivedData(byte[] arrData)
        {
            //throw new NotImplementedException();
            //if (breastBle.ReceiveData.Mode == EnumReceiveDataMode.OFF)
            //{
            //    breastBle.WriteCmd(EnumCmdWrite.OpenFan);
            //}
            //else if (breastBle.ReceiveData.Mode == EnumReceiveDataMode.Fan)
            //{
            //    breastBle.WriteCmd(EnumCmdWrite.Stop);
            //}
        }

        /// <summary>
        /// 丰胸仪蓝牙发送数据
        /// </summary>
        /// <param name="arrData"></param>
        private void BreastBle_EventAddCmdWrite(byte[] arrData)
        {
            AddSerialWrite(arrData);
        }
        #endregion

        #region USB4704
        /// <summary>
        /// 初始化USB-4704
        /// </summary>
        private void InitUsb4704()
        {
            //初始化设备
            DialogResult dialogResult = DialogResult.Retry;
            while (dialogResult == DialogResult.Retry)
            {
                dialogResult = DialogResult.OK;
                USB4704 = new DAQNaviHelper("USB-4704,BID#0");
                if (!USB4704.InitDevice(out string message))
                {
                    dialogResult = initError(message, "USB-4704初始化失败");
                    if (dialogResult == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                }
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
        private DialogResult initError(string message, string title)
        {
            LogHelper.LogError(title + message);
            if (message.Contains("The device is not available")) //该设备不可用
            {
                return MessageBox.Show("该设备不可用，请检查测试工装是否连接，或是否被其他程序占用。", title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            else
            {
                return MessageBox.Show(message, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            //Application.Exit();
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
            else if (message.Contains("索引超出了数组界限"))
            {
                LogHelper.LogWarn("设备异常\t" + message);
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
                    //Console.WriteLine("DI_P2 = " + Convert.ToBoolean(data));
                    //if (!Convert.ToBoolean(data) && isTestRun == false)
                    //{
                    //    LogHelper.LogInfo("外部按钮触发");
                    //    Common.CallOnClick(btnInitSwitch);
                    //}
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
            string strTestNumber = GetTestNumber();
            timerTest.Enabled = true;
            LogHelper.LogInfo("测试开始\t测试编码：" + strTestNumber);
            labelTestNumber.Text = strTestNumber;
        }

        /// <summary>
        /// 暂停/停止测试
        /// </summary>
        private void StopTest()
        {
            isTestRun = false;
            timerTest.Enabled = false;
            btnInitSwitch.Enabled = true;
            USB4704.IDevice.StopCntMode();
            USB4704.IDevice.StopAiMode();
            LogHelper.LogInfo("停止测试\r\n");
        }

        /// <summary>
        /// 测试下一项
        /// </summary>
        private void NextTest()
        {
            if (isTestRun)
            {
                SetProgressStatus(true);
                DetectDoneLabelStatus(labelStatus, listViewStatus);
                StopTest();
            }
            else
            {
                nowTestItem++;
                CntTimes = 0;
                timerTest.Interval = 400;
            }
        }

        /// <summary>
        /// 指定测试项目
        /// </summary>
        /// <param name="index"></param>
        private void SetTest(int index)
        {
            isTestRun = true;
            nowTestItem = index;
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
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_4);
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_4A);
                        USB4704.IDevice.StartAiMode(TestCheckShortUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试1\t选择电池4V，放电电流4A，模拟电池短路，检查短路USB电压0V。");
                    }
                    else if (CntTimes > 10)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_0);
                        SelectPower(enumTestPower.Charging);
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
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_1A);
                        USB4704.IDevice.StartAiMode(TestCheckLowUsbVoltage, 0.3, true);
                        LogHelper.LogInfo("开始测试4\t选择电池2.3V，检查低电压USB输出电压。");
                    }
                    else if (CntTimes > 10)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
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
                        USB4704.IDevice.StartAiMode(TestCheckSprayVoltage, 2, true);
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
                    if (vol < 1.5)
                    {
                        SelectDichargingCurrent(enumTestDischargingCurrent.Cur_0A);
                        SelectBatteryVoltage(enumTestBatteryVoltage.Vol4_0);
                        SelectPower(enumTestPower.Charging);
                        USB4704.IDevice.StopAiMode();
                        SetSuccessStatus(nowTestItem);
                        LogHelper.LogInfo("结束测试" + (index + 1) + "\t通过。");
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
                    if ((vol > 5.0 && vol < 5.2) && (vol1 > 2.1 && vol1 < 2.3) && (vol2 > 1.5 && vol2 < 1.7))
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
                    if (vol > 4.0) //4.9
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
                    if (vol < 1.5)
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
                    if (green > 2.6 && green < 3.3) // 3.1-3.3
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
                double bat = aiModeData.Avg[AI_AD0] - aiModeData.Avg[AI_AD7];
                string log = string.Format("喷雾峰值电压AD6*11 = {0:0.000}\t蓝灯电压AD2 = {1:0.000}\t电池电压AD0-AD7 = {2:0.000}", vol, blue, bat);
                Console.WriteLine(log);
                LogHelper.LogInfo("\t\t" + log);
                if ((vol < 45 && vol > 11) && (blue < 2.3 && blue > 1.0)) // 30 - 45 / 1.0 - 2.3
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
                if (freq < 118000 && freq > 106000) // 118K - 109K
                {
                    isTestCheckSprayCoun = true;
                }
            }
        }

        #endregion

        #region 测试编码
        /// <summary>
        /// 获取测试编码
        /// </summary>
        /// <returns>返回测试编码</returns>
        private string GetTestNumber()
        {
            string strNow = DateTime.Now.ToString("yyyyMMdd");
            int num = 1;
            if (appConfig.GetConfig("TestDate") == strNow)
            {
                string strNum = appConfig.GetConfig("TestNumber");
                if (strNum != null)
                {
                    num = Convert.ToInt32(strNum) + 1;
                }
            }
            appConfig.SetConfig("TestDate", strNow);
            appConfig.SetConfig("TestNumber", num.ToString());
            return string.Format("{0} - {1:0000}", strNow, num);
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 开启测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitSwitch_Click(object sender, EventArgs e)
        {
            if (serialPortHelper.IsOpen == false)
            {
                MessageBox.Show("请先开启蓝牙串口并连接丰胸仪蓝牙。", "无法开启", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (serialBle.SerialBleStatus != enumBleStatus.Run)
            {
                MessageBox.Show("请先连接丰胸仪蓝牙。", "无法开启", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InitTest();
            btnInitSwitch.Enabled = false;
        }

        /// <summary>
        /// 强制停止测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestStop_Click(object sender, EventArgs e)
        {
            LogHelper.LogInfo("触发强制停止按钮");
            StopTest();
            InitIO();
            InitLabelStatus(labelStatus);
            InitProgressStatus(progressBarStatus);
            InitListViewStatus(listViewStatus);
        }

        /// <summary>
        /// 日志目录打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelTestNumber_DoubleClick(object sender, EventArgs e)
        {
            LogHelper.OpenLogFilePath();
        }

        /// <summary>
        /// 打开/关闭串口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerialPortSwitch_Click(object sender, EventArgs e)
        {
            if (btnSerialPortSwitch.Text == "打开")
            {
                serialPortHelper.OpenCom(configCom.GetConfigComData(), out string strError);
                if (strError != "null")
                {
                    MessageBox.Show(strError);
                }
                else
                {
                    Console.WriteLine("开启{0}端口成功。", configCom.PortName);
                    toolBleWrite.Enabled = !checkAutoLinkBle.Checked;
                    btnSerialPortSwitch.Text = "关闭";
                    //ClearListViewSerialReceviedValue();
                    AddSerialWrite("AT");
                    //SaveSerialConfig(configCom.GetConfigComData());
                }
            }
            else
            {
                serialPortHelper.CloseCom(out string strError);
                if (strError != "null")
                {
                    MessageBox.Show(strError);
                }
                else
                {
                    OutputBleLog("蓝牙串口已关闭");
                    Console.WriteLine("关闭端口成功。");
                    toolBleWrite.Enabled = false;
                    btnSerialPortSwitch.Text = "打开";
                }
            }
        }

        /// <summary>
        /// 蓝牙命令发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBleWrite_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine("选择蓝牙命令：" + e.ClickedItem.Text);
            serialBle.WriteBleCmd((enumBleCmd)Convert.ToInt32(e.ClickedItem.Tag));
        }

        /// <summary>
        /// 自动连接蓝牙开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAutoLinkBle_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                toolBleWrite.Enabled = false;
                serialBle.IsAutoLink = true;
                if (serialPortHelper.IsOpen && serialBle.SerialBleStatus != enumBleStatus.Run && serialBle.SerialBleStatus != enumBleStatus.Link)
                {
                    serialBle.WriteBleCmd(enumBleCmd.Find);
                }
            }
            else
            {
                toolBleWrite.Enabled = (btnSerialPortSwitch.Text == "关闭");
                serialBle.IsAutoLink = false;
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
