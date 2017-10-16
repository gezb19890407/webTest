using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using testWebApplication.work.amazonSync.helper;
using testWebApplication.work.amazonSync.productSync.module;
using System.Diagnostics;

namespace testWebApplication.work.amazonSync.productSync
{
    public partial class AmazonDataAccess
    {

        public DataTable queryAmazonListingData(List<T_Am_ListingData> entityList)
        {
            StringBuilder sqlWhere = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var entity in entityList)
            {
                sqlWhere.AppendFormat(@" (pid = {0} and listing_id = '{1}' AND product_id = '{2}') OR ", entity.pid, entity.listing_id, entity.product_id);
            } 
            string strSQL = string.Format(@"
SELECT 
    listing_id,
    product_id,
    pid 
FROM T_Am_ListingData with(nolock)
WHERE {0}", sqlWhere.ToString().Substring(0, sqlWhere.ToString().LastIndexOf("OR")));
            stopwatch.Stop();
            var q = stopwatch.ElapsedMilliseconds;
            return DbHelper.ExecuteTable(CommandType.Text, strSQL, null);
        }

        public int DelListingsData(string pid)
        {
            string strSQL = string.Format(" UPDATE T_Am_ListingData SET IsDel = 1 WHERE pid = {0}", pid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, null);
        }


        public int deleteAmazonListingData(string pid)
        {
            string strSQL = string.Format(@" 
DELETE T_Am_ListingDataInsert WHERE pid = {0}
UPDATE T_Am_ListingData SET IsDel = 1 WHERE pid = {0}", pid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, null);
        }

        public int updateAmazonListingData(string pid)
        {
            string strSQL = string.Format(@" 
UPDATE  T_Am_ListingData
SET     item_name = tbInsert.item_name ,
        item_description = tbInsert.Item_description ,
        seller_sku = tbInsert.seller_sku ,
        price = tbInsert.price ,
        quantity = tbInsert.quantity ,
        open_date = tbInsert.open_date ,
        image_url = tbInsert.image_url ,
        item_is_marketplace = tbInsert.item_is_marketplace ,
        product_id_type = tbInsert.product_id_type ,
        zshop_shipping_fee = tbInsert.zshop_shipping_fee ,
        item_note = tbInsert.item_note ,
        item_condition = tbInsert.item_condition ,
        zshop_category1 = tbInsert.zshop_category1 ,
        zshop_browse_path = tbInsert.zshop_browse_path ,
        zshop_storefront_feature = tbInsert.zshop_storefront_feature ,
        asin1 = tbInsert.asin1 ,
        asin2 = tbInsert.asin2 ,
        asin3 = tbInsert.asin3 ,
        will_ship_internationally = tbInsert.will_ship_internationally ,
        expedited_shipping = tbInsert.expedited_shipping ,
        zshop_boldface = tbInsert.zshop_boldface ,
        bid_for_featured_placement = tbInsert.bid_for_featured_placement ,
        add_delete = tbInsert.add_delete ,
        pending_quantity = tbInsert.pending_quantity ,
        fulfillment_channel = tbInsert.fulfillment_channel ,
        UpdateDateTime = tbInsert.UpdateDateTime ,
        IsDel = tbInsert.IsDel
FROM    T_Am_ListingDataInsert tbInsert
WHERE   T_Am_ListingData.listing_id = tbInsert.listing_id
        AND T_Am_ListingData.product_id = tbInsert.product_id
        AND T_Am_ListingData.pid = tbInsert.pid 
    AND T_Am_ListingData.pid = {0}", pid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, null);
        }

        public int AddOrUpdateListingsData(ListingsDataModel model)
        {
            SqlParameter[] param = {
                new SqlParameter("@Item_name",SqlDbType.VarChar),
                new SqlParameter("@Item_description",SqlDbType.VarChar),
            };

            param[0].Value = model.item_name;
            param[1].Value = model.item_description;

            string strSQL = string.Format(@"
if exists(select 1 from T_Am_ListingData where listing_id='{0}' and product_id = '{22}' and pid = {1} )
update T_Am_ListingData set  item_name = @Item_name ,item_description = @Item_description ,seller_sku = '{4}',price = '{5}',quantity = '{6}',open_date='{29}',image_url = '{7}',item_is_marketplace = '{8}',
product_id_type = '{9}',zshop_shipping_fee = '{10}',item_note = '{11}',item_condition = '{12}',zshop_category1 = '{13}',zshop_browse_path = '{14}',zshop_storefront_feature = '{15}',
asin1 = '{16}',asin2 = '{17}',asin3 = '{18}',will_ship_internationally = '{19}',expedited_shipping = '{20}',zshop_boldface = '{21}',bid_for_featured_placement = '{23}',
add_delete = '{24}',pending_quantity = '{25}',fulfillment_channel = '{26}',UpdateDateTime = '{27}',IsDel = {28} 
where listing_id='{0}' and product_id = '{22}' and   pid = {1} 
else 
insert into T_Am_ListingData ([pid],[item_name],[item_description],[listing_id],[seller_sku],[price],[quantity],[open_date],[image_url],[item_is_marketplace],[product_id_type],[zshop_shipping_fee],[item_note]
,[item_condition],[zshop_category1],[zshop_browse_path],[zshop_storefront_feature],[asin1],[asin2],[asin3],[will_ship_internationally],[expedited_shipping],[zshop_boldface],[product_id],[bid_for_featured_placement]
,[add_delete],[pending_quantity],[fulfillment_channel],[UpdateDateTime],[IsDel]) Values( 
{1}, @Item_name ,@Item_description, '{0}','{4}', '{5}', '{6}','{29}','{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}',
'{26}','{27}',{28} 
)
", model.listing_id, model.pid, model.item_name, model.item_description, model.seller_sku, model.price, model.quantity, model.image_url, model.item_is_marketplace,
 model.product_id_type, model.zshop_shipping_fee, model.item_note, model.item_condition, model.zshop_category1, model.zshop_browse_path, model.zshop_storefront_feature,
 model.asin1, model.asin2, model.asin3, model.will_ship_internationally, model.expedited_shipping, model.zshop_boldface, model.product_id, model.bid_for_featured_placement,
 model.add_delete, model.pending_quantity, model.fulfillment_channel, model.UpdateDateTime.ToString("yyyy-MM-dd HH:mm:ss"), model.IsDel == true ? 1 : 0, model.open_date);
            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, param);
        }

        public DataTable GetListingsDataByPid(string pid)
        {
            string strSQL = string.Format(@"SELECT   *   FROM T_Am_ListingData  where IsDel  = 0 and pid = {0} ", pid);
            //测试用sql，废弃
            //string strSQL = string.Format(@"  SELECT   *   FROM T_Am_ListingData a
            //left join [db_titlekeyword].[dbo].[T_Am_Price]b  on a.pid = b.accountid and a.seller_sku = b.sellersku
            //where IsDel  = 0 and pid =  {0}
            //and isnull(b.currencycode,0) = '' ", pid);
            return DbHelper.ExecuteTable(CommandType.Text, strSQL, null);
        }

        public bool InsertOrUpdateListPrice(AmazonSKUPrice price)
        {
            string strSQL = string.Format(@"
if exists(select 1 from T_Am_Price where SellerSKU='{1}'  and AccountId = {0} )
update T_Am_Price set SellerId = '{2}',LanderPrice = {3},ListingPrice = {4},ShippingPrice = {5},RegularPrice = {6},CurrencyCode = '{8}',AM_ASIN = '{9}'
where SellerSKU='{1}'  and AccountId = {0}
else insert into T_Am_Price (SellerId,SellerSKU,LanderPrice,ListingPrice,ShippingPrice,RegularPrice,CreateTime,AccountId,CurrencyCode,AM_ASIN) 
Values('{2}','{1}',{3},{4},{5},{6},'{7}',{0},'{8}','{9}')", price.PID, price.Sku, "", price.LandedPrice.Amount, price.ListingPrice.Amount, price.ShippingCost.Amount, price.RegularPrice.Amount, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), price.LandedPrice.CurrencyCode, price.AM_ASIN);

            return DbHelper.ExecuteNonQuery(CommandType.Text, strSQL, null) > 0;
        }

    }
}
