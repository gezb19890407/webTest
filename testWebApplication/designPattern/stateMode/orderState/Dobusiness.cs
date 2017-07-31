using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.stateMode.orderState
{
    public class Dobusiness
    {
        void doBusiness()
        {
            long userId = 22222;
            string orderId = "";
            int state = 1;
            int newState = 2;
            var orderStateHelper = new OrderStateHelper(userId, orderId, state);
            var isChange = orderStateHelper.ChangState(newState);
        }
    }
}