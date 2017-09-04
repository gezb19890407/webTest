using SCKJ.SSO.AuthenticateChildSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.sso
{
    public partial class pageRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AuthenticateClient.CheckUserTicket();
                var userInfo = AuthenticateClient.CurrentUser;
            }
        }
    }
}