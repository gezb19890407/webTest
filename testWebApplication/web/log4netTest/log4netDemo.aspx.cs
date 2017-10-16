using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.log4netTest
{
    public partial class log4netDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            testLog4net();
        }

        void testLog4net()
        {
            //ILog Log = LogManager.GetLogger("AmazonApIHelperLog");

            //log4net.Core.LogImpl logImpl = Log as log4net.Core.LogImpl;
            //AppenderCollection ac = ((log4net.Repository.Hierarchy.Logger)logImpl.Logger).Appenders;
            //if (ac.Count > 0)
            //{
            //    for (int i = 0; i < ac.Count; i++)
            //    {
            //        RollingFileAppender rfa = ac[i] as RollingFileAppender;
            //    }
            //}
            //Log.Info("test");
            //AppenderSkeleton appender = new CacheBaseAppender("AmazonApIHelperLog") { };
            //log4net.Config.BasicConfigurator.Configure(appender);
            
            ILog logger = CustomLogManager.GetCustomLogger("GetListingsDataLog", "15");
            //log4net.Config.BasicConfigurator.Configure(textBox_logAppender);


            ILog Log = LogManager.GetLogger("AmazonApIHelperLog");
            
            Log.Info("test");


        }
    }
}