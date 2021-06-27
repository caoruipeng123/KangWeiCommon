using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class StringExtensionTest
    {
        [TestMethod]
        public void IsNullOrEmptyTest()
        {
            string str = null;
            Ensure.Eq("", str.IsNullOrEmpty(), true);
            Ensure.Eq("", (str = "").IsNullOrEmpty(), true);
            Ensure.Eq("", (str = " ").IsNullOrEmpty(), true);
            Ensure.Eq("", (str = string.Empty).IsNullOrEmpty(), true);
            Assert.AreEqual((str = "test").IsNullOrEmpty(), false);
            Ensure.Eq("test", (str = "test").IsNullOrEmpty(), false);
        }
        [TestMethod]
        public void IsNullOrEmptyActionTest()
        {
            string str = null;
            int flag = 0;
            Ensure.Eq("", str.IsNullOrEmpty(() => flag = 3), true);
            Assert.AreEqual(flag, 3);
            Ensure.Eq("", (str = "").IsNullOrEmpty(() => flag = 4), true);
            Assert.AreEqual(flag, 4);
            Ensure.Eq("", (str = " ").IsNullOrEmpty(() => flag = 5), true);
            Assert.AreEqual(flag, 5);
            Ensure.Eq("", (str = string.Empty).IsNullOrEmpty(() => flag = 6), true);
            Assert.AreEqual(flag, 6);
            Ensure.Eq("test", (str = "test").IsNullOrEmpty(() => flag = 7), false);
            Assert.AreEqual(flag, 6);
        }
        [TestMethod]
        public void IsNotNullOrEmptyTest()
        {
            string str = null;
            Ensure.Eq("", str.IsNotNullOrEmpty(), false);
            Ensure.Eq("", (str = "").IsNotNullOrEmpty(), false);
            Ensure.Eq("", (str = " ").IsNotNullOrEmpty(), false);
            Ensure.Eq("", (str = string.Empty).IsNotNullOrEmpty(), false);
            Assert.AreEqual((str = "test").IsNotNullOrEmpty(), true);
            Ensure.Eq("test", (str = "test").IsNotNullOrEmpty(), true);
        }
        [TestMethod]
        public void IsNotNullOrEmptyActionTest()
        {
            string str = null;
            int flag = 0;
            Ensure.Eq("", str.IsNotNullOrEmpty(() => flag = 3), false);
            Assert.AreEqual(flag, 0);
            Ensure.Eq("", (str = "").IsNotNullOrEmpty(() => flag = 4), false);
            Assert.AreEqual(flag, 0);
            Ensure.Eq("", (str = " ").IsNotNullOrEmpty(() => flag = 5), false);
            Assert.AreEqual(flag, 0);
            Ensure.Eq("", (str = string.Empty).IsNotNullOrEmpty(() => flag = 6), false);
            Assert.AreEqual(flag, 0);
            Ensure.Eq("test", (str = "test").IsNotNullOrEmpty(() => flag = 7), true);
            Assert.AreEqual(flag, 7);
        }
        [TestMethod]
        public void IsDecimalTest()
        {
            Assert.AreEqual("2.36".IsDecimal(), true);
            Assert.AreEqual("2.36r".IsDecimal(), false);

            Assert.AreEqual("2.36".IsDecimal(out decimal a), true);
            Assert.AreEqual(a, 2.36m);

            Assert.AreEqual("2.36r".IsDecimal(out decimal b), false);
            Assert.AreEqual(b, 0);
        }
        [TestMethod]
        public void ToDecimalTest()
        {
            Assert.AreEqual("2.36".ToDecimal(), 2.36m);
            Assert.AreEqual("2.36rr".ToDecimal(), 0);

            Assert.AreEqual("2.36".ToDecimal(1), 2.4m);
            Assert.AreEqual("2.35".ToDecimal(1), 2.4m);
            Assert.AreEqual("2.33".ToDecimal(1), 2.3m);

            Assert.AreEqual("2.354".ToDecimal(2), 2.35m);
            Assert.AreEqual("2.355".ToDecimal(2), 2.36m);
            Assert.AreEqual("2.356".ToDecimal(2), 2.36m);
        }
        [TestMethod]
        public void IsDoubleTest()
        {
            Assert.AreEqual("2.36".IsDouble(), true);
            Assert.AreEqual("2.36r".IsDouble(), false);

            Assert.AreEqual("2.36".IsDouble(out double a), true);
            Assert.AreEqual(a, 2.36);

            Assert.AreEqual("2.36r".IsDouble(out double b), false);
            Assert.AreEqual(b, 0);
        }
        [TestMethod]
        public void ToDoubleTest()
        {
            Assert.AreEqual("2.36".ToDouble(), 2.36);
            Assert.AreEqual("2.36rr".ToDouble(), 0);
            //todo ToDouble转换保留小数位的操作后续完善
            //Assert.AreEqual("2.36".ToDouble(1), 2.4m);
            //Assert.AreEqual("2.35".ToDouble(1), 2.4m);
            //Assert.AreEqual("2.33".ToDouble(1), 2.3m);

            //Assert.AreEqual("2.354".ToDouble(2), 2.35m);
            //Assert.AreEqual("2.355".ToDouble(2), 2.36m);
            //Assert.AreEqual("2.356".ToDouble(2), 2.36m);
        }
        [TestMethod]
        public void IsInt32Test()
        {
            Assert.AreEqual("2".IsInt(), true);
            Assert.AreEqual("2.36r".IsInt(), false);

            Assert.AreEqual("2.36".IsInt(out int a), false);
            Assert.AreEqual(a, 0);

            Assert.AreEqual("55".IsInt(out int b), true);
            Assert.AreEqual(b, 55);
        }
        [TestMethod]
        public void ToIntTest()
        {
            Assert.AreEqual("66".ToInt(), 66);
            Assert.AreEqual("2.36rr".ToInt(), 0);
            Assert.AreEqual("2.36rr".ToInt(55), 55);
        }
        [TestMethod]
        public void ToIntOrNullTest()
        {
            Assert.AreEqual("66".ToIntOrNull(), 66);
            Assert.AreEqual("2.36rr".ToIntOrNull(), null);
            Assert.AreEqual("2.36rr".ToIntOrNull(), null);
        }
        [TestMethod]
        public void IsShortTest()
        {
            Assert.AreEqual("2".IsShort(), true);
            Assert.AreEqual("2.36r".IsShort(), false);

            Assert.AreEqual("2.36".IsShort(out short a), false);
            Assert.AreEqual(a, 0);

            Assert.AreEqual("55".IsShort(out short b), true);
            Assert.AreEqual(b, 55);
        }
        [TestMethod]
        public void ToShortTest()
        {
            Assert.AreEqual("66".ToShort(), 66);
            Assert.AreEqual("2.36rr".ToShort(), 0);
            Assert.AreEqual("2.36rr".ToShort(55), 55);
        }
        [TestMethod]
        public void IsLongTest()
        {
            Assert.AreEqual("2".IsLong(), true);
            Assert.AreEqual("2.36r".IsLong(), false);

            Assert.AreEqual("222".IsLong(out long a), true);
            Assert.AreEqual(a, 222);

            Assert.AreEqual("55".IsLong(out long b), true);
            Assert.AreEqual(b, 55);

            Assert.AreEqual("55t".IsLong(out long c), false);
            Assert.AreEqual(c, 0);
        }
        [TestMethod]
        public void ToLongTest()
        {
            Assert.AreEqual("66".ToLong(), 66);
            Assert.AreEqual("2.36rr".ToLong(), 0);
            Assert.AreEqual("2.36rr".ToLong(55), 55);
        }
        [TestMethod]
        public void ToDateTimeOrNullTest()
        {
            string time = "20210621";
            DateTime? t = time.ToDateTimeOrNull();
            Assert.AreEqual(t, null);
        }
        [TestMethod]
        public void ToDateTimeTest()
        {
            string time = "20210621";
            DateTime? t = time.ToDateTime("yyyyMMdd");
            Assert.AreEqual(t, new DateTime(2021, 6, 21));

            time = "2021062112";
            t = time.ToDateTime("yyyyMMddHH");
            Assert.AreEqual(t, new DateTime(2021, 6, 21, 12, 0, 0));
        }
        [TestMethod]
        public void ToBooleanTest()
        {
            Assert.AreEqual("true".ToBoolean(), true);
            Assert.AreEqual("false".ToBoolean(), false);
            Assert.AreEqual("TRUe".ToBoolean(), true);
            Assert.AreEqual("fALSE".ToBoolean(), false);
            Assert.AreEqual("555".ToBoolean(), false);
        }
        [TestMethod]
        public void LeftTest()
        {
            Assert.AreEqual("true".Left(1), "t");
            Assert.AreEqual("true".Left(2), "tr");
            Assert.AreEqual("true".Left(66), "true");
            int flag = 0;
            try
            {
                string str = null;
                str.Left(5);
            }
            catch
            {
                flag = 1;
            }
            Assert.AreEqual(flag, 1);

            try
            {
                flag = 0;
                string str = "aaaa";
                str.Left(0);
            }
            catch
            {
                flag = 1;
            }
            Assert.AreEqual(flag, 1);
        }
        [TestMethod]
        public void RightTest()
        {
            Assert.AreEqual("true".Right(1), "e");
            Assert.AreEqual("true".Right(2), "ue");
            Assert.AreEqual("true".Right(66), "true");
            int flag = 0;
            try
            {
                string str = null;
                str.Right(5);
            }
            catch
            {
                flag = 1;
            }
            Assert.AreEqual(flag, 1);

            try
            {
                flag = 0;
                string str = "aaaa";
                str.Right(0);
            }
            catch
            {
                flag = 1;
            }
            Assert.AreEqual(flag, 1);
        }
        [TestMethod]
        public void IsGuidTest()
        {
            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642e".IsGuid(), true);
            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642".IsGuid(), false);

            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642e".IsGuid(out Guid a), true);
            Assert.AreEqual(a.ToString(), "3ba44621-4bca-464f-860c-2fb1069a642e");


            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642".IsGuid(out Guid c), false);
            Assert.AreEqual(c, Guid.Empty);
        }
        [TestMethod]
        public void ToGuidTest()
        {
            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642e".ToGuid(), new Guid("3ba44621-4bca-464f-860c-2fb1069a642e"));
            Assert.AreEqual("3ba44621-4bca-464f-860c-2fb1069a642".ToGuid(), Guid.Empty);
        }
        [TestMethod]
        public void RemoveSpaceLineTest()
        {
            Assert.AreEqual("Remove   SpaceLine\r\n".RemoveSpaceLine(), "RemoveSpaceLine");
        }

        [TestMethod]
        public void IsFloatTest()
        {
            Assert.AreEqual("2.33".IsFloat(), true);
            Assert.AreEqual("2.36r".IsFloat(), false);

            Assert.AreEqual("2.36".IsFloat(out float a), true);
            Assert.AreEqual(a, 2.36f);

            Assert.AreEqual("55".IsFloat(out float b), true);
            Assert.AreEqual(b, 55);
        }
        [TestMethod]
        public void ToFloatTest()
        {
            Assert.AreEqual("66.77".ToFloat(), 66.77f);
            Assert.AreEqual("2.36rr".ToFloat(), 0);
            Assert.AreEqual("2.36rr".ToFloat(55), 55);
        }
    }
}
