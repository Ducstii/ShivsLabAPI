using CustomPlayerEffects;
using LabApi.Features.Wrappers;

namespace ShivsLabAPI.ShivManagement;

public class Attack
{
    public static void DealDamage(Player player)
    {
        player.Damage(ShivPlugin.Instance.Config.ShivDamageAmount, "Shiv", "Stabbed By Shiv");
        player.EnableEffect<Slowness>(10, 10f, false);
    }
    
}