using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogicSystemClassDisplayAttribute : Attribute
    {
        private string displayName;

        public LogicSystemClassDisplayAttribute(string displayName)
        {
            this.displayName = displayName;
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }
    }
}
