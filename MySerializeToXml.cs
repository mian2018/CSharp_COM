using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CSharp_串口助手
{
    /// <summary>
    /// 序列化
    /// </summary>
    public static class MySerializeToXml
    {
        /// <summary>
        /// 将对象序列化为XML数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="filePath">xml存放路径</param>
        public static void SerializeToXml<T>(T obj, string filePath)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using(Stream fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                xs.Serialize(fStream, obj);
            }
        }

        /// <summary>
        /// 将XML数据反序列化为指定类型对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="filePath">xml文件存放路径</param>
        /// <returns></returns>
        public static T DeserializeWithXml<T>(string filePath)
        {
            object obj = new object();

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using(Stream fStream = File.OpenRead(filePath))
            {
                obj = xs.Deserialize(fStream);
            }
            return (T)obj;
        }
    }
}
