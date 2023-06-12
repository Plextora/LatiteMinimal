using System;
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
            var dllPath = Downloader.DownloadDll(_selectedVersion);
            
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
            var filePath = Console.ReadLine();

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
            Console.Clear();
            WriteColor("What version of Latite Client would you like to use?", ConsoleColor.White);
            Console.WriteLine("[1] 1.20.0");
            Console.WriteLine("[2] 1.19.83");
            Console.WriteLine("[3] 1.19.81");
            Console.WriteLine("[4] 1.19.80");
            Console.WriteLine("[5] 1.19.73");
            Console.WriteLine("[6] 1.19.71");
            Console.WriteLine("[7] 1.19.63");
            Console.WriteLine("[8] 1.19.62");
            Console.WriteLine("[9] 1.19.60");
            Console.WriteLine("[10] 1.19.51");
            Console.WriteLine("[11] 1.18.12");
            Console.WriteLine("[12] 1.18");
            Console.WriteLine("[13] 1.17.41");
            Console.WriteLine("[14] Exit\n");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        _selectedVersion = "1.20.0";
                        break;
                    case "2":
                        _selectedVersion = "1.19.83";
                        break;
                    case "3":
                        _selectedVersion = "1.19.81";
                        break;
                    case "4":
                        _selectedVersion = "1.19.80";
                        break;
                    case "5":
                        _selectedVersion = "1.19.73";
                        break;
                    case "6":
                        _selectedVersion = "1.19.71";
                        break;
                    case "7":
                        _selectedVersion = "1.19.63";
                        break;
                    case "8":
                        _selectedVersion = "1.19.62";
                        break;
                    case "9":
                        _selectedVersion = "1.19.60";
                        break;
                    case "10":
                        _selectedVersion = "1.19.51";
                        break;
                    case "11":
                        _selectedVersion = "1.18.12";
                        break;
                    case "12":
                        _selectedVersion = "1.18";
                        break;
                    case "13":
                        _selectedVersion = "1.17.41";
                        break;
                    case "14":
                        Environment.Exit(0);
                        break;
                    default:
                        WriteColor(
                            "Invalid option! (Example option selection: Enter 14 for the \"[14] Exit\" option)",
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
                    "It looks like you're running a 32 bit OS/Computer. Sadly, you cannot use Latite Client with a 32 bit OS/Computer.\nPlease do not report this as a bug, make a ticket, or ask how to switch to 64 bit in the Discord, you cannot use Latite Client AT ALl!!!",
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