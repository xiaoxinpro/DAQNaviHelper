using Automation.BDaq;
using DAQNavi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace DAQNavi.Device
{
    public class USB4704 : InterfaceDevice
    {
        #region 字段
        //USB4704字段
        private WaveformAiCtrl waveformAiCtrlUsb4704 = new WaveformAiCtrl();
        private InstantDiCtrl instantDiCtrlUsb4704 = new InstantDiCtrl();
        private InstantDoCtrl instantDoCtrlUsb4704 = new InstantDoCtrl();
        private EventCounterCtrl eventCounterCtrlUsb4704 = new EventCounterCtrl();
        private InstantAoCtrl instantAoCtrlUsb4704 = new InstantAoCtrl();

        //模拟输入字段
        private double[] arrAiData;
        private Int32 numTimeAi;
        private System.Timers.Timer timerAi;
        private Queue queueAiData;
        private AiModeType aiModeData;

        //数字输入字段
        private byte[] arrDiData;
        private System.Timers.Timer timerDi;

        //脉冲计数器字段
        private System.Timers.Timer timerCnt;

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
                instantAoCtrlUsb4704.SelectedDevice = deviceInformation;

                //初始化
                InitWaveformAiCtrlUsb4704();
                InitInstantDiCtrlUsb4704();
                InitInstantDoCtrlUsb4704();
                InitEventCounterCtrlUsb4704();

            }
            catch (Exception error)
            {
                message = error.Message;
                ActiveEventError(message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭驱动
        /// </summary>
        public void CloseDevice()
        {
            timerAi.Enabled = false;
            timerDi.Enabled = false;
            timerCnt.Enabled = false;
            waveformAiCtrlUsb4704.Dispose();
            instantDiCtrlUsb4704.Dispose();
            instantDoCtrlUsb4704.Dispose();
            eventCounterCtrlUsb4704.Dispose();
            instantAoCtrlUsb4704.Dispose();
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
        /// 脉冲计数事件
        /// </summary>
        public event DAQNaviHelper.DelegateCntEvent EventCnt;
        public void ActiveEventCnt(int channel, int freq)
        {
            EventCnt?.Invoke(channel, freq);
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
        /// <param name="e">事件入口</param>
        /// <param name="timers">检测时间（秒）</param>
        /// <param name="isAutoReset">是否循环获取</param>
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
            waveformAiCtrlUsb4704.Conversion.ClockRate = 38400; //AD转换频率（32-47619）
            waveformAiCtrlUsb4704.Record.SectionLength = 512;    //采样缓存区大小（保证通信间隔大于100us）
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
        public byte[] StartDiMode(DAQNaviHelper.DelegateDiChangeEvent e)
        {
            ErrorCode err = instantDiCtrlUsb4704.Read(0, out byte portData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("开启数字输入失败：" + err.ToString());
                return arrDiData;
            }
            EventDiChange = e;
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

        public bool GetDiMode(out byte[] arrPortData)
        {
            arrPortData = new byte[8];
            ErrorCode err = instantDiCtrlUsb4704.Read(0, out byte portData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("获取数字输入失败：" + err.ToString());
                return false;
            }
            for (int i = 0; i < arrPortData.Length; i++)
            {
                arrPortData[i] = Convert.ToByte((portData >> i) & 0x01);
            }
            return true;
        }

        public bool GetDiModeBit(int bit, byte portData)
        {
            portData = new byte();
            ErrorCode err =  instantDiCtrlUsb4704.ReadBit(0, bit, out byte data);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("获取数字输入失败：" + err.ToString());
                return false;
            }
            portData = data;
            return true;
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
        public bool SetDoMode(byte[] arrPortData)
        {
            byte byteData = 0x00;
            for (int i = 0; i < arrPortData.Length; i++)
            {
                byteData |= Convert.ToByte(arrPortData[i] << i);
            }
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

        public bool GetDoMode(out byte[] arrByteData)
        {
            arrByteData = new byte[8];
            ErrorCode err = instantDoCtrlUsb4704.Read(0, out byte portData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("获取数字输出失败：" + err.ToString());
                return false;
            }
            for (int i = 0; i < arrByteData.Length; i++)
            {
                arrByteData[i] = Convert.ToByte((portData >> i) & 0x01);
            }
            return true;
        }

        public bool GetDoModeBit(int bit, out byte bitData)
        {
            ErrorCode err = instantDoCtrlUsb4704.ReadBit(0, bit, out bitData);
            if (err != ErrorCode.Success)
            {
                ActiveEventError("获取数字输出失败：" + err.ToString());
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

        #region 脉冲计数器
        /// <summary>
        /// 开启脉冲计数模式
        /// </summary>
        /// <param name="e">事件入口</param>
        /// <param name="timers">采样周期（秒）</param>
        public void StartCntMode(DAQNaviHelper.DelegateCntEvent e, double timers = 1)
        {
            try
            {
                EventCnt = e;
                timerCnt.Interval = (timers > 0.1 ? timers : 0.1) * 1000;
                eventCounterCtrlUsb4704.Enabled = true;
                timerCnt.Enabled = true;
                MarkTimeHelper.MarkTime(MarkTimeStatus.Start, "计数定时器");
            }
            catch (Exception error)
            {
                ActiveEventError("开启脉冲计数失败：" + error.Message);
            }
        }

        /// <summary>
        /// 关闭脉冲计数模式
        /// </summary>
        public void StopCntMode()
        {
            try
            {
                timerCnt.Enabled = false;
                eventCounterCtrlUsb4704.Enabled = false;
            }
            catch (Exception error)
            {
                ActiveEventError("关闭脉冲计数失败：" + error.Message);
            }
        }

        /// <summary>
        /// 初始化脉冲计数
        /// </summary>
        private void InitEventCounterCtrlUsb4704()
        {
            eventCounterCtrlUsb4704.ChannelStart = 0;
            eventCounterCtrlUsb4704.ChannelCount = 1;
            eventCounterCtrlUsb4704.Enabled = false;

            timerCnt = new System.Timers.Timer(1000);
            timerCnt.Elapsed += new ElapsedEventHandler(TimerCnt_TimerEvent);
            timerCnt.AutoReset = true;
            timerCnt.Enabled = false;
        }

        /// <summary>
        /// 脉冲计数定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerCnt_TimerEvent(object sender, ElapsedEventArgs e)
        {
            ErrorCode err = eventCounterCtrlUsb4704.Read(out int cntData);
            eventCounterCtrlUsb4704.Enabled = false;
            TimeSpan timeSpan = MarkTimeHelper.MarkTime(MarkTimeStatus.End, err + "计数定时器", cntData.ToString());
            eventCounterCtrlUsb4704.Enabled = true;
            MarkTimeHelper.MarkTime(MarkTimeStatus.Start, "计数定时器");
            if (err != ErrorCode.Success)
            {
                ActiveEventError("采集脉冲数失败：" + err.ToString());
                return;
            }

            if ((timeSpan.TotalSeconds * 1000) < (timerCnt.Interval + 50))
            {
                ActiveEventCnt(0, Convert.ToInt32(cntData / timeSpan.TotalSeconds));
            }
        }
        #endregion

        #region 模拟输出
        /// <summary>
        /// 设置模拟输出电压
        /// </summary>
        /// <param name="ch">通道号0-1</param>
        /// <param name="data">电压值0-5V</param>
        /// <returns>是否设置成功</returns>
        public bool SetAoMode(int ch, double data)
        {
            if (ch < instantAoCtrlUsb4704.ChannelCount && data >= 0 && data <= 5) 
            {
                ErrorCode err = instantAoCtrlUsb4704.Write(ch, data);
                if (err != ErrorCode.Success)
                {
                    ActiveEventError("设置模拟输出失败：" + err.ToString());
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        public void Test(object e)
        {
            ActiveEventError("测试异常事件：" + e.ToString());
        }

    }
}
