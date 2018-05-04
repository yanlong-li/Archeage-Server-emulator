using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Inventory
{
    public enum OperationResults
    {
        OK,
        FAILED,
        NEW_INDEX,
        STACK_UPDATE,
        ALL_DELETE,
        INVENTORY_FULL,
        EQUIPT,
        USED,
        SOLD,
    }
}
