using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using InventorySystem.Items;
using PluginAPI.Core.Items;

namespace InfernalExtensions.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is an ammo.
        /// </summary>
        /// <param name="item">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an ammo or not.</returns>
        public static bool IsAmmo(this ItemType item) => item is ItemType.Ammo9x19 or ItemType.Ammo12gauge or ItemType.Ammo44cal or ItemType.Ammo556x45 or ItemType.Ammo762x39;

        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a weapon.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <param name="checkMicro">Indicates whether the MicroHID item should be taken into account or not.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a weapon or not.</returns>
        public static bool IsWeapon(this ItemType type, bool checkMicro = true) => type switch
        {
            ItemType.GunCrossvec or ItemType.GunCom45 or ItemType.GunLogicer or ItemType.GunRevolver or ItemType.GunShotgun or ItemType.GunAK
                or ItemType.GunCOM15 or ItemType.GunCOM18 or ItemType.GunE11SR or ItemType.GunFSP9
                or ItemType.ParticleDisruptor => true,
            ItemType.MicroHID when checkMicro => true,
            _ => false,
        };
        
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is an SCP.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an SCP or not.</returns>
        public static bool IsScp(this ItemType type) => type is ItemType.SCP018 or ItemType.SCP500 or ItemType.SCP268 or ItemType.SCP207 or ItemType.SCP244a or ItemType.SCP244b or ItemType.SCP2176 or ItemType.SCP1853;
        
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a throwable item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a throwable item or not.</returns>
        public static bool IsThrowable(this ItemType type) => type is ItemType.SCP018 or ItemType.GrenadeHE or ItemType.GrenadeFlash or ItemType.SCP2176;
        
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a medical item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a medical item or not.</returns>
        public static bool IsMedical(this ItemType type) => type is ItemType.Painkillers or ItemType.Medkit or ItemType.SCP500 or ItemType.Adrenaline;
        
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a utility item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an utilty item or not.</returns>
        public static bool IsUtility(this ItemType type) => type is ItemType.Flashlight or ItemType.Radio;
        
        /// <summary>
        /// Check if a <see cref="ItemType"/> is an armor item.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is an armor or not.</returns>
        public static bool IsArmor(this ItemType type) => type is ItemType.ArmorCombat or ItemType.ArmorHeavy or ItemType.ArmorLight;
        
        /// <summary>
        /// Check if an <see cref="ItemType">item</see> is a keycard.
        /// </summary>
        /// <param name="type">The item to be checked.</param>
        /// <returns>Returns whether the <see cref="ItemType"/> is a keycard or not.</returns>
        public static bool IsKeycard(this ItemType type) => type is ItemType.KeycardJanitor or ItemType.KeycardScientist or
            ItemType.KeycardResearchCoordinator or ItemType.KeycardZoneManager or ItemType.KeycardGuard or ItemType.KeycardNTFOfficer or
            ItemType.KeycardContainmentEngineer or ItemType.KeycardNTFLieutenant or ItemType.KeycardNTFCommander or
            ItemType.KeycardFacilityManager or ItemType.KeycardChaosInsurgency or ItemType.KeycardO5;

        
        /// <summary>
        /// Given an <see cref="ItemType"/>, returns the matching <see cref="ItemBase"/>, casted to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/>.</param>
        /// <typeparam name="T">The type to cast the <see cref="ItemBase"/> to.</typeparam>
        /// <returns>The <see cref="ItemBase"/> casted to <typeparamref name="T"/>, or <see langword="null"/> if not found or couldn't be casted.</returns>
        public static T GetItemBase<T>(this ItemType type)
            where T : ItemBase
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
                return null;

            return itemBase as T;
        }

        /// <summary>
        /// Given an <see cref="ItemType"/>, returns the matching <see cref="ItemBase"/>.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/>.</param>
        /// <returns>The <see cref="ItemBase"/>, or <see langword="null"/> if not found.</returns>
        public static ItemBase GetItemBase(this ItemType type)
        {
            if (!InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
                return null;

            return itemBase;
        }
        
        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> of <see cref="Item"/>s into the corresponding <see cref="IEnumerable{T}"/> of <see cref="ItemType"/>s.
        /// </summary>
        /// <param name="items">The items to convert.</param>
        /// <returns>A new <see cref="List{T}"/> of <see cref="ItemType"/>s.</returns>
        public static IEnumerable<ItemType> GetItemTypes(this IEnumerable<Item> items)
        {
            Item[] arr = items.ToArray();
            for (int i = 0; i < arr.Length; i++)
                yield return arr[i].Type;
        }
    }
}