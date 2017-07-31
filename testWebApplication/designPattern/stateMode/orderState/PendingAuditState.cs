using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.stateMode.orderState
{
    public class PendingAuditState : OrderState
    {
        /// <summary>
        /// 待审核可以变成审核未通过状态，所以重写审核未通过方法
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        public override bool AuditNotPass(OrderStateHelper helper)
        {
            return UpdateState(helper, OrderStateEnum.AuditNotPass);
        }

        /// <summary>
        /// 待审核可以变成审核通过状态，所以重写审核通过方法
        /// </summary>
        /// <param name="helper">订单状态操作类</param>
        /// <returns></returns>
        public override bool AuditPass(OrderStateHelper helper)
        {
            return UpdateState(helper, OrderStateEnum.AuditPass);
        }
    }
}