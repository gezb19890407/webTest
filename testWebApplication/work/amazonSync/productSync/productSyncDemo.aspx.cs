using AutoMapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using testWebApplication.work.amazonSync.helper;
using testWebApplication.work.amazonSync.productSync.DTO;
using testWebApplication.work.amazonSync.productSync.module;
using AASV.Log4NetPack;
using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Layout;
using System.IO;

namespace testWebApplication.work.amazonSync.productSync
{
    public partial class productSyncDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Type t = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            //BasicConfigurator.Configure(appender);

            //ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //return;

            //初始化log日志
            //LogHelper.LoadFileAppender(AppDomain.CurrentDomain.BaseDirectory + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            log.Info("123");
            insertToServer();
        }

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        void insertToServer()
        {
            List<T_Am_ListingData> entityList = new List<T_Am_ListingData>();
            ProductSyncHelper product = new ProductSyncHelper();
            string directoryName = @"H:\work\AmazonSync\AmazonSync\EverPretty.Platform.Client\bin\Debug\SubmissionResult";
            foreach (string fileName in Directory.GetFiles(directoryName))
            {
                product.SaveListingsDataReport(fileName);
            }
        }

        void mapModelToDTO()
        {
            var entity = new AmListingDataDto();
            Mapper.Initialize(p => p.CreateMap<AmListingDataDto, T_Am_ListingData>()
                .BeforeMap((sourceType, destinationType)
                    =>
                {
                    sourceType.add_delete = destinationType.add_delete;
                    sourceType.asin1 = destinationType.asin1;
                }));
            T_Am_ListingData entityDTO = Mapper.Map<AmListingDataDto, T_Am_ListingData>(entity);

            SqlCommand command = new SqlCommand();
            //command.Parameters.Add
            ITransactionCustom tran = new SqlTransactionCustom();
            tran.BeginTransaction();
            CommandStaticHelper.ExecuteNonQuery(tran, new List<ICommandCustom>() { });
            tran.Commit();

            EntityToCommandStaticHelper.queryList<AmListingDataDto>(new SqlCommandCustom());
        }
    }
}