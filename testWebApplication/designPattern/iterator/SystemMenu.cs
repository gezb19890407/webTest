using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.iterator
{
    public class SystemMenu
    {
        GenericMenu genericMenu;

        public SystemMenu(GenericMenu genericMenu)
        {
            this.genericMenu = genericMenu;
        }

        public void printMenu()
        {
            Iterator iterator = genericMenu.createIterator();
            IEnumerable iEnumerable = genericMenu.createIEnumerator();

            printMenu(iterator);
            printMenu(iEnumerable);
        }

        public void printMenu(Iterator iterator)
        {
            while (iterator.hasNext())
            {
                MenuItem menuItem = (MenuItem)iterator.next();
                Console.WriteLine("name：" + menuItem.name + "，sequence：" + menuItem.sequence);
            }
        }

        public void printMenu(IEnumerable iEnumerable)
        {
            foreach (object enumObject in iEnumerable)
            {
                MenuItem menuItem = (MenuItem)enumObject;
                Console.WriteLine("name：" + menuItem.name + "，sequence：" + menuItem.sequence);
            }
        }
    }
}