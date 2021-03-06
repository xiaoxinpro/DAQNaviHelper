﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace 丰胸仪波形测试工装
{
    /// <summary>
    /// 输出文件类
    /// </summary>
    public class OutputFile
    {
        #region 字段
        private Queue<string> WriteQueue;
        private Thread WriteThread;
        private AutoResetEvent aEvent = new AutoResetEvent(false);
        private string strFilePath = "";
        private decimal numFileSplitCount = 0;
        private bool _enable = false;
        #endregion

        #region 属性
        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                _enable = value;
                if (value)
                {
                    aEvent.Set();
                }
            }
        }
        public string FilePath { get; set; }
        public decimal FileSplitSize { get; set; }
        public decimal FileSplitNumber { get; set; }
        public EnumFileSplit FileSplit { get; set; }
        #endregion

        #region 构造函数
        public OutputFile()
        {
            //初始化属性
            Enable = false;
            FilePath = "";
            FileSplitSize = 10240;
            FileSplitNumber = 1020000;
            FileSplit = EnumFileSplit.None;

            //初始化字段
            WriteQueue = new Queue<string>();
            WriteThread = new Thread(ThreadWriteProcess);
            WriteThread.IsBackground = true;
            WriteThread.Start();
        }
        #endregion

        #region 文件输出函数(线程)
        /// <summary>
        /// 输出文件线程
        /// </summary>
        private void ThreadWriteProcess()
        {
            while (true)
            {
                if (Enable)
                {
                    CheckStrFilePath(FilePath);
                    if (!Directory.Exists(Path.GetDirectoryName(strFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(strFilePath));
                    }

                    var fileStreamWriter = new StreamWriter(strFilePath, true);
                    while (WriteQueue.Count > 0)
                    {
                        Monitor.Enter(WriteQueue);
                        string msg = WriteQueue.Dequeue();
                        Monitor.Exit(WriteQueue);
                        fileStreamWriter.WriteLine(msg);
                        numFileSplitCount++;
                        if (CheckStrFilePath(strFilePath, false)) 
                        {
                            break;
                        }
                    }
                    fileStreamWriter.Flush();
                    fileStreamWriter.Close();

                    if (WriteQueue.Count <= 0)
                    {
                        aEvent.WaitOne();
                    }
                }
                else
                {
                    aEvent.WaitOne();
                }
            }
        }

        /// <summary>
        /// 检验文件路径
        /// </summary>
        /// <param name="path">源设定路径</param>
        /// <returns>返回路径是否变化，路径改变返回True，路径不变返回False</returns>
        private bool CheckStrFilePath(string path, bool isUpdata = true)
        {
            bool ret = false;
            switch (FileSplit)
            {
                case EnumFileSplit.None:
                    strFilePath = path;
                    break;
                case EnumFileSplit.Size:
                    if (!File.Exists(strFilePath))
                    {
                        strFilePath = path;
                        break;
                    }
                    long fileSize = new FileInfo(strFilePath).Length / 1024;
                    if (fileSize >= FileSplitSize)
                    {
                        ret = true;
                        if (isUpdata)
                        {
                            strFilePath = GetNextFilePath(path);
                        }
                    }
                    break;
                case EnumFileSplit.Number:
                    if (!File.Exists(strFilePath))
                    {
                        strFilePath = path;
                        break;
                    }
                    if (numFileSplitCount >= FileSplitNumber)
                    {
                        ret = true;
                        if (isUpdata)
                        {
                            strFilePath = GetNextFilePath(path);
                            numFileSplitCount = 0;
                        }
                    }
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 获取下一个路径
        /// </summary>
        /// <param name="path">源设定路径</param>
        /// <returns>可用路径地址</returns>
        private string GetNextFilePath(string path)
        {
            if (!File.Exists(path))
            {
                return path;
            }
            else
            {
                int numFileCnt = 0;
                string strPath = Path.GetDirectoryName(path) + @"\";
                string strName = Path.GetFileNameWithoutExtension(path);
                string strType = Path.GetExtension(path);
                string strBakName = strName;
                while (File.Exists(strPath + strBakName + strType))
                {
                    numFileCnt++;
                    strBakName = strName + "-" + numFileCnt.ToString();
                }
                return (strPath + strBakName + strType);
            }
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 输出一行数据
        /// </summary>
        /// <param name="arrData">每个元素一行</param>
        public void AddWriteLine(params string[] arrData)
        {
            Monitor.Enter(WriteQueue);
            foreach (string item in arrData)
            {
                WriteQueue.Enqueue(item);
            }
            Monitor.Exit(WriteQueue);
            if (Enable)
            {
                aEvent.Set();
            }
        }

        /// <summary>
        /// 输出数据
        /// </summary>
        /// <param name="flag">数组间隔</param>
        /// <param name="arrData">数据内容</param>
        public void AddWrite(string flag = ",", params string[] arrData)
        {
            AddWriteLine(string.Join(flag, arrData));
        }
        #endregion


    }

    /// <summary>
    /// 文件拆分方式
    /// </summary>
    public enum EnumFileSplit
    {
        None,       //不拆分
        Size,       //文件大小拆分
        Number      //数据数量拆分
    }
}
