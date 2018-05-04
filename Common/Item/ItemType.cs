using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Item
{
    public enum ItemType
    {
        Usable_Item,
        Trash,
        Costume = 4,
        Bagua,
        Weapon_Gem,
        Material,
        CostumeAccessory = 8,
        Potion = 9,
        Food = 10,
        Weapon_SW = 12,//Sword
        Weapon_GT = 13,//Gauntlet
        Weapon_ST = 14,//Staff
        Weapon_AB = 15,//Aura Bangle
        Weapon_DG = 16,//Dagger
        Weapon_TA = 27,//Two-handed Axe
        Eyeware = 18,
        Hat = 19,
        Acc_Ring = 20,
        Acc_Neckless,
        Acc_Ear,
        RepairKit,
        Teleport_Ticket = 25,
        QuestItem = 28

    }

    public enum Containers : byte
    {
        Equipment = 1,
        Inventory,
        Warehouse,
    }
}
