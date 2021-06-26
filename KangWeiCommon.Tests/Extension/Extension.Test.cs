using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void BetwweenTest()
        {
            Assert.AreEqual(5.IsBetween(1, 7), true);
            Assert.AreEqual(5.IsBetween(5, 7), false);
            Assert.AreEqual(5.IsBetween(4, 5), false);
            Assert.AreEqual(5.IsBetween(6, 7), false);
        }
        [TestMethod]
        public void InRangeTest()
        {
            Assert.AreEqual(5.IsInRange(1, 7), true);
            Assert.AreEqual(5.IsInRange(5, 7), true);
            Assert.AreEqual(5.IsInRange(4, 5), true);
            Assert.AreEqual(5.IsInRange(6, 7), false);
        }
    }
}
