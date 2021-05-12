using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTask
{
    /// <summary>
    /// 定时任务管理
    /// </summary>
    public class BllTaskManager
    {
        private static SortedDictionary<string, IBllTask> __processConfig = new SortedDictionary<string, IBllTask>();
        /// <summary>
        /// 初始化
        /// </summary>
        static BllTaskManager()
        {
            try
            {
                InitConfig();
                Console.WriteLine("TaskService Init Success.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskService Init Fail." + ex.Message);
            }
        }
        /// <summary>
        /// 初始化配置
        /// </summary>

        private static void InitConfig()
        {
            __processConfig.Add(Robot.TASK_NAME, new Robot());
  
        }
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            try
            {
                foreach (var k in __processConfig.Keys)
                    __processConfig[k].Start();
                Console.WriteLine("TaskService Start Success.");
            }
            catch (Exception ex)
            { Console.WriteLine("TaskService Start Fail." + ex.Message); }

        }
        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            try
            {
                foreach (var k in __processConfig.Keys)
                    __processConfig[k].Stop();
                Console.WriteLine("TaskService Stop Success.");
            }
            catch (Exception ex)
            {
                try
                {
                    Console.WriteLine("TaskService Stop Fail." + ex.Message);
                }
                catch { }
            }
        }
    }
}
