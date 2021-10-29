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
            long id = IdWorkerUtil.GetNextId();
            Assert.IsTrue(id > 0);
        }
    }
}
