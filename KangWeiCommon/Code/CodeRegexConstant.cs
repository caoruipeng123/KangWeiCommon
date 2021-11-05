using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon.Code
{
    /// <summary>
    /// 常用编码规范、命名方式正则表达式
    /// </summary>
    public class CodeRegexConstant
    {
        /// <summary>
        /// <para>方法正则表达式</para>
        /// <para>规范:大驼峰命名法</para>
        /// <para>字符:大写字符、小写字符、数字</para>
        /// </summary>
        public static readonly string Method = "^[A-Z][a-zA-Z0-9]*$";

        /// <summary>
        /// <para>属性正则表达式</para>
        /// <para>规范:大驼峰命名法</para>
        /// <para>字符:大写字符、小写字符</para>
        /// </summary>
        public static readonly string Property = "^[A-Z][a-zA-Z]*$";

        /// <summary>
        /// <para>命名空间正则表达式</para>
        /// <para>规范:大驼峰命名法</para>
        /// <para>字符:大写字符、小写字符</para>
        /// </summary>
        public static readonly string NameSpace = $"^[A-Z][a-zA-Z]*$";
    }
}
