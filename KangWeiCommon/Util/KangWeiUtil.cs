using System;
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
    }
}
