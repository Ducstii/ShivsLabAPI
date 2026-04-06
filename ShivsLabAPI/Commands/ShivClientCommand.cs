using System;
using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using RemoteAdmin;

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
                if (!(sender is PlayerCommandSender playerSender))
                {
                    response = "Only players can use this.";
                    return false;
                }

                Player player = Player.Get(playerSender.ReferenceHub);
                if (UnityEngine.Random.Range(1, ShivPlugin.Instance.Config.SuccessChance + 1) == 1)
                {
                    response = "Success! You crafted a shiv!";
                    // TODO: implement a method in shivmanager to give a shiv item that we track.
                }
                else
                {
                    response = "You made yourself bleed from scratching the wall.";
                    if (player != null)
                        player.ReferenceHub.playerStats.DealDamage(new CustomReasonDamageHandler("Bloodbath", 1f));
                }
                return  true;
            }
        }
    }
}