using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KangWeiCommon
{
    /// <summary>
    /// 通用辅助类
    /// </summary>
    public class KangWeiUtil
    {
        /// <summary>
        /// ip地址转换为UInt类型
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        /// <example>192.168.20.46</example>
        public static uint IpToUInt(string ip)
        {
            string[] array = ip.Split('.');
            uint[] arr = new uint[4];
            for (int i = 0; i < array.Length; i++)
            {
                arr[i] = Convert.ToByte(array[i]);
            }
            uint ret = 0;
            ret = ((arr[0] & 0xff) | ret) << 8;
            ret = ((arr[1] & 0xff) | ret) << 8;
            ret = ((arr[2] & 0xff) | ret) << 8;
            ret = ((arr[3] & 0xff) | ret);
            return ret;
        }
       
        /// <summary>
        /// int转换为ip地址
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string UIntToIp(uint number)
        {
            return new StringBuilder()
                   .Append((number >> 24) & 0xff)
                   .Append('.')
                   .Append((number >> 16) & 0xff)
                   .Append('.')
                   .Append((number >> 8) & 0xff)
                   .Append('.')
                   .Append((number & 0xff))
                   .ToString();
        }
        /// <summary>
        /// <para>列表导出CSV，支持Excel打开</para>
        /// <para>默认情况下，设置Exel列名为属性名称。<see cref="CSVColumnAttribute"/>属性可以自定义列名</para>
        /// <para>默认情况下，导出所有属性。如果有某些属性不需要导出，可以设置<see cref="CSVIgnoreAttribute"/>属性</para> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName">文件保存路径</param>
        /// <exception cref="NullReferenceException"></exception>
        public static void ExportCSV<T>(IEnumerable<T> list, string fileName)
        {
            if (list == null || !list.Any())
            {
                throw new NullReferenceException($"参数{nameof(list)}不能为空集合！");
            }
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < properties.Length; i++)
            {
                var igonreAttribute = properties[i].GetCustomAttribute<CSVIgnoreAttribute>();
                if (igonreAttribute != null)
                {
                    continue;
                }
                string name = properties[i].Name;
                var nameAttribute = properties[i].GetCustomAttribute<CSVColumnAttribute>();
                if (nameAttribute != null && !string.IsNullOrWhiteSpace(nameAttribute.Name))
                {
                    name = nameAttribute.Name;
                }
                if (i == properties.Length - 1)
                {
                    builder.Append(name);
                }
                else
                {
                    builder.Append(name + ",");
                }
            }
            builder.AppendLine();
            foreach (var item in list)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    var igonreAttribute = properties[i].GetCustomAttribute<CSVIgnoreAttribute>();
                    if (igonreAttribute != null)
                    {
                        continue;
                    }

                    object value = properties[i].GetValue(item);
                    if (i == properties.Length - 1)
                    {
                        builder.Append(value);
                    }
                    else
                    {
                        builder.Append(value + ",");
                    }
                }
                builder.AppendLine();
            }
            File.WriteAllText(fileName, builder.ToString());
        }
        /// <summary>
        /// 读取csv文件并转换为集合
        /// todo caoruipeng
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static List<T> ImportCSV<T>(string fileName)
        {
            return null;
        }
    }
}
