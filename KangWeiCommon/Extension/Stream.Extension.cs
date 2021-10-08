using System;
using System.Collections.Generic;
using System.IO;
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
        ///  读取Stream流返回字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <param name="position">流的起始位置</param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream @this, Encoding encoding = null, long position = 0)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            @this.Position = position;
            using (var sr = new StreamReader(@this, encoding))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
