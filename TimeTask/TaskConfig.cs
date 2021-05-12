using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTask
{
    /// <summary>
    /// 任务配置
    /// </summary>
    internal class TaskConfig
    {
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public MyTaskType TaskType { get; set; }
        /// <summary>
        /// 时间间隔，循环任务时必须设置
        /// </summary>
        public TimeSpan TaskInterval { get; set; }
        /// <summary>
        /// 循环周期
        /// </summary>
        public MyCirclePeriod CirclePeriod { get; set; }
        /// <summary>
        /// 执行一次的指定执行时间(日：只取时间，周：只取时间，月：取日和时间)
        /// </summary>
        public DateTime ExeTime { get; set; }
        /// <summary>
        /// 周周期的时候必须设置
        /// </summary>
        public DayOfWeek WeekDay { get; set; }

    }
    /// <summary>
    /// 任务类型
    /// </summary>
    internal enum MyTaskType
    {
        /// <summary>
        /// 定时
        /// </summary>
        Timing,
        /// <summary>
        /// 循环
        /// </summary>
        Circle,
        /// <summary>
        /// 执行一次，指定执行时间
        /// </summary>
        Once
    }
    /// <summary>
    /// 周期
    /// </summary>
    internal enum MyCirclePeriod
    {
        /// <summary>
        /// 天
        /// </summary>
        Day,
        /// <summary>
        /// 周
        /// </summary>
        Week,
        /// <summary>
        /// 月
        /// </summary>
        Month
    }
}
