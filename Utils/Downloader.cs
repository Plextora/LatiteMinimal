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
        Client?.DownloadFile(LatiteDllDownloadUrl, "LatiteClientDLLs.zip");
        WriteColor("Downloaded LatiteClientDLLs.zip!", ConsoleColor.Green);
        ZipFile.ExtractToDirectory("LatiteClientDLLs.zip",
            $"{Directory.GetCurrentDirectory()}\\DLLs");
        WriteColor("Extracted LatiteClientDLLs.zip!", ConsoleColor.Green);
        File.Delete("LatiteClientDLLs.zip");

        var latiteDLLs = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\DLLs");
        foreach (var i in latiteDLLs)
            if (i.Contains(mcVersion))
            {
                WriteColor($"The file {Regex.Match(i, @"[^\\]+$")} will be injected into Minecraft!",
                    ConsoleColor.Yellow);
                return i;
            }

        return "Failed to get DLL";
    }
}