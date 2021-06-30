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
        private DataTable dataTable = new DataTable("CMD");
        private string dataTablePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CSharp_串口助手\appConfig.xml";
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
        }

        private void DataTableSave()
        {
            MySerializeToXml.SerializeToXml<DataTable>(dataTable, dataTablePath);
        }
        private void DataGridViewCmd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(((DataGridView)sender).Columns.Count - 1))
            {
                txbRx.AppendText(e.RowIndex.ToString() + "\r\n");
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


        private void DataGridViewCmd_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex.Equals(3) || e.ColumnIndex.Equals(4))
            {
                if (e.RowIndex > 0 && dataTable.Rows.Count > e.RowIndex)
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
