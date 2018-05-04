using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartEngine.Network.Utils;

namespace SagaBNS.Common
{
    public static class Utils
    {
        public static unsafe Guid ToGUID(this uint id)
        {
            Random random = new Random((int)id);
            byte[] buf = new byte[8];
            random.NextBytes(buf);
            return new Guid((int)(id ^ 0x85118708), (short)random.Next(), (short)random.Next(), buf);
        }

        public static unsafe uint ToUInt(this Guid id)
        {
            fixed (byte* ptr = id.ToByteArray())
            {
                return *(uint*)ptr ^ 0x85118708;
            }
        }

        public static byte[] slot2GuidBytes(int i)
        {
            byte[] guid = Conversions.HexStr2Bytes(((uint)i).ToGUID().ToString().Replace("-", ""));

            byte[] temp = new byte[4];
            Array.Copy(guid, 0, temp, 0, 4);
            temp = temp.Reverse().ToArray();
            Array.Copy(temp, 0, guid, 0, 4);
            temp = new byte[2];
            Array.Copy(guid, 4, temp, 0, 2);
            temp = temp.Reverse().ToArray();
            Array.Copy(temp, 0, guid, 4, 2);
            temp = new byte[2];
            Array.Copy(guid, 6, temp, 0, 2);
            temp = temp.Reverse().ToArray();
            Array.Copy(temp, 0, guid, 6, 2);
            return guid;
        }
        
        public static Inventory.InventoryEquipSlot ToInventoryEquipSlot(this ushort slot)
        {
            switch (slot)
            {
                case 1:
                    return Inventory.InventoryEquipSlot.Weapon;
                case 3:
                    return Inventory.InventoryEquipSlot.Costume;
                case 5:
                    return Inventory.InventoryEquipSlot.Earring;
                case 6:
                    return Inventory.InventoryEquipSlot.Eye;
                case 7:
                    return Inventory.InventoryEquipSlot.Hat;
                case 8:
                    return Inventory.InventoryEquipSlot.Ring;
                case 9:
                    return Inventory.InventoryEquipSlot.Necklace;
                case 10:
                    return Inventory.InventoryEquipSlot.BaoPai1;
                case 11:
                    return Inventory.InventoryEquipSlot.BaoPai2;
                case 12:
                    return Inventory.InventoryEquipSlot.BaoPai3;
                case 13:
                    return Inventory.InventoryEquipSlot.BaoPai4;
                case 14:
                    return Inventory.InventoryEquipSlot.BaoPai5;
                case 15:
                    return Inventory.InventoryEquipSlot.BaoPai6;
                case 16:
                    return Inventory.InventoryEquipSlot.BaoPai7;
                case 17:
                    return Inventory.InventoryEquipSlot.BaoPai8;
                case 18:
                    return Inventory.InventoryEquipSlot.Earring2;
                case 19:
                    return Inventory.InventoryEquipSlot.Ring2;
                case 20:
                    return Inventory.InventoryEquipSlot.CostumeAccessory;
                default:
                    return Inventory.InventoryEquipSlot.None;
            }
        }

        public static Inventory.InventoryEquipSlot ToInventoryEquipSlot(this Inventory.EquipSlot slot)
        {
            switch (slot)
            {
                case Inventory.EquipSlot.BaoPai1:
                    return Inventory.InventoryEquipSlot.BaoPai1;
                case Inventory.EquipSlot.BaoPai2:
                    return Inventory.InventoryEquipSlot.BaoPai2;
                case Inventory.EquipSlot.BaoPai3:
                    return Inventory.InventoryEquipSlot.BaoPai3;
                case Inventory.EquipSlot.BaoPai4:
                    return Inventory.InventoryEquipSlot.BaoPai4;
                case Inventory.EquipSlot.BaoPai5:
                    return Inventory.InventoryEquipSlot.BaoPai5;
                case Inventory.EquipSlot.BaoPai6:
                    return Inventory.InventoryEquipSlot.BaoPai6;
                case Inventory.EquipSlot.BaoPai7:
                    return Inventory.InventoryEquipSlot.BaoPai7;
                case Inventory.EquipSlot.BaoPai8:
                    return Inventory.InventoryEquipSlot.BaoPai8;
                case Inventory.EquipSlot.Costume:
                    return Inventory.InventoryEquipSlot.Costume;
                case Inventory.EquipSlot.Ring:
                    return Inventory.InventoryEquipSlot.Ring;
                case Inventory.EquipSlot.Weapon:
                    return Inventory.InventoryEquipSlot.Weapon;
                case Inventory.EquipSlot.Earing:
                    return Inventory.InventoryEquipSlot.Earring;
                case Inventory.EquipSlot.Eyes:
                    return Inventory.InventoryEquipSlot.Eye;
                case Inventory.EquipSlot.Hat:
                    return Inventory.InventoryEquipSlot.Hat;
                case Inventory.EquipSlot.Necklace:
                    return Inventory.InventoryEquipSlot.Necklace;
                case Inventory.EquipSlot.CostumeAccessory:
                    return Inventory.InventoryEquipSlot.CostumeAccessory;
                default:
                    return Inventory.InventoryEquipSlot.None;
            }
        }
    }
}
