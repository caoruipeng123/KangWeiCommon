using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon
{
    /// <summary>
    /// 生成IdWorker辅助类
    /// </summary>
    public class IdWorkerUtil
    {
        //机器Id
        static long _workerId = 1;
        //数据中心Id
        static long _datacenterId = 1;
        //用于生成全局Id的唯一实例
        static IdWorker worker = null;

        /// <summary>
        /// 设置节点Id和数据中心Id，该方法全局调用一次即可。如果不掉用过，默认workerId=1，datacenterId=1
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="datacenterId"></param>
        public static void SetConfig(long workerId, long datacenterId)
        {
            if (worker != null)
            {
                throw new Exception("机器Id和数据中心全局调用一次即可！");
            }
            _workerId = workerId;
            _datacenterId = datacenterId;
            worker = new IdWorker(_workerId, _datacenterId);
        }

        /// <summary>
        /// 生成全局唯一Id[曹瑞鹏]
        /// </summary>
        /// <returns></returns>
        public static long GetNextId()
        {
            if (worker == null)
            {
                worker = new IdWorker(_workerId, _datacenterId);
            }
            return worker.NextId();
        }

        /// <summary>
        /// 返回IdWorker的唯一实例
        /// </summary>
        public static IdWorker Worker
        {
            get
            {
                return worker;
            }
        }
    }
}
