using System;
using CommandSystem;

namespace ShivsLabAPI.Commands
{
    public class ShivGiveRemoteAdmin
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public abstract class CraftShivCommand : ICommand
        {
            public string Command => "shiv";
            public string[] Aliases => new[] { "sh" };
            public string Description => "Craft Shiv";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                if (sender == null)
                {
                    response = "You are not a player";
                }

                return true;

            }

        }
    }
}       