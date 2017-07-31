using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.iterator
{
    public class IteratorRealization : Iterator
    {
        List<IMenu> menuItemList;
        int position = 0;

        public IteratorRealization(List<IMenu> menuItemList)
        {
            this.menuItemList = menuItemList;
        }

        public bool hasNext()
        {
            if (position >= menuItemList.Count || menuItemList[position] == null)
            {
                return false;
            }
            return true;
        }

        public object next()
        {
            IMenu menu = menuItemList[position];
            position++;
            return menu;
        }
    }

    public interface IMenu
    {
        string name { get; set; }
    }

    public class MenuItem : IMenu
    {
        public string code { get; set; }

        public string name { get; set; }

        public decimal sequence { get; set; }
    }
}