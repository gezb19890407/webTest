using System.ComponentModel;

namespace testWebApplication.designPattern.stateMode.orderState
{
    /// <summary>
    /// 订单状态枚举
    /// </summary>
    public enum OrderStateEnum
    {
        #region 订单状态枚举

        /// <summary>
        ///待提交
        /// </summary>
        [Description("待提交")]
        PendingSubmission = 1,

        /// <summary>
        ///待审核
        /// </summary>
        [Description("待审核")]
        PendingAudit = 3,

        /// <summary>
        ///审核通过
        /// </summary>
        [Description("审核通过")]
        AuditPass = 5,
        /// <summary>

        ///审核未通过
        /// </summary>
        [Description("审核未通过")]
        AuditNotPass = 7
        #endregion 订单状态枚举
    }
}