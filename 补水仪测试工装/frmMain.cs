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
        #endregion

        #region 字段
        private int nowTestItem = 0;
        private DAQNaviHelper USB4704;
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
            progress.Maximum = STR_TEST_NAME.Length;
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
            SetProgressStatus(progressBarStatus, nowTestItem);
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
            MessageBox.Show(message);
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
    #endregion
}
