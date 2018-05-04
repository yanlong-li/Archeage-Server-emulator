using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Actors;
using SagaBNS.Common.Inventory;
using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T> : DefaultClient<CharacterPacketOpcode>
    {
        ConcurrentDictionary<long, Common.Item.Item> createItems = new ConcurrentDictionary<long, Common.Item.Item>();
        public void OnItemCreateResult(SM_ITEM_CREATE_RESULT p)
        {
            long session = p.SessionID;
            if (createItems.ContainsKey(session))
            {
                createItems[session].ID = p.ItemID;
                Common.Item.Item removed;
                createItems.TryRemove(session, out removed);
            }
        }

        public void CreateItem(Common.Item.Item item)
        {
            long session = Global.PacketSession;
            createItems[session] = item;

            CM_ITEM_CREATE p = new CM_ITEM_CREATE();
            p.SessionID = session;
            p.Item = item;

            this.Network.SendPacket(p);
        }

        public void SaveInventory(Inventory.Inventory inv)
        {
            foreach (Common.Item.Item i in inv.Container[Item.Containers.Inventory])
            {
                if (i != null)
                    SaveItem(i);
            }
            foreach (Common.Item.Item i in inv.Container[Item.Containers.Equipment])
            {
                if (i != null)
                    SaveItem(i);
            }
            foreach (Common.Item.Item i in inv.Container[Item.Containers.Warehouse])
            {
                if (i != null)
                    SaveItem(i);
            }
            SaveItem(inv.SoldItems);
        }

        public void SaveItem(Common.Item.Item item)
        {
            CM_ITEM_SAVE p = new CM_ITEM_SAVE();
            p.Item = item;

            this.Network.SendPacket(p);
        }

        public void SaveItem(List<Common.Item.Item> items)
        {
            CM_ITEM_LIST_SAVE p = new CM_ITEM_LIST_SAVE();
            p.Items = items;
            this.Network.SendPacket(p);
        }

        public void DeleteItem(List<Common.Item.Item> items)
        {
            var ids = from item in items
                      select item.ID;
            CM_ITEM_DELETE p = new CM_ITEM_DELETE();
            p.ItemIDs = ids.ToList();
            this.Network.SendPacket(p);
        }

        public void GetInventory(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_ITEM_INVENTORY_GET p = new CM_ITEM_INVENTORY_GET();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void OnItemInventoryItem(SM_ITEM_INVENTORY_ITEM p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryGetValue(session, out client))
            {
                OnGotInventoryItem(client, p.Item, p.End);
                if (p.End)
                    packetSessions.TryRemove(session, out client);
            }
        }

        protected abstract void OnGotInventoryItem(T client, Item.Item item, bool end);
        
    }
}
