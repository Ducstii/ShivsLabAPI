using System.Collections.Generic;
using System.Collections;
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
            string userId = player.UserId;
            _craftCooldowns.Add(userId);
            Timing.RunCoroutine(ExpireCooldown(_craftCooldowns, userId, ShivPlugin.Instance.Config.CraftCooldown));
        }

        public static bool IsOnAttackCooldown(Player player) => _attackCooldowns.Contains(player.UserId);

        public static void SetAttackCooldown(Player player)
        {
            string userId = player.UserId;
            _attackCooldowns.Add(userId);
            Timing.RunCoroutine(ExpireCooldown(_attackCooldowns, userId, ShivPlugin.Instance.Config.AttackCooldown));
        }

        private static IEnumerator<float> ExpireCooldown(HashSet<string> set, string userId, float delay)
        {
            yield return Timing.WaitForSeconds(delay);
            set.Remove(userId);
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