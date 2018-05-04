using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    public class LogicSystemBrowsableAttribute : Attribute
    {
        private bool browsable;

        public LogicSystemBrowsableAttribute(bool browsable)
        {
            this.browsable = browsable;
        }

        public bool Browsable
        {
            get
            {
                return this.browsable;
            }
        }
    }


}
