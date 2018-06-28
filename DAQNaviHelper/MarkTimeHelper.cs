using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAQNavi
{
    public class MarkTimeHelper
    {
        private static TimeSpan startTime;
        private static TimeSpan endTime;
        /// <summary>
        /// 记时
        /// </summary>
        /// <param name="state">0：开始计时；1：计时结束</param>
        /// <param name="type">随便填字符串,用于区分记录,不影响功能</param>
        /// <param name="str">随便填字符串,用于区分记录,不影响功能</param>
        public static TimeSpan MarkTime(MarkTimeStatus state, string type = "", string str = "")
        {
            if (state == MarkTimeStatus.Start)
            {
                //记录调用接口前的时间  
                startTime = new TimeSpan(DateTime.Now.Ticks);
                //Console.WriteLine("开始记录接口" + str + "的相应时间：");
                return startTime;
            }
            else
            {
                //记录调用接口后的时间  
                endTime = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts = endTime.Subtract(startTime).Duration();
                //计算时间间隔，求出调用接口所需要的时间  
                String spanTime;
                if (ts.Minutes > 0)
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
                //打印时间  
                //if (ts.Minutes > 1 || ts.Seconds > 5)
                //{
                //    Logger.Info(string.Format("{0}接口耗时:{1}", str, spanTime));
                //}
                Console.WriteLine(str + type + "耗时:" + spanTime);
                return ts;
            }
        }
    }

    public enum MarkTimeStatus
    {
        Start,
        End,
    }
}
