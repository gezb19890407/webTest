using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.objectToListString
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
        }

        void test()
        {
            ObjectToListString objectClass = new ObjectToListString();
            objectClass.name = "test";
            objectClass.infoList = new List<string>() { "测试1", "测试2" };

            //object classObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectClass);
            PropertyInfo pi = objectClass.GetType().GetProperty("infoList");
            object infoList = pi.GetValue(objectClass, null);
            foreach (var str in infoList as IEnumerable)
            {

            }

        }

        class ObjectToListString
        {
            public string name { get; set; }

            public List<string> infoList { get; set; }
        }
    }
}