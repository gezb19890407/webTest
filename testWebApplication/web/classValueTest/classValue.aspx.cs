using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Caching;

namespace testWebApplication.web.classValueTest
{
    public partial class classValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
            test();
        }
        ObjectCache cache = MemoryCache.Default;
        int i = 0;

        void test()
        {
            ClassValueTest classValueTest = new ClassValueTest()
            {
                code = (i++).ToString(),
                value = "test"
            };
            cache["classValueTest"] = classValueTest;
            classValueTest = null;

            ClassValueTest classValueTest1 = (ClassValueTest)((ClassValueTest)cache["classValueTest"]).Clone();
            cache["classValueTest2"] = classValueTest1;
            classValueTest1.value = "test333";
            classValueTest1 = null;
        }
    }

    public class ClassValueTest : ICloneable
    {
        public string code { get; set; }

        public string value { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}