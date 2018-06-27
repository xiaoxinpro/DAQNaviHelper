using Automation.BDaq;
using DAQNaviHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DAQNaviHelper.Device
{
    public class USB4704 : InterfaceDevice
    {
        #region 字段
        //USB4704字段
        private WaveformAiCtrl waveformAiCtrlUsb4704 = new WaveformAiCtrl();
        private InstantDiCtrl instantDiCtrlUsb4704 = new InstantDiCtrl();
        private InstantDoCtrl instantDoCtrlUsb4704 = new InstantDoCtrl();
        private EventCounterCtrl eventCounterCtrlUsb4704 = new EventCounterCtrl();

        //模拟输入字段
        private double[] arrAiData;
        private Int32 numTimeAi;
        private System.Timers.Timer timerAi;
        private Queue queueAiData;
        private AiModeType aiModeData;

        //数字输入字段
        private byte[] arrDiData;
        private System.Timers.Timer timerDi;
        #endregion

        #region 构造函数
        public USB4704()
        {

        }
        #endregion

        #region 实现接口
        /// <summary>
        /// 初始化驱动
        /// </summary>
        public bool InitDevice(DeviceInformation deviceInformation, out string message)
        {
            message = "";
            try
            {
                //绑定设备
                waveformAiCtrlUsb4704.SelectedDevice = deviceInformation;
                instantDiCtrlUsb4704.SelectedDevice = deviceInformation;
                instantDoCtrlUsb4704.SelectedDevice = deviceInformation;
                eventCounterCtrlUsb4704.SelectedDevice = deviceInformation;

                //初始化
                InitWaveformAiCtrlUsb4704();
                InitInstantDiCtrlUsb4704();
                InitInstantDoCtrlUsb4704();

            }
            catch (Exception error)
            {
                message = error.Message;
                ActiveEventError(message);
                return false;
            }
            return true;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 模拟输入事件
        /// </summary>
        public event DAQNaviHelper.DelegateAiEvent EventAi;
        public void ActiveEventAi(AiModeType aiModeData)
        {
            EventAi?.Invoke(aiModeData);
        }

        /// <summary>
        /// 数字输入状态改变事件
        /// </summary>
        public event DAQNaviHelper.DelegateDiChangeEvent EventDiChange;
        public void ActiveEventDiChange(int bit, byte data)
        {
            EventDiChange?.Invoke(bit, data);
        }

        /// <summary>
        /// 异常事件
        /// </summary>
        public event DAQNaviHelper.DelegateErrorEvent EventError;
        private void ActiveEventError(string message)
        {
            EventError?.Invoke(message);
        }


        #endregion

        #region 模拟输入
        /// <summary>
        /// 设置模拟输入模式
        /// </summary>
        /// <param name="mode">模式</param>
        /// <param name="e">事件入口</param>
        /// <param name="timers">检测时间（秒）</param>
        public void StartAiMode(DAQNaviHelper.DelegateAiEvent e, double timers = 1, bool isAutoReset = false)
        {
            try
            {
                aiModeData = new AiModeType();
                EventAi = e;
                numTimeAi = (timers > 0.3) ? Convert.ToInt32(timers * 1000) : 300;
                queueAiData.Clear();

                timerAi.Interval = numTimeAi;
                timerAi.AutoReset = isAutoReset;
                timerAi.Enabled = true;

                ErrorCode err = waveformAiCtrlUsb4704.Prepare();
                if (err == ErrorCode.Success)
                {
                    err = waveformAiCtrlUsb4704.Start();
                }
                if (err != ErrorCode.Success)
                {
                    ActiveEventError("设置模拟输入功能开启失败：" + err.ToString());
                    return;
                }
            }
            catch (Exception error)
            {
                ActiveEventError("设置模拟输入模式失败：" + error.Message);
            }

        }

        /// <summary>
        /// 关闭模拟输入模式
        /// </summary>
        public void StopAiMode()
        {
            queueAiData.Clear();
            timerAi.Enabled = false;
            ErrorCode err = waveformAiCtrlUsb4704.Stop();
            if (err != ErrorCode.Success)
            {
                Console.WriteLine("关闭模拟输入失败：" + err);
                return;
            }
        }

        /// <summary>
        /// 模拟输入功能初始化
        /// </summary>
        private void InitWaveformAiCtrlUsb4704()
        {
            waveformAiCtrlUsb4704.Conversion.ClockRate = 1024; //AD转换频率（32-47619）
            waveformAiCtrlUsb4704.Record.SectionLength = 8;    //采样缓存区大小（保证通信间隔大于100us）
            waveformAiCtrlUsb4704.Conversion.ChannelStart = 0;  //采样起始通道
            waveformAiCtrlUsb4704.Conversion.ChannelCount = 8;  //采样通道数
            waveformAiCtrlUsb4704.DataReady += new EventHandler<BfdAiEventArgs>(waveformAiCtrlUsb4704_DataReady);
            int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
            int sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
            arrAiData = new double[chanCount * sectionLength];  //初始化缓存
            if (!waveformAiCtrlUsb4704.Initialized)
            {
                ActiveEventError("设置模拟输入模式失败：USB4704驱动不存在。");
                return;
            }

            timerAi = new System.Timers.Timer();
            timerAi.Elapsed += new ElapsedEventHandler(TimerAi_Timer);

            queueAiData = Queue.Synchronized(new Queue());
        }

        /// <summary>
        /// 模拟输入接收事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveformAiCtrlUsb4704_DataReady(object sender, Automation.BDaq.BfdAiEventArgs e)
        {
            try
            {
                if (waveformAiCtrlUsb4704.State == ControlState.Idle)
                {
                    return;
                }

                if (arrAiData.Length < e.Count)
                {
                    arrAiData = new double[e.Count];
                }

                int chanCount = waveformAiCtrlUsb4704.Conversion.ChannelCount;
                int sectionLength = waveformAiCtrlUsb4704.Record.SectionLength;
                ErrorCode err = waveformAiCtrlUsb4704.GetData(e.Count, arrAiData);
                if (err == ErrorCode.WarningFuncStopped || err == ErrorCode.WarningFuncTimeout)
                {
                    Console.WriteLine("发生1次" + err + "警告");
                    return;
                }
                if (err != ErrorCode.Success && err != ErrorCode.WarningRecordEnd)
                {
                    ActiveEventError("模拟输入接收失败：" + err.ToString());
                    return;
                }

                for (int i = 0; i < sectionLength; i++)
                {
                    double[] arrData = new double[chanCount];
                    for (int j = 0; j < chanCount; j++)
                    {
                        int cnt = i * chanCount + j;
                        arrData[j] = arrAiData[cnt];
                    }
                    queueAiData.Enqueue(arrData);
                }

            }
            catch (Exception error)
            {
                ActiveEventError("模拟输入中断异常：" + error.Message);
            }
        }

        /// <summary>
        /// 模拟输入接收处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerAi_Timer(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (queueAiData.Count > 0)
                {
                    Console.WriteLine("模拟输入采集完成，共计：" + queueAiData.Count);
                    if (!timerAi.Enabled)
                    {
                        waveformAiCtrlUsb4704.Stop();
                    }
                    int cntArray = ((double[])queueAiData.Peek()).Length;
                    aiModeData.Max = new double[cntArray];
                    aiModeData.Min = new double[cntArray];
                    aiModeData.Avg = new double[cntArray];
                    aiModeData.Real = new double[cntArray][];
                    for (int i = 0; i < cntArray; i++)
                    {
                        aiModeData.Real[i] = new double[queueAiData.Count];
                    }
                    int cnt = 0;
                    while (queueAiData.Count > 0)
                    {
                        double[] item = (double[])queueAiData.Dequeue();
                        for (int i = 0; i < item.Length; i++)
                        {
                            aiModeData.Real[i][cnt] = item[i];
                        }
                        cnt++;
                    }

                    for (int i = 0; i < cntArray; i++)
                    {
                        aiModeData.Max[i] = aiModeData.Real[i].Max();
                        aiModeData.Min[i] = aiModeData.Real[i].Min();
                        aiModeData.Avg[i] = aiModeData.Real[i].Average();
                    }

                    Console.WriteLine("模拟输入计算完成！");
                    ActiveEventAi(aiModeData);
                }
            }
            catch (Exception error)
            {
                ActiveEventError("模拟输入数据处理失败：" + error.Message);
            }

        }
        #endregion

        #region 数字输入
        public byte[] StartDiMode()
        {
            ErrorCode err = instantDiCtrlUsb4704.Read(0, out byte portData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("开启数字输入失败：" + err.ToString());
                return arrDiData;
            }
            timerDi.Enabled = true;
            arrDiData = new byte[8];
            for (int i = 0; i < arrDiData.Length; i++)
            {
                arrDiData[i] = Convert.ToByte((portData >> i) & 0x01);
            }
            return arrDiData;
        }

        public void StopDiMode()
        {
            timerDi.Enabled = false;
        }

        private void InitInstantDiCtrlUsb4704()
        {
            timerDi = new System.Timers.Timer(50);
            timerDi.Elapsed += new ElapsedEventHandler(TimerDi_TimerEvent);
            timerDi.AutoReset = true;
            timerDi.Enabled = false;
        }

        private void TimerDi_TimerEvent(object sender, ElapsedEventArgs e)
        {
            ErrorCode err = instantDiCtrlUsb4704.Read(0, out byte portData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("检测数字输入失败：" + err.ToString());
                return;
            }
            for (int i = 0; i < arrDiData.Length; i++)
            {
                byte bitData = Convert.ToByte((portData >> i) & 0x01);
                if (arrDiData[i] != bitData)
                {
                    arrDiData[i] = bitData;
                    ActiveEventDiChange(i, bitData);
                }
            }
        }
        #endregion

        #region 数字输出
        public bool SetDoMode(byte byteData)
        {
            ErrorCode err = instantDoCtrlUsb4704.Write(0, byteData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("初始化数字输出失败：" + err.ToString());
                return false;
            }
            return true;
        }

        public bool SetDoModeBit(int bit, byte data)
        {
            ErrorCode err = instantDoCtrlUsb4704.WriteBit(0, bit, data);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("初始化数字输出失败：" + err.ToString());
                return false;
            }
            return true;
        }

        private void InitInstantDoCtrlUsb4704()
        {
            ErrorCode err = instantDoCtrlUsb4704.Write(0, 0xFF);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("初始化数字输出失败：" + err.ToString());
            }

        }
        #endregion

        public void Test(object e)
        {
            ActiveEventError("测试异常事件：" + e.ToString());
        }

    }
}
