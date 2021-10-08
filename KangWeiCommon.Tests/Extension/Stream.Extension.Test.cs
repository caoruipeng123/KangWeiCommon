using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class StreamExtensionTest
    {
        [TestMethod]
        public void ReadToEndTest()
        {
            FileStream stream = new FileStream("test.txt",FileMode.Open);
            string text = stream.ReadToEnd();
            Assert.AreEqual(text, "text");

            stream = new FileStream("test.txt", FileMode.Open);
            text = stream.ReadToEnd(position: 0,encoding:Encoding.UTF8);
            //todo 后续继续测试
            //Assert.AreEqual(text,"ext");
        }
    }
}
