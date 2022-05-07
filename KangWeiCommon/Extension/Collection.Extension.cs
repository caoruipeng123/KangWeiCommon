using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// 将集合中的字符串用指定字符串拼接起来
        /// </summary>
        /// <param name="strList"></param>
        /// <param name="split">字符串拼接符</param>
        /// <param name="appendNullStr">是否拼接空字符串，默认拼接</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> strList, string split, bool appendNullStr = true)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerator<string> enumerator = strList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                sb.Append(split+ enumerator.Current);
            }
            return sb.ToString().Substring(1);
        }
    }
}
