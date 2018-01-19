using ArcheAge.ArcheAge.Net.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Structuring
{
    //Event Definitions - Used For Invoking When Any Event Happen.

    public delegate void InvokeCharacterCreated(User.User user);
    public delegate void InvokeCharacterDeleted(User.User user);
    public delegate void InvokeCharacterLoggedIn(ClientConnection con);
}
