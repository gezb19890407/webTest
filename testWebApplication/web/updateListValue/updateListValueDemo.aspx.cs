using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.updateListValue
{
    public partial class updateListValueDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
        }

        void test()
        {
            List<ListTest> list = new List<ListTest>() {
                 new ListTest() {
                      code="1",
                      value="1"
                 },
                 new ListTest() {
                      code="2",
                      value="2"
                 },
                 new ListTest() {
                      code="2",
                      value="3"
                 }
            };

            ListTest obj1;
            List<ListTest> newList = new List<ListTest>();
            for (var i = 0; i < list.Count(); i++)
            {
                //obj1 = new ListTest();
                var isExists = newList.Where(p => p.code == list[i].code).Count() > 0;
                obj1 = isExists ? newList.Find(p => p.code == list[i].code) : new ListTest();
                obj1.value = list[i].value;
                if (!isExists)
                {
                    obj1.code = list[i].code;
                    newList.Add(obj1);
                }
                obj1 = null;
            }
        }
    }

    public class ListTest
    {

        public string code { get; set; }

        public string value { get; set; }
    }
}