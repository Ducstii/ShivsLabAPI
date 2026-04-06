using LabApi.Features.Wrappers;
using UnityEngine;

namespace ShivsLabAPI.ShivManagement;

public static class Wallcaster
{
    public static bool IsNearWall(Player player, float maxDistance = 1.5f)
    {
        Transform camera = player.Camera;
        RaycastHit[] hits = Physics.RaycastAll(camera.position, camera.forward, maxDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponentInParent<ReferenceHub>() != null)
                continue;

            return true;
        }

        return false;
    }
}