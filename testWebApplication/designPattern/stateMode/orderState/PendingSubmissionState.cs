using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.stateMode.orderState
{
    public class PendingSubmissionState : OrderState
    {
        /// <summary>
        /// 待提交可以变成待审核状态，所以重写待审核方法
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public override bool PendingAudit(OrderStateHelper helper)
        {
            return UpdateState(helper, OrderStateEnum.PendingAudit);
        }
    }
}