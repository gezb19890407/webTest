using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.clrVia.objectEquals
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 obj1 = 1, obj2 = 2;
            var q = obj1 ^ obj2;
        }

        void test()
        {
            dynamic obj = new
            {
                a = 1
            };
            obj.b = 2;
        }
    }
}