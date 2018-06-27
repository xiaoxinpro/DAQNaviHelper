using DAQNavi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAQNaviHelperDemo
{
    public partial class frmDemo : Form
    {
        DAQNaviHelper USB4704;

        public frmDemo()
        {
            InitializeComponent();
        }

        private void frmDemo_Load(object sender, EventArgs e)
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

        private void initError(string message, string title)
        {
            if (message.Contains("The device is not available")) //该设备不可用
            {
                MessageBox.Show("该设备不可用，请检查设备是否连接，或是否被其他程序占用。", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }

        private void USB4704_EventError(string message)
        {
            MessageBox.Show(message);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            USB4704.Test("123456789");
        }

        private void btnGetAiData_Click(object sender, EventArgs e)
        {
            double interval = Convert.ToDouble(txtAiInterval.Value);
            USB4704.IDevice.StartAiMode(new DAQNavi.DAQNaviHelper.DelegateAiEvent(USB4704_AiEvent), interval);
            txtAiData.Clear();
        }

        private void btnAiStop_Click(object sender, EventArgs e)
        {
            USB4704.IDevice.StopAiMode();
        }

        private void USB4704_AiEvent(AiModeType aiModeData)
        {
            Console.WriteLine(aiModeData);
            string strDataMax = "最大电压：";
            string strDataMin = "最小电压：";
            string strDataAvg = "平均电压：";
            for (int i = 0; i < aiModeData.Max.Length; i++)
            {
                strDataMax += aiModeData.Max[i].ToString("f2") + " ";
                strDataMin += aiModeData.Min[i].ToString("f2") + " ";
                strDataAvg += aiModeData.Avg[i].ToString("f2") + " ";
            }
            this.Invoke(new Action(() =>
            {
                txtAiData.Clear();
                txtAiData.AppendText(strDataMax + "\r\n");
                txtAiData.AppendText(strDataMin + "\r\n");
                txtAiData.AppendText(strDataAvg + "\r\n");
            }));
        }

        private void btnDiSwitch_Click(object sender, EventArgs e)
        {
            if (btnDiSwitch.Text == "开启")
            {
                btnDiSwitch.Text = "关闭";
                byte[] arrBitData = USB4704.IDevice.StartDiMode(USB4704_DiEventChange);
                chkDiList.Items.Clear();
                if (arrBitData != null)
                {
                    for (int i = 0; i < arrBitData.Length; i++)
                    {
                        chkDiList.Items.Add("DI" + i, Convert.ToBoolean(arrBitData[i]));
                    }
                }
            }
            else
            {
                btnDiSwitch.Text = "开启";
                USB4704.IDevice.StopDiMode();
                chkDiList.Items.Clear();
            }
            
        }

        private void USB4704_DiEventChange(int bit,byte data)
        {
            this.Invoke(new Action(() =>
            {
                Console.WriteLine("改变数字输入状态：DI" + bit + " = " + Convert.ToBoolean(data));
                chkDiList.SetItemChecked(bit, Convert.ToBoolean(data));
            }));
        }

        private void btnDoSwitch_Click(object sender, EventArgs e)
        {
            if (btnDoSwitch.Text == "开启")
            {

                if(USB4704.IDevice.GetDoMode(out byte[] arrBitData))
                {
                    btnDoSwitch.Text = "关闭";
                    chkDoList.Enabled = true;
                    chkDoList.Items.Clear();
                    if (arrBitData != null)
                    {
                        for (int i = 0; i < arrBitData.Length; i++)
                        {
                            chkDoList.Items.Add("DO" + i, Convert.ToBoolean(arrBitData[i]));
                        }
                    }
                }
            }
            else
            {
                btnDoSwitch.Text = "开启";
                USB4704.IDevice.StopDiMode();
                chkDoList.Items.Clear();
                chkDoList.Enabled = false;
            }
        }

        private void chkDoList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Console.WriteLine("改变数字输出状态：" + chkDoList.Items[e.Index].ToString() + " = " + !chkDoList.GetItemChecked(e.Index));
            USB4704.IDevice.SetDoModeBit(e.Index, Convert.ToByte(!chkDoList.GetItemChecked(e.Index)));
        }
    }
}
