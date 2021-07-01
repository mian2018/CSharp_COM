using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public partial class 串口助手 : Form
    {
        private int _rxCounter = 0;
        /// <summary>
        /// 接收字节数
        /// </summary>
        private int RxCounter
        {
            get { return _rxCounter; }
            set { _rxCounter = value; toolStatusRxCounter.Text = _rxCounter.ToString().PadLeft(9, ' '); }
        }

        private int _txCounter = 0;
        /// <summary>
        /// 发送字节数
        /// </summary>
        private int TxCounter
        {
            get { return _txCounter; }
            set { _txCounter = value; toolStatusTxCounter.Text = _txCounter.ToString().PadLeft(9, ' '); }
        }

        /// <summary>
        /// 存放接收文件路径
        /// </summary>
        private string CurrentFilePath
        {
            get 
            {
                //if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CurrentFilePath))
                //{
                //    //result: X:\xxx\xxx\ (.exe文件所在的目录 + "\")
                //    CurrentFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                //}
                return Properties.Settings.Default.CurrentFilePath;
            }
            set 
            {
                Properties.Settings.Default.CurrentFilePath = value;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 发送文本框编码格式
        /// </summary>
        private Encoding TxEncoding = Encoding.Default;
        private Encoding RxEncoding = Encoding.Default;

        /// <summary>
        /// 接收处理
        /// </summary>
        /// <param name="data">串口接收字节数据</param>
        private void DisplayRxInfo(byte[] data)
        {
            try
            {
                if (ckbStopDisPlay.Checked)
                {
                    //停止显示
                    return;
                }

                string str = "";
                if (ckbRxHex.Checked)
                {
                    //十六进制显示
                    str = MyConver.ByteToHex(data);
                    if (ckbTimeStamp.Checked)
                    {
                        //时间戳
                        str = str.Replace("0A", "0A \r\n[" + DateTime.Now.ToString("HH':'mm':'ss'.'fff") + "]->>>");
                    }
                }
                else
                {
                    str = RxEncoding.GetString(data);
                    if (ckbTimeStamp.Checked)
                    {
                        //时间戳
                        str = str.Replace("\n", "\n[" + DateTime.Now.ToString("HH':'mm':'ss'.'fff") + "]->>>");
                    }
                }


                if (ckbAutoClear.Checked && txbRx.TextLength > 4096)
                {
                    //自动清除
                    txbRx.Text = "";
                }
                txbRx.AppendText(str);

                if (ckbSaveRxFile.Checked && !string.IsNullOrWhiteSpace(CurrentFilePath))
                {
                    //将接收信息写入文件
                    File.AppendAllText(CurrentFilePath, str);
                }
                else
                {
                    ckbSaveRxFile.Checked = false;
                }

                RxCounter += data.Length;
            }
            catch
            {

            }
        }

        private string OpenFile( string path)
        {
            string str = "";
            if(File.Exists(path))
            {
                if (Path.GetExtension(path).ToLower().Equals(".bin"))
                {
                    ckbTxHex.Checked = true;
                    str = MyConver.ByteToHex(File.ReadAllBytes(path));
                }
                else
                {
                    str = TxEncoding.GetString(File.ReadAllBytes(path));
                }
                SaveFilePath(path);
            }
            return str;
        }

        /// <summary>
        /// 记录历史文件信息
        /// </summary>
        /// <param name="path"></param>
        private void SaveFilePath(params string[] path)
        {
            List<string> strList = new List<string>();

            strList.Add(Properties.Settings.Default.logPath1);
            strList.Add(Properties.Settings.Default.logPath2);
            strList.Add(Properties.Settings.Default.logPath3);
            strList.Add(Properties.Settings.Default.logPath4);
            strList.Add(Properties.Settings.Default.logPath5);
            foreach (var item in path)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    strList.Remove(item);
                    strList.Insert(0, item);
                }
            }

            ///* 保存当前路径 */
            Properties.Settings.Default.logPath1 = strList[0];
            Properties.Settings.Default.logPath2 = strList[1];
            Properties.Settings.Default.logPath3 = strList[2];
            Properties.Settings.Default.logPath4 = strList[3];
            Properties.Settings.Default.logPath5 = strList[4];
            Properties.Settings.Default.Save();

            /* 历史文件菜单 */
            toolStripMenuItem2.Text = strList[0];
            toolStripMenuItem3.Text = strList[1];
            toolStripMenuItem4.Text = strList[2];
            toolStripMenuItem5.Text = strList[3];
            toolStripMenuItem6.Text = strList[4];
        }
        /// <summary>
        /// 发送字节数组
        /// </summary>
        /// <param name="bytes"></param>
        private void Write(byte[] bytes)
        {
            try
            {
                serialPortCOM.Write(bytes, 0, bytes.Length);
                TxCounter += bytes.Length;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 加载参数
        /// </summary>
        private void LoadParam()
        {
            UpdateSerialName(cmbSerialName, GetComName(), Properties.Settings.Default.serialPortName);
            //UpdateSerialName(cmbSerialName, SerialPort.GetPortNames(), Properties.Settings.Default.serialPortName);

            cmbBaudRate.Text = Properties.Settings.Default.serialPortBaud;
            cmbParity.SelectedIndex = cmbParity.Items.IndexOf(Properties.Settings.Default.serialPortParity);
            cmbDataBits.Text = Properties.Settings.Default.serialPortDataBits;
            cmbStopBits.Text = Properties.Settings.Default.serialPortStopBits;

            ckbStopDisPlay.Checked = Properties.Settings.Default.ckbStopDisPlay;
            ckbAutoClear.Checked = Properties.Settings.Default.ckbAutoClear;
            ckbRxHex.Checked = Properties.Settings.Default.ckbRxHex;
            ckbSaveRxFile.Checked = Properties.Settings.Default.ckbSaveRxFile;
            ckbTimeStamp.Checked = Properties.Settings.Default.ckbTimeStamp;
            ckbRxWordWrap.Checked = Properties.Settings.Default.ckbRxWordWrap;
            ckbRxUTF8.Checked = Properties.Settings.Default.ckbRxUTF8;
            ckbAutoTx.Checked = Properties.Settings.Default.ckbAutoTx;
            ckbTxWordWrap.Checked = Properties.Settings.Default.ckbTxWordWrap;
            ckbTxUTF8.Checked = Properties.Settings.Default.ckbTxUTF8;
            txbTxAutoTime.Text = Properties.Settings.Default.txbTxAutoTime;
            ckbTxHex.Checked = Properties.Settings.Default.ckbTxHex;

        }

        /// <summary>
        /// 保存参数
        /// </summary>
        private void SaveParam()
        {
            Properties.Settings.Default.serialPortName = cmbSerialName.Text;
            Properties.Settings.Default.serialPortBaud = cmbBaudRate.Text;
            Properties.Settings.Default.serialPortParity = cmbParity.Text;
            Properties.Settings.Default.serialPortDataBits = cmbDataBits.Text;
            Properties.Settings.Default.serialPortStopBits = cmbStopBits.Text;

            Properties.Settings.Default.ckbStopDisPlay = ckbStopDisPlay.Checked;
            Properties.Settings.Default.ckbAutoClear = ckbAutoClear.Checked;
            Properties.Settings.Default.ckbRxHex = ckbRxHex.Checked;
            Properties.Settings.Default.ckbSaveRxFile = ckbSaveRxFile.Checked;
            Properties.Settings.Default.ckbTimeStamp = ckbTimeStamp.Checked;
            Properties.Settings.Default.ckbRxWordWrap = ckbRxWordWrap.Checked;
            Properties.Settings.Default.ckbRxUTF8 = ckbRxUTF8.Checked;
            Properties.Settings.Default.ckbAutoTx = ckbAutoTx.Checked;
            Properties.Settings.Default.ckbTxWordWrap = ckbTxWordWrap.Checked;
            Properties.Settings.Default.ckbTxUTF8 = ckbTxUTF8.Checked;
            Properties.Settings.Default.txbTxAutoTime = txbTxAutoTime.Text;
            Properties.Settings.Default.ckbTxHex = ckbTxHex.Checked;
            Properties.Settings.Default.Save();
            
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        private void OpenSerialPort()
        {
            try
            {
                serialPortCOM.Close();
                serialPortCOM.PortName = cmbSerialName.Text;
                serialPortCOM.BaudRate = int.Parse(cmbBaudRate.Text);
                serialPortCOM.Parity = (Parity)cmbParity.SelectedIndex;
                serialPortCOM.DataBits = int.Parse(cmbDataBits.Text);
                serialPortCOM.StopBits = (StopBits)int.Parse(cmbStopBits.Text);
                serialPortCOM.Open();
                toolStatusCOM.ForeColor = this.ForeColor;
                toolStatusCOM.Text = "已打开";
                btnOpen.Text = "关闭串口";
                cmbSerialName.Enabled = false;
                cmbBaudRate.Enabled = false;
                cmbDataBits.Enabled = false;
                cmbParity.Enabled = false;
                cmbStopBits.Enabled = false;
            }
            catch
            {
                CloseSerialPort();
                toolStatusCOM.ForeColor = Color.Red;
                toolStatusCOM.Text = "打开出错";
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        private void CloseSerialPort()
        {
            btnOpen.Text = "打开串口";
            cmbSerialName.Enabled = true;
            cmbBaudRate.Enabled = true;
            cmbDataBits.Enabled = true;
            cmbParity.Enabled = true;
            cmbStopBits.Enabled = true;
            try
            {
                serialPortCOM.Close();
                toolStatusCOM.ForeColor = this.ForeColor;
                toolStatusCOM.Text = "等待打开";
                
            }
            catch
            {
                toolStatusCOM.ForeColor = Color.Red;
                toolStatusCOM.Text = "关闭出错";
            }
        }
        DataGridView dgv = new DataGridView();
        public 串口助手()
        {
            InitializeComponent();
        }
        private void 串口助手_Load(object sender, EventArgs e)
        {
            TxCounter = 0;
            RxCounter = 0;
            SaveFilePath("");
            LoadParam();
            DataTableInit();
        }
        private void 串口助手_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveParam();
            DataTableSave();
        }
        private void tabControlCOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlCOM.SelectedTab.Controls.Add(groupBoxCOMInfo);
            tabControlCOM.SelectedTab.Controls.Add(groupBoxRxInfo);
            tabControlCOM.SelectedTab.Controls.Add(groupBoxTxInfo);
            tabControlCOM.SelectedTab.Controls.Add(txbRx);
            tabControlCOM.SelectedTab.Controls.Add(状态栏);

            if(tabControlCOM.SelectedIndex == 1)
            {
                groupBoxTxInfo.Enabled = false;
                ckbAutoTx.Checked = false;
            }
            else if (tabControlCOM.SelectedIndex == 0)
            {
                groupBoxTxInfo.Enabled = true;
            }
        }

        private void toolStatusRxCounter_DoubleClick(object sender, EventArgs e)
        {
            RxCounter = 0;
        }

        private void toolStatusTxCounter_DoubleClick(object sender, EventArgs e)
        {
            TxCounter = 0;
        }

        /// <summary>
        /// 串口接收回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPortCOM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //50ms处理一次串口接收
            Thread.Sleep(50);

            if(!serialPortCOM.IsOpen)
            {
                return;
            }

            Byte[] recvByteTemp = new Byte[serialPortCOM.BytesToRead];
            serialPortCOM.Read(recvByteTemp, 0, recvByteTemp.Length);
            this.BeginInvoke(new Action<byte[]>((byte[] data)=> { DisplayRxInfo(data); }), recvByteTemp);
        }


        /// <summary>
        /// 清空接收按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearTx_Click(object sender, EventArgs e)
        {
            txbRx.Text = "";
            TxCounter = 0;
            RxCounter = 0;
        }

        /// <summary>
        /// 发送文本框自动换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbTxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            txbTx.WordWrap = ckbTxWordWrap.Checked;
        }
        /// <summary>
        /// 接收文本框自动换行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbRxWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            txbRx.WordWrap = ckbRxWordWrap.Checked;
        }

        /// <summary>
        /// 手动发送按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTx_Click(object sender, EventArgs e)
        {
            if (serialPortCOM.IsOpen)
            {
                byte[] byteBuf;
                if (ckbTxHex.Checked)
                {
                    //十六进制发送 
                    byteBuf = MyConver.HexToByte(txbTx.Text);
                }
                else
                {
                    byteBuf =  TxEncoding.GetBytes(txbTx.Text);
                }
                Write(byteBuf);
            }
        }

        /// <summary>
        /// 发送区 十六进制勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbTxHex_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbTxHex.Checked)
            {
                //十六进制显示
                Byte[] byteBuf = TxEncoding.GetBytes(txbTx.Text);
                txbTx.Text = MyConver.ByteToHex(byteBuf);
            }
            else
            {
                Byte[] byteBuf = MyConver.HexToByte(txbTx.Text);
                txbTx.Text = TxEncoding.GetString(byteBuf);
            }
        }

        private void ckbSaveRxFile_CheckedChanged(object sender, EventArgs e)
        {
            if(!ckbSaveRxFile.Checked)
            {
                CurrentFilePath = "";
            }
            else if(string.IsNullOrWhiteSpace(CurrentFilePath))
            {
                if (保存文件.ShowDialog() == DialogResult.OK)
                {
                    CurrentFilePath = 保存文件.FileName;
                }
            }
        }

        /// <summary>
        /// 接收区 编码格式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbRxUTF8_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbRxUTF8.Checked)
            {
                RxEncoding = Encoding.UTF8;
            }
            else
            {
                RxEncoding = Encoding.Default;
            }
        }

        /// <summary>
        /// 发送区 编码格式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbTxUTF8_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTxUTF8.Checked)
            {
                TxEncoding = Encoding.UTF8;
            }
            else
            {
                TxEncoding = Encoding.Default;
            }
        }

        /// <summary>
        /// 自动发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbAutoTx_CheckedChanged(object sender, EventArgs e)
        {
            TxAutoSendTimer.Enabled = ckbAutoTx.Checked;
        }

        /// <summary>
        /// 发送间隔修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTxAutoTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TxAutoSendTimer.Interval = int.Parse(txbTxAutoTime.Text.Trim()) ;
            }
            catch
            {
                TxAutoSendTimer.Interval = 500;
                txbTxAutoTime.Text = "500";
            }
        }

        /// <summary>
        /// 自动发送定时回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxAutoSendTimer_Tick(object sender, EventArgs e)
        {
            btnTx.PerformClick();
        }

        /// <summary>
        /// 发送文本框 文件拖拽支持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTx_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            txbTx.Text = "";

            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            txbTx.Text = OpenFile(path);
        }

        /// <summary>
        /// 发送文本框 文件拖拽支持
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTx_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);

                if(path.Length == 1 && path[0].Contains("."))
                {
                    //改变鼠标样式
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                } 
            }
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(打开文件.ShowDialog() == DialogResult.OK)
            {
                txbTx.Text = "";
                txbTx.Text = OpenFile(打开文件.FileName);
            }
        }

        private void 清空文本框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTx.Text = "";
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbTx.Text = "";
            txbTx.Text = OpenFile(((ToolStripMenuItem)sender).Text);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(serialPortCOM.IsOpen)
            {
                CloseSerialPort();
            }
            else
            {
                OpenSerialPort();
            }
        }
        private void cmbSerialName_DropDown(object sender, EventArgs e)
        {
            UpdateSerialName(cmbSerialName, GetComName(), cmbSerialName.Text);
        }

        public const int WM_DEVICE_CHANGE = 0x219;            //设备改变           
        public const int DBT_DEVICEARRIVAL = 0x8000;          //设备插入
        public const int DBT_DEVICE_REMOVE_COMPLETE = 0x8004; //设备移除
        /// <summary>
        /// USB热插拔支持
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.WParam.ToInt32())                                  //判断消息类型
            {
                case DBT_DEVICEARRIVAL:
                    {
                        if (serialPortCOM.IsOpen)
                        {

                        }
                        else
                        {
                            this.BeginInvoke(new Action(() => {
                                UpdateSerialName(cmbSerialName, GetComName(), cmbSerialName.Text);
                            }));
                        }
                    }
                    break;
                case DBT_DEVICE_REMOVE_COMPLETE:
                    {
                        if (serialPortCOM.IsOpen)
                        {

                        }
                        else
                        {
                            this.BeginInvoke(new Action(() => { 
                                CloseSerialPort(); 
                                UpdateSerialName(cmbSerialName, GetComName(), cmbSerialName.Text); 
                            }));
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// 更新ports字符串数组的串口号到combox上 如果存在名字comName的，则combox默认为该名字
        /// </summary>
        /// <param name="combox"></param>
        /// <param name="comName"></param>
        public void UpdateSerialName(ComboBox combox, string[] ports, string comName)
        {
            try
            {
                Array.Sort(ports);

                combox.Items.Clear();
                combox.Items.AddRange(ports);

                /* 设置默认 com口 */
                if (combox.Items.Contains(comName))
                {
                    combox.SelectedIndex = combox.Items.IndexOf(comName);
                }
                else
                {
                    combox.SelectedIndex = combox.Items.Count - 1;
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 获取COM口 从设备管理器获取COM口详细信息，筛选后返回符合要求的COM口（筛选包涵 str 字符串的COM口）
        /// </summary>
        /// <returns></returns>
        public string[] GetComName()
        {
            List<string> coms = new List<string>();
            string str = "COM";  //筛选关键字 可自行修改
            try
            {
                //搜索设备管理器中的所有条目
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PnPEntity"))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties["Name"].Value != null)
                        {
                            if (hardInfo.Properties["Name"].Value.ToString().Contains("(COM"))
                            {
                                coms.Add(hardInfo.Properties["Name"].Value.ToString());
                            }
                        }
                    }
                    searcher.Dispose();
                }

                List<string> strs = new List<string>();
                foreach (string portName in coms)
                {
                    if (portName.Contains(str))
                    {
                        strs.Add(portName.Substring(portName.IndexOf("(COM")).Replace('(', ' ').Replace(')', ' ').Trim());
                    }
                }

                return strs.ToArray();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 扫描PC当前 COM口 如果含有名字为str的COM口则返回str 否则返回扫描到的第一个COM名字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetComName(string str)
        {
            string[] strings = GetComName();
            if (strings.Contains(str) && !string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (strings.Count() > 0)
            {
                return strings[0];
            }
            return null;
        }

    }
}
