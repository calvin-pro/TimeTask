using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTask
{
    /// <summary>
    /// 定时任务接口
    /// </summary>
    internal interface ITask
    {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 停止
        /// </summary>
        void Stop();
    }
}
