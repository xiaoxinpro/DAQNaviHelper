using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 丰胸仪波形测试工装
{
    /// <summary>
    /// 周期判定类
    /// </summary>
    public class CycleJudge
    {
        #region 字段
        private Queue<double> DataQueue;
        private Queue<double> CycleQueue;
        private Thread JudgeThread;
        private AutoResetEvent aEvent = new AutoResetEvent(false);
        private decimal cntCycleThresholdLength = 0;
        #endregion

        #region 属性
        public decimal CycleBufferLength { get; set; }      //周期缓存长度
        public decimal CycleThresholdVoltage { get; set; }  //周期阈值电压
        public decimal CycleThresholdLength { get; set; }   //周期有效阈值长度
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CycleJudge()
        {
            //字段初始化
            DataQueue = new Queue<double>();
            CycleQueue = new Queue<double>();

            //初始化属性值
            CycleBufferLength = 10000;
            CycleThresholdVoltage = 5;
            CycleThresholdLength = 100;

            //线程初始化
            JudgeThread = new Thread(JudgeThreadProcess);
            JudgeThread.IsBackground = true;
            JudgeThread.Start();

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e">周期判定完成事件</param>
        public CycleJudge(DelegateCycleJudgeDone e) : this()
        {
            EventCycleJudgeDone += e;
        }
        #endregion

        #region 周期判定线程
        private void JudgeThreadProcess()
        {
            double bakCycleData = 0;
            bool isCycleData = false;
            while (true)
            {
                if (DataQueue.Count > 0)
                {
                    //获取队列数据
                    Monitor.Enter(DataQueue);
                    double data = DataQueue.Dequeue();
                    Monitor.Exit(DataQueue);

                    //判定数据有效
                    if (Math.Abs(data) > Convert.ToDouble(CycleThresholdVoltage))
                    {
                        cntCycleThresholdLength = 0;
                        isCycleData = true;
                    }

                    //压入周期队列
                    CycleQueue.Enqueue(data);

                    //判定周期完成
                    if (cntCycleThresholdLength++ > CycleThresholdLength && isCycleData == true)
                    {
                        //触发周期判定完成事件
                        ActiveEventCycleJudgeDone(CycleQueue.ToArray());

                        //复位周期队列
                        CycleQueue.Clear();
                        cntCycleThresholdLength = 0;
                        bakCycleData = 0;
                        isCycleData = false;
                        continue;
                    }

                    //判定队列满
                    if (CycleQueue.Count > CycleBufferLength)
                    {
                        Console.WriteLine("CycleQueue队列已满现已清空。");
                        CycleQueue.Clear();
                        cntCycleThresholdLength = 0;
                        bakCycleData = 0;
                        isCycleData = false;
                        continue;
                    }
                    bakCycleData = data;
                }
                else
                {
                    aEvent.WaitOne();
                }
            }
        }

        #endregion

        #region 周期判定完成事件
        /// <summary>
        /// 定义周期判定完成委托
        /// </summary>
        /// <param name="arrData"></param>
        public delegate void DelegateCycleJudgeDone(double[] arrData);

        /// <summary>
        /// 定义周期判定完成事件
        /// </summary>
        public event DelegateCycleJudgeDone EventCycleJudgeDone;

        /// <summary>
        /// 触发周期判定完成事件
        /// </summary>
        /// <param name="arrData"></param>
        private void ActiveEventCycleJudgeDone(double[] arrData)
        {
            EventCycleJudgeDone?.Invoke(arrData);
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 添加数据到周期判定队列
        /// </summary>
        /// <param name="arrData"></param>
        private void AddData(params double[] arrData)
        {
            Monitor.Enter(DataQueue);
            foreach (double item in arrData)
            {
                DataQueue.Enqueue(item);
            }
            Monitor.Exit(DataQueue);
            aEvent.Set();
        }
        #endregion

    }
}
