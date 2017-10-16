using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testWebApplication.work.amazonSync.productSync
{
    public class AmazonSKUPrice
    {  /// <summary>
       /// 编号
       /// </summary>
        public int ID
        {
            get;
            set;
        }

        public int PID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AM_ASIN
        {
            get;
            set;
        }

        private MoneyType _LandedPrice;

        /// <summary>
        /// 
        /// </summary>
        public MoneyType LandedPrice
        {
            get
            {
                if (_LandedPrice == null)
                {
                    _LandedPrice = new MoneyType();
                }
                return _LandedPrice;
            }
            set
            {
                _LandedPrice = value;
            }
        }

        private MoneyType _ListingPrice;

        /// <summary>
        /// 
        /// </summary>
        public MoneyType ListingPrice
        {
            get
            {
                if (_ListingPrice == null)
                {
                    _ListingPrice = new MoneyType();
                }
                return _ListingPrice;
            }
            set
            {
                _ListingPrice = value;
            }
        }

        private MoneyType _RegularPrice;

        /// <summary>
        /// 
        /// </summary>
        public MoneyType RegularPrice
        {
            get
            {
                if (_RegularPrice == null)
                {
                    _RegularPrice = new MoneyType();
                }
                return _RegularPrice;
            }
            set
            {
                _RegularPrice = value;
            }
        }

        private MoneyType _ShippingCost;

        /// <summary>
        /// 
        /// </summary>
        public MoneyType ShippingCost
        {
            get
            {
                if (_ShippingCost == null)
                {
                    _ShippingCost = new MoneyType();
                }
                return _ShippingCost;
            }
            set
            {
                _ShippingCost = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Sku
        {
            get;
            set;
        }

        public string Sku_Online
        {
            get;
            set;
        }

        public string FulfillmentChannel
        {
            get;
            set;
        }

        private int inpid = -1;

        /// <summary>
        /// 是否还在平台  0 ：不在 1：在
        /// </summary>
        public int inPID
        {
            get
            {
                return inpid;
            }
            set
            {
                inpid = value;
            }
        }
    }

    public class MoneyType
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
