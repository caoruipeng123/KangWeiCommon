using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KangWeiCommon.Tests
{
    [TestClass]
    public class EnsureTest
    {
        [TestMethod]
        public void NotNullOrEmptyTest()
        {
            int flag = 0;
            try
            {
                Ensure.NotNullOrEmpty("���Բ���", null);
            }
            catch (Exception ex)
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.NotNullOrEmpty("���Բ���", "");
            }
            catch (Exception ex)
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.NotNullOrEmpty("���Բ���", "   ");
            }
            catch (Exception ex)
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.NotNullOrEmpty("���Բ���", string.Empty);
            }
            catch (Exception ex)
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void NotNullTest()
        {
            int flag = 0;
            try
            {
                Ensure.NotNull("", null);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void NullTest()
        {
            int flag = 0;
            try
            {
                Ensure.Null("", " ");
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void HasLengthTest()
        {
            int flag = 0;

            //����Ϊnull���쳣
            List<int> list = null;
            try
            {
                Ensure.HasLength("", list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            //���ϲ�Ϊnull��û�г���Ҳ���쳣
            list = new List<int>();
            try
            {
                Ensure.HasLength("", list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void GtTest()
        {
            int flag = 0;
            try
            {
                Ensure.Gt("", 1, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.Gt("", -3, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.Gt("", 3, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);
        }
        [TestMethod]
        public void GtOrEqTest()
        {
            int flag = 0;
            try
            {
                Ensure.GtOrEq("", 1, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            flag = 0;
            try
            {
                Ensure.GtOrEq("", -3, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            flag = 0;
            try
            {
                Ensure.GtOrEq("", 3, 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);
        }
        [TestMethod]
        public void LtTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.Lt("", 1, 1);//���ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            try
            {
                flag = 0;
                Ensure.Lt("", -3, 1);//С�ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);


            try
            {
                flag = 0;
                Ensure.Lt("", 3, 1);//���ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void LtOrEqTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.LtOrEq("", 1, 1);//���ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            try
            {
                flag = 0;
                Ensure.LtOrEq("", -3, 1);//С�ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);


            try
            {
                flag = 0;
                Ensure.LtOrEq("", 3, 1);//���ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void EqTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.Eq("", 1, 1);//���ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            try
            {
                flag = 0;
                Ensure.Eq("", -3, 1);//С�ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);


            try
            {
                flag = 0;
                Ensure.Eq("", 3, 1);//���ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void NotEqTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.NotEq("", 1, 1);//���ڱ���
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            try
            {
                flag = 0;
                Ensure.NotEq("", -3, 1);//С�ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);


            try
            {
                flag = 0;
                Ensure.NotEq("", 3, 1);//���ڲ�����
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);
        }
        [TestMethod]
        public void InTest()
        {
            List<int> list = new List<int>() { 1, 2 };
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.In("", 1, list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            try
            {
                flag = 0;
                Ensure.In("", 4, list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void NotInTest()
        {
            List<int> list = new List<int>() { 1, 2 };
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.NotIn("", 1, list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            try
            {
                flag = 0;
                Ensure.NotIn("", 4, list);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);
        }
        [TestMethod]
        public void ParamsInTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.In("", 1, 1, 4);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            try
            {
                flag = 0;
                Ensure.In("", 4, 3, 6);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        [TestMethod]
        public void ParamsNotInTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.NotIn("", 1, 1, 4);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);

            try
            {
                flag = 0;
                Ensure.NotIn("", 4, 3, 6);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);
        }
        [TestMethod]
        public void EnumInTest()
        {
            int flag = 0;
            try
            {
                flag = 0;
                Ensure.EnumIn<EnmuType>("", 1);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 0);

            try
            {
                flag = 0;
                Ensure.EnumIn<EnmuType>("", 5);
            }
            catch
            {
                flag = 1;
            }
            Ensure.Eq("", flag, 1);
        }
        public enum EnmuType
        {
            One,
            Two
        }
    }
}
