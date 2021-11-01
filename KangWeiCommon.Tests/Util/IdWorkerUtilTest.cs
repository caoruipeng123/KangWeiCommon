using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Util
{
    [TestClass]
    public class IdWorkerUtilTest
    {
        [TestMethod]
        public void NextIdTest()
        {
            int length = 10000;
            List<long> list = new List<long>(length);
            IdWorkerUtil.SetConfig(1, 1);
            while (list.Count <= length)
            {
                long a = IdWorkerUtil.Worker.NextId();
                //long a = IdWorkerUtil.GetNextId();
                list.Add(a);
            }
            list.Sort();
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(list[i]);
                Assert.IsTrue(list[i] < list[i + 1]);
            }
            bool flag = false;
            try
            {
                IdWorkerUtil.SetConfig(1, 1);
            }
            catch
            {
                flag = true;
            }
            Assert.IsTrue(flag);
        }
        [TestMethod]
        public void IdWorkerTest()
        {
            long id = IdWorkerUtil.GetNextId();
            long a = id;
            Assert.IsTrue(id > 0);
            id = IdWorkerUtil.GetNextId();
            long b = id;
            Assert.IsTrue(id > 0);
            Assert.AreNotEqual(a, b);
        }
    }
}
