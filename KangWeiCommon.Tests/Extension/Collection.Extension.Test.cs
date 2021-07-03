using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    /// <summary>
    /// 集合相关扩展方法
    /// </summary>
    [TestClass]
    public class CollectionExtensionTest
    {
        /// <summary>
        /// 是否有长度
        /// </summary>
        [TestMethod]
        public void HasLength()
        {
            string[] array = null;
            Assert.AreEqual(array.HasLength(), false);

            array = new string[0];
            Assert.AreEqual(array.HasLength(), false);

            array = new string[1] { "111" };
            Assert.AreEqual(array.HasLength(), true);
        }
        /// <summary>
        /// 是否没有长度
        /// </summary>
        [TestMethod]
        public void HasNotLength()
        {
            string[] array = null;
            Assert.AreEqual(array.HasNotLength(), true);

            array = new string[0];
            Assert.AreEqual(array.HasNotLength(), true);

            array = new string[1] { "111" };
            Assert.AreEqual(array.HasNotLength(), false);
        }
    }
}
