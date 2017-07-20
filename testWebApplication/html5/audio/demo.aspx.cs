using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using utilityClass.audio;
using utilityClass.generic;

namespace testWebApplication.html5.audio
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string basePath = GenericData.BaseDirectory + "html5/audio/";
            string pathBefore = basePath + "bdbeee99-24d2-4dcc-8c8a-2550565bb673.amr";
            string pathAfter = basePath + "a.mp3";
            AmrToMp3.ConvertToMp3(pathBefore, pathAfter);
        }
    }
}