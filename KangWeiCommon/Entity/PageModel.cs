using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon.Entity
{
    /// <summary>
    /// 通用分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageModel<T>
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public List<T> PageData { get; set; }
        /// <summary>
        /// 页码（统一从1开始）
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页容量（每页多少条数据）
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount { get; set; }
    }
}
