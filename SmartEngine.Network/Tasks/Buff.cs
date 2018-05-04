using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using SmartEngine.Core;
using SmartEngine.Network.Map;

namespace SmartEngine.Network.Tasks
{
    public class Buff: Addition
    {
        DateTime endTime;
        int lifeTime;
        public delegate void StartEventHandler(Actor actor, Buff skill);
        public delegate void EndEventHandler(Actor actor, Buff skill,bool cancel);
        public delegate void UpdateEventHandler(Actor actor, Buff skill);
        
        ConcurrentDictionary<string, int> Variable = new ConcurrentDictionary<string, int>();
        /// <summary>
        /// 附加状态启动事件
        /// </summary>
        public event StartEventHandler OnAdditionStart;
        /// <summary>
        /// 附加状态中止事件
        /// </summary>
        public event EndEventHandler OnAdditionEnd;
        /// <summary>
        /// 附加状态周期更新事件
        /// </summary>
        public event UpdateEventHandler OnUpdate;
        
        /// <summary>
        /// 初始化一个新的增减益效果实例
        /// </summary>
        /// <param name="actor">Actor</param>
        /// <param name="name">名称</param>
        /// <param name="lifetime">生命周期</param>
        public Buff(Actor actor, string name, int lifetime)
            : this(actor, name, lifetime, lifetime)
        {

        }

        /// <summary>
        /// 初始化一个新的增减益效果实例
        /// </summary>
        /// <param name="actor">Actor</param>
        /// <param name="name">名称</param>
        /// <param name="lifetime">生命周期</param>
        /// <param name="period">更新周期</param>
        public Buff(Actor actor, string name, int lifetime, int period)
            : base(period, period, name)
        {
            this.AttachedActor = actor;
            this.lifeTime = lifetime;
        }

        /// <summary>
        /// 取得一个附加Integer参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>若存在则返回该参数值，不存在则返回0</returns>
        public int this[string name]
        {
            get
            {
                int value;
                if (Variable.TryGetValue(name, out value))
                    return value;
                else
                    return 0;
            }
            set
            {
                Variable[name] = value;
            }
        }

        /// <summary>
        /// 剩余生命周期
        /// </summary>
        public override int RestLifeTime
        {
            get
            {
                return (int)(this.endTime - DateTime.Now).TotalMilliseconds;
            }
        }

        /// <summary>
        /// 总生命周期
        /// </summary>
        public override int TotalLifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                int delta = value - lifeTime;
                lifeTime = value;
                this.endTime = endTime.AddMilliseconds(delta);
                NextUpdateTime = this.endTime;
            }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                this.endTime = value;
                NextUpdateTime = endTime;
            }
        }

        protected override void AdditionEnd()
        {
            try
            {
                if (OnAdditionEnd != null)
                    OnAdditionEnd.Invoke(this.AttachedActor, this, DateTime.Now < endTime);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        protected override void AdditionStart()
        {
            if (period != int.MaxValue)
            {
                this.endTime = DateTime.Now.AddMilliseconds(lifeTime);
                dueTime = 0;
            }
            if (OnAdditionStart != null)
                OnAdditionStart.Invoke(this.AttachedActor, this);            
        }

        protected override void OnTimerUpdate()
        {
            if (OnUpdate != null)
                OnUpdate.Invoke(this.AttachedActor, this);
        }
    }
}
