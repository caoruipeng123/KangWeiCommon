using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Util
{
    /// <summary>
    /// 雪花算法自增生成器测试
    /// </summary>
    [TestClass]
    public class IdWorkerTest
    {
        /// <summary>
        /// 是否有长度
        /// </summary>
        [TestMethod]
        public void IdWorker()
        {
            IdWorker worker = new IdWorker(11, 11);
            int length = 10000;
            List<long> list = new List<long>(length);
            while (list.Count <= length)
            {
                long a = worker.NextId();
                list.Add(a);
            }
            list.Sort();
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(list[i]);
                Assert.IsTrue(list[i] < list[i + 1]);
            }
        }
    }
}
