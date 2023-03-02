using System;
using System.Diagnostics;
using static LatiteMinimal.Program;

namespace LatiteMinimal.Utils;

public class Logging
{
    public static void ErrorLogging(Exception error)
    {
        WriteColor(error.ToString(), ConsoleColor.Red);

        WriteColor(
            "\nAn error has occurred! Please report this error to the developers!\nIf you don't know how to report errors, enter \"y\" into this console to visit the #bugs forum in the Discord (Make sure to read the \"Read this before posting your bug report!\" post!",
            ConsoleColor.Red);

        var option = Console.ReadLine();

        if (option == "y")
        {
            if (Process.GetProcessesByName("Discord").Length > 0)
                Process.Start(new ProcessStartInfo
                {
                    FileName = "discord://-/invite/2ZFsuTsfeX",
                    UseShellExecute = true
                });
            else
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://discord.gg/zcJfXxKTA4",
                    UseShellExecute = true
                });
        }

        Environment.Exit(1);
    }
}