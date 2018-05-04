using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SmartEngine.Core
{
    public interface _IWrappedPropertyDescriptor
    {
        object GetWrappedOwner();
        PropertyInfo GetWrappedProperty();
    }
}
