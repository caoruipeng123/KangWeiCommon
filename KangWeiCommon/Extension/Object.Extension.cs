using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KangWeiCommon
{
    /// <summary>
    /// 系统类的扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 判断一个object类型是否为空，为空返回true。
        /// </summary>
        /// <param name="this"></param>
        /// <returns>对象为空返回true，否则返回false</returns>
        public static bool IsNull(this object @this)
        {
            return @this == null;
        }
       
        /// <summary>
        /// 判断一个object类型是否为空，为空返回true并执行指定委托
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action">对象为空是，要执行的委托</param>
        /// <returns>对象为空返回true，否则返回false</returns>
        public static bool IsNull(this object @this, Action action)
        {
            if (@this == null)
            {
                if (action != null)
                {
                    action();
                }
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// 判断一个object类型是否不为空，如果不为空，返回true
        /// </summary>
        /// <param name="this"></param>
        /// <returns>对象不为空返回true，否则返回false</returns>
        public static bool IsNotNull(this object @this)
        {
            return @this != null;
        }
      
        /// <summary>
        /// 判断一个object类型是否不为空，如果不为空，返回true
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action">对象不为空时，要执行的委托</param>
        /// <returns>对象不为空返回true，否则返回false</returns>
        public static bool IsNotNull(this object @this, Action action)
        {
            if (@this != null)
            {
                if (action != null)
                    action();
                return true;
            }
            return false;
        }
      
        /// <summary>
        /// 将object类型转换为int类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ToInt(this object @this)
        {
            if (@this == null)
                return default;
            return Convert.ToInt32(@this);
        }
      
        /// <summary>
        /// 将object类型转换为decimal类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object @this)
        {
            if (@this == null)
                return default;
            return Convert.ToDecimal(@this);
        }
      
        /// <summary>
        /// 将object类型转换为Double类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ToDouble(this object @this)
        {
            if (@this == null)
                return default;
            return Convert.ToDouble(@this);
        }
      
        /// <summary>
        /// 将object类型转换为单精度Single（float）类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static float ToFloat(this object @this)
        {
            if (@this == null)
                return default;
            return Convert.ToSingle(@this);
        }
      
        /// <summary>
        /// 将对象序列化为xml字符串
        /// </summary>
        /// <param name="this">要序列化的对象</param>
        /// <remarks>被序列化的对象需要实现 <see cref="SerializableAttribute"/>特性 </remarks>
        /// <returns>xml格式字符串</returns>
        public static string SerializeToXml(this object @this)
        {
            if (@this == null) 
            { 
                return null;
            }
            Type type = @this.GetType();
            if (type.IsSerializable)
            {
                XmlSerializer xs = new XmlSerializer(type);
                using (var stringWriter = new StringWriter())
                {
                    xs.Serialize(stringWriter, @this);
                    using (var streamReader = new StringReader(stringWriter.GetStringBuilder().ToString()))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            else
            {
                throw new Exception("对象需要实现Serializable特性！");
            }
        }

        /// <summary>
        /// 对象序列化为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns>Json字符串</returns>
        public static string SerializeToJson<T>(this T @this, Encoding encoding=null)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            if(encoding == null)
            {
                encoding = Encoding.Default;
            }
            using (var memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, @this);
                return encoding.GetString(memoryStream.ToArray());
            }
        }

    }
}
