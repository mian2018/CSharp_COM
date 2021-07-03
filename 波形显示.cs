using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public partial class 串口助手 : Form
    {
        private const int MaxSeriesLen = 10000;
        private int WaveIndex = 0;
        object chartLock = new object();
        private DataTable dataTableType = new DataTable("Wave");
        List<DataTable> dataTables = new List<DataTable>(1000);
        //图表刷新线程
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

            for (int i = 0; i < 1000; i++)
            {
                dataTables.Add(dataTableType.Clone());
            }
            //绑定数据源
            chartWave.DataSource = dataTables[WaveIndex];

            cmbWaveType.SelectedIndex = 0;
        }

        byte frameHead = 0xaa;
        int frameLen = 38;
        private void Analysis()
        {
            //取消串口接收回调事件
            serialPortCOM.DataReceived -= serialPortCOM_DataReceived;
            List<byte> listBytes = new List<byte>(1024);
            int counter = 0;
            int yValue = int.MaxValue;
            while (true)
            {
                Thread.Sleep(50);
                yValue = int.MaxValue;
                counter++;
                if (serialPortCOM.IsOpen)
                {
                    byte[] bytes = new byte[serialPortCOM.BytesToRead];
                    serialPortCOM.Read(bytes, 0, bytes.Length);
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
                                dataTables[WaveIndex].Rows.Add(Wave1, Wave2, Wave3, Wave4, Wave5, Wave6, Wave7, Wave8, 0);
                                
                                //记录y值， 后面修改y轴位置
                                for(int i = 0; i < 8; i++)
                                {
                                    if(chartWave.Series[i].Enabled)
                                    {   
                                        yValue = (int)dataTables[WaveIndex].Rows[dataTables[WaveIndex].Rows.Count - 1][i];
                                        break;
                                    }
                                }

                                //表中超过 MaxSeriesLen 条数据 切换下一个表 一个表中数据越长则图表刷新时间越长
                                if (dataTables[WaveIndex].Rows.Count >= MaxSeriesLen)
                                {
                                    WaveIndex++;
                                    for (int i = MaxSeriesLen/2; i < MaxSeriesLen; i++)
                                    {
                                        dataTables[WaveIndex].Rows.Add(dataTables[WaveIndex - 1].Rows[i].ItemArray);
                                    }

                                    this.BeginInvoke(new Action(() => trackBar1.Maximum += 10));

                                    if (ckbUpdata.Checked)
                                    {
                                        //显示最新数据点
                                        chartWave.DataSource = dataTables[WaveIndex];

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
                        counter = 0;
                        this.BeginInvoke(new Action(() => {

                            lock (chartLock)
                            {
                                //X轴 滚动条位置 保持最新位置 - 99
                                if (ckbUpdata.Checked)
                                {
                                    if (dataTables[dataTables.Count - 1].Rows.Count > chartWave.ChartAreas[0].AxisX.ScaleView.Size)
                                    {
                                        chartWave.ChartAreas[0].AxisX.ScaleView.Position = dataTables[WaveIndex].Rows.Count - 1 - chartWave.ChartAreas[0].AxisX.ScaleView.Size;
                                    }
                                    else
                                    {
                                        chartWave.ChartAreas[0].AxisX.ScaleView.Position = 0;
                                    }

                                    //Y轴 滚动条位置 保持最新位置 - 50
                                    chartWave.ChartAreas[0].AxisY.ScaleView.Position = yValue - 50;
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
            colorDialogWave.Color = ((Label)sender).ForeColor;
            if (colorDialogWave.ShowDialog() == DialogResult.OK)
            {
                ((Label)sender).ForeColor = colorDialogWave.Color;
            }
        }

        private void cmbWaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType seriesChartType = new System.Windows.Forms.DataVisualization.Charting.SeriesChartType();
            if ((string)cmbWaveType.SelectedValue == "曲线")
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else if ((string)cmbWaveType.SelectedValue == "折线")
            {
                seriesChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            }
            else if ((string)cmbWaveType.SelectedValue == "柱状图")
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

        private void txbXSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse(txbXSize.Text);

                if (temp < 10000 && temp > 0)
                {
                    chartWave.ChartAreas[0].AxisX.ScaleView.Size = temp;
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
                for (int i = 0; i <= WaveIndex; i++)
                {
                    dataTables[i].Clear();
                }
                WaveIndex = 0;
                //绑定数据源
                chartWave.DataSource = dataTables[0];
            }
        }

        private void btnWaveDisplay_Click(object sender, EventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Abort();
                btnWaveDisplay.Text = "开始显示";
            }
            else
            {
                //图表刷新线程
                thread = new Thread(Analysis);
                thread.IsBackground = true;
                thread.Start();
                btnWaveDisplay.Text = "暂停显示";
            }

        }
    }
}
