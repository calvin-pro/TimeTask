using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTask
{
    /// <summary>
    /// 测试
    /// </summary>
    internal class Robot : BllTaskBase
    {
        public const string TASK_NAME = "robot";
        /// <summary>
        /// 实始化配置
        /// </summary>
        public Robot()
        {
            TaskConfig config = new TaskConfig();
            config.TaskName = TASK_NAME;
            config.TaskType = MyTaskType.Timing;  //定时任务
            config.CirclePeriod = MyCirclePeriod.Day;  //每天执行
            config.ExeTime = new DateTime(2000, 1, 1, 17, 0, 0);
            SetConfig(config);
        }
        /// <summary>
        /// 处理方法
        /// </summary>
        protected override void DoProcess()
        {
            try
            {
                Console.WriteLine("new time task");
            }
            catch (Exception ex)
            {
                throw;
            }
          
        }
    }
}
