using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testWebApplication.clrVia.packing
{
    public partial class packDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Point p = new Point(1, 1);
            var p1 = p.ToString();

            p.Change(2, 2);
            var p2 = p.ToString();

            Object o = p;
            var p3 = o.ToString();

            ((Point)o).Change(3, 3);
            var p4 = o.ToString();

            ((IChangeBoxedPoint)p).Change(4, 4);
            var p5 = p.ToString();

            ((IChangeBoxedPoint)o).Change(5, 5);
            var p6 = o.ToString();

            
        }
    }

    

    internal interface IChangeBoxedPoint
    {
        void Change(Int32 x, Int32 y);
    }

    internal struct Point : IChangeBoxedPoint
    {
        private Int32 m_x, m_y;

        public Point(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }

        public void Change(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", m_x.ToString(), m_y.ToString());
        }


    }
}