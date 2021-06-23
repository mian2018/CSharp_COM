using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.CurrentFilePath) || !File.Exists(Properties.Settings.Default.CurrentFilePath))
                {
                    //result: X:\xxx\xxx\ (.exe文件所在的目录 + "\")
                    CurrentFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                }

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
                    str = str.Replace("0A", "0A \r\n[" + DateTime.Now.ToString() + "]->>>");
                }
            }
            else
            {
                str = RxEncoding.GetString(data);
                if(ckbTimeStamp.Checked)
                {
                    //时间戳
                    str = str.Replace("\n", "\n[" + DateTime.Now.ToString() + "]->>>");
                }
            }

            if (txbRx.TextLength > 4096)
            {
                //自动清除
                txbRx.Text = "";
            }
            txbRx.AppendText(str);

            if(ckbSaveRxFile.Checked)
            {
                //将接收信息写入文件
                File.AppendAllText(CurrentFilePath, str);
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public 串口助手()
        {
            InitializeComponent();
        }
        private void 串口助手_Load(object sender, EventArgs e)
        {
            TxCounter = 0;
            RxCounter = 0;
            SaveFilePath("");
        }
        private void 串口助手_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void tabControlCOM_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlCOM.SelectedTab.Controls.Add(groupBoxCOMInfo);
            tabControlCOM.SelectedTab.Controls.Add(groupBoxRxInfo);
            tabControlCOM.SelectedTab.Controls.Add(groupBoxTxInfo);
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
            //10ms处理一次串口接收
            Thread.Sleep(10);

            if(!serialPortCOM.IsOpen)
            {
                return;
            }
            Byte[] recvByteTemp = new Byte[serialPortCOM.BytesToRead];
            serialPortCOM.Read(recvByteTemp, 0, recvByteTemp.Length);

            this.Invoke(new Action<byte[]>((byte[] data)=> { DisplayRxInfo(data); }), recvByteTemp);
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
                if (ckbRxHex.Checked)
                {
                    //十六进制发送 
                    Byte[] byteBuf = MyConver.HexToByte(txbTx.Text);
                    Write(byteBuf);
                    TxCounter += byteBuf.Length / 2;
                }
                else
                {
                    byte[] byteArray =  TxEncoding.GetBytes(txbTx.Text);
                    Write(byteArray);
                    TxCounter += byteArray.Length;
                }
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
                        if (serialPort1.IsOpen)
                        {

                        }
                        else
                        {
                            UpdateSerialName();
                        }
                    }
                    break;
                case DBT_DEVICE_REMOVE_COMPLETE:
                    {
                        if (serialPort1.IsOpen)
                        {

                        }
                        else
                        {
                            serialPort1.Close();
                            UpdateSerialName();
                            linkLabel1.Text = "准备就绪";
                            button1.Text = "打开串口";
                            comboBoxSerialName.Enabled = true;
                            comboBoxBaudRate.Enabled = true;
                            comboBoxDataBits.Enabled = true;
                            comboBoxCheck.Enabled = true;
                            comboBoxStopBits.Enabled = true;
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
            /* 获取com口标号 */
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
        /// 获取COM口
        /// </summary>
        /// <returns></returns>
        public string[] GetComName()
        {
            List<string> coms = new List<string>();
            string str = "COM";
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
        /// <summary>
        /// 设置波特率
        /// </summary>
        /// <param name="baud"></param>
        public void SetComBaud(int baud)
        {
            scanSerialPort.BaudRate = baud;
        }

        /// <summary>
        /// 打开扫码枪串口
        /// </summary>
        /// <param name="portName">串口名</param>
        /// <param name="portBaud">波特率</param>
        public void Open(string portName, int portBaud)
        {
            try
            {
                if (!IsOK || scanSerialPort.PortName != portName || scanSerialPort.BaudRate != portBaud)
                {
                    scanSerialPort.Close();
                    scanSerialPort.PortName = portName;
                    scanSerialPort.BaudRate = portBaud;
                    scanSerialPort.Open();
                    IsOK = true;
                }
            }
            catch (Exception e)
            {
                IsOK = false;
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 打开扫码枪串口，如果参数串口名字不存在，则自动打开存在的
        /// </summary>
        /// <param name="comName">串口名，如果该串口名不存在，则打开存在的串口</param>
        public void Open(string comName)
        {
            string str = GetComName(comName);
            if (!string.IsNullOrEmpty(str))
            {
                Open(str, 115200);
            }
        }
        /// <summary>
        /// 关闭扫码枪
        /// </summary>
        public void Close()
        {
            scanSerialPort.Close();
            scanRxBuff.Clear();
            IsOK = false;
            bytesCodeIsOK = false;
        }

        
    }
}
