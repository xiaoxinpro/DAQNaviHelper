using Automation.BDaq;
using DAQNavi.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;

namespace DAQNavi
{
    public class DAQNaviHelper
    {

        #region 字段
        //private InterfaceDevice IDevice;
        #endregion

        #region 属性
        public DeviceInformation DeviceInformation { get; private set; }
        public DAQNaviDecvice DeviceType { get; private set; }
        public InterfaceDevice IDevice{get; set;}
        #endregion

        #region 构造函数
        public DAQNaviHelper()
        {

        }

        public DAQNaviHelper(int deviceNumber):this()
        {
            SetDeviceInformation(deviceNumber);
        }

        public DAQNaviHelper(string description):this()
        {
            SetDeviceInformation(description);
        }

        #endregion

        #region 事件
        /// <summary>
        /// 异常事件委托
        /// </summary>
        /// <param name="message"></param>
        public delegate void DelegateErrorEvent(string message);

        /// <summary>
        /// 绑定异常事件
        /// </summary>
        /// <param name="errorEvent"></param>
        public void BindErrorEvent(DelegateErrorEvent errorEvent)
        {
            if (DeviceType != DAQNaviDecvice.None && IDevice != null)
            {
                IDevice.EventError += errorEvent;
            }
        }

        /// <summary>
        /// 模拟输入委托
        /// </summary>
        /// <param name="arrData"></param>
        public delegate void DelegateAiEvent(AiModeType aiModeData);

        /// <summary>
        /// 绑定模拟输入事件
        /// </summary>
        /// <param name="e"></param>
        public void BindAiEvent(DelegateAiEvent e)
        {
            if (DeviceType != DAQNaviDecvice.None && IDevice != null)
            {
                IDevice.EventAi += e;
            }
        }

        /// <summary>
        /// 数字输入状态改变委托
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="data"></param>
        public delegate void DelegateDiChangeEvent(int bit, byte data);

        /// <summary>
        /// 绑定输入状态改变事件
        /// </summary>
        /// <param name="e"></param>
        public void BindDiChangeEvent(DelegateDiChangeEvent e)
        {
            if (DeviceType != DAQNaviDecvice.None && IDevice != null)
            {
                IDevice.EventDiChange += e;
            }
        }

        #endregion

        #region 公共函数
        /// <summary>
        /// 初始化驱动
        /// </summary>
        /// <param name="message">返回信息</param>
        /// <returns>是否成功</returns>
        public bool InitDevice(out string message)
        {
            bool isSuccess = false;
            message = "发生未知错误。";
            switch (DeviceType)
            {
                case DAQNaviDecvice.USB4704:
                    IDevice = new USB4704();
                    isSuccess = IDevice.InitDevice(DeviceInformation, out message);
                    break;
                case DAQNaviDecvice.USB4751:
                    break;
                case DAQNaviDecvice.None:
                default:
                    message = "无法获取驱动类型。";
                    isSuccess = false;
                    break;
            }
            return isSuccess;
        }

        /// <summary>
        /// 设置驱动信息
        /// </summary>
        /// <param name="deviceNumber">驱动号</param>
        public void SetDeviceInformation(int deviceNumber)
        {
            DeviceInformation = new DeviceInformation(deviceNumber);
            SetDeviceType(DeviceInformation.Description);
        }

        /// <summary>
        /// 设置驱动信息
        /// </summary>
        /// <param name="description">驱动描述</param>
        public void SetDeviceInformation(string description)
        {
            DeviceInformation = new DeviceInformation(description);
            SetDeviceType(DeviceInformation.Description);
        }

        public void Test(object e)
        {
            IDevice.Test(e);
        }

        #endregion

        #region 静态函数
        /// <summary>
        /// 设置驱动类型
        /// </summary>
        /// <param name="strDevice"></param>
        private void SetDeviceType(string strDevice)
        {
            if (strDevice == null)
            {
                DeviceType = DAQNaviDecvice.None;
            }
            else if (strDevice.Contains("USB-4704"))
            {
                DeviceType = DAQNaviDecvice.USB4704;
            }
            else if (strDevice.Contains("USB-4751"))
            {
                DeviceType = DAQNaviDecvice.USB4751;
            }
            else
            {
                DeviceType = DAQNaviDecvice.None;
            }
        }
        #endregion

        #region ?
        #endregion
    }

    /// <summary>
    /// 研华驱动类型枚举
    /// </summary>
    public enum DAQNaviDecvice
    {
        None,
        USB4704,
        USB4751
    }

    public struct AiModeType
    {
        public double[] Max;
        public double[] Min;
        public double[] Avg;
        public double[][] Real;
    }
}
