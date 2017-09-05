using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Wintellect, PublicKey=12345678...90abcdef")]
[assembly: InternalsVisibleTo("Microsoft, PublicKey=b77a5c56...1934e089")]
internal class SomeInternalType
{

}

namespace testWebApplication.clrVia.friendAssembly
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SomeInternalType s = new SomeInternalType();
        }
    }

}
