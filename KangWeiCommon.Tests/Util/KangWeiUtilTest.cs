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
            for(int i = 0; i < 10; i++)
            {
                string ip = $"{random.Next(1,215)}.{random.Next(1, 215)}.{random.Next(1, 215)}.{random.Next(1, 215)}";
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
            Assert.AreEqual(imports[0].Id , "1");
            Assert.AreEqual(imports[0].Name, "name1");
            Assert.AreEqual(imports[0].Age, 1);

            Assert.AreEqual(imports[1].Id, "2");
            Assert.AreEqual(imports[1].Name, "name2");
            Assert.AreEqual(imports[1].Age, 2);
        }
        //[TestMethod]
        //public void ConvertTypeTest()
        //{
        //    var types = TypeCode.Char;
        //    string val = "true";

        //    object obj = KangWeiUtil.ConvertType<bool>(val);
        //    Assert.AreEqual(obj, true);

        //    val = "false";
        //    obj = KangWeiUtil.ConvertType<bool>(val);
        //    Assert.AreEqual(obj, false);

        //    val = "f";
        //    obj = KangWeiUtil.ConvertType<char>(val);
        //    Assert.AreEqual(obj, 'f');

        //    val = "3";
        //    obj = KangWeiUtil.ConvertType<sbyte>(val);
        //    Assert.AreEqual(obj, (sbyte)3);

        //    val = "4";
        //    obj = KangWeiUtil.ConvertType<byte>(val);
        //    Assert.AreEqual(obj, (byte)4);

        //    val = "5";
        //    obj = KangWeiUtil.ConvertType<Int16>(val);
        //    Assert.AreEqual(obj, (Int16)5);

        //    val = "6";
        //    obj = KangWeiUtil.ConvertType<UInt16>(val);
        //    Assert.AreEqual(obj, (UInt16)6);
        //}
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
