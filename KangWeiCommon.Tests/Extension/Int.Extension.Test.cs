using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class IntExtensionTest
    {
        [TestMethod]
        public void ToIntTest()
        {
            int? a = null;
            Assert.AreEqual(a.ToInt(), 0);
            a = 5;
            Assert.AreEqual(a.ToInt(), 5);
        }
        [TestMethod]
        public void IsLeapYearTest()
        {
            Assert.AreEqual(2004.IsLeapYear(), true);
            Assert.AreEqual(1901.IsLeapYear(), false);
        }
        [TestMethod]
        public void DaysInMonthTest()
        {
            Assert.AreEqual(2021.DaysInMonth(2), 28);
            Assert.AreEqual(2021.DaysInMonth(5), 31);
        }
    }
}
