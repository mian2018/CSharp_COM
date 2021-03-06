using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public partial class 串口助手 : Form
    {
        /// <summary>
        /// chart控件数据表最大长度， 实测超过1w后巨卡
        /// </summary>
        private const int MaxSeriesLen = 10000;

        /// <summary>
        /// 记录x轴坐标
        /// </summary>
        private int WaveXIndex = 0;
        object chartLock = new object();
        /// <summary>
        /// chart控件数据源类型
        /// </summary>
        private DataTable dataTableType = new DataTable("Wave");
        /// <summary>
        /// chart控件数据源列表
        /// </summary>
        List<DataTable> dataTables = new List<DataTable>();
        /// <summary>
        /// 图表刷新线程
        /// </summary>
        Thread thread;
        private void ChartWaveInit()
        {
            //创建一个dataTable 10列 第一列存数据点x坐标 其它九列存y
            for(int i = 0; i < 10; i ++)
            {
                dataTableType.Columns.Add("Series"+i);
                dataTableType.Columns["Series" + i].DataType = typeof(int);
            }

            //绑定数据列 给九条曲线指定x y列名
            for(int i = 0; i < chartWave.Series.Count; i++)
            {
                chartWave.Series[i].XValueMember = "Series0";
                chartWave.Series[i].YValueMembers = "Series"+ (i+1);
            }

            dataTables.Add(dataTableType.Clone());
            //绑定数据源
            chartWave.DataSource = dataTables[0];

            //加载数据
            ckbWave1.Checked = Properties.Settings.Default.ckbWave1;
            ckbWave2.Checked = Properties.Settings.Default.ckbWave2;
            ckbWave3.Checked = Properties.Settings.Default.ckbWave3;
            ckbWave4.Checked = Properties.Settings.Default.ckbWave4;
            ckbWave5.Checked = Properties.Settings.Default.ckbWave5;
            ckbWave6.Checked = Properties.Settings.Default.ckbWave6;
            ckbWave7.Checked = Properties.Settings.Default.ckbWave7;
            ckbWave8.Checked = Properties.Settings.Default.ckbWave8;
            ckbUpdata.Checked = Properties.Settings.Default.ckbUpdata;
            cmbWaveType.SelectedIndex = Properties.Settings.Default.cmbWaveType;
            txbXSize.Text = Properties.Settings.Default.txbXSize;
            labWaveColor1.ForeColor = Properties.Settings.Default.labWaveColor1;
            chartWave.Series[0].Color = labWaveColor1.ForeColor;
            labWaveColor2.ForeColor = Properties.Settings.Default.labWaveColor2;
            chartWave.Series[1].Color = labWaveColor2.ForeColor;
            labWaveColor3.ForeColor = Properties.Settings.Default.labWaveColor3;
            chartWave.Series[2].Color = labWaveColor3.ForeColor;
            labWaveColor4.ForeColor = Properties.Settings.Default.labWaveColor4;
            chartWave.Series[3].Color = labWaveColor4.ForeColor;
            labWaveColor5.ForeColor = Properties.Settings.Default.labWaveColor5;
            chartWave.Series[4].Color = labWaveColor5.ForeColor;
            labWaveColor6.ForeColor = Properties.Settings.Default.labWaveColor6;
            chartWave.Series[5].Color = labWaveColor6.ForeColor;
            labWaveColor7.ForeColor = Properties.Settings.Default.labWaveColor7;
            chartWave.Series[6].Color = labWaveColor7.ForeColor;
            labWaveColor8.ForeColor = Properties.Settings.Default.labWaveColor8;
            chartWave.Series[7].Color = labWaveColor8.ForeColor;
        }

        private void SaveSeriesParame()
        {
            Properties.Settings.Default.ckbWave1 = ckbWave1.Checked;
            Properties.Settings.Default.ckbWave2 = ckbWave2.Checked;
            Properties.Settings.Default.ckbWave3 = ckbWave3.Checked;
            Properties.Settings.Default.ckbWave4 = ckbWave4.Checked;
            Properties.Settings.Default.ckbWave5 = ckbWave5.Checked;
            Properties.Settings.Default.ckbWave6 = ckbWave6.Checked;
            Properties.Settings.Default.ckbWave7 = ckbWave7.Checked;
            Properties.Settings.Default.ckbWave8 = ckbWave8.Checked;
            Properties.Settings.Default.ckbUpdata = ckbUpdata.Checked;
            Properties.Settings.Default.cmbWaveType = cmbWaveType.SelectedIndex;
            Properties.Settings.Default.labWaveColor1 = labWaveColor1.ForeColor;
            Properties.Settings.Default.labWaveColor2 = labWaveColor2.ForeColor;
            Properties.Settings.Default.labWaveColor3 = labWaveColor3.ForeColor;
            Properties.Settings.Default.labWaveColor4 = labWaveColor4.ForeColor;
            Properties.Settings.Default.labWaveColor5 = labWaveColor5.ForeColor;
            Properties.Settings.Default.labWaveColor6 = labWaveColor6.ForeColor;
            Properties.Settings.Default.labWaveColor7 = labWaveColor7.ForeColor;
            Properties.Settings.Default.labWaveColor8 = labWaveColor8.ForeColor;
            Properties.Settings.Default.txbXSize = txbXSize.Text;
            Properties.Settings.Default.Save();

        }

        /// <summary>
        /// 帧头
        /// </summary>
        byte frameHead = 0xaa;
        /// <summary>
        /// 帧长
        /// </summary>
        int frameLen = 38;
        private void Analysis()
        {
            
            List<byte> listBytes = new List<byte>(1024);
            int counter = 0;
            int yValue = int.MaxValue;
            while (true)
            {
                Thread.Sleep(50);
                counter++;
                if (serialPortCOM.IsOpen)
                {
                    byte[] bytes = new byte[serialPortCOM.BytesToRead];
                    serialPortCOM.Read(bytes, 0, bytes.Length);
                    this.Invoke(new Action(() => RxCounter += bytes.Length));
                    listBytes.AddRange(bytes);

                    while (listBytes.Contains(frameHead) && (listBytes.Count - listBytes.IndexOf(frameHead)) >= frameLen)
                    {
                        int Index = listBytes.IndexOf(frameHead);
                        if (listBytes[Index + 1] == 0xFF && listBytes[Index + 2] == 0xF1 && listBytes[Index + 3] == 32)
                        {
                            byte[] tempBytes = listBytes.ToArray();
                            int Wave1 = BitConverter.ToInt32(tempBytes, Index + 4);
                            int Wave2 = BitConverter.ToInt32(tempBytes, Index + 8);
                            int Wave3 = BitConverter.ToInt32(tempBytes, Index + 12);
                            int Wave4 = BitConverter.ToInt32(tempBytes, Index + 16);
                            int Wave5 = BitConverter.ToInt32(tempBytes, Index + 20);
                            int Wave6 = BitConverter.ToInt32(tempBytes, Index + 24);
                            int Wave7 = BitConverter.ToInt32(tempBytes, Index + 28);
                            int Wave8 = BitConverter.ToInt32(tempBytes, Index + 32);

                            lock (chartLock)
                            {
                                dataTables[dataTables.Count - 1].Rows.Add(WaveXIndex++, Wave1, Wave2, Wave3, Wave4, Wave5, Wave6, Wave7, Wave8);
                                
                                //记录y值， 后面修改y轴位置
                                for(int i = 1; i < 9; i++)
                                {
                                    if(chartWave.Series[i].Enabled)
                                    {   
                                        yValue = (int)dataTables[dataTables.Count - 1].Rows[dataTables[dataTables.Count - 1].Rows.Count - 1][i];
                                        break;
                                    }
                                }

                                //表中超过 MaxSeriesLen 条数据 切换下一个表 一个表中数据越长则图表刷新时间越长
                                if (dataTables[dataTables.Count - 1].Rows.Count >= MaxSeriesLen)
                                {
                                    dataTables.Add(dataTableType.Clone());
                                    for (int i = MaxSeriesLen/2; i < MaxSeriesLen; i++)
                                    {
                                        dataTables[dataTables.Count - 1].Rows.Add(dataTables[dataTables.Count - 1 - 1].Rows[i].ItemArray);
                                    }

                                    this.BeginInvoke(new Action(() => trackBar1.Maximum += 10));

                                    if (ckbUpdata.Checked)
                                    {
                                        //显示最新数据点
                                        chartWave.DataSource = dataTables[dataTables.Count - 1];

                                        this.BeginInvoke(new Action(() => { trackBar1.Value = trackBar1.Maximum; }));
                                    }
                                }
                            }
                            listBytes.RemoveRange(0, Index + frameLen);
                        }
                        else
                        {
                            listBytes.RemoveRange(0, Index);
                        }
                    }

                    if (counter > 20 && yValue < int.MaxValue)
                    {
                        int temp = yValue;
                        counter = 0;
                        yValue = int.MaxValue;
                        this.BeginInvoke(new Action(() => {
                            
                            lock (chartLock)
                            {
                                //X轴 滚动条位置 保持最新位置 - 99
                                if (ckbUpdata.Checked)
                                {
                                    if (dataTables[dataTables.Count - 1].Rows.Count > chartWave.ChartAreas[0].AxisX.ScaleView.Size)
                                    {
                                        chartWave.ChartAreas[0].AxisX.ScaleView.Position = WaveXIndex - chartWave.ChartAreas[0].AxisX.ScaleView.Size;
                                    }
                                    else
                                    {
                                        chartWave.ChartAreas[0].AxisX.ScaleView.Position = 0;
                                    }

                                    //Y轴 滚动条位置 保持最新位置
                                    chartWave.ChartAreas[0].AxisY.ScaleView.Position = temp - chartWave.ChartAreas[0].AxisY.ScaleView.Size/2;
                                }

                                //X轴 数据的起始位置和结束位置 
                                //chart1.ChartAreas[0].AxisX.Minimum = 1000;
                                //chart1.ChartAreas[0].AxisX.Maximum =  dataTables[dataTables.Count - 1].Rows.Count;


                                //刷新图表
                                chartWave.DataBind();
                            }

                        }));
                        
                    }

                }
            }

        }

        private void ckbWave_CheckedChanged(object sender, EventArgs e)
        {
            int Index = int.Parse(((CheckBox)sender).Name.Substring(((CheckBox)sender).Name.Length - 1))-1;
            chartWave.Series[Index].Enabled = ((CheckBox)sender).Checked;
        }

        private void chartWave_CursorPositionChanged(object sender, System.Windows.Forms.DataVisualization.Charting.CursorEventArgs e)
        {
            if (e.Axis.Name.ToLower().StartsWith("x"))
            {
                labX.Text = e.NewPosition.ToString();
            }
            else if (e.Axis.Name.ToLower().StartsWith("y"))
            {
                labY.Text = e.NewPosition.ToString();
            }
        }


        private void labWaveColor_Click(object sender, EventArgs e)
        {
            int Index = int.Parse(((Label)sender).Name.Substring(((Label)sender).Name.Length - 1)) - 1;
            colorDialogWave.Color = ((Label)sender).ForeColor;
            if (colorDialogWave.ShowDialog() == DialogResult.OK)
            {
                ((Label)sender).ForeColor = colorDialogWave.Color;
                chartWave.Series[Index].Color = colorDialogWave.Color;
            }
        }

        private void cmbWaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType seriesChartType = new System.Windows.Forms.DataVisualization.Charting.SeriesChartType();
            if (cmbWaveType.SelectedIndex == 2)
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else if (cmbWaveType.SelectedIndex == 1)
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            }
            else if (cmbWaveType.SelectedIndex == 3)
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            }
            else
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            }

            foreach (var item in chartWave.Series)
            {
                item.ChartType = seriesChartType;
            }
        }
        private void txbXSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                chartWave.ChartAreas[0].AxisX.ScaleView.Size = int.Parse(txbXSize.Text);
            }
        }
        private void txbXSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(txbXSize.Text);

                if (temp < MaxSeriesLen && temp > 0)
                {
                    chartWave.ChartAreas[0].AxisX.ScaleView.Size = int.Parse(txbXSize.Text);
                }
                else
                {
                    txbXSize.Text = chartWave.ChartAreas[0].AxisX.ScaleView.Size.ToString();
                }
            }
            catch
            {
                txbXSize.Text = chartWave.ChartAreas[0].AxisX.ScaleView.Size.ToString();
            }
        }

        private void btnWaveClear_Click(object sender, EventArgs e)
        {
            lock (chartLock)
            {
                dataTables.Clear();
                WaveXIndex = 0;
                trackBar1.Maximum = 0;
                dataTables.Add(dataTableType.Clone());
                //绑定数据源
                chartWave.DataSource = dataTables[0];

                //刷新图表
                chartWave.DataBind();
            }
        }
        
        private void btnWaveDisplay_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
                //刷新图表
                chartWave.DataBind();
                btnWaveDisplay.Text = "开始显示";
                btnWaveSave.Enabled = true;
                btnWaveLoad.Enabled = true;
                //恢复串口接收回调事件
                serialPortCOM.DataReceived += serialPortCOM_DataReceived;
            }
            else
            {
                //取消串口接收回调事件
                serialPortCOM.DataReceived -= serialPortCOM_DataReceived;

                btnWaveSave.Enabled = false;
                btnWaveLoad.Enabled = false;
                //图表刷新线程
                thread = new Thread(Analysis);
                thread.IsBackground = true;
                thread.Start();
                btnWaveDisplay.Text = "暂停显示";
            }

        }
        private void btnWaveLoad_Click(object sender, EventArgs e)
        {
            btnWaveClear.PerformClick();

            if(openWaveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(openWaveFile.FileName);
                    int Wave0 = 0;
                    int Wave1 = 0;
                    int Wave2 = 0;
                    int Wave3 = 0;
                    int Wave4 = 0;
                    int Wave5 = 0;
                    int Wave6 = 0;
                    int Wave7 = 0;
                    int Wave8 = 0;
                    foreach (string item in lines)
                    {
                        //if (item.Length > 2)
                        {
                            string[] str = item.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            //Wave0 = Convert.ToInt32(str[0]);
                            Wave1 = Convert.ToInt32(str[1]);
                            Wave2 = Convert.ToInt32(str[2]);
                            Wave3 = Convert.ToInt32(str[3]);
                            Wave4 = Convert.ToInt32(str[4]);
                            Wave5 = Convert.ToInt32(str[5]);
                            Wave6 = Convert.ToInt32(str[6]);
                            Wave7 = Convert.ToInt32(str[7]);
                            Wave8 = Convert.ToInt32(str[8]);
                            dataTables[dataTables.Count -1].Rows.Add(WaveXIndex++, Wave1, Wave2, Wave3, Wave4, Wave5, Wave6, Wave7, Wave8);

                            //表中超过 MaxSeriesLen 条数据 切换下一个表 一个表中数据越长则图表刷新时间越长
                            if (dataTables[dataTables.Count - 1].Rows.Count >= MaxSeriesLen)
                            {
                                dataTables.Add(dataTableType.Clone());
                                for (int i = MaxSeriesLen / 2; i < MaxSeriesLen; i++)
                                {
                                    dataTables[dataTables.Count - 1].Rows.Add(dataTables[dataTables.Count - 1 - 1].Rows[i].ItemArray);
                                }

                                this.BeginInvoke(new Action(() => trackBar1.Maximum += 10));

                                if (ckbUpdata.Checked)
                                {
                                    //显示最新数据点
                                    chartWave.DataSource = dataTables[dataTables.Count - 1];

                                    this.BeginInvoke(new Action(() => { trackBar1.Value = trackBar1.Maximum; }));
                                }
                            }
                        }
                    }
                    chartWave.ChartAreas[0].AxisX.ScaleView.Position = ((WaveXIndex-1)/ MaxSeriesLen )* MaxSeriesLen;
                    chartWave.DataBind();
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void btnWaveSave_Click(object sender, EventArgs e)
        {
            if(WaveXIndex > 0)
            {
                if (saveWaveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (File.Exists(saveWaveFile.FileName))
                        {
                            File.Delete(saveWaveFile.FileName);
                        }

                        for (int i = 0; i < dataTables.Count; i += 2)
                        {
                            foreach (DataRow item in dataTables[i].Rows)
                            {
                                File.AppendAllText(saveWaveFile.FileName, item[0].ToString().PadLeft(10, ' ')
                                                                    + item[1].ToString().PadLeft(10, ' ')
                                                                    + item[2].ToString().PadLeft(10, ' ')
                                                                    + item[3].ToString().PadLeft(10, ' ')
                                                                    + item[4].ToString().PadLeft(10, ' ')
                                                                    + item[5].ToString().PadLeft(10, ' ')
                                                                    + item[6].ToString().PadLeft(10, ' ')
                                                                    + item[7].ToString().PadLeft(10, ' ')
                                                                    + item[8].ToString().PadLeft(10, ' ') + "\r\n");
                            }
                        }
                    }
                    catch(Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }

                }
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            {
                //切换数据源
                chartWave.DataSource = dataTables[trackBar1.Value / 10];
                chartWave.ChartAreas[0].AxisX.ScaleView.Position = trackBar1.Value / 10 * MaxSeriesLen / 2;
            }

            if ((trackBar1.Value % 10) / 10.0 * (dataTables[trackBar1.Value / 10].Rows.Count / 2) + chartWave.ChartAreas[0].AxisX.ScaleView.Size < MaxSeriesLen)
            {
                //获取x轴起始坐标
                chartWave.ChartAreas[0].AxisX.ScaleView.Position = trackBar1.Value / 10 * MaxSeriesLen/2 + (trackBar1.Value % 10) / 10.0 * (dataTables[trackBar1.Value / 10].Rows.Count / 2);
            }
            else
            {
                chartWave.ChartAreas[0].AxisX.ScaleView.Position = 0;
            }

            chartWave.DataBind();
        }
    }
}
