using System;
using System.Net;
using LabApi.Features.Console;

namespace ShivsLabAPI;

public static class GitUpdater
{
    private const string Repo = "Ducstii/ShivsLabAPI";

    public static void CheckForUpdates()
    {
        try
        {
            using WebClient client = new WebClient();
            client.Headers.Add("User-Agent", "ShivsLabAPI");

            string json = client.DownloadString($"https://api.github.com/repos/{Repo}/releases/latest");

            string tag = Extract(json, "tag_name");
            if (tag == null)
            {
                Logger.Warn("ShivsLabAPI: Could not parse latest release tag.");
                return;
            }

            Version latest = new Version(tag.TrimStart('v'));
            Version current = ShivPlugin.Instance.Version;

            if (latest > current)
                Logger.Warn($"ShivsLabAPI is outdated! Current: v{current} | Latest: v{latest}");
            else
                Logger.Info("ShivsLabAPI is up to date.");
        }
        catch (Exception e)
        {
            Logger.Warn($"ShivsLabAPI update check failed: {e.Message}");
        }
    }

    private static string Extract(string json, string key)
    {
        string search = $"\"{key}\":\"";
        int start = json.IndexOf(search, StringComparison.Ordinal);
        if (start < 0) return null;
        start += search.Length;
        int end = json.IndexOf('"', start);
        return end < 0 ? null : json.Substring(start, end - start);
    }
}