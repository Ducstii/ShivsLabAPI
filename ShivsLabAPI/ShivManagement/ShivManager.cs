using System.Collections.Generic;
using MEC;
using LabApi.Features.Wrappers;

namespace ShivsLabAPI.ShivManagement
{
    public class ShivManager
    {
        private static readonly HashSet<ushort> _shivs = new();
        private static readonly HashSet<string> _craftCooldowns = new();
        private static readonly HashSet<string> _attackCooldowns = new();

        public static bool IsOnCraftCooldown(Player player) => _craftCooldowns.Contains(player.UserId);

        public static void SetCraftCooldown(Player player)
        {
            _craftCooldowns.Add(player.UserId);
            Timing.CallDelayed(ShivPlugin.Instance.Config.CraftCooldown, () => _craftCooldowns.Remove(player.UserId));
        }

        public static bool IsOnAttackCooldown(Player player) => _attackCooldowns.Contains(player.UserId);

        public static void SetAttackCooldown(Player player)
        {
            _attackCooldowns.Add(player.UserId);
            Timing.CallDelayed(ShivPlugin.Instance.Config.AttackCooldown, () => _attackCooldowns.Remove(player.UserId));
        }

        public static Item SpawnShiv(Player player)
        {
            Item item = player.AddItem(ItemType.Adrenaline);
            _shivs.Add(item.Serial);
            return item;
        }

        public static bool IsShiv(ushort serial) => _shivs.Contains(serial);

        public static void RemoveShivsFromDictionary()
        {
            _shivs.Clear();
            _craftCooldowns.Clear();
            _attackCooldowns.Clear();
        }

        public static void RemoveShiv(ushort serial, bool destroyitem = false)
        {
            if (destroyitem)
            {
                Item item = Item.Get(serial);
                item?.CurrentOwner?.RemoveItem(item);
            }
            _shivs.Remove(serial);
        }
    }
}