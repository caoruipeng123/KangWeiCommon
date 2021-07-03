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
        /// 判断一个集合中是否有元素。有返回true
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool HasLength(this System.Collections.IEnumerable collection)
        {
            if (collection == null)
            {
                return false;
            }
            var a = collection.GetEnumerator();
            return a.MoveNext();
        }
        /// <summary>
        /// 判断一个集合中是否 没有元素。没有返回true
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool HasNotLength<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return true;
            }
            var a = collection.GetEnumerator();
            return !a.MoveNext();
        }
    }
}
