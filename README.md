# C# 学习笔记（13）自己的串口助手
## UI界面
>界面部分参考野火串口助手，自己拖控件拖一个即可

![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630200615106.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)


## 功能实现
### 扫描串口
1. 方法一 使用串口自带的get函数
>比较简单实用

```csharp
SerialPort.GetPortNames();
```

2. 方法二 扫描设备管理器
>如果开发特定设备具有特定串口名，可以扫描设备管理器获取串口全名，筛选含有特定名称的串口

![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630201129190.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)

```csharp
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
```

### 串口热插拔
>当使用串口时，如果串口断开连接，立即关闭串口，当有新的串口插入，刷新串口列表

```csharp
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
```

### 接收显示
![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630202703537.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)

```csharp
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
    //异步处理 防止UI界面卡死
    this.BeginInvoke(new Action<byte[]>((byte[] data)=> { DisplayRxInfo(data); }), recvByteTemp);
}

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
                str = str.Replace("0A", "0A \r\n[" + DateTime.Now.Millisecond.ToString() + "]->>>");
            }
        }
        else
        {
            str = RxEncoding.GetString(data);
            if (ckbTimeStamp.Checked)
            {
                //时间戳
                str = str.Replace("\n", "\n[" + DateTime.Now.Millisecond.ToString() + "]->>>");
            }
        }


        if (ckbAutoClear.Checked && txbRx.TextLength > 4096)
        {
            //自动清除
            txbRx.Text = "";
        }
        txbRx.AppendText(str);

        if (ckbSaveRxFile.Checked)
        {
            //将接收信息写入文件
            File.AppendAllText(CurrentFilePath, str);
        }

        RxCounter += data.Length;
    }
    catch
    {

    }
}
```

### 进制转换

```csharp
public static class MyConver
{
    /// <summary>
    /// 字节数组转16进制字符串
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ByteToHex(byte[] data)
    {
        StringBuilder stringBuilder = new StringBuilder(1024);
        for (int i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("X2") + " ");
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// hex string字节数组转byte
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] HexToByte(string str)
    {
        str = str.Replace(" ", "");

        if (str.Length % 2 != 0)
        {
            str = str.Insert(str.Length - 1, "0");
        }
        byte[] bytesHex = new byte[str.Length / 2];

        try
        {
            for (int i = 0; i < str.Length / 2; i++)
            {
                bytesHex[i] = Convert.ToByte(str.Substring(2 * i, 2), 16);
            }
        }
        catch
        {

        }
        return bytesHex;
    }
}
```

### 保存配置信息
>c# 提供了setting文件，可以十分方便的保存配置信息

![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630202146269.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)
* 保存时一定要记得使用 Properties.Settings.Default.Save();保存
```csharp
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

    Properties.Settings.Default.Save();
}
```

### 历史路径

```csharp
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
```

### 窗口拖拽
* 开启控件拖拽
![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630202905503.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)
* 添加事件
![在这里插入图片描述](https://img-blog.csdnimg.cn/20210630202932978.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MjM3ODMxOQ==,size_16,color_FFFFFF,t_70)

```csharp
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
```


[源码](https://github.com/mian2018/CSharp_COM) https://github.com/mian2018/CSharp_COM
