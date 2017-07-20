using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.plugInUnit.validate
{
    ///
    ///验证类型
    ///
    [Flags]
    public enum ValidateType
    {
        ///
        ///字段或属性是否为空字串
        ///
        IsEmpty = 0x0001,

        ///
        ///字段或属性的最小长度
        ///
        MinLength = 0x0002,

        ///
        ///字段或属性的最大长度
        ///
        MaxLength = 0x0004,

        ///
        ///字段或属性的值是否为数值型
        ///
        IsNumber = 0x0008,

        ///
        ///字段或属性的值是否为时间类型
        ///
        IsDateTime = 0x0010,

        ///
        ///字段或属性的值是否为正确的浮点类型
        ///
        IsDecimal = 0x0020,

        ///
        ///字段或属性的值是否包含在指定的数据源数组中 
        ///
        IsInCustomArray = 0x0040,

        ///
        ///字段或属性的值是否为固定电话号码格式
        ///
        IsTelphone = 0x0080,

        ///
        ///字段或属性的值是否为手机号码格式
        ///
        IsMobile = 0x0100
    }
}