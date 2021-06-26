
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class GuidExtensionTest
    {
        [TestMethod]
        public void IsEmptyTest()
        {
            Assert.AreEqual(Guid.Empty.IsEmpty(), true);
            Assert.AreEqual(Guid.NewGuid().IsEmpty(), false);
        }
    }
}
