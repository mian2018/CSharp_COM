using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_串口助手
{
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
}
