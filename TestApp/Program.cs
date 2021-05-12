using System;
using TimeTask;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //启动定时任务服务
            TaskManager.Start();
        }
    }
}
