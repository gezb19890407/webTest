namespace testWebApplication.designPattern.stateMode.orderState
{
    public abstract class OrderState
    {
        #region 更新状态
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        /// <param name="orderStateEnum">要更新的订单状态</param>
        protected virtual bool UpdateState(OrderStateHelper helper, OrderStateEnum orderStateEnum)
        {
            bool result = false;
            result = new OrderInfoBll().UpdateValue(helper.OrderId, orderStateEnum);
            return result;
        }
        #endregion

        #region 虚函数

        #region 货主

        /// <summary>
        /// 待提交
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        public virtual bool PendingSubmission(OrderStateHelper helper)
        {
            return false;
        }

        /// <summary>
        /// 待审核
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        public virtual bool PendingAudit(OrderStateHelper helper)
        {
            return false;
        }

        /// <summary>
        /// 审核未通过
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        public virtual bool AuditNotPass(OrderStateHelper helper)
        {
            return false;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        /// <returns></returns>
        public virtual bool AuditPass(OrderStateHelper helper)
        {
            return false;
        }
        #endregion
        #endregion
    }
}