using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartEngine.Core;

using SmartEngine.Network.Utils;
using SmartEngine.Network;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Item;

namespace SagaBNS.Common.Inventory
{
    public class Inventory
    {
        Dictionary<Containers, FixedCapacityList<Item.Item>> containers = new Dictionary<Containers, FixedCapacityList<Item.Item>>();
        List<Item.Item> soldItems = new List<Item.Item>();
        ActorPC pc;

        public Dictionary<Containers,FixedCapacityList<Item.Item>> Container { get { return containers; } }
        public byte ListSize;
        public List<Item.Item> SoldItems { get { return soldItems; } }

        public Inventory(ActorPC pc)
        {
            this.pc = pc;
            containers[Containers.Inventory] = new FixedCapacityList<Item.Item>(120);
            containers[Containers.Equipment] = new FixedCapacityList<Item.Item>(30);
            containers[Containers.Warehouse] = new FixedCapacityList<Item.Item>(120);
        }

        public OperationResults MoveItem(Containers src, ushort slot, ushort count,Containers dst, ushort targetSlot,out List<Item.Item> affected,out List<Item.Item> deleted)
        {
            affected = new List<Item.Item>();
            deleted = new List<Item.Item>();
            if (slot < ListSize)
            {
                Item.Item ori = containers[src][slot];
                if (ori !=null && ori.Count >= count)
                {
                    if (targetSlot < ListSize)
                    {
                        Item.Item target = containers[dst][targetSlot];

                        if (target != null && ori.ItemID == target.ItemID)
                        {
                            if (target.Count + ori.Count <= target.MaxStackableCount)
                            {
                                target.Count += ori.Count;
                                ori.Count = 0;
                                containers[src][slot] = null;
                                affected.Add(target);
                                deleted.Add(ori);
                            }
                            else
                            {
                                int allowed = target.MaxStackableCount - target.Count;
                                target.Count = (ushort)(target.Count + allowed);
                                ori.Count = (ushort)(ori.Count - allowed);
                                affected.Add(target);
                                affected.Add(ori);
                            }
                        }
                        else
                        {
                            if (target != null)//swap
                            {
                                target.SlotID = slot;
                                ori.SlotID = targetSlot;
                                containers[src][slot] = target;
                                containers[dst][targetSlot] = ori;
                                affected.Add(ori);
                                affected.Add(target);
                            }
                            else//move
                            {
                                ori.SlotID = targetSlot;
                                containers[src][slot] = null;
                                containers[dst][targetSlot] = ori;
                                affected.Add(ori);
                                return OperationResults.NEW_INDEX;
                            }
                        }

                        return OperationResults.OK;
                    }
                    else
                        return OperationResults.FAILED;
                }
                else
                    return OperationResults.FAILED;
            }
            else
                return OperationResults.FAILED;
        }

        public OperationResults AddItem(Containers container, Item.Item item, out List<Item.Item> affected)
        {
            var items = (from i in
                             ((from i in containers[container]
                               where i != null && i.ItemID == item.ItemID && i.Count < i.MaxStackableCount
                               select i).ToArray())
                         where i.SlotID < ListSize
                         select i).ToArray();

            affected = new List<Item.Item>();
            foreach (Item.Item i in items)
            {
                if (item.Count == 0)
                {
                    break;
                }
                int rest = i.MaxStackableCount - i.Count;
                int count = rest < item.Count ? rest : item.Count;
                i.Count += (ushort)count;
                item.Count -= (ushort)count;
                affected.Add(i);
            }
            if (item.Count > 0)
            {
                int freeIndex = FindFreeIndex(container);
                if (freeIndex >= 0)
                {
                    item.SlotID = (byte)freeIndex;
                    containers[container][freeIndex] = item;
                    return OperationResults.NEW_INDEX;
                }
                else
                    return OperationResults.INVENTORY_FULL;
            }
            else
                return OperationResults.NEW_INDEX;
        }

        public List<Item.Item> CheckDuplicateSlot()
        {
            List<Item.Item> res = new List<Item.Item>();
            HashSet<ushort> ids = new HashSet<ushort>();
            foreach (Containers c in Enum.GetValues(typeof(Containers)))
            {
                foreach (Item.Item i in containers[c])
                {
                    if (i == null || i.SlotID == 255)
                        continue;
                    if (!ids.Contains(i.SlotID))
                        ids.Add(i.SlotID);
                    else
                        res.Add(i);
                }
            }
            return res;
        }

        public int FindFreeIndex(Containers container)
        {
            for (int i = 0; i < ListSize; i++)
            {
                if (containers[container][i] == null)
                    return i;
            }
            return -1;
        }

        public int CountItem(uint itemID, Containers container = Containers.Inventory)
        {
            var items = from item in
                            (from item in containers[container]
                             where item != null && item.ItemID == itemID
                             select item)
                        where item.SlotID < ListSize
                        select item;
            int count = 0;
            foreach (Item.Item i in items)
                count += i.Count;
            return count;
        }

        public void RemoveItemSlot(Containers container,ushort slotID, ushort count, out List<Item.Item> updated, out List<Item.Item> removed)
        {
            updated = new List<Item.Item>();
            removed = new List<Item.Item>();
            FixedCapacityList<Item.Item> containers = this.containers[container];
            if (containers[slotID] != null)
            {
                if (containers[slotID].Count > count)
                {
                    containers[slotID].Count -= count;
                    updated.Add(containers[slotID]);
                }
                else
                {
                    containers[slotID].Count = 0;
                    removed.Add(containers[slotID]);
                    /*if (containers[slotID].InventoryEquipSlot != InventoryEquipSlot.None)
                    {
                        equip[containers[slotID].InventoryEquipSlot] = null;
                    } */
                    containers[slotID] = null;                    
                }
            }
        }

        public void RemoveItem(Containers container, uint itemID, ushort count, out List<Item.Item> updated, out List<Item.Item> removed)
        {
            updated = new List<Item.Item>();
            removed = new List<Item.Item>();
            var items = from item in
                            (from item in containers[container]
                             where item != null && item.ItemID == itemID
                             select item)
                        where item.SlotID < ListSize
                        select item;
            foreach (Item.Item i in items)
            {
                if (i.Count >= count)
                {
                    i.Count -= count;
                    if (i.Count > 0)
                        updated.Add(i);
                    else
                    {
                        removed.Add(i);
                        containers[container].Remove(i);
                        /*if (i.InventoryEquipSlot != InventoryEquipSlot.None)
                        {
                            equip[i.InventoryEquipSlot] = null;
                        }*/
                    }
                }
                else
                {
                    count -= i.Count;
                    removed.Add(i);
                    containers[container].Remove(i);
                    /*if (i.InventoryEquipSlot != InventoryEquipSlot.None)
                    {
                        equip[i.InventoryEquipSlot] = null;
                    }*/
                }
            }
        }

        public OperationResults UnequipItem(Item.Item item)
        {
            /*if (item.InventoryEquipSlot != InventoryEquipSlot.None)
            {
                if (equip[item.InventoryEquipSlot] == item)
                {
                    equip[item.InventoryEquipSlot] = null;
                    item.InventoryEquipSlot = InventoryEquipSlot.None;
                    return OperationResults.EQUIPT;

                }
                else
                    return OperationResults.FAILED;
            }
            else*/
                return OperationResults.FAILED;
        }

        public OperationResults EquipItem(Item.Item item, out Item.Item oldItem, InventoryEquipSlot preferSlot = InventoryEquipSlot.None)
        {
            oldItem = null;
            /*if (item.InventoryEquipSlot == InventoryEquipSlot.None)
            {
                InventoryEquipSlot target= InventoryEquipSlot.None;
                target = GetEquipSlot(item);
                if ((target == InventoryEquipSlot.Earring && preferSlot == InventoryEquipSlot.Earring2) || (target == InventoryEquipSlot.Ring && preferSlot == InventoryEquipSlot.Ring2))
                {
                    target = preferSlot;
                }
                if (target != InventoryEquipSlot.None)
                {
                    if (equip[target] == null)
                    {
                        equip[target] = item;
                        item.InventoryEquipSlot = target;
                        return OperationResults.EQUIPT;
                    }
                    else
                    {
                        oldItem = equip[target];
                        oldItem.InventoryEquipSlot = InventoryEquipSlot.None;
                        equip[target] = item;
                        item.InventoryEquipSlot = target;
                        return OperationResults.EQUIPT;
                    }
                }
                else
                    return OperationResults.FAILED;
            }
            else*/
                return OperationResults.FAILED;
        }

        InventoryEquipSlot GetEquipSlot(Item.Item item)
        {
            return item.BaseData.EquipSlot.ToInventoryEquipSlot();
        }
    }
}
