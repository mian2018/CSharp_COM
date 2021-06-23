﻿
namespace CSharp_串口助手
{
    partial class 串口助手
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlCOM = new System.Windows.Forms.TabControl();
            this.Page1 = new System.Windows.Forms.TabPage();
            this.状态栏 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusCOM = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusTxCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusRxCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxCOMInfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCheck = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSerialName = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txbRx = new System.Windows.Forms.TextBox();
            this.txbTx = new System.Windows.Forms.TextBox();
            this.groupBoxTxInfo = new System.Windows.Forms.GroupBox();
            this.ckbTxUTF8 = new System.Windows.Forms.CheckBox();
            this.ckbTxWordWrap = new System.Windows.Forms.CheckBox();
            this.txbTxAutoTime = new System.Windows.Forms.TextBox();
            this.btnTx = new System.Windows.Forms.Button();
            this.ckbTxHex = new System.Windows.Forms.CheckBox();
            this.ckbAutoTx = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxRxInfo = new System.Windows.Forms.GroupBox();
            this.ckbRxUTF8 = new System.Windows.Forms.CheckBox();
            this.ckbStopDisPlay = new System.Windows.Forms.CheckBox();
            this.ckbRxWordWrap = new System.Windows.Forms.CheckBox();
            this.ckbSaveRxFile = new System.Windows.Forms.CheckBox();
            this.ckbTimeStamp = new System.Windows.Forms.CheckBox();
            this.ckbRxHex = new System.Windows.Forms.CheckBox();
            this.ckbAutoClear = new System.Windows.Forms.CheckBox();
            this.btnClearRx = new System.Windows.Forms.Button();
            this.Page2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Page3 = new System.Windows.Forms.TabPage();
            this.Page4 = new System.Windows.Forms.TabPage();
            this.菜单 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPortCOM = new System.IO.Ports.SerialPort(this.components);
            this.接收窗口右键菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.发送窗体右键菜单 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TxAutoSendTimer = new System.Windows.Forms.Timer(this.components);
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.历史路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件 = new System.Windows.Forms.OpenFileDialog();
            this.清空文本框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlCOM.SuspendLayout();
            this.Page1.SuspendLayout();
            this.状态栏.SuspendLayout();
            this.groupBoxCOMInfo.SuspendLayout();
            this.groupBoxTxInfo.SuspendLayout();
            this.groupBoxRxInfo.SuspendLayout();
            this.Page2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.菜单.SuspendLayout();
            this.发送窗体右键菜单.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlCOM
            // 
            this.tabControlCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCOM.Controls.Add(this.Page1);
            this.tabControlCOM.Controls.Add(this.Page2);
            this.tabControlCOM.Controls.Add(this.Page3);
            this.tabControlCOM.Controls.Add(this.Page4);
            this.tabControlCOM.Location = new System.Drawing.Point(0, 32);
            this.tabControlCOM.Name = "tabControlCOM";
            this.tabControlCOM.SelectedIndex = 0;
            this.tabControlCOM.Size = new System.Drawing.Size(553, 573);
            this.tabControlCOM.TabIndex = 0;
            this.tabControlCOM.SelectedIndexChanged += new System.EventHandler(this.tabControlCOM_SelectedIndexChanged);
            // 
            // Page1
            // 
            this.Page1.Controls.Add(this.状态栏);
            this.Page1.Controls.Add(this.groupBoxCOMInfo);
            this.Page1.Controls.Add(this.txbRx);
            this.Page1.Controls.Add(this.txbTx);
            this.Page1.Controls.Add(this.groupBoxTxInfo);
            this.Page1.Controls.Add(this.groupBoxRxInfo);
            this.Page1.Location = new System.Drawing.Point(4, 22);
            this.Page1.Name = "Page1";
            this.Page1.Padding = new System.Windows.Forms.Padding(3);
            this.Page1.Size = new System.Drawing.Size(545, 547);
            this.Page1.TabIndex = 0;
            this.Page1.Text = "串口助手";
            this.Page1.UseVisualStyleBackColor = true;
            // 
            // 状态栏
            // 
            this.状态栏.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.状态栏.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStatusCOM,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStatusTxCounter,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel7,
            this.toolStatusRxCounter});
            this.状态栏.Location = new System.Drawing.Point(3, 522);
            this.状态栏.Name = "状态栏";
            this.状态栏.Size = new System.Drawing.Size(539, 22);
            this.状态栏.TabIndex = 29;
            this.状态栏.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(65, 17);
            this.toolStripStatusLabel1.Text = "当前状态：";
            // 
            // toolStatusCOM
            // 
            this.toolStatusCOM.Name = "toolStatusCOM";
            this.toolStatusCOM.Size = new System.Drawing.Size(53, 17);
            this.toolStatusCOM.Text = "等待打开";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabel4.Text = "已发送字节：";
            // 
            // toolStatusTxCounter
            // 
            this.toolStatusTxCounter.Name = "toolStatusTxCounter";
            this.toolStatusTxCounter.Size = new System.Drawing.Size(53, 17);
            this.toolStatusTxCounter.Text = "99999999";
            this.toolStatusTxCounter.ToolTipText = "双击清零";
            this.toolStatusTxCounter.DoubleClick += new System.EventHandler(this.toolStatusTxCounter_DoubleClick);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel6.Text = "|";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(77, 17);
            this.toolStripStatusLabel7.Text = "已接收字节：";
            // 
            // toolStatusRxCounter
            // 
            this.toolStatusRxCounter.Name = "toolStatusRxCounter";
            this.toolStatusRxCounter.Size = new System.Drawing.Size(53, 17);
            this.toolStatusRxCounter.Text = "99999999";
            this.toolStatusRxCounter.ToolTipText = "双击清零";
            this.toolStatusRxCounter.DoubleClick += new System.EventHandler(this.toolStatusRxCounter_DoubleClick);
            // 
            // groupBoxCOMInfo
            // 
            this.groupBoxCOMInfo.AutoSize = true;
            this.groupBoxCOMInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBoxCOMInfo.Controls.Add(this.label6);
            this.groupBoxCOMInfo.Controls.Add(this.label5);
            this.groupBoxCOMInfo.Controls.Add(this.cmbStopBits);
            this.groupBoxCOMInfo.Controls.Add(this.label3);
            this.groupBoxCOMInfo.Controls.Add(this.cmbDataBits);
            this.groupBoxCOMInfo.Controls.Add(this.label4);
            this.groupBoxCOMInfo.Controls.Add(this.cmbCheck);
            this.groupBoxCOMInfo.Controls.Add(this.label2);
            this.groupBoxCOMInfo.Controls.Add(this.cmbBaudRate);
            this.groupBoxCOMInfo.Controls.Add(this.label1);
            this.groupBoxCOMInfo.Controls.Add(this.cmbSerialName);
            this.groupBoxCOMInfo.Controls.Add(this.btnOpen);
            this.groupBoxCOMInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxCOMInfo.Location = new System.Drawing.Point(0, 3);
            this.groupBoxCOMInfo.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxCOMInfo.Name = "groupBoxCOMInfo";
            this.groupBoxCOMInfo.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxCOMInfo.Size = new System.Drawing.Size(197, 261);
            this.groupBoxCOMInfo.TabIndex = 11;
            this.groupBoxCOMInfo.TabStop = false;
            this.groupBoxCOMInfo.Text = " ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(4, 218);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "串口操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(4, 179);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "停止位";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbStopBits.Location = new System.Drawing.Point(98, 180);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(90, 22);
            this.cmbStopBits.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "数据位";
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.cmbDataBits.Location = new System.Drawing.Point(98, 140);
            this.cmbDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(90, 22);
            this.cmbDataBits.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "校验位";
            // 
            // cmbCheck
            // 
            this.cmbCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheck.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCheck.FormattingEnabled = true;
            this.cmbCheck.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cmbCheck.Location = new System.Drawing.Point(98, 100);
            this.cmbCheck.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCheck.Name = "cmbCheck";
            this.cmbCheck.Size = new System.Drawing.Size(90, 22);
            this.cmbCheck.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(4, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "波特率";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200",
            "自定义"});
            this.cmbBaudRate.Location = new System.Drawing.Point(98, 60);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(90, 22);
            this.cmbBaudRate.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "端口号";
            // 
            // cmbSerialName
            // 
            this.cmbSerialName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbSerialName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialName.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSerialName.FormattingEnabled = true;
            this.cmbSerialName.Location = new System.Drawing.Point(98, 20);
            this.cmbSerialName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSerialName.Name = "cmbSerialName";
            this.cmbSerialName.Size = new System.Drawing.Size(90, 22);
            this.cmbSerialName.TabIndex = 9;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.DeepPink;
            this.btnOpen.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(98, 218);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 24);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = false;
            // 
            // txbRx
            // 
            this.txbRx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbRx.BackColor = System.Drawing.SystemColors.Control;
            this.txbRx.Location = new System.Drawing.Point(200, 3);
            this.txbRx.MaxLength = 10485760;
            this.txbRx.Multiline = true;
            this.txbRx.Name = "txbRx";
            this.txbRx.ReadOnly = true;
            this.txbRx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbRx.Size = new System.Drawing.Size(345, 396);
            this.txbRx.TabIndex = 27;
            this.txbRx.WordWrap = false;
            // 
            // txbTx
            // 
            this.txbTx.AcceptsReturn = true;
            this.txbTx.AcceptsTab = true;
            this.txbTx.AllowDrop = true;
            this.txbTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTx.ContextMenuStrip = this.发送窗体右键菜单;
            this.txbTx.Location = new System.Drawing.Point(200, 398);
            this.txbTx.MaxLength = 10485760;
            this.txbTx.Multiline = true;
            this.txbTx.Name = "txbTx";
            this.txbTx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbTx.Size = new System.Drawing.Size(345, 121);
            this.txbTx.TabIndex = 28;
            this.txbTx.WordWrap = false;
            this.txbTx.DragDrop += new System.Windows.Forms.DragEventHandler(this.txbTx_DragDrop);
            this.txbTx.DragEnter += new System.Windows.Forms.DragEventHandler(this.txbTx_DragEnter);
            // 
            // groupBoxTxInfo
            // 
            this.groupBoxTxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTxInfo.AutoSize = true;
            this.groupBoxTxInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBoxTxInfo.Controls.Add(this.ckbTxUTF8);
            this.groupBoxTxInfo.Controls.Add(this.ckbTxWordWrap);
            this.groupBoxTxInfo.Controls.Add(this.txbTxAutoTime);
            this.groupBoxTxInfo.Controls.Add(this.btnTx);
            this.groupBoxTxInfo.Controls.Add(this.ckbTxHex);
            this.groupBoxTxInfo.Controls.Add(this.ckbAutoTx);
            this.groupBoxTxInfo.Controls.Add(this.label7);
            this.groupBoxTxInfo.Location = new System.Drawing.Point(0, 398);
            this.groupBoxTxInfo.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxTxInfo.MinimumSize = new System.Drawing.Size(197, 114);
            this.groupBoxTxInfo.Name = "groupBoxTxInfo";
            this.groupBoxTxInfo.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxTxInfo.Size = new System.Drawing.Size(197, 120);
            this.groupBoxTxInfo.TabIndex = 26;
            this.groupBoxTxInfo.TabStop = false;
            // 
            // ckbTxUTF8
            // 
            this.ckbTxUTF8.AutoSize = true;
            this.ckbTxUTF8.Location = new System.Drawing.Point(97, 87);
            this.ckbTxUTF8.Name = "ckbTxUTF8";
            this.ckbTxUTF8.Size = new System.Drawing.Size(54, 16);
            this.ckbTxUTF8.TabIndex = 29;
            this.ckbTxUTF8.Text = "UTF-8";
            this.ckbTxUTF8.UseVisualStyleBackColor = true;
            this.ckbTxUTF8.CheckedChanged += new System.EventHandler(this.ckbTxUTF8_CheckedChanged);
            // 
            // ckbTxWordWrap
            // 
            this.ckbTxWordWrap.AutoSize = true;
            this.ckbTxWordWrap.Location = new System.Drawing.Point(3, 87);
            this.ckbTxWordWrap.Name = "ckbTxWordWrap";
            this.ckbTxWordWrap.Size = new System.Drawing.Size(72, 16);
            this.ckbTxWordWrap.TabIndex = 27;
            this.ckbTxWordWrap.Text = "自动换行";
            this.ckbTxWordWrap.UseVisualStyleBackColor = true;
            this.ckbTxWordWrap.CheckedChanged += new System.EventHandler(this.ckbTxWordWrap_CheckedChanged);
            // 
            // txbTxAutoTime
            // 
            this.txbTxAutoTime.Font = new System.Drawing.Font("隶书", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbTxAutoTime.Location = new System.Drawing.Point(98, 14);
            this.txbTxAutoTime.MaxLength = 10;
            this.txbTxAutoTime.Name = "txbTxAutoTime";
            this.txbTxAutoTime.Size = new System.Drawing.Size(90, 23);
            this.txbTxAutoTime.TabIndex = 26;
            this.txbTxAutoTime.Text = "500";
            this.txbTxAutoTime.TextChanged += new System.EventHandler(this.txbTxAutoTime_TextChanged);
            // 
            // btnTx
            // 
            this.btnTx.BackColor = System.Drawing.Color.DeepPink;
            this.btnTx.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTx.Location = new System.Drawing.Point(98, 43);
            this.btnTx.Margin = new System.Windows.Forms.Padding(4);
            this.btnTx.Name = "btnTx";
            this.btnTx.Size = new System.Drawing.Size(90, 24);
            this.btnTx.TabIndex = 21;
            this.btnTx.Text = "手动发送";
            this.btnTx.UseVisualStyleBackColor = false;
            this.btnTx.Click += new System.EventHandler(this.btnTx_Click);
            // 
            // ckbTxHex
            // 
            this.ckbTxHex.AutoSize = true;
            this.ckbTxHex.Location = new System.Drawing.Point(3, 65);
            this.ckbTxHex.Name = "ckbTxHex";
            this.ckbTxHex.Size = new System.Drawing.Size(96, 16);
            this.ckbTxHex.TabIndex = 23;
            this.ckbTxHex.Text = "十六进制发送";
            this.ckbTxHex.UseVisualStyleBackColor = true;
            this.ckbTxHex.CheckedChanged += new System.EventHandler(this.ckbTxHex_CheckedChanged);
            // 
            // ckbAutoTx
            // 
            this.ckbAutoTx.AutoSize = true;
            this.ckbAutoTx.Location = new System.Drawing.Point(3, 43);
            this.ckbAutoTx.Name = "ckbAutoTx";
            this.ckbAutoTx.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoTx.TabIndex = 13;
            this.ckbAutoTx.Text = "自动发送";
            this.ckbAutoTx.UseVisualStyleBackColor = true;
            this.ckbAutoTx.CheckedChanged += new System.EventHandler(this.ckbAutoTx_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "自动发送周期";
            // 
            // groupBoxRxInfo
            // 
            this.groupBoxRxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxRxInfo.AutoSize = true;
            this.groupBoxRxInfo.BackColor = System.Drawing.Color.DodgerBlue;
            this.groupBoxRxInfo.Controls.Add(this.ckbRxUTF8);
            this.groupBoxRxInfo.Controls.Add(this.ckbStopDisPlay);
            this.groupBoxRxInfo.Controls.Add(this.ckbRxWordWrap);
            this.groupBoxRxInfo.Controls.Add(this.ckbSaveRxFile);
            this.groupBoxRxInfo.Controls.Add(this.ckbTimeStamp);
            this.groupBoxRxInfo.Controls.Add(this.ckbRxHex);
            this.groupBoxRxInfo.Controls.Add(this.ckbAutoClear);
            this.groupBoxRxInfo.Controls.Add(this.btnClearRx);
            this.groupBoxRxInfo.Location = new System.Drawing.Point(0, 267);
            this.groupBoxRxInfo.MinimumSize = new System.Drawing.Size(197, 114);
            this.groupBoxRxInfo.Name = "groupBoxRxInfo";
            this.groupBoxRxInfo.Size = new System.Drawing.Size(197, 128);
            this.groupBoxRxInfo.TabIndex = 13;
            this.groupBoxRxInfo.TabStop = false;
            // 
            // ckbRxUTF8
            // 
            this.ckbRxUTF8.AutoSize = true;
            this.ckbRxUTF8.Location = new System.Drawing.Point(97, 91);
            this.ckbRxUTF8.Name = "ckbRxUTF8";
            this.ckbRxUTF8.Size = new System.Drawing.Size(54, 16);
            this.ckbRxUTF8.TabIndex = 28;
            this.ckbRxUTF8.Text = "UTF-8";
            this.ckbRxUTF8.UseVisualStyleBackColor = true;
            this.ckbRxUTF8.CheckedChanged += new System.EventHandler(this.ckbRxUTF8_CheckedChanged);
            // 
            // ckbStopDisPlay
            // 
            this.ckbStopDisPlay.AutoSize = true;
            this.ckbStopDisPlay.Location = new System.Drawing.Point(2, 22);
            this.ckbStopDisPlay.Name = "ckbStopDisPlay";
            this.ckbStopDisPlay.Size = new System.Drawing.Size(72, 16);
            this.ckbStopDisPlay.TabIndex = 27;
            this.ckbStopDisPlay.Text = "停止显示";
            this.ckbStopDisPlay.UseVisualStyleBackColor = true;
            // 
            // ckbRxWordWrap
            // 
            this.ckbRxWordWrap.AutoSize = true;
            this.ckbRxWordWrap.Location = new System.Drawing.Point(97, 70);
            this.ckbRxWordWrap.Name = "ckbRxWordWrap";
            this.ckbRxWordWrap.Size = new System.Drawing.Size(72, 16);
            this.ckbRxWordWrap.TabIndex = 26;
            this.ckbRxWordWrap.Text = "自动换行";
            this.ckbRxWordWrap.UseVisualStyleBackColor = true;
            this.ckbRxWordWrap.CheckedChanged += new System.EventHandler(this.ckbRxWordWrap_CheckedChanged);
            // 
            // ckbSaveRxFile
            // 
            this.ckbSaveRxFile.AutoSize = true;
            this.ckbSaveRxFile.Location = new System.Drawing.Point(2, 92);
            this.ckbSaveRxFile.Name = "ckbSaveRxFile";
            this.ckbSaveRxFile.Size = new System.Drawing.Size(72, 16);
            this.ckbSaveRxFile.TabIndex = 25;
            this.ckbSaveRxFile.Text = "保存接收";
            this.ckbSaveRxFile.UseVisualStyleBackColor = true;
            // 
            // ckbTimeStamp
            // 
            this.ckbTimeStamp.AutoSize = true;
            this.ckbTimeStamp.Location = new System.Drawing.Point(97, 46);
            this.ckbTimeStamp.Name = "ckbTimeStamp";
            this.ckbTimeStamp.Size = new System.Drawing.Size(60, 16);
            this.ckbTimeStamp.TabIndex = 24;
            this.ckbTimeStamp.Text = "时间戳";
            this.ckbTimeStamp.UseVisualStyleBackColor = true;
            // 
            // ckbRxHex
            // 
            this.ckbRxHex.AutoSize = true;
            this.ckbRxHex.Location = new System.Drawing.Point(2, 69);
            this.ckbRxHex.Name = "ckbRxHex";
            this.ckbRxHex.Size = new System.Drawing.Size(96, 16);
            this.ckbRxHex.TabIndex = 23;
            this.ckbRxHex.Text = "十六进制接收";
            this.ckbRxHex.UseVisualStyleBackColor = true;
            // 
            // ckbAutoClear
            // 
            this.ckbAutoClear.AutoSize = true;
            this.ckbAutoClear.Location = new System.Drawing.Point(2, 46);
            this.ckbAutoClear.Name = "ckbAutoClear";
            this.ckbAutoClear.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoClear.TabIndex = 13;
            this.ckbAutoClear.Text = "自动清空";
            this.ckbAutoClear.UseVisualStyleBackColor = true;
            // 
            // btnClearRx
            // 
            this.btnClearRx.BackColor = System.Drawing.Color.DeepPink;
            this.btnClearRx.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearRx.Location = new System.Drawing.Point(97, 15);
            this.btnClearRx.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearRx.Name = "btnClearRx";
            this.btnClearRx.Size = new System.Drawing.Size(90, 24);
            this.btnClearRx.TabIndex = 22;
            this.btnClearRx.Text = "清空接收";
            this.btnClearRx.UseVisualStyleBackColor = false;
            this.btnClearRx.Click += new System.EventHandler(this.btnClearTx_Click);
            // 
            // Page2
            // 
            this.Page2.Controls.Add(this.dataGridView1);
            this.Page2.Location = new System.Drawing.Point(4, 22);
            this.Page2.Name = "Page2";
            this.Page2.Padding = new System.Windows.Forms.Padding(3);
            this.Page2.Size = new System.Drawing.Size(545, 547);
            this.Page2.TabIndex = 1;
            this.Page2.Text = "多条发送";
            this.Page2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(194, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(343, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Page3
            // 
            this.Page3.Location = new System.Drawing.Point(4, 22);
            this.Page3.Name = "Page3";
            this.Page3.Size = new System.Drawing.Size(545, 547);
            this.Page3.TabIndex = 2;
            this.Page3.Text = "波形显示";
            this.Page3.UseVisualStyleBackColor = true;
            // 
            // Page4
            // 
            this.Page4.Location = new System.Drawing.Point(4, 22);
            this.Page4.Name = "Page4";
            this.Page4.Size = new System.Drawing.Size(545, 547);
            this.Page4.TabIndex = 3;
            this.Page4.Text = "modus";
            this.Page4.UseVisualStyleBackColor = true;
            // 
            // 菜单
            // 
            this.菜单.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.菜单.Location = new System.Drawing.Point(0, 0);
            this.菜单.Name = "菜单";
            this.菜单.Size = new System.Drawing.Size(553, 24);
            this.菜单.TabIndex = 1;
            this.菜单.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // serialPortCOM
            // 
            this.serialPortCOM.ReadBufferSize = 40960;
            this.serialPortCOM.WriteBufferSize = 4096000;
            this.serialPortCOM.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortCOM_DataReceived);
            // 
            // 接收窗口右键菜单
            // 
            this.接收窗口右键菜单.Name = "接收窗口右键菜单";
            this.接收窗口右键菜单.Size = new System.Drawing.Size(61, 4);
            // 
            // 发送窗体右键菜单
            // 
            this.发送窗体右键菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.历史路径ToolStripMenuItem,
            this.清空文本框ToolStripMenuItem});
            this.发送窗体右键菜单.Name = "发送窗体右键菜单";
            this.发送窗体右键菜单.Size = new System.Drawing.Size(137, 70);
            // 
            // TxAutoSendTimer
            // 
            this.TxAutoSendTimer.Interval = 500;
            this.TxAutoSendTimer.Tick += new System.EventHandler(this.TxAutoSendTimer_Tick);
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Image = global::CSharp_串口助手.Properties.Resources.打开;
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 历史路径ToolStripMenuItem
            // 
            this.历史路径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.历史路径ToolStripMenuItem.Image = global::CSharp_串口助手.Properties.Resources.历史记录;
            this.历史路径ToolStripMenuItem.Name = "历史路径ToolStripMenuItem";
            this.历史路径ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.历史路径ToolStripMenuItem.Text = "历史路径";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = " ";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = " ";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = " ";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem5.Text = " ";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem6.Text = " ";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // 打开文件
            // 
            this.打开文件.DefaultExt = "txt";
            this.打开文件.FileName = "打开文件";
            this.打开文件.Filter = "文本文件|*.txt|二进制文件|*.bin|十六进制文件|*.hex|所有文件|*.*";
            // 
            // 清空文本框ToolStripMenuItem
            // 
            this.清空文本框ToolStripMenuItem.Image = global::CSharp_串口助手.Properties.Resources.清除;
            this.清空文本框ToolStripMenuItem.Name = "清空文本框ToolStripMenuItem";
            this.清空文本框ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清空文本框ToolStripMenuItem.Text = "清空文本框";
            this.清空文本框ToolStripMenuItem.Click += new System.EventHandler(this.清空文本框ToolStripMenuItem_Click);
            // 
            // 串口助手
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 607);
            this.Controls.Add(this.tabControlCOM);
            this.Controls.Add(this.菜单);
            this.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.菜单;
            this.MinimumSize = new System.Drawing.Size(511, 622);
            this.Name = "串口助手";
            this.Text = "串口助手";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.串口助手_FormClosing);
            this.Load += new System.EventHandler(this.串口助手_Load);
            this.tabControlCOM.ResumeLayout(false);
            this.Page1.ResumeLayout(false);
            this.Page1.PerformLayout();
            this.状态栏.ResumeLayout(false);
            this.状态栏.PerformLayout();
            this.groupBoxCOMInfo.ResumeLayout(false);
            this.groupBoxCOMInfo.PerformLayout();
            this.groupBoxTxInfo.ResumeLayout(false);
            this.groupBoxTxInfo.PerformLayout();
            this.groupBoxRxInfo.ResumeLayout(false);
            this.groupBoxRxInfo.PerformLayout();
            this.Page2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.菜单.ResumeLayout(false);
            this.菜单.PerformLayout();
            this.发送窗体右键菜单.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlCOM;
        private System.Windows.Forms.TabPage Page1;
        private System.Windows.Forms.TabPage Page2;
        private System.Windows.Forms.MenuStrip 菜单;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.TextBox txbRx;
        private System.Windows.Forms.TextBox txbTx;
        private System.Windows.Forms.GroupBox groupBoxTxInfo;
        private System.Windows.Forms.TextBox txbTxAutoTime;
        private System.Windows.Forms.Button btnTx;
        private System.Windows.Forms.CheckBox ckbTxHex;
        private System.Windows.Forms.CheckBox ckbAutoTx;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxRxInfo;
        private System.Windows.Forms.CheckBox ckbTimeStamp;
        private System.Windows.Forms.CheckBox ckbRxHex;
        private System.Windows.Forms.CheckBox ckbAutoClear;
        private System.Windows.Forms.Button btnClearRx;
        private System.Windows.Forms.GroupBox groupBoxCOMInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSerialName;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.TabPage Page3;
        private System.Windows.Forms.TabPage Page4;
        private System.Windows.Forms.StatusStrip 状态栏;
        private System.Windows.Forms.CheckBox ckbSaveRxFile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusCOM;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusTxCounter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusRxCounter;
        private System.IO.Ports.SerialPort serialPortCOM;
        private System.Windows.Forms.CheckBox ckbTxWordWrap;
        private System.Windows.Forms.CheckBox ckbRxWordWrap;
        private System.Windows.Forms.CheckBox ckbStopDisPlay;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.CheckBox ckbTxUTF8;
        private System.Windows.Forms.CheckBox ckbRxUTF8;
        private System.Windows.Forms.ContextMenuStrip 接收窗口右键菜单;
        private System.Windows.Forms.ContextMenuStrip 发送窗体右键菜单;
        private System.Windows.Forms.Timer TxAutoSendTimer;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 历史路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.OpenFileDialog 打开文件;
        private System.Windows.Forms.ToolStripMenuItem 清空文本框ToolStripMenuItem;
    }
}

