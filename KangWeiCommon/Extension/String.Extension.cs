using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KangWeiCommon
{
    /// <summary>
    /// 系统类的扩展
    /// </summary>
    public static partial class Extensions
    {
        #region 判断字符串为空或者不为空
        /// <summary>
        /// 判断字符串是否为空。 null、string.empty、""、" "都返回true
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
       
        /// <summary>
        /// 判断字符串是否为空。为空时返回ture并执行指定的委托
        /// </summary>
        /// <param name="str"></param>
        /// <param name="action">字符串为空时，执行的委托</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str, Action action)
        {
            if (str == null || string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                if (action != null)
                    action();
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// 判断字符串是否不为空。null、string.empty、""、" ",任何一种情况都会视为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }
     
        /// <summary>
        /// 断字符串是否不为空。不为空时返回true并执行指定委托
        /// </summary>
        /// <param name="str"></param>
        /// <param name="action">对象不为空时，指定的委托</param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str, Action action)
        {
            if (!str.IsNullOrEmpty())
            {
                if (action != null)
                    action();
                return true;
            }
            return false;
        }
        #endregion

        #region 字符串和Decimal类型
        /// <summary>
        /// 判断字符串是否能转换为Decimal类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string str)
        {
            
            return decimal.TryParse(str, out decimal returnValue);
        }
       
        /// <summary>
        /// 判断字符串是否能转换为Decimal类型。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string str, out decimal returnValue)
        {
            return decimal.TryParse(str, out returnValue);
        }
      
        /// <summary>
        /// 字符串转换为Decimal类型，如果转换失败，返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue = 0)
        {
            return decimal.TryParse(str, out decimal returnValue) ? returnValue : defaultValue;
        }
      
        /// <summary>
        /// 字符串转换为Decimal类型并四舍五入保留指定位数的小数
        /// </summary>
        /// <param name="str">被转换的字符串</param>
        /// <param name="decimals">保留的小数位数，默认值为2</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, int decimals)
        {
            return decimal.TryParse(str, out decimal returnValue) ?
                decimal.Round(returnValue, decimals, MidpointRounding.AwayFromZero)
                : default;
        }
        #endregion

        #region 字符串和Float类型
        /// <summary>
        /// 判断字符串是否能转换为Float类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(this string str)
        {
            return float.TryParse(str, out float returnValue);
        }
       
        /// <summary>
        /// 判断字符串是否能转换为Float类型。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsFloat(this string str, out float returnValue)
        {
            return float.TryParse(str, out returnValue);
        }
       
        /// <summary>
        /// 字符串转换为Float类型，如果转换失败，返回defaultValue
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue = 0)
        {
            return float.TryParse(str, out float returnValue) ? returnValue : defaultValue;
        }
        #endregion

        #region 字符串和Double类型
        /// <summary>
        /// 判断字符串能否成功转换为Double类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(this string str)
        {
            return double.TryParse(str, out double returnValue);
        }
      
        /// <summary>
        /// 判断字符串能否成功转换为Double类型。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsDouble(this string str, out double returnValue)
        {
            return double.TryParse(str, out returnValue);
        }
      
        /// <summary>
        /// 字符串转换为double类型。如果转换失败，返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = 0)
        {
            return double.TryParse(str, out double returnValue) ? returnValue : defaultValue;
        }
        #endregion

        #region 字符串和Int16类型
        /// <summary>
        /// 判断字符串能否转换为Short类型(int16)。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsShort(this string str)
        {
            return short.TryParse(str, out short returnValue);
        }
      
        /// <summary>
        /// 判断字符串能否转换为Short类型(int16)。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsShort(this string str, out short returnValue)
        {
            return short.TryParse(str, out returnValue);
        }
      
        /// <summary>
        /// 字符串转换为Short类型(Int16类型)。如果转换失败，返回指定的默认值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short ToShort(this string str, short defaultValue = 0)
        {
            return short.TryParse(str, out short returnValue) ? returnValue : defaultValue;
        }
        #endregion

        #region 字符串和Int32类型
        /// <summary>
        /// 判断字符串能否转换为Int32类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(this string str)
        {
            return int.TryParse(str, out int returnValue);
        }
       
        /// <summary>
        /// 判断字符串能否成功转换为Int32类型。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsInt(this string str, out int returnValue)
        {
            return int.TryParse(str, out returnValue);
        }
       
        /// <summary>
        /// 字符串转换为int类型。如果转换失败，返回指定的默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue = 0)
        {
            return int.TryParse(str, out int returnValue) ? returnValue : defaultValue;
        }
       
        /// <summary>
        /// 字符串转换为int类型。如果转换失败，返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str)
        {
            if (int.TryParse(str, out int returnValue))
            {
                return returnValue;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 字符串和Int64类型
        /// <summary>
        /// 判断字符串能否转换为Int64类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLong(this string str)
        {
            return long.TryParse(str, out long returnValue);
        }
       
        /// <summary>
        /// 判断字符串能否成功转换为Int64类型。如果转换失败，返回指定的默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsLong(this string str, out long returnValue)
        {
            return long.TryParse(str, out returnValue);
        }
       
        /// <summary>
        /// 字符串转换为Long类型(int64)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str, long defaultValue = 0)
        {
            return long.TryParse(str, out long returnValue) ? returnValue : defaultValue;
        }
        #endregion

        #region 字符串和DateTime类型
        /// <summary>
        /// 字符串转换为DateTime类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string format = "yyyyMMddHHmmss")
        {
            return DateTime.ParseExact(str, format, CultureInfo.CurrentCulture);
        }
       
        /// <summary>
        /// 字符串转换为DateTime类型，如果转换失败，返回null 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str)
        {
            if (DateTime.TryParse(str, out DateTime returnValue))
            {
                return returnValue;
            }
            return null;
        }
        #endregion

        #region 字符串和Boolean类型
        /// <summary>
        /// 字符串转换为bool类型。如果转换失败，返回false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            return bool.TryParse(str, out bool returnValue) ? returnValue : default;
        }
        #endregion

        #region 字符串和Guid类型        
        /// <summary>
        /// 判断字符串能够转换为Guid类型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str)
        {
            return Guid.TryParse(str, out Guid returnValue);
        }
       
        /// <summary>
        ///判断字符串能够转为Guid类型。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str, out Guid returnValue)
        {
            return Guid.TryParse(str, out returnValue);
        }
        
        /// <summary>
        /// 字符串转换为Guid类型，如果转换失败，返回Guid.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            if (Guid.TryParse(str, out Guid test))
            {
                return test;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region  字符串和Enum类型
        /// <summary>
        /// 字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string @this)
        {
            return (T)Enum.Parse(typeof(T), @this);
        }
        #endregion

        #region 字符串截取
        /// <summary>
        /// 从左边开始截取长度的字符串
        /// length小于1时或字符串传null的时候，抛出异常。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (str == null || length < 1) { throw new Exception("字符串不能为空,或者length必须大于1"); }
            if (length < str.Length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
      
        /// <summary>
        /// 从右边开始截取长度的字符串
        /// length小于1时或字符串传null的时候，抛出异常。
        /// length大于被截取的字符串长度时，返回源字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string str, int length)
        {
            if (str == null || length < 1) { throw new Exception("字符串不能为空,或者length必须大于1"); }
            if (length < str.Length)
            {
                return str.Substring(str.Length - length);
            }
            else
            {
                return str;
            }
        }
        #endregion

        #region 其他扩展方法
        /// <summary>
        /// 移除字符串中的空格和换行(\r、\n)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpaceLine(this string str)
        {
            if (str.IsNullOrEmpty()) return "";
            return str.Replace(" ", "").Replace("\r", "").Replace("\n", "");
        }
        #endregion
    }
}
