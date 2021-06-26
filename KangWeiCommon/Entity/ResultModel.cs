using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon.Entity
{
    /// <summary>
    /// 返回结果实体类（泛型）
    /// </summary>
    public class ResultModel<T>
    {
        /// <summary>
        /// 午餐构造函数
        /// </summary>
        public ResultModel()
        {
        }
        /// <summary>
        /// 有残构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        public ResultModel(int code = 0, T data = default, string msg = "")
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回详细信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }
}
