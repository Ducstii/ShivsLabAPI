using System;
using CommandSystem;
using Mirror;

namespace ShivsLabAPI.Commands
{
    public class ShivToggleCommand
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public class Toggleshivcommand : ICommand
        {

            public string Command => "toggleshiv";
            public string[] Aliases => new []{"tsh"};
            public string Description => "Toggles shivs";

            public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                ShivPlugin.ShivsEnabled = !ShivPlugin.ShivsEnabled;
                response = ShivPlugin.ShivsEnabled ? "Shivs enabled." : "Shivs disabled.";
                return true;
            }
        }
    }
}