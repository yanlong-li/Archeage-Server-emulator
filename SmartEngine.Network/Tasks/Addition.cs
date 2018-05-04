using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network.Map;

namespace SmartEngine.Network.Tasks
{
    /// <summary>
    /// 用于实现某些附加于Actor的，基于定时器的附加状态
    /// </summary>
    public abstract class Addition: Task 
    {
        public Addition(int dueTime,int period, string name)
            :base(dueTime,period,name)
        {
            
        }
        #region Fields
        private Actor m_myActor;

        #endregion

        #region Properties

        /// <summary>
        /// Actor that get attached to this addition
        /// </summary>
        /// 
        public Actor AttachedActor
        {
            get
            {
                return m_myActor;
            }
            set
            {
                m_myActor = value;
            }
        }

        #endregion

        public override string ToString()
        {
            return this.Name;
        }

        #region Virtual methodes
        /// <summary>
        /// 该附加状态的总生命时间
        /// </summary>
        public virtual int TotalLifeTime
        {
            get { return int.MaxValue; }
            set { }
        }

        /// <summary>
        /// 该附加状态剩余生命时间
        /// </summary>
        public virtual int RestLifeTime
        {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// 附加状态开始时需呼叫的方法
        /// </summary>
        protected abstract void AdditionStart();

        /// <summary>
        /// 附加状态开始时需呼叫的方法
        /// </summary>
        protected abstract void AdditionEnd();

        /// <summary>
        /// 定时器每次更新需呼叫的方法
        /// </summary>
        protected virtual void OnTimerUpdate()
        {
        }

        /// <summary>
        /// Method that be called once Timer get stoped
        /// </summary>
        protected virtual void OnTimerEnd()
        {
        }

        #endregion

        public override void CallBack()
        {
            if (this.RestLifeTime > 0)
                this.OnTimerUpdate();
            else
            {
                this.Deactivate();
            }
        }

        protected override void OnActivate()
        {
            AdditionStart();
        }

        protected override void OnDeactivate()
        {
            AdditionEnd();
        }
    }
}
