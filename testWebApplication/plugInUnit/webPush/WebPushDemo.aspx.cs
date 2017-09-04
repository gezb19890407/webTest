using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.plugInUnit.webPush
{
    public partial class WebPushDemo : System.Web.UI.Page
    {
        public const string SubscribeKey = "BS-3c1d5720d5654f31bb35376be447a815";
        public const string CommonKey = "BC-d1188086f6b944d1a76c8ce4088ad5f1";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void push() { }



        protected void Button1_Click(object sender, EventArgs e)
        {
            var content = "后端推送的消息";
            dynamic argument = new
            {
                appkey = CommonKey,
                channel = "notification",
                content = content
            };
            try
            {
                HttpClientUtil.doPostMethodToObj("publish", JsonConvert.SerializeObject(argument));
                //ExecuteScript(content);
            }
            catch (Exception ex)
            {
                ExecuteScript(content + "错误：" + ex.Message);
            }
        }


        private object ExecuteScript(string message)
        {
            string jsCode = string.Format(@"function writePanel(message, title) {{
alert('{0}');
                var newDiv = document.createElement('div');
                var date = new Date().Format('yyyy-MM-dd HH:mm:ss');
                newDiv.innerHTML = date + '：' + (title != null ? '标题：' + title : '') + message + '<br />';
                document.getElementById('divPanel').appendChild(newDiv);
            }}", message);
            object o = ExecuteScript("writePanel('" + message + "')", jsCode);
            return o;
        }

        /// <summary>
        /// 执行JS
        /// </summary>
        /// <param name="sExpression">参数体</param>
        /// <param name="sCode">JavaScript代码的字符串</param>
        /// <returns></returns>
        private string ExecuteScript(string sExpression, string sCode)
        {
            MSScriptControl.ScriptControl scriptControl = new MSScriptControl.ScriptControl();
            scriptControl.UseSafeSubset = true;
            scriptControl.Language = "JScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return null;
        }
    }
}