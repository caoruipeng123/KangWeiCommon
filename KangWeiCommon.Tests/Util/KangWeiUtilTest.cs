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
            for (int i = 0; i < 10; i++)
            {
                string ip = $"{random.Next(1, 215)}.{random.Next(1, 215)}.{random.Next(1, 215)}.{random.Next(1, 215)}";
                uint ipNumber = KangWeiUtil.IpToUInt(ip);
                string ips = KangWeiUtil.UIntToIp(ipNumber);
                Assert.AreEqual(ip, ips);
            }
        }

        /// <summary>
        /// CSV文件导出
        /// </summary>
        [TestMethod]
        public void ExportCSVTest()
        {
            List<CSVDemo> list = new List<CSVDemo>();
            CSVDemo d1 = new CSVDemo() { Id = "1", Name = "name1", Age = 1 };
            list.Add(d1);
            CSVDemo d2 = new CSVDemo() { Id = "2", Name = "name2", Age = 2 };
            list.Add(d2);
            string fileName = $"testcsv.csv";
            KangWeiUtil.ExportCSV(list, fileName);

            List<CSVDemo> imports = KangWeiUtil.ImportCSV<CSVDemo>(fileName);
            Assert.IsNotNull(imports);
            Assert.AreEqual(imports[0].Id, "1");
            Assert.AreEqual(imports[0].Name, "name1");
            Assert.AreEqual(imports[0].Age, 1);

            Assert.AreEqual(imports[1].Id, "2");
            Assert.AreEqual(imports[1].Name, "name2");
            Assert.AreEqual(imports[1].Age, 2);
        }

        /// <summary>
        /// 配置文件导出
        /// </summary>
        [TestMethod]
        public void ReadConfigTest()
        {
            string fileName = "doc/defaults.properties";
            Dictionary<string, string> dic = KangWeiUtil.ReadConfig(fileName);
            Assert.IsNotNull(dic);
            Assert.IsTrue(dic.Count > 0);
        }

        [TestMethod]
        public void TimespanTest()
        {
            long timetamp = KangWeiUtil.GetTimestamp();
            Assert.IsTrue(timetamp > 0);
        }
        [TestMethod]
        public void TimespanTestTwo()
        {
            long timetamp = KangWeiUtil.GetTimestamp(new DateTime(2019, 01, 01, 1, 1, 1));
            Assert.IsTrue(timetamp > 0);
        }

        [TestMethod]
        public void GetDateTime()
        {
            long timetamp = KangWeiUtil.GetTimestamp(new DateTime(2019, 01, 01, 1, 1, 1));
            DateTime time = KangWeiUtil.GetDateTime(timetamp);
            Assert.IsTrue(time == new DateTime(2019, 01, 01, 1, 1, 1));
        }

        [TestMethod]
        public void GetWeekFirstTest()
        {
            DateTime firstWeek = KangWeiUtil.GetWeekFirst(new DateTime(2022, 09, 15, 12, 15, 12));
            Assert.IsTrue(firstWeek.Year == 2022);
            Assert.IsTrue(firstWeek.Month == 9);
            Assert.IsTrue(firstWeek.Day == 12);
        }

        [TestMethod]
        public void GetWeekLastTest()
        {
            DateTime firstWeek = KangWeiUtil.GetWeekLast(new DateTime(2022, 09, 15, 12, 15, 12));
            Assert.IsTrue(firstWeek.Year == 2022);
            Assert.IsTrue(firstWeek.Month == 9);
            Assert.IsTrue(firstWeek.Day == 18);
        }
    }
    public class CSVDemo
    {
        [CSVColumn("Id")]
        public string Id { get; set; }
        [CSVColumn("名称")]
        public string Name { get; set; }
        [CSVColumn("年龄")]
        public int Age { get; set; }
    }
}
