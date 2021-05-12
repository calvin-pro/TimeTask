using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TimeTask
{
    /// <summary>
    /// 基类
    /// </summary>
    internal abstract class BllTaskBase : IBllTask
    {
        /// <summary>
        /// 配置
        /// </summary>
        public TaskConfig Config { get; protected set; }
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer __timer = null;
        /// <summary>
        /// 是否在处理中
        /// </summary>
        private bool __IsDoing = false;
        /// <summary>
        /// 最后一次处理时间
        /// </summary>
        protected DateTime __LastTime = DateTime.MinValue;
        /// <summary>
        /// 初始化
        /// </summary>
        public BllTaskBase()
        { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public BllTaskBase(TaskConfig config)
        {
            SetConfig(config);
        }
        /// <summary>
        /// 设置并检查配置
        /// </summary>
        /// <param name="config"></param>
        protected void SetConfig(TaskConfig config)
        {
            if (config == null) throw new Exception("任务配置不能空");
            if (string.IsNullOrWhiteSpace(config.TaskName)) throw new Exception("任务名不能为空");
            if(config.TaskType== MyTaskType.Circle)
            {
                if (config.TaskInterval.TotalSeconds == 0) throw new Exception("请设置循环时间间隔");
            }else if(config.TaskType== MyTaskType.Once)
            {
                if (config.ExeTime == DateTime.MinValue) throw new Exception("请设置执行时间");
            }else
            {
                if (config.ExeTime == DateTime.MinValue) throw new Exception("请设置执行时间");
            }
            this.Config = config;
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            try {
                if (__timer == null)
                {
                    if (Config.TaskType == MyTaskType.Circle)
                        __timer = new Timer(new TimerCallback(TimerInterval), this, Config.TaskInterval, this.Config.TaskInterval);
                    else
                        __timer = new Timer(new TimerCallback(TimingInterval),this, 1000, 1000);//每秒判断一次
                }
            }catch(Exception ex)
            {
                Console.WriteLine(string.Format("定时任务[{0}]启动失败，{1}",Config==null?"未知": Config.TaskName, ex.Message));
            }
        }
        /// <summary>
        /// 循环任务
        /// </summary>
        /// <param name="sender"></param>
        private void TimerInterval(object sender)
        {
            try
            {
                if (__IsDoing) return;
                __IsDoing = true;
                __LastTime = DateTime.Now;
                DoProcess();
                Console.WriteLine(string.Format("循环任务[{0}]处理成功.", Config.TaskName));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(string.Format("循环任务[{0}]处理失败，{1}", Config.TaskName, ex.Message));
            }
            finally { __IsDoing = false; }
        }
        /// <summary>
        /// 其它任务
        /// </summary>
        /// <param name="sender"></param>
        private void TimingInterval(object sender)
        {
            DateTime ldtNow = DateTime.Now;
            try
            {
                if (__IsDoing) return;
                __IsDoing = true;
                
                if(Config.TaskType == MyTaskType.Once)
                {
                    if(__LastTime==DateTime.MinValue)
                    {
                        var ts = ldtNow.Subtract(this.Config.ExeTime);
                        if (Math.Abs(ts.TotalSeconds) < 5)
                        {
                            __LastTime = ldtNow;
                            DoProcess();
                            Console.WriteLine(string.Format("定时任务[{0}]处理成功.", Config.TaskName));
                        }
                    }
                }
                else
                {
                    if(Config.CirclePeriod == MyCirclePeriod.Day)
                    {
                        if (__LastTime.Year == ldtNow.Year && __LastTime.Month == ldtNow.Month && __LastTime.Day == ldtNow.Day) return;//当天已有执行过
                        var ts = ldtNow.TimeOfDay.Subtract(this.Config.ExeTime.TimeOfDay);
                        if (Math.Abs(ts.TotalSeconds) < 5)
                        {
                            __LastTime = ldtNow;
                            DoProcess();
                            Console.WriteLine(string.Format("定时任务[{0}]处理成功.", Config.TaskName));
                        }
                    }
                    else if(Config.CirclePeriod == MyCirclePeriod.Week)
                    {
                        if (ldtNow.DayOfWeek != this.Config.WeekDay) return;
                        if (__LastTime.Year == ldtNow.Year && __LastTime.Month == ldtNow.Month && __LastTime.Day == ldtNow.Day) return;//当天已有执行过
                        var ts = ldtNow.TimeOfDay.Subtract(this.Config.ExeTime.TimeOfDay);
                        if (Math.Abs(ts.TotalSeconds) < 5)
                        {
                            __LastTime = ldtNow;
                            DoProcess();
                            Console.WriteLine(string.Format("定时任务[{0}]处理成功.", Config.TaskName));
                        }
                    }
                    else if(Config.CirclePeriod == MyCirclePeriod.Month)
                    {
                        if (__LastTime.Year == ldtNow.Year && __LastTime.Month == ldtNow.Month ) return;//当月已有执行过
                        if (__LastTime.Year == ldtNow.Year && __LastTime.Month == ldtNow.Month&&__LastTime.Day == ldtNow.Day) return; //当天有执行过
                        if (ldtNow.Day != this.Config.ExeTime.Day) return;//不是执行的那一天
                        var ts = ldtNow.TimeOfDay.Subtract(this.Config.ExeTime.TimeOfDay);
                        if (Math.Abs(ts.TotalSeconds) < 5)
                        {
                            __LastTime = ldtNow;
                            DoProcess();
                            Console.WriteLine(string.Format("定时任务[{0}]处理成功.", Config.TaskName));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(string.Format("定时任务[{0}]处理失败，{1}", Config.TaskName, ex.Message));
            }
            finally { __IsDoing = false; }
        }
        /// <summary>
        /// 处理程序
        /// </summary>
        protected abstract void DoProcess();
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (__timer != null) __timer.Dispose();
        }
    }
}
