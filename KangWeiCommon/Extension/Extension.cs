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
        /// 判断一个值是否在一个范围之内.如果minValue&lt; 输入值 &gt;maxValue,返回true
        /// </summary>
        /// <param name="this">被比较的值</param>
        /// <param name="minValue">比较范围最大值</param>
        /// <param name="maxValue">比较范围最小值</param>
        /// <returns>如果minValue&lt;输入值&gt;maxValue,返回true</returns>
        public static bool IsBetween<T>(this T @this, T minValue, T maxValue) where T : IComparable
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }
        /// <summary>
        /// 判断一个值是否在一个范围之内.如果minValue&lt;= 输入值 &gt;=maxValue,返回true
        /// </summary>
        /// <param name="this">被比较的值</param>
        /// <param name="minValue">比较范围最大值</param>
        /// <param name="maxValue">比较范围最小值</param>
        /// <returns>如果minValue&lt;= 输入值 &gt;=maxValue,返回true</returns>
        public static bool IsInRange<T>(this T @this, T minValue, T maxValue) where T : IComparable
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
    }
}
