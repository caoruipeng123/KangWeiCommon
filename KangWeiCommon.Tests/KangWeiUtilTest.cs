using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests
{
    [TestClass]
    public class KangWeiUtilTest
    {
        /// <summary>
        /// IP地址和Uint类型相互转换
        /// </summary>
        [TestMethod]
        public void IpUIntTest()
        {
            Random random = new Random();
            for(int i = 0; i < 1000000; i++)
            {
                string ip = $"{random.Next(1,215)}.{random.Next(1, 215)}.{random.Next(1, 215)}.{random.Next(1, 215)}";
                uint ipNumber = KangWeiUtil.IpToUInt(ip);
                string ips = KangWeiUtil.UIntToIp(ipNumber);
                Assert.AreEqual(ip, ips);
            }

        }
    }
}
