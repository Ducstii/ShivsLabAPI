using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;
using ShivsLabAPI.ShivManagement;
using UnityEngine;

namespace ShivsLabAPI.Events
{
    public static class EventHandler
    {
        public static void OnPickedEvent(PlayerPickingUpItemEventArgs ev)
        {
            if (!ShivPlugin.ShivsEnabled && ShivManager.IsShiv(ev.Pickup.Serial))
            {
                ev.IsAllowed = false;
                ev.Player.SendHint("The Shiv system is disabled. You cannot have one. Fuck you");
            }
        }

        public static void OnInteractedEvent(PlayerUsingItemEventArgs ev)
        {
            if (!ShivManager.IsShiv(ev.UsableItem.Serial))
                return;

            ev.IsAllowed = false;

            if (!ShivPlugin.ShivsEnabled)
                return;

            Transform camera = ev.Player.Camera;
            Player target = null;

            RaycastHit[] hits = Physics.SphereCastAll(camera.position, 0.3f, camera.forward, ShivPlugin.Instance.Config.Range);
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
            foreach (RaycastHit hit in hits)
            {
                ReferenceHub hub = hit.collider.GetComponentInParent<ReferenceHub>();
                if (hub == null) continue;
                Player p = Player.Get(hub);
                if (p != null && p != ev.Player)
                {
                    target = p;
                    break;
                }
            }

            if (ShivManager.IsOnAttackCooldown(ev.Player))
            {
                ev.Player.SendHint("<color=red>Attack on cooldown!</color>", 2f);
                return;
            }

            if (target != null)
            {
                ShivManager.SetAttackCooldown(ev.Player);
                Attack.DealDamage(target);
                ShivManager.RemoveShiv(ev.UsableItem.Serial, true);
                ev.Player.SendHint("You stabbed " + target.DisplayName + "!", 3f);
                target.SendHint("<color=red>You were stabbed by</color> " + ev.Player.DisplayName + "<color=red>!</color>", 3f);
            }
            else
            {
                ev.Player.SendHint("<color=red>Missed!</color>", 2f);
            }
        }

        public static void OnRoundEnded(RoundEndedEventArgs ev)
        {
            ShivManager.RemoveShivsFromDictionary();
        }

        public static void OnChangedItem(PlayerChangedItemEventArgs ev)
        {
            if (ev.NewItem != null && !ev.NewItem.IsDestroyed && ShivManager.IsShiv(ev.NewItem.Serial))
            {
                ev.Player.SendHint("<color=yellow>Shiv</color>", 9999f);
            }
            else if (ev.OldItem != null && !ev.OldItem.IsDestroyed && ShivManager.IsShiv(ev.OldItem.Serial))
            {
                ev.Player.SendHint("", 0.1f);
            }
        }
        
    }
}