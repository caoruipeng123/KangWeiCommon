using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon.DataAnnotations
{
    ///// <summary>
    ///// 校验最大值
    ///// </summary>
    //public class MaxValueAttribute : ValidationAttribute
    //{
    //    private IComparable _max;
    //    private bool _isContain;
    //    public MaxValueAttribute(IComparable comparable, bool isContain = true)
    //    {
    //        _max = comparable;
    //        _isContain = isContain;
    //    }
    //    public override bool IsValid(object? value)
    //    {
    //        if (_max == null)
    //        {
    //            return false;
    //        }
    //        if (value == null)
    //        {
    //            return false;
    //        }
    //        var type = value.GetType();
    //        if (type.IsAssignableFrom(typeof(IComparable)))
    //        {
    //            return false;
    //        }
    //        IComparable b = value as IComparable;
    //        if (_isContain)
    //        {
    //            return b.CompareTo(_max) >= 0;
    //        }
    //        return b.CompareTo(_max) > 0;
    //    }
    //}
}
