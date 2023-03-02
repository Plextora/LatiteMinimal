using System;
using System.Diagnostics;
using LatiteMinimal.Utils;

namespace LatiteMinimal
{
    internal class Program
    {
        public static string SelectedVersion;
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
            var dllPath = Downloader.DownloadDll(SelectedVersion);
            
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
            Console.WriteLine("[1] 1.19.63");
            Console.WriteLine("[2] 1.19.62");
            Console.WriteLine("[3] 1.19.60");
            Console.WriteLine("[4] 1.19.51");
            Console.WriteLine("[5] 1.18.12");
            Console.WriteLine("[6] 1.18");
            Console.WriteLine("[7] 1.17.41");
            Console.WriteLine("[8] Exit\n");
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        SelectedVersion = "1.19.63";
                        break;
                    case "2":
                        SelectedVersion = "1.19.62";
                        break;
                    case "3":
                        SelectedVersion = "1.19.60";
                        break;
                    case "4":
                        SelectedVersion = "1.19.51";
                        break;
                    case "5":
                        SelectedVersion = "1.18.12";
                        break;
                    case "6":
                        SelectedVersion = "1.18";
                        break;
                    case "7":
                        SelectedVersion = "1.17.41";
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        WriteColor(
                            "Invalid option! (Example option selection: Enter 8 for the \"[8] Exit\" option)",
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