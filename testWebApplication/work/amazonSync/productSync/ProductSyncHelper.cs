using AASV.SqlServerKit;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using testWebApplication.work.amazonSync.helper;
using testWebApplication.work.amazonSync.productSync.module;

namespace testWebApplication.work.amazonSync.productSync
{
    public class ProductSyncHelper
    {
        public ILog Log = LogManager.GetLogger("AmazonApIHelperLog");

        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings["db_titlekeyword"].ConnectionString;

        public void SaveListingsDataReport(string filename)
        {
            Log = LogManager.GetLogger("AmazonApIHelperLog");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<T_Am_ListingData> lds = new List<T_Am_ListingData>();
            int accountId = int.Parse(filename.Substring(filename.LastIndexOf("\\") + 1, 2));
            using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
            {
                String line = "";
                int i = 0;

                DataTable dt = new DataTable();

                while ((line = sr.ReadLine()) != null)
                {
                    i++;
                    Regex regex = new Regex("\t");//以cjlovefl分割
                    string[] bit = regex.Split(line);

                    try
                    {
                        if (i == 1)
                        {
                            for (int j = 0; j < bit.Length; j++)
                            {
                                DataColumn dc = new DataColumn();
                                dc.ColumnName = bit[j].ToString();
                                dt.Columns.Add(dc);
                            }
                            continue;//跳过第一行
                        }

                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dr[j] = bit[j].ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                    catch (Exception ex)
                    {
                        Log.ErrorFormat("加载{0}条记录：{1}记录发生错误", i, ex);
                    }
                }
                Stopwatch stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                DataTable tableColumns = DbHelper.GetTableColumnsByName(sqlConnectionString, "T_Am_ListingData");
                PropertyInfo[] properties = DbHelper.GetPropertyInfos<T_Am_ListingData>();
                //循环dt
                if (dt.Rows.Count > 0)
                {
                    T_Am_ListingData ld;
                    for (int n = 0; n < dt.Rows.Count; n++)
                    {
                        DataRow bit = dt.Rows[n];
                        ld = new T_Am_ListingData();
                        ld.item_name = dt.Columns.Contains("item-name") ? bit["item-name"].ToString() : "";
                        //ld.item_name = dt.Columns.Contains("item-name") ? bit["item-name"].ToString() : "";
                        ld.item_description = dt.Columns.Contains("item-description") ? bit["item-description"].ToString() : "";
                        ld.listing_id = dt.Columns.Contains("listing-id") ? bit["listing-id"].ToString() : "";
                        ld.seller_sku = dt.Columns.Contains("seller-sku") ? bit["seller-sku"].ToString() : "";
                        ld.price = dt.Columns.Contains("price") ? bit["price"].ToString() : "";
                        ld.quantity = dt.Columns.Contains("quantity") ? bit["quantity"].ToString() : "";
                        ld.open_date = dt.Columns.Contains("open-date") ? bit["open-date"].ToString() : "";
                        ld.image_url = dt.Columns.Contains("image-url") ? bit["image-url"].ToString() : "";
                        ld.item_is_marketplace = dt.Columns.Contains("item-is-marketplace") ? bit["item-is-marketplace"].ToString() : "";
                        ld.product_id_type = dt.Columns.Contains("product-id-type") ? bit["product-id-type"].ToString() : "";
                        ld.zshop_shipping_fee = dt.Columns.Contains("zshop-shipping-fee") ? bit["zshop-shipping-fee"].ToString() : "";
                        ld.item_note = dt.Columns.Contains("item-note") ? bit["item-note"].ToString() : "";
                        ld.item_condition = dt.Columns.Contains("item-condition") ? bit["item-condition"].ToString() : "";
                        ld.zshop_category1 = dt.Columns.Contains("zshop-category1") ? bit["zshop-category1"].ToString() : "";
                        ld.zshop_browse_path = dt.Columns.Contains("zshop-browse-path") ? bit["zshop-browse-path"].ToString() : "";
                        ld.zshop_storefront_feature = dt.Columns.Contains("zshop-storefront-feature") ? bit["zshop-storefront-feature"].ToString() : "";
                        ld.asin1 = dt.Columns.Contains("asin1") ? bit["asin1"].ToString() : "";
                        ld.asin2 = dt.Columns.Contains("asin2") ? bit["asin2"].ToString() : "";
                        ld.asin3 = dt.Columns.Contains("asin3") ? bit["asin3"].ToString() : "";
                        ld.will_ship_internationally = dt.Columns.Contains("will-ship-internationally") ? bit["will-ship-internationally"].ToString() : "";
                        ld.expedited_shipping = dt.Columns.Contains("expedited-shipping") ? bit["expedited-shipping"].ToString() : "";
                        ld.zshop_boldface = dt.Columns.Contains("zshop-boldface") ? bit["zshop-boldface"].ToString() : "";
                        ld.product_id = dt.Columns.Contains("product-id") ? bit["product-id"].ToString() : "";
                        ld.bid_for_featured_placement = dt.Columns.Contains("bid-for-featured-placement") ? bit["bid-for-featured-placement"].ToString() : "";
                        ld.add_delete = dt.Columns.Contains("add-delete") ? bit["add-delete"].ToString() : "";
                        ld.pending_quantity = dt.Columns.Contains("pending-quantity") ? bit["pending-quantity"].ToString() : "";
                        ld.fulfillment_channel = dt.Columns.Contains("fulfillment-channel") ? bit["fulfillment-channel"].ToString() : "";


                        if (ld.seller_sku.ToUpper().Contains("MISSING") || string.IsNullOrEmpty(ld.seller_sku))
                        {
                            continue;
                        }
                        ld.pid = accountId;// Convert.ToInt32(15);
                        ld.UpdateDateTime = System.DateTime.Now;
                        ld.IsDel = false;
                        List<string> errorList;
                        if (DbHelper.valudateEntity(ld, tableColumns, properties, out errorList))
                        {
                            lds.Add(ld);
                        }
                        else
                        {
                            Log.InfoFormat("更新产品失败，pid = {0},listing_id={1},product_id={2}");
                        }
                    }
                    stopwatch1.Stop();
                    Log.Info("验证花费时间：" + (Int32)stopwatch1.ElapsedMilliseconds / 1000 + "秒！");
                    Log.Info("开始保存数据-------------------------");
                    SaveListingsData(lds);
                    Log.Info("保存数据完成-------------------------");
                }
            }
            stopwatch.Stop();
            Log.Info("同步一次产品花费时间：" + (Int32)stopwatch.ElapsedMilliseconds / 1000 + "秒！");
        }

        void doBusiness(List<T_Am_ListingData> lds, ref List<T_Am_ListingData> existsList, ref List<T_Am_ListingData> notExistsList, AmazonDataAccess dataAccess)
        {
            DataTable dataTableExists = new DataTable();
            try
            {
                int count = 1000;
                while (lds.Count > 0)
                {
                    count = count > lds.Count ? lds.Count : count;
                    List<T_Am_ListingData> list = lds.Take(count).ToList();
                    dataTableExists = dataAccess.queryAmazonListingData(list);
                    if (dataTableExists != null && dataTableExists.Rows.Count > 0)
                    {
                        bool isExists;
                        foreach (var entity in list)
                        {
                            isExists = false;
                            foreach (DataRow row in dataTableExists.Rows)
                            {
                                if (row["pid"].ToString() == entity.pid.ToString()
                                    && row["listing_id"].ToString() == entity.listing_id
                                    && row["product_id"].ToString() == entity.product_id)
                                {
                                    isExists = true;
                                }
                            }
                            if (isExists)
                            {
                                existsList.Add(entity);
                            }
                            else
                            {
                                notExistsList.Add(entity);
                            }
                        };
                    }
                    else
                    {
                        notExistsList.AddRange(list);
                    }
                    SqlCommand command = new SqlCommand();
                    //command.Transaction = new SqlTransaction()

                    //DbHelper.ExecuteNonQuery(tr)
                    //lds = lds.Skip(count).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Info("转换失败：" + ex);
            }
        }

        public void SaveListingsData(List<T_Am_ListingData> lds)
        {
            AmazonDataAccess dataAccess = new AmazonDataAccess();

            try
            {
                //先删除该店类所有产品
                dataAccess.deleteAmazonListingData(lds[0].pid.ToString());
                //DbHelper.Insert("T_Am_ListingData", lds);
            }
            catch (Exception EX)
            {
                Log.ErrorFormat("初始化数据失败：{0}", EX);
            }


            //List<string> lstEle = new List<string>();
            ////SalaryDetailModel model = new SalaryDetailModel();
            //foreach (PropertyInfo prop in typeof(T_Am_ListingData).GetProperties())
            //{
            //    if (prop.Name.ToLower() == "id") continue;
            //    lstEle.Add(prop.Name);

            //}

            //lds = lds.Take(100).ToList();
            //try
            //{
            //    var result = SqlServerRepo.ExecuteInsertOrUpdateSqlTran(lds, "T_Am_ListingData", sqlConnectionString, new string[] { "pid", "listing_id", "product_id" },
            //        //lstEle.ToArray(), lstEle.ToArray(),
            //        null, null,
            //        50, 10);
            //}
            //catch (Exception ex)
            //{
            //}
            //return;

            List<T_Am_ListingData> existsList = new List<T_Am_ListingData>();
            List<T_Am_ListingData> notExistsList = new List<T_Am_ListingData>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            doBusiness(lds, ref existsList, ref notExistsList, dataAccess);
            stopwatch.Stop();
            Log.Info("查询产品是否存在花费时间：" + (Int32)stopwatch.ElapsedMilliseconds / 1000 + "秒！");
            DbHelper.Insert("T_Am_ListingData", notExistsList);
            //lds[0].item_name = lds[0].item_name + " 查询条件  exec('select top 1 * from T_Am_ListingData')";
            DbHelper.Insert("T_Am_ListingDataInsert", lds);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"
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
        AND T_Am_ListingData.product_id = tbInsert.listing_id
        AND T_Am_ListingData.pid = tbInsert.listing_id
");
            try
            {
                stopwatch.Restart();
                //更新产品数据
                dataAccess.updateAmazonListingData(lds[0].pid.ToString().ToString());
                stopwatch.Stop();
                Log.Info("更新产品数据花费时间：" + (Int32)stopwatch.ElapsedMilliseconds / 1000 + "秒！");
            }
            catch (Exception EX)
            {
                Log.ErrorFormat("更新产品失败：{0}", EX);
            }
            //foreach (var ld in lds)
            //{
            //    try
            //    {
            //        int i = dataAccess.AddOrUpdateListingsData(ld);
            //        if (count % 1000 == 0)
            //        {
            //            Log.InfoFormat("第：{0}条更新成功", i);
            //        }
            //        count++;
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.ErrorFormat("保存:{0}发生错误{1}", ld.seller_sku, ex);
            //    }
            //}
        }

    }
}