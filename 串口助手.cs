using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_串口助手
{
    public partial class 串口助手 : Form
    {
        private UInt32 _rxCounter = 0;
        private UInt32 RxCounter
        {
            get { return _rxCounter; }
            set { _rxCounter = value; toolStatusRxCounter.Text = _rxCounter.ToString(); }
        }

        private UInt32 _txCounter = 0;
        private UInt32 TxCounter
        {
            get { return _txCounter; }
            set { _txCounter = value; toolStatusTxCounter.Text = _txCounter.ToString(); }
        }
        public 串口助手()
        {
            InitializeComponent();
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
    }
}
