using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using InventorySystem.Items;
using PluginAPI.Core.Items;

namespace InfernalExtensions.Extensions
{
    public static class ItemExtensions
    {
        public static bool IsAmmo(this ItemType item) => item is ItemType.Ammo9x19 or ItemType.Ammo12gauge or ItemType.Ammo44cal or ItemType.Ammo556x45 or ItemType.Ammo762x39;
        
        public static bool IsWeapon(this ItemType type, bool checkMicro = true) => type switch
        {
            ItemType.GunCrossvec or ItemType.GunCom45 or ItemType.GunLogicer or ItemType.GunRevolver or ItemType.GunShotgun or ItemType.GunAK
                or ItemType.GunCOM15 or ItemType.GunCOM18 or ItemType.GunE11SR or ItemType.GunFSP9
                or ItemType.ParticleDisruptor => true,
            ItemType.MicroHID when checkMicro => true,
            _ => false,
        };
        public static bool IsScp(this ItemType type) => type is ItemType.SCP018 or ItemType.SCP500 or ItemType.SCP268 or ItemType.SCP207 or ItemType.SCP244a or ItemType.SCP244b or ItemType.SCP2176 or ItemType.SCP1853;
        public static bool IsThrowable(this ItemType type) => type is ItemType.SCP018 or ItemType.GrenadeHE or ItemType.GrenadeFlash or ItemType.SCP2176;

        public static bool IsMedical(this ItemType type) => type is ItemType.Painkillers or ItemType.Medkit or ItemType.SCP500 or ItemType.Adrenaline;

        public static bool IsUtility(this ItemType type) => type is ItemType.Flashlight or ItemType.Radio;

        public static bool IsArmor(this ItemType type) => type is ItemType.ArmorCombat or ItemType.ArmorHeavy or ItemType.ArmorLight;

        public static bool IsKeycard(this ItemType type) => type is ItemType.KeycardJanitor or ItemType.KeycardScientist or
            ItemType.KeycardResearchCoordinator or ItemType.KeycardZoneManager or ItemType.KeycardGuard or ItemType.KeycardNTFOfficer or
            ItemType.KeycardContainmentEngineer or ItemType.KeycardNTFLieutenant or ItemType.KeycardNTFCommander or
            ItemType.KeycardFacilityManager or ItemType.KeycardChaosInsurgency or ItemType.KeycardO5;

        
        public static T GetItemBase<T>(this ItemType type)
            where T : ItemBase
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
                return null;

            return itemBase as T;
        }

        public static ItemBase GetItemBase(this ItemType type)
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
                return null;

            return itemBase;
        }

        public static IEnumerable<ItemType> GetItemTypes(this IEnumerable<Item> items)
        {
            Item[] arr = items.ToArray();
            for (int i = 0; i < arr.Length; i++)
                yield return arr[i].Type;
        }
    }
}