using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.work.amazonSync.productSync.DTO
{
    public class AmListingDataDto
    {
        public AmListingDataDto()
        { }
        #region Model
        private int _id;
        private int? _pid;
        private string _item_name;
        private string _item_description;
        private string _listing_id;
        private string _seller_sku;
        private string _price;
        private string _quantity;
        private string _open_date;
        private string _image_url;
        private string _item_is_marketplace;
        private string _product_id_type;
        private string _zshop_shipping_fee;
        private string _item_note;
        private string _item_condition;
        private string _zshop_category1;
        private string _zshop_browse_path;
        private string _zshop_storefront_feature;
        private string _asin1;
        private string _asin2;
        private string _asin3;
        private string _will_ship_internationally;
        private string _expedited_shipping;
        private string _zshop_boldface;
        private string _product_id;
        private string _bid_for_featured_placement;
        private string _add_delete;
        private string _pending_quantity;
        private string _fulfillment_channel;
        private DateTime? _updatedatetime;
        private bool _isdel;
        /// <summary>
        /// 
        /// </summary>
        //public int Id
        //{
        //    set { _id = value; }
        //    get { return _id; }
        //}
        /// <summary>
        /// 
        /// </summary>
        public int? pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_name
        {
            set { _item_name = value; }
            get { return _item_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_description
        {
            set { _item_description = value; }
            get { return _item_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string listing_id
        {
            set { _listing_id = value; }
            get { return _listing_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seller_sku
        {
            set { _seller_sku = value; }
            get { return _seller_sku; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string open_date
        {
            set { _open_date = value; }
            get { return _open_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string image_url
        {
            set { _image_url = value; }
            get { return _image_url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_is_marketplace
        {
            set { _item_is_marketplace = value; }
            get { return _item_is_marketplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string product_id_type
        {
            set { _product_id_type = value; }
            get { return _product_id_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zshop_shipping_fee
        {
            set { _zshop_shipping_fee = value; }
            get { return _zshop_shipping_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_note
        {
            set { _item_note = value; }
            get { return _item_note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string item_condition
        {
            set { _item_condition = value; }
            get { return _item_condition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zshop_category1
        {
            set { _zshop_category1 = value; }
            get { return _zshop_category1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zshop_browse_path
        {
            set { _zshop_browse_path = value; }
            get { return _zshop_browse_path; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zshop_storefront_feature
        {
            set { _zshop_storefront_feature = value; }
            get { return _zshop_storefront_feature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string asin1
        {
            set { _asin1 = value; }
            get { return _asin1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string asin2
        {
            set { _asin2 = value; }
            get { return _asin2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string asin3
        {
            set { _asin3 = value; }
            get { return _asin3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string will_ship_internationally
        {
            set { _will_ship_internationally = value; }
            get { return _will_ship_internationally; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string expedited_shipping
        {
            set { _expedited_shipping = value; }
            get { return _expedited_shipping; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zshop_boldface
        {
            set { _zshop_boldface = value; }
            get { return _zshop_boldface; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string product_id
        {
            set { _product_id = value; }
            get { return _product_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bid_for_featured_placement
        {
            set { _bid_for_featured_placement = value; }
            get { return _bid_for_featured_placement; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string add_delete
        {
            set { _add_delete = value; }
            get { return _add_delete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pending_quantity
        {
            set { _pending_quantity = value; }
            get { return _pending_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fulfillment_channel
        {
            set { _fulfillment_channel = value; }
            get { return _fulfillment_channel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDateTime
        {
            set { _updatedatetime = value; }
            get { return _updatedatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        #endregion Model
    }
}