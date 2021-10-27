using System;

namespace KangWeiCommon
{
    /// <summary>
    /// 设置导出CSV的列名
    /// </summary>
    public class CSVColumnAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public CSVColumnAttribute(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }
    }
}
