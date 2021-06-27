using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class BoolExtensionTest
    {
        [TestMethod]
        public void IfTrueTest()
        {
            bool a = true;
            int flag = 0;
            a.IfTrue(() => flag = 1);
            Assert.AreEqual(flag, 1);
        }
        [TestMethod]
        public void IfFalseTest()
        {
            bool a = true;
            int flag = 0;
            a.IfFalse(() => flag = 1);
            Assert.AreEqual(flag, 0);

            a = false;
            a.IfFalse(() => flag = 1);
            Assert.AreEqual(flag, 1);
        }
    }
}
