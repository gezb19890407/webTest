using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.web.parallel
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            testParallel();
        }

        void testParallel()
        {
            var entityList = new List<ParallelTest>() {
                new ParallelTest() {
                    id="a",
                    name="a"
                },
                new ParallelTest() {
                    id="b",
                    name="b"
                }
            };
            //Parallel.ForEach(entityList, (item, loopState) =>
            //{
            //    item.name = item.id + item.name;
            //});
            foreach (var item in entityList)
            {
                item.name = item.id + item.name;
            }
        }

    }

    public class ParallelTest
    {
        public string id { get; set; }

        public string name { get; set; }
    }
}