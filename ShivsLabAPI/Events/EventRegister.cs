using LabApi.Events.Handlers;
namespace ShivsLabAPI.Events
{
    public abstract class EventRegister
    {
        public static bool UnregisterEvents()
        {
            try
            {
                PlayerEvents.PickingUpItem -= EventHandler.OnPickedEvent;
                PlayerEvents.UsingItem -= EventHandler.OnInteractedEvent;
                ServerEvents.RoundEnded -= EventHandler.OnRoundEnded;
                PlayerEvents.ChangedItem -= EventHandler.OnChangedItem;
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static bool RegisterEvents()
        {
            try
            {
                PlayerEvents.PickingUpItem += EventHandler.OnPickedEvent;
                PlayerEvents.UsingItem += EventHandler.OnInteractedEvent;
                ServerEvents.RoundEnded += EventHandler.OnRoundEnded;
                PlayerEvents.ChangedItem += EventHandler.OnChangedItem;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}