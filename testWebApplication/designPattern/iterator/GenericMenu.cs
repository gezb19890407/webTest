using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWebApplication.designPattern.iterator
{
    public class GenericMenu
    {
        private List<IMenu> menuList;
        private Iterator iterator;

        public Iterator createIterator()
        {
            menuList = new List<IMenu>(){
                new MenuItem()
                {
                    code = "1",
                    name = "系统配置",
                    sequence = 99
                },
                 new MenuItem()
                {
                    code = "2",
                    name = "首页",
                    sequence = 1
                },
            };
            iterator = new IteratorRealization(menuList);
            return iterator;
        }

        private List<IMenu> iEnumerator;

        public IEnumerable createIEnumerator()
        {
            iEnumerator = new List<IMenu>()
            {
                new MenuItem()
                {
                    code = "1",
                    name = "系统配置",
                    sequence = 99
                },
                 new MenuItem()
                 {
                     code = "2",
                     name = "首页",
                     sequence = 1
                 },
            };

            return iEnumerator.AsEnumerable();
        }

    }
}