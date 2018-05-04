using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    public static class _PropertyGridUtils
    {
        private static bool dropDownCreated;

        public static bool EditValueDropDownControlCreated
        {
            get
            {
                return dropDownCreated;
            }
            set
            {
                dropDownCreated = value;
            }
        }
    }
}
