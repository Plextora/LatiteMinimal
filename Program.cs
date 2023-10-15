using System;
using System.Collections.Generic;
using System.Diagnostics;
using LatiteMinimal.Utils;

namespace LatiteMinimal
{
    internal class Program
    {
        private static string _selectedVersion;
        public static Process MinecraftProcess;

        private static void OnUnhandledException(object sender,
            UnhandledExceptionEventArgs e)
        {
            Logging.ErrorLogging(e.ExceptionObject as Exception);
        }

        public static void WriteColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static async void LaunchLatiteClient()
        {
            string dllPath = Downloader.DownloadDll(_selectedVersion);
            
            if (Process.GetProcessesByName("Minecaft.Windows").Length != 0) return;

            Process.Start("minecraft:");

            while (true)
            {
                if (Process.GetProcessesByName("Minecraft.Windows").Length == 0) continue;
                MinecraftProcess = Process.GetProcessesByName("Minecraft.Windows")[0];
                break;
            }

            await Injector.WaitForModules();
            Injector.Inject(dllPath);
        }

        private static async void LaunchCustomDll()
        {
            Console.Clear();
            WriteColor(
                $"Please input the file path of the DLL you want to inject (Example: {Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\DLLs\\Horion.dll)\n",
                ConsoleColor.White);

            Console.Write("> ");
            string filePath = Console.ReadLine();

            if (Process.GetProcessesByName("Minecaft.Windows").Length != 0) return;

            Process.Start("minecraft:");

            while (true)
            {
                if (Process.GetProcessesByName("Minecraft.Windows").Length == 0) continue;
                MinecraftProcess = Process.GetProcessesByName("Minecraft.Windows")[0];
                break;
            }

            await Injector.WaitForModules();
            Injector.Inject(filePath);
        }

        private static void LatiteClient()
        {
            Dictionary<string, string> versionMap = new()
            {
                ["1"] = "1.20.32",
                ["2"] = "1.20.15",
                ["3"] = "1.20.12",
                ["4"] = "1.20.10",
                ["5"] = "1.20.1",
                ["6"] = "1.20.0",
                ["7"] = "1.19.83",
                ["8"] = "1.19.81",
                ["9"] = "1.19.80",
                ["10"] = "1.19.73",
                ["11"] = "1.19.71",
                ["12"] = "1.19.63",
                ["13"] = "1.19.62",
                ["14"] = "1.19.60",
                ["15"] = "1.19.51",
                ["16"] = "1.18.12",
                ["17"] = "1.18",
                ["18"] = "1.17.41",
            };

            Console.Clear();
            WriteColor("What version of Latite Client would you like to use?", ConsoleColor.White);
            foreach (KeyValuePair<string, string> kvp in versionMap)
                Console.WriteLine($"[{kvp.Key}] {kvp.Value}");
            Console.WriteLine("[19] Exit");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ResetColor();

                string readline = Console.ReadLine();

                /*
                 * We use TryGetValue to retrieve the corresponding version string based on user input.
                 * If the number inputted (readline) is found in the dictionary, it assigns the version string to _selectedVersion.
                 * If readline (user input) is null, automatically use "1" (version 1.20.15)
                 * Otherwise, it assigns null.
                 */
                _selectedVersion = versionMap.TryGetValue(readline ?? "1", out string selectedVersion) ? selectedVersion : null;

                if (_selectedVersion == null)
                {
                    if (readline == "18")
                        Environment.Exit(0);
                    else
                        WriteColor(
                            "Invalid option! (Example option selection: Enter 18 for the \"[18] Exit\" option)",
                            ConsoleColor.Red);
                    continue;
                }


                break;
            }

            LaunchLatiteClient();
            Console.ReadLine();
        }

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (!Environment.Is64BitOperatingSystem)
            {
                WriteColor(
                    "It looks like you're running a 32 bit OS/Computer. Sadly, you cannot use Latite Client with a 32 bit OS/Computer.\nPlease do not report this as a bug, make a ticket, or ask how to switch to 64 bit in the Discord, you cannot use Latite Client AT ALL!!!",
                    ConsoleColor.Red);
                Console.ReadLine();
                Environment.Exit(1);
            }

            Console.Title = "Latite Minimal";
            
            Console.Clear();
            WriteColor("Do you want to use Latite Client or a custom DLL?", ConsoleColor.White);
            Console.WriteLine("[1] Latite Client");
            Console.WriteLine("[2] Custom DLL");
            Console.WriteLine("[3] Exit\n");
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        LatiteClient();
                        break;
                    case "2":
                        LaunchCustomDll();
                        Console.ReadLine();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        WriteColor(
                            "Invalid option! (Example option selection: Enter 1 for the \"[1] Latite Client\" option)",
                            ConsoleColor.Red);
                        continue;
                }

                break;
            }
        }
    }
}
