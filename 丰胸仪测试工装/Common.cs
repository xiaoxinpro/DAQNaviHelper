using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 丰胸仪测试工装
{
    public static class Common
    {
        /// <summary>
        /// 计算校验和
        /// </summary>
        /// <param name="bytes">检验数组</param>
        /// <param name="start">校验起始位置</param>
        /// <param name="length">校验长度</param>
        /// <returns>校验和</returns>
        public static byte ByteCheakSum(byte[] bytes, int start = 0, int length = 0)
        {
            UInt32 sum = 1;
            if (length == 0)
            {
                length = bytes.Length - start;
            }
            for (int i = start; i < length + start; i++)
            {
                sum += Convert.ToUInt32(bytes[i]);
            }
            return Convert.ToByte(sum & 0xFF);
        }

        /// <summary>
        /// 检测二进制某一位
        /// </summary>
        /// <param name="Num">二进制数</param>
        /// <param name="Bin">需要检查的位置</param>
        /// <returns></returns>
        public static bool IsBinTest(byte Num, int Bin)
        {
            if (((Num >> Bin) & 1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// GroupBox开关
        /// </summary>
        /// <param name="groupBox">GroupBox名称</param>
        /// <param name="isEnable">开/关</param>
        public static void GroupEnable(GroupBox groupBox, bool isEnable)
        {
            foreach (Control obj in groupBox.Controls)
            {
                obj.Enabled = isEnable;
            }
        }

        /// <summary>
        /// 调用按钮事件
        /// </summary>
        /// <param name="btn">按钮名称</param>
        public static void CallOnClick(Button btn)
        {
            //建立一个类型  
            Type t = typeof(Button);
            //参数对象  
            object[] p = new object[1];
            //产生方法  
            MethodInfo m = t.GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            //参数赋值。传入函数  
            p[0] = EventArgs.Empty;
            //调用  
            m.Invoke(btn, p);
            return;
        }
    }
}
