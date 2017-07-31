using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.stateMode.orderState
{
    public class OrderStateHelper
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单父类
        /// </summary>
        public OrderState _orderState { get; set; }

        public OrderStateHelper(long userId, string orderId, int state)
        {
            UserId = userId;
            OrderId = orderId;
            SetState(state);
        }

        string _enumValue;

        #region 设置当前订单状态类（实例化子类）
        /// <summary>
        /// 设置当前订单状态
        /// </summary>
        /// <param name="value">当前订单状态值</param>
        public void SetState(int value)
        {
            //设置当前枚举值
            _enumValue = EnumHelper.GetInstance<OrderStateEnum>(value);

            #region 通过命名空间+类名 实例化具体的子类
            string adaptorName = this.GetType().Namespace + "." + _enumValue + "State";
            var adaptorType = Type.GetType(adaptorName);

            if (adaptorType != null)
            {
                _orderState = Activator.CreateInstance(Type.GetType(adaptorName), null) as OrderState;
            }
            #endregion
        }
        #endregion

        #region 改变订单状态为其他状态
        /// <summary>
        /// 改变订单状态为其他状态
        /// </summary>
        /// <param name="value">新的订单状态值</param>
        public bool ChangState(int value)
        {
            //设置当前枚举值
            _enumValue = EnumHelper.GetInstance<OrderStateEnum>(value);
            #region 通过反射方法名字符串动态调用方法
            Type type = _orderState.GetType();
            var method = type.GetMethod(_enumValue.ToString());
            return (bool)method.Invoke(_orderState, new object[] { this });
            #endregion
        }
        #endregion 
    }

    public class EnumHelper
    {
        public static string GetInstance<T>(int value)
        {
            string name = "";
            foreach (int c in (int[])Enum.GetValues(typeof(OrderStateEnum)))
            {
                if (c == value)
                {
                    name = Enum.GetValues(typeof(OrderStateEnum)).GetValue(c).ToString();
                }
            }
            return name;
        }
    }
}