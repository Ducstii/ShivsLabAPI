using System;
using CommandSystem;
using LabApi.Features.Wrappers;
using ShivsLabAPI.ShivManagement;

namespace ShivsLabAPI.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CraftShivCommand : ICommand
    {
        public string Command => "giveshiv";
        public string[] Aliases => new[] { "gvshiv" };
        public string Description => "Give a shiv to a player. Usage: shiv <player id>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Usage: shiv <player id>";
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = "Invalid player ID.";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"Player {playerId} not found.";
                return false;
            }

            ShivManager.SpawnShiv(player);
            response = $"Shiv given to {player.DisplayName}.";
            return true;
        }
    }
}