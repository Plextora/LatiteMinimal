using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using static LatiteMinimal.Program;

namespace LatiteMinimal.Utils;

public class Downloader
{
    private const string LatiteDllDownloadUrl =
        "https://drive.google.com/uc?export=download&id=1uN8M0hn_bg3vImidA8VQxKwj7QW2pEFs";

    private static readonly WebClient Client = new();

    public static string DownloadDll(string mcVersion)
    {
        if (!Directory.Exists("DLLs"))
        {
            WriteColor("Downloading Latite Client...", ConsoleColor.Yellow);
            Client?.DownloadFile(LatiteDllDownloadUrl, "LatiteClientDLLs.zip");
            WriteColor("Downloaded Latite Client!", ConsoleColor.Green);
            WriteColor($"Extracting Latite Client to {Directory.GetCurrentDirectory()}\\DLLs ...", ConsoleColor.Yellow);
            ZipFile.ExtractToDirectory("LatiteClientDLLs.zip",
                $"{Directory.GetCurrentDirectory()}\\DLLs");
            WriteColor($"Extracted Latite Client to {Directory.GetCurrentDirectory()}\\DLLs!", ConsoleColor.Green);
            File.Delete("LatiteClientDLLs.zip");

            string[] latiteDLLs = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\DLLs");
            foreach (string i in latiteDLLs)
                if (i.Contains(mcVersion))
                {
                    WriteColor($"The file {Regex.Match(i, @"[^\\]+$")} will be injected into Minecraft!",
                        ConsoleColor.Yellow);
                    return i;
                }
        }
        else if (Directory.Exists("DLLs"))
        {
            WriteColor("Found Latite Client directory!", ConsoleColor.Yellow);
            string[] latiteDLLs = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\DLLs");
            foreach (string i in latiteDLLs)
                if (i.Contains(mcVersion))
                {
                    WriteColor($"The file {Regex.Match(i, @"[^\\]+$")} will be injected into Minecraft!",
                        ConsoleColor.Yellow);
                    return i;
                }
        }

        return "Failed to get DLL";
    }
}