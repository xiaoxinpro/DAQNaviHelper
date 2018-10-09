using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 丰胸仪测试工装
{
    public class BreastBle
    {
        #region 字段
        private bool IsChangeReceiveData = false;
        #endregion

        #region 属性
        public ListView ListViewSerialReceived { get; set; }
        #endregion

        #region 构造函数
        public BreastBle(ListView listViewSerialReceived)
        {
            ListViewSerialReceived = listViewSerialReceived;
            InitListViewSerialReceived(ListViewSerialReceived);
        }
        #endregion

        #region 事件接口
        /// <summary>
        /// 定义发送数据委托
        /// </summary>
        /// <param name="arrData"></param>
        public delegate void DelegateAddCmdWrite(byte[] arrData);

        /// <summary>
        /// 发送命令数据事件
        /// </summary>
        public event DelegateAddCmdWrite EventAddCmdWrite;

        /// <summary>
        /// 定义接收数据改变委托
        /// </summary>
        /// <param name="arrData"></param>
        public delegate void DelegateChangeReceivedData(byte[] arrData);

        /// <summary>
        /// 接收数据改变事件
        /// </summary>
        public event DelegateChangeReceivedData EventChangeReceivedData;
        #endregion

        #region 串口列表函数
        /// <summary>
        /// 初始化ListView
        /// </summary>
        private void InitListViewSerialReceived(ListView listView)
        {
            string[] arrListName = { "模式", "强度", "状态", "剩余时间", "加热", "电量" };
            string[] arrListMark = { "当前运行的模式", "当前模式运行的强度", "停止、运行、暂停", "秒", "加热状态", "电池剩余电量" };
            //基本属性设置
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.View = View.Details;

            //创建列表头
            listView.Columns.Add("序号", 50, HorizontalAlignment.Center);
            listView.Columns.Add("名称", 100, HorizontalAlignment.Center);
            listView.Columns.Add("值", 100, HorizontalAlignment.Center);
            listView.Columns.Add("备注", 200, HorizontalAlignment.Left);

            //添加数据
            listView.BeginUpdate();
            for (int i = 0; i < arrListName.Length; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = i.ToString();
                listViewItem.SubItems.Add(arrListName[i]);
                listViewItem.SubItems.Add("");
                listViewItem.SubItems.Add(arrListMark[i]);
                listView.Items.Add(listViewItem);
            }
            listView.EndUpdate();
        }

        /// <summary>
        /// 编辑串口接收数据值
        /// </summary>
        /// <param name="line">行号</param>
        /// <param name="value">值</param>
        private void EditListViewSerialReceviedValue(int line, string value, bool flag = false)
        {
            if (line >= 0 && line < ListViewSerialReceived.Items.Count)
            {
                if (ListViewSerialReceived.Items[line].SubItems[2].Text != value)
                {
                    IsChangeReceiveData = true;
                    ListViewSerialReceived.Items[line].SubItems[2].Text = value;
                    ListViewSerialReceived.Items[line].BackColor = flag ? System.Drawing.Color.Red : System.Drawing.Color.White;
                }
            }
        }

        /// <summary>
        /// 清空串口解析列表
        /// </summary>
        private void ClearListViewSerialReceviedValue()
        {
            if (ListViewSerialReceived != null)
            {
                ListViewSerialReceived.BeginUpdate();
                for (int i = 0; i < ListViewSerialReceived.Items.Count; i++)
                {
                    ListViewSerialReceived.Items[i].SubItems[2].Text = "";
                }
                ListViewSerialReceived.EndUpdate();
            }
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 数据接收函数
        /// </summary>
        /// <param name="arrData"></param>
        public void BytesReceviedDataProcess(byte[] arrData)
        {
            if (arrData.Length == 12 && arrData[0] == 0x52 && arrData[1] == 0x03 && CheckReceiveData(arrData))
            {
                //上报数据
                if (arrData[2] == 0x00 && arrData[3] == 0x08)
                {
                    int index = Convert.ToInt32(arrData[4]);
                    IsChangeReceiveData = false;
                    //模式
                    string[] arrMode = { "关机", "振动模式", "电疗模式", "振动电疗模式", "综合模式" };
                    if (index < arrMode.Length)
                    {
                        EditListViewSerialReceviedValue(0, arrMode[index]);
                    }
                    else
                    {
                        EditListViewSerialReceviedValue(0, "未知：0x" + index.ToString("x2"));
                    }

                    //强度
                    EditListViewSerialReceviedValue(1, Convert.ToInt32(arrData[5]).ToString());

                    //状态
                    index = Convert.ToInt32(arrData[6]);
                    string[] arrStatus = { "停止", "运行", "暂停" };
                    if (index < arrStatus.Length)
                    {
                        EditListViewSerialReceviedValue(2, arrStatus[index]);
                    }
                    else
                    {
                        EditListViewSerialReceviedValue(2, "未知：0x" + index.ToString("x2"));
                    }

                    //剩余时间
                    index = (arrData[7] << 8) | arrData[8];
                    EditListViewSerialReceviedValue(3, index.ToString());

                    //加热
                    index = arrData[9];
                    string strTmp;
                    bool isFlag = false;
                    switch (index)
                    {
                        case 0x00:
                            strTmp = "关闭加热";
                            break;
                        case 0x10:
                            strTmp = "低档加热中";
                            break;
                        case 0x20:
                            strTmp = "高档加热中";
                            break;
                        case 0x11:
                            strTmp = "低档加热温度到达";
                            isFlag = true;
                            break;
                        case 0x21:
                            strTmp = "高档加热温度到达";
                            isFlag = true;
                            break;
                        default:
                            strTmp = "未知：0x" + index.ToString("x2");
                            break;
                    }
                    EditListViewSerialReceviedValue(4, strTmp, isFlag);

                    //电量
                    index = arrData[10];
                    switch (index)
                    {
                        case 0x00:
                            strTmp = "无电量";
                            break;
                        case 0x01:
                            strTmp = "20%电量";
                            break;
                        case 0x02:
                            strTmp = "40%电量";
                            break;
                        case 0x03:
                            strTmp = "60%电量";
                            break;
                        case 0x04:
                            strTmp = "80%电量";
                            break;
                        case 0x05:
                            strTmp = "100%电量";
                            break;
                        case 0x06:
                            strTmp = "充电中";
                            break;
                        case 0x07:
                            strTmp = "充电完成";
                            break;
                        default:
                            strTmp = "未知:0x" + index.ToString("X2");
                            break;
                    }
                    EditListViewSerialReceviedValue(5, strTmp);

                    //发送应答位
                    byte[] buffer = { 0x52, 0x03, 0x02, 0x08, 0x00, 0x60 };
                    EventAddCmdWrite(CheckWriteData(buffer));

                    //检查是否改变数据
                    if (IsChangeReceiveData)
                    {
                        EventChangeReceivedData(arrData);
                    }
                }
            }

        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 计算数据校验和
        /// </summary>
        /// <param name="arrData">Byte数据数组</param>
        /// <returns>检验和</returns>
        private byte CheckDataSum(byte[] arrData)
        {
            uint sum = 1;
            for (int i = 0; i < arrData.Length - 1; i++)
            {
                sum += Convert.ToUInt32(arrData[i]);
            }
            return Convert.ToByte(sum & 0xFF);
        }

        /// <summary>
        /// 获取发送数据校验
        /// </summary>
        /// <param name="arrData">发送有效数据</param>
        /// <returns></returns>
        private byte[] CheckWriteData(byte[] arrData)
        {
            arrData[arrData.Length - 1] = CheckDataSum(arrData);
            return arrData;
        }

        /// <summary>
        /// 检验接收数据校验位
        /// </summary>
        /// <param name="arrData">接收数据</param>
        /// <returns>校验结果</returns>
        private bool CheckReceiveData(byte[] arrData)
        {
            if (CheckDataSum(arrData) != arrData[arrData.Length - 1])
            {
                Console.WriteLine("校验和错误:" + arrData[arrData.Length - 1].ToString() + " 不等于 " + CheckDataSum(arrData).ToString());
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
