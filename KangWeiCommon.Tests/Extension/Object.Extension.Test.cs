using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class ObjectExtensionTest
    {
        [TestMethod]
        public void IsNullTest()
        {
            object a = null;
            Assert.AreEqual(a.IsNull(), true);
            a = new object();
            Assert.AreEqual(a.IsNull(), false);

            a = null;
            int flag = 0;
            Assert.AreEqual(a.IsNull(() => flag = 1), true);
            Assert.AreEqual(flag, 1);

            a = new object();
            flag = 0;
            Assert.AreEqual(a.IsNull(() => flag = 1), false);
            Assert.AreEqual(flag, 0);
        }
        [TestMethod]
        public void IsNotNullTest()
        {
            object a = null;
            Assert.AreEqual(a.IsNotNull(), false);
            a = new object();
            Assert.AreEqual(a.IsNotNull(), true);

            a = null;
            int flag = 0;
            Assert.AreEqual(a.IsNotNull(() => flag = 1), false);
            Assert.AreEqual(flag, 0);

            a = new object();
            flag = 0;
            Assert.AreEqual(a.IsNotNull(() => flag = 1), true);
            Assert.AreEqual(flag, 1);
        }
        [TestMethod]
        public void ToIntTest()
        {
            object a = "2";
            Assert.AreEqual(a.ToInt(), 2);
        }
        [TestMethod]
        public void ToDecimalTest()
        {
            object a = "2.66";
            Assert.AreEqual(a.ToDecimal(), 2.66m);
        }
        [TestMethod]
        public void ToDoubleTest()
        {
            object a = "2.66";
            Assert.AreEqual(a.ToDouble(), 2.66d);
        }
        [TestMethod]
        public void ToFloatTest()
        {
            object a = "2.66";
            Assert.AreEqual(a.ToFloat(), 2.66f);
        }
        /// <summary>
        /// TODO 后续处理
        /// </summary>
        [TestMethod]
        public void SerializeToXmlTest()
        {
            Person Person = new Person { Name = "testName", Age = 29 };
            string xmlStr = Person.SerializeToXml();
            Console.WriteLine(xmlStr);
        }
        /// <summary>
        /// TODO 后续在完善
        /// </summary>
        [TestMethod]
        public void ToTest()
        {
            //Person person = new Person();
            //Student student = person.To<Student>();
        }
        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Height { get; set; }
        }
        public class Student:Person
        {
            public string SchoolName { get; set; }
        }
    }
}
