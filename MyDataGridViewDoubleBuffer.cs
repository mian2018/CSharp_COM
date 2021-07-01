using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public static class MyDataGridViewDoubleBuffer
    {
        ///   <summary>
        ///   将给定的DataGridView设置双缓冲
        ///   </summary>
        ///   <param name="datagrid"> 给定的DataGridView </param>
        ///   <param name="opened"> 设置为ture即打开双缓冲 </param>
        public static void SetDoubleBuffered(this DataGridView datagrid, bool opened)
        {
            var dgvType = datagrid.GetType();
            var properInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            properInfo.SetValue(datagrid, opened, null);
        }
    }
}
