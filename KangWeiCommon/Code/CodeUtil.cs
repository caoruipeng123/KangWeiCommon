using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KangWeiCommon.Code
{
    /// <summary>
    /// 代码辅助类
    /// </summary>
    public class CodeUtil
    {

        /// <summary>
        /// 校验 名称时是否匹配对应的正则表达式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <see cref="CodeRegexConstant"/>
        /// <returns></returns>
        public static bool Check(string str, string pattern)
        {
            var match = Regex.IsMatch(str, pattern);
            return match;
        }
        /// <summary>
        /// 校验方法名是否满足命名规则
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="pattern"></param>
        /// 如果pattern为空,默认使用 <see cref="CodeRegexConstant.Method"/>
        /// <returns></returns>
        public static bool CheckMethod(string methodName, string pattern = null)
        {
            return Regex.IsMatch(methodName, pattern.IsNullOrEmpty() ? CodeRegexConstant.Method : pattern);
        }
        /// <summary>
        /// 校验属性是否满足命名规则
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="pattern"></param>
        /// 如果pattern为空,默认使用 <see cref="CodeRegexConstant.Property"/>
        /// <returns></returns>
        public static bool CheckProperty(string propertyName, string pattern = null)
        {
            return Regex.IsMatch(propertyName, pattern.IsNullOrEmpty() ? CodeRegexConstant.Property : pattern);
        }
        /// <summary>
        /// 校验命名空间名称是否满足命名规则
        /// </summary>
        /// <param name="nameSpaceName"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool CheckNameSpace(string nameSpaceName, string pattern = null)
        {
            return Regex.IsMatch(nameSpaceName, pattern.IsNullOrEmpty() ? CodeRegexConstant.NameSpace : pattern);
        }
    }
}
