using System.Collections.Generic;
using LabApi.Features.Wrappers;

namespace ShivsLabAPI.ShivManagement
{
    public class ShivManager
    {
        private static readonly Dictionary<ushort, ShivData> _shivs = new();

        public static Item SpawnShiv(Player player)
        {
            Item item = player.AddItem(ItemType.Adrenaline);
            _shivs[item.Serial] = new ShivData { Owner = player.UserId };
            return item;
        }
        public static bool IsShiv(ushort serial) => _shivs.ContainsKey(serial);
        public static bool TryGetShiv(ushort serial, out ShivData data) => _shivs.TryGetValue(serial, out data);

        public static void RemoveShivsFromDictionary()
        {
            _shivs.Clear();
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

    public class ShivData
    {
        public string Owner { get; set; }
    }


}