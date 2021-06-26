using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon
{
    /// <summary>
    /// 系统类的扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 将Int?转化为Int。如果int?为null，返回0；
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ToInt(this int? data)
        {
            if (data == null)
                return default;
            return Convert.ToInt32(data);
        }
        /// <summary>
        /// 判断一个年份是否为闰年
        /// </summary>
        /// <param name="year">4位数年份</param>
        /// <returns></returns>
        public static bool IsLeapYear(this int year)
        {
            return DateTime.IsLeapYear(year);
        }
        /// <summary>
        /// 返回指定年和月中的天数。
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份（1-12）</param>
        /// <returns></returns>
        public static int DaysInMonth(this int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }
    }
}
