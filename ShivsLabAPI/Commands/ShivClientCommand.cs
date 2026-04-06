using System;
using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using RemoteAdmin;
using ShivsLabAPI.ShivManagement;
namespace ShivsLabAPI.Commands
{
    public class ShivClientCommand
    {
        [CommandHandler(typeof(ClientCommandHandler))]
        public class CraftShivCommand : ICommand
        {
            public string Command => "shiv";
            public string[] Aliases  => new []{"sh"};
            public string Description => "Craft Shiv";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (!ShivPlugin.ShivsEnabled)
                {
                    response = "Shivs are disabled.";
                    return false;
                }

                if (!(sender is PlayerCommandSender playerSender))
                {
                    response = "Only players can use this.";
                    return false;
                }

                Player player = Player.Get(playerSender.ReferenceHub);

                if (UnityEngine.Random.Range(1, ShivPlugin.Instance.Config.SuccessChance + 1) == 1)
                {
                    if (!player.IsInventoryFull)
                    {
                        ShivManager.SpawnShiv(player);
                        response = "Success! You crafted a shiv!"; 
                    }
                    else
                    {
                        response = "Your inventory is full.";
                    }
                }
                else
                {
                    player.ReferenceHub.playerStats.DealDamage(new CustomReasonDamageHandler("Bloodbath", ShivPlugin.Instance.Config.DamageAmount));
                    response = "You made yourself bleed from scratching the wall.";
                }

                return true;
            }
        }
    }
}