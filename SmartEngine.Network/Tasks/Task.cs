using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Core;

namespace SmartEngine.Network.Tasks
{
    /// <summary>
    /// 任务，可用于定时器，或者某些需要重复执行的服务器系统任务
    /// </summary>
    public abstract class Task
    {
        /// <summary>
        /// 启动延迟(ms)
        /// </summary>
        protected int dueTime;
        /// <summary>
        /// 运行周期(ms)
        /// </summary>
        protected int period;

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime NextUpdateTime = DateTime.Now;

        /// <summary>
        /// 如果Callback执行时间较长，请将此属性设置为true
        /// </summary>
        public bool IsSlowTask { get; set; }
        bool activate = false;
        internal bool executing;
        string name;
        internal DateTime TaskBeginTime;
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// 创建一个新任务实例
        /// </summary>
        /// <param name="dueTime">启动延迟</param>
        /// <param name="period">运行周期</param>
        /// <param name="name">名称</param>
        public Task(int dueTime, int period, string name)
        {
            if (period <= 0)
                Logger.ShowDebug("period <= 0");
            this.dueTime = dueTime;
            this.period = period;
            this.name = name;
        }

        /// <summary>
        /// 任务每次运行时调用的回调函数
        /// </summary>
        public abstract void CallBack();

        protected virtual void OnActivate()
        {
        }

        /// <summary>
        /// 任务是否处于活动状态
        /// </summary>
        public bool Activated
        {
            get
            {
                return activate;
            }
        }

        /// <summary>
        /// 启动延迟(ms)
        /// </summary>
        public int DueTime { get { return dueTime; } set { dueTime = value; } }

        /// <summary>
        /// 运行周期(ms)
        /// </summary>        
        public int Period { get { return period; } set { period = value; } }

        /// <summary>
        /// 激活任务
        /// </summary>
        public void Activate()
        {
            NextUpdateTime = DateTime.Now.AddMilliseconds(dueTime);
            TaskManager.Instance.RegisterTask(this);
            activate = true;
            OnActivate();
        }

        /// <summary>
        /// 将任务处于非激活状态
        /// </summary>
        public void Deactivate()
        {
            TaskManager.Instance.RemoveTask(this);
            if (activate)
            {
                activate = false;
                OnDeactivate();
            }
        }

        protected virtual void OnDeactivate()
        {

        }

        public override string ToString()
        {
            if (name != null)
                return name;
            else
                return base.ToString();
        }

    }
}
