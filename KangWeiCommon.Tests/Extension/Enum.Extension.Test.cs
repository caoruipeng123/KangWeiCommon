using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KangWeiCommon.Tests.Extension
{
    [TestClass]
    public class EnumExtensionTest
    {
        [TestMethod]
        public void Description()
        {
            Assert.AreEqual(OrderStatu.NotPay.GetDescription(), "未支付");
            Assert.AreEqual(OrderStatu.Success.GetDescription(), "Success");
            Assert.AreEqual(OrderStatu.Success.GetDescription(false), "");
        }
        
    }
    public enum OrderStatu
    {
        /// <summary>
        /// 未支付
        /// </summary>
        [System.ComponentModel.Description("未支付")]
        NotPay = 0,
        /// <summary>
        /// 支付成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 转入退款
        /// </summary>
        [System.ComponentModel.Description("转入退款")]
        Refund = 2,
        /// <summary>
        /// 已关闭
        /// </summary>
        [System.ComponentModel.Description("已关闭")]
        Closed = 3,
        /// <summary>
        /// 用户支付中
        /// </summary>
        [System.ComponentModel.Description("用户支付中")]
        UserPaying = 4,
        /// <summary>
        /// 支付失败
        /// </summary>
        [System.ComponentModel.Description("支付失败")]
        PayError = 5,
        /// <summary>
        /// 已撤销
        /// </summary>
        [System.ComponentModel.Description("已撤销")]
        Revoked = 6
    }
}
