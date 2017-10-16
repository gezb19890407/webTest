using myTest.test.monitorTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace myTest
{
    class Program
    {
        static void Main(string[] args)
        {
            printMonitorTest();
        }

        static void printMonitorTest()
        {
            Thread[] threads = new Thread[10];
            int j = 0;
            for (int i = 0; i < threads.Length; i++)
            {
                //通过循环创建10个线程。
                threads[i] = new Thread(() =>
                 {
                     Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms") + "：" + MonitorDemo.doBusiness(j.ToString()));
                     j++;
                 });
                //为每个线程设置一个名字
                threads[i].Name = "thread" + i;

            }
            //开启创建的十个线程
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
            Console.Read();
        }
    }
}
