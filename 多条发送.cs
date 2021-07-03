using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public partial class 串口助手 : Form
    {
        private const int HexIndex = 3;
        private const int WordWarpIndex = 4;
        private const int SendIndex = 5;
        private const int CmdIndex = 1;
        private DataTable dataTable = new DataTable("CMD");
        private string dataTablePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CSharp_串口助手\appConfig.xml";

        /// <summary>
        /// 读取xml中的列表 反序列化
        /// </summary>
        private void DataTableInit()
        {
            if(File.Exists(dataTablePath))
            {
                dataTable = MySerializeToXml.DeserializeWithXml<DataTable>(dataTablePath);
            }

            if (dataTable.Columns.Count == 0)
            {
                for (int i = 0; i < DataGridViewCmd.Columns.Count; i++)
                {
                    dataTable.Columns.Add("DataTable" + DataGridViewCmd.Columns[i].Name);
                }
                dataTable.Rows.Add("0");
            }
            
            DataGridViewCmd.DataSource = dataTable;
            DataGridViewCmd.SetDoubleBuffered(true);
        }

        /// <summary>
        /// 将列表序列化并存入xml中
        /// </summary>
        private void DataTableSave()
        {
            MySerializeToXml.SerializeToXml<DataTable>(dataTable, dataTablePath);
        }

        /// <summary>
        /// 获取列表中checkBox勾选状态
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool GetChecked(DataGridView dataGridView, int row, int column)
        {
            bool b = false;
            try
            {
                b = Convert.ToBoolean(dataGridView.Rows[row].Cells[column].Value);
            }
            catch
            {

            }
            return b;
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewCmd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex.Equals(SendIndex))
            {
                if (serialPortCOM.IsOpen)
                {
                    List<byte> byteBuf = new List<byte>();
                    if (GetChecked((DataGridView)sender, e.RowIndex, HexIndex))
                    {
                        //十六进制发送 
                        byteBuf = MyConver.HexToByte(Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value)).ToList();
                    }
                    else
                    {
                        byteBuf = TxEncoding.GetBytes(Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value)).ToList();
                    }

                    if (GetChecked((DataGridView)sender, e.RowIndex, WordWarpIndex))
                    {
                        byteBuf.Add(0x0d);
                        byteBuf.Add(0x0a);
                    }
                    Write(byteBuf.ToArray());
                }
            }
        }

        private void DataGridViewCmd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex.Equals(HexIndex))
            {
                if (GetChecked((DataGridView)sender, e.RowIndex, HexIndex))
                {
                    //十六进制显示
                    byte[] byteBuf = TxEncoding.GetBytes(Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value));
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value = MyConver.ByteToHex(byteBuf);
                }
                else
                {
                    byte[] byteBuf = MyConver.HexToByte(Convert.ToString(((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value));
                    ((DataGridView)sender).Rows[e.RowIndex].Cells[CmdIndex].Value = TxEncoding.GetString(byteBuf);
                }
            }
        }

        /// <summary>
        /// 添加新行时 自动添加序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewCmd_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ((DataGridView)sender).Rows[e.Row.Index-1].Cells[0].Value = (e.Row.Index-1).ToString();
        }

        /// <summary>
        /// 将字符串转成表中的checkBox格式显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewCmd_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex.Equals(HexIndex) || e.ColumnIndex.Equals(WordWarpIndex))
            {
                if (e.RowIndex > -1 && dataTable.Rows.Count > e.RowIndex)
                {
                    try
                    {
                        e.Value = bool.Parse(dataTable.Rows[e.RowIndex].ItemArray[e.ColumnIndex].ToString());
                    }
                    catch { }
                }
            }
        }
    }
}
