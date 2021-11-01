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
        /// <para>列表导出为CSV文件，支持Excel打开</para>
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
            //导出列名
            for (int i = 0; i < properties.Length; i++)
            {
                //是否忽略
                var igonreAttribute = properties[i].GetCustomAttribute<CSVIgnoreAttribute>();
                if (igonreAttribute != null)
                {
                    continue;
                }

                //获取列名
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
            builder.Append(Environment.NewLine);
            //导出内容
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
                builder.Append(Environment.NewLine);
            }
            File.WriteAllText(fileName, builder.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 读取csv文件并转换为集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">csv文件路径</param>
        /// <exception cref="Exception">找不到路径</exception>
        /// <returns></returns>
        public static List<T> ImportCSV<T>(string fileName) where T : new()
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"找不到路径{fileName}");
            }
            string[] array = File.ReadAllLines(fileName, Encoding.UTF8);
            if (array == null || array.Length <= 0)
            {
                throw new Exception($"文件内容不能为空！");
            }
            //第一行为表头
            if (array.Length == 1)
            {
                return null;
            }
            List<string> headers = array[0].Split(',').ToList();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //每一列对应哪个属性
            List<(int Index, PropertyInfo Property)> infos = new List<(int Index, PropertyInfo Property)>();
            for (int i = 0; i < headers.Count; i++)
            {
                var property = properties.FirstOrDefault(e =>
                e.Name == headers[i] || e.GetCustomAttribute<CSVColumnAttribute>()?.Name == headers[i]);
                if (property == null)
                {
                    throw new Exception($"列名{headers[i]}在类型{typeof(T)}中找不到对应的属性！");
                }
                //忽略
                if (property.GetCustomAttribute<CSVIgnoreAttribute>() != null)
                {
                    continue;
                }
                infos.Add((i, property));
            }

            List<T> list = new List<T>();
            for (int i = 1; i < array.Length; i++)
            {
                string[] textArray = array[i].Split(',');
                if (textArray == null || textArray.Length != headers.Count)
                {
                    throw new Exception($"第{i}行文件内容不能为空或者长度必须为{headers.Count},实际长度为:{textArray?.Length ?? 0}！");
                }
                T item = new T();
                foreach (var info in infos)
                {
                    info.Property.SetValue(item, Convert.ChangeType(textArray[info.Index], info.Property.PropertyType));
                }
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        private static T ConvertType<T>(object val)
        {
            if (val == null)
            {
                return default(T);
            }
            Type type = typeof(T);
            if (type == val.GetType())
            {
                return (T)val;
            }
            if (type == typeof(string))
            {
                return (T)val;
            }
            //枚举
            if (type.IsEnum)
            {
                if (val is string)
                    return (T)Enum.Parse(type, val.ToString());
                else
                    return (T)Enum.ToObject(type, val);
            }
            //泛型
            if (!type.IsInterface && type.IsGenericType)
            {
                //Type innerType = type.GetGenericArguments()[0];
                //object innerValue = ChangeType(val, innerType);
                //return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (val is string && type == typeof(Guid))
            {
                //return new Guid(val as string);
            }
            if (val is string && type == typeof(Version))
            {
                //return new Version(val as string);
            }
            if (!(val is IConvertible))
            {
                return default(T);
            }
            return (T)Convert.ChangeType(val, type);
        }
    }
}
