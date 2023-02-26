#nullable enable
using System;
using System.IO;
using System.Net;

namespace LatiteMinimal.Utils;

public class Downloader
{
    private const string DLL_VERSION_URL =
        "https://raw.githubusercontent.com/Imrglop/Latite-Releases/main/latest_version.txt";
    
    private static readonly WebClient? Client = new WebClient();
    
    private static string? GetLatestDllVersion()
    {
        try
        {
            var latestVersion = Client?.DownloadString(
                DLL_VERSION_URL);
            latestVersion = latestVersion?.Replace("\n", "");
            return latestVersion;
        }
        catch
        {
            Console.WriteLine("Failed to check latest version of dll. Are you connected to the internet?");
            throw new Exception("Cannot get latest DLL!");
        }
    }
}