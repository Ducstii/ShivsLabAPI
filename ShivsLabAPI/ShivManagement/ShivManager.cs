using System.Collections.Generic;
using MEC;
using LabApi.Features.Wrappers;

namespace ShivsLabAPI.ShivManagement
{
    public class ShivManager
    {
        private static readonly HashSet<ushort> _shivs = new();
        private static readonly Dictionary<string, CoroutineHandle> _craftCooldowns = new();
        private static readonly Dictionary<string, CoroutineHandle> _attackCooldowns = new();

        public static bool IsOnCraftCooldown(Player player) =>
            _craftCooldowns.TryGetValue(player.UserId, out var handle) && Timing.IsRunning(handle);

        public static void SetCraftCooldown(Player player)
        {
            string userId = player.UserId;
            if (_craftCooldowns.TryGetValue(userId, out var existing) && Timing.IsRunning(existing))
                return;
            _craftCooldowns[userId] = Timing.RunCoroutine(ExpireCooldown(_craftCooldowns, userId, ShivPlugin.Instance.Config.CraftCooldown));
        }

        public static bool IsOnAttackCooldown(Player player) =>
            _attackCooldowns.TryGetValue(player.UserId, out var handle) && Timing.IsRunning(handle);

        public static void SetAttackCooldown(Player player)
        {
            string userId = player.UserId;
            if (_attackCooldowns.TryGetValue(userId, out var existing) && Timing.IsRunning(existing))
                return;
            _attackCooldowns[userId] = Timing.RunCoroutine(ExpireCooldown(_attackCooldowns, userId, ShivPlugin.Instance.Config.AttackCooldown));
        }

        private static IEnumerator<float> ExpireCooldown(Dictionary<string, CoroutineHandle> dict, string userId, float delay)
        {
            yield return Timing.WaitForSeconds(delay);
            dict.Remove(userId);
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
            foreach (var handle in _craftCooldowns.Values) Timing.KillCoroutines(handle);
            foreach (var handle in _attackCooldowns.Values) Timing.KillCoroutines(handle);
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