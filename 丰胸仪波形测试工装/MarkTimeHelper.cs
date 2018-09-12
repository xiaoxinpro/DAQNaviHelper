using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 丰胸仪波形测试工装
{
    public class MarkTimeHelper
    {

        #region 字段

        #endregion

        #region 属性
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public string MarkName { get; set; }
        #endregion

        #region 构造函数
        public MarkTimeHelper(string mark = "")
        {
            MarkName = mark;
            markTime(MarkTimeStatus.Start);
        }

        #endregion

        #region 私有函数
        /// <summary>
        /// 时间管理函数
        /// </summary>
        /// <param name="state">
        ///     Start  -> 开始计时
        ///     End    -> 结束计时并返回时间间隔
        ///     Reload -> 重启计时并返回上次时间间隔 
        /// </param>
        /// <returns></returns>
        private TimeSpan markTime(MarkTimeStatus state)
        {
            if (state == MarkTimeStatus.Start)
            {
                StartTime = new TimeSpan(DateTime.Now.Ticks);
                return StartTime;
            }
            else if(state == MarkTimeStatus.Reload)
            {
                TimeSpan timeSpan = markTime(MarkTimeStatus.End);
                markTime(MarkTimeStatus.Start);
                return timeSpan;
            }
            else
            {
                EndTime = new TimeSpan(DateTime.Now.Ticks);
                return EndTime.Subtract(StartTime).Duration();
            }
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 获取时间间隔
        /// </summary>
        /// <param name="state">结束方式</param>
        ///     Start  -> 开始计时
        ///     End    -> 结束计时并返回时间间隔
        ///     Reload -> 重启计时并返回上次时间间隔 
        /// <returns>返回结果</returns>
        public TimeSpan MarkTime(MarkTimeStatus state = MarkTimeStatus.Reload)
        {
            return markTime(state);
        }

        /// <summary>
        /// 获取时间间隔字符串
        /// </summary>
        /// <param name="state">结束方式</param>
        ///     Start  -> 开始计时
        ///     End    -> 结束计时并返回时间间隔
        ///     Reload -> 重启计时并返回上次时间间隔 
        /// <returns>返回结果</returns>
        public string StrMarkTime(MarkTimeStatus state = MarkTimeStatus.Reload)
        {
            string spanTime;
            TimeSpan ts = markTime(state);
            if (ts.Days > 0)
            {
                spanTime = ts.Days.ToString() + "天" + ts.Hours.ToString() + "时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒" + ts.Milliseconds.ToString() + "毫秒";
            }
            else if (ts.Hours > 0)
            {
                spanTime = ts.Hours.ToString() + "时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒" + ts.Milliseconds.ToString() + "毫秒";
            }
            else if (ts.Minutes > 0)
            {
                spanTime = ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒" + ts.Milliseconds.ToString() + "毫秒";
            }
            else if (ts.Seconds > 0)
            {
                spanTime = ts.Seconds.ToString() + "秒" + ts.Milliseconds.ToString() + "毫秒";
            }
            else
            {
                spanTime = ts.Milliseconds.ToString() + "毫秒";
            }
            return spanTime;
        }
        #endregion

    }

    /// <summary>
    /// 时间管理状态
    /// </summary>
    public enum MarkTimeStatus
    {
        Start,
        Reload,
        End,
    }
}
