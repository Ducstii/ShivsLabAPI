using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using ShivsLabAPI.Events;
namespace ShivsLabAPI
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "Ducstii";
        public override string Name => "ShivsLabAPI";
        public override string Description => "Add fockin shivs to sl mate ueah you know it";
        public override Version RequiredApiVersion =>  new Version(1, 0, 0);
        public override Version Version => new Version(1, 0, 0);

        public override void Enable()
        {
            if (!EventRegister.RegisterEvents())
            {
                Logger.Error("Failed to register events");
            }
            Logger.Info("ShivsLabAPI started");

        }

        public override void Disable()
        {
            if (!EventRegister.UnregisterEvents())
            {
                Logger.Error("Failed to unregister events");
            }
            Logger.Info("ShivsLabAPI stopped");
            
        }
        
    }

    public class Config
    {
        public bool Enabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}