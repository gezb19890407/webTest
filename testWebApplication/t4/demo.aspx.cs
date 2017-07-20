using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using testWebApplication.t4.Template;

namespace testWebApplication.t4
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
        }

        void test()
        {
            TemplateEntity entity = new TemplateEntity();
            entity.code = "123";
            if (ModelState.IsValid)
            {

            }
        }
    }
}