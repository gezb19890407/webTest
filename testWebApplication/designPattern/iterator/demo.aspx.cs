using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.designPattern.iterator
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            test();
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.createIterator();
            SystemMenu systemMenu = new SystemMenu(genericMenu);

            systemMenu.printMenu();
        }

        void test()
        {
            ICollection iCollection;
            List<object> objectList;
            Stack stack = new Stack();
            foreach (IEnumerable enumerable in stack)
            {
                
            }
        }
    }
}