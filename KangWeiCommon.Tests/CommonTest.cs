using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace KangWeiCommon.Tests
{
    [TestClass]
    public class CommonTest
    {
        /// <summary>
        /// 所有的方法命名是否否和规范
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            Assembly assembly = Assembly.Load("KangWeiCommon");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods();
                if (methods != null)
                {
                    foreach(MethodInfo method in methods)
                    {
                        string methodName = method.Name;
                    }
                }
            }
        }
        [TestMethod]
        public void Check()
        {
            string pattern = "^[A-Z][a-zA-Z0-9]*$";
            var match = Regex.IsMatch("BoyFriend1", pattern);
        }
    }
}
