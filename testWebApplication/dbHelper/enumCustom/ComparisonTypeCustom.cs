using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{
    /// <summary>
    /// 影响的比较规则
    /// </summary>
    public enum ComparisonTypeCustom
    {

        /// <summary>
        /// 等于
        /// </summary>
        Equal = 1,

        /// <summary>
        /// 等于
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// 大于
        /// </summary>
        Than = 3,

        /// <summary>
        /// 大于等于
        /// </summary>
        ThanEqual = 4,

        /// <summary>
        /// 小于
        /// </summary>
        Less = 5,

        /// <summary>
        /// 小于
        /// </summary>
        LessEqual = 6
    }
}