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
        static long _workerId = 1;
        static long _datacenterId = 1;
        private static IdWorker worker = null;
        static  bool flag = false;
        /// <summary>
        /// 设置节点Id和数据中心Id，该方法全局调用一次即可。如果不掉用过，默认workerId=1，datacenterId=1
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="datacenterId"></param>
        public static void SetConfig(long workerId, long datacenterId)
        {
            if (flag)
            {
                throw new Exception("机器Id和数据中心全局调用一次即可！");
            }
            _workerId = workerId;
            _datacenterId = datacenterId;
            flag = true;
        }
        /// <summary>
        /// 生成全局唯一Id[曹瑞鹏]
        /// </summary>
        /// <returns></returns>
        public static long GetNextId()
        {
            InitWorker();
            return worker.NextId();
        }
        /// <summary>
        /// 初始化Worker
        /// </summary>
        private static void InitWorker()
        {
            if (worker == null)
            {
                flag = true;
                worker = new IdWorker(_workerId, _datacenterId);
            }
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
