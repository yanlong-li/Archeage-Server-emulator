using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogicSystemMethodDisplayAttribute : Attribute
    {
        private string displayText;
        private string formatText;

        public LogicSystemMethodDisplayAttribute(string displayText, string formatText)
        {
            this.displayText = displayText;
            this.formatText = formatText;
        }

        public string DisplayText
        {
            get
            {
                return this.displayText;
            }
        }

        public string FormatText
        {
            get
            {
                return this.formatText;
            }
        }
    }
}
