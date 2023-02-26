using System;
using System.Diagnostics;

namespace LatiteMinimal
{
    internal class Program
    {
        public static string SelectedVersion;
        public static Process MinecraftProcess;
        
        public static void WriteColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
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
        }

        public static void Main(string[] args)
        {
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