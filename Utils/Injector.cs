﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LatiteMinimal.Program;

namespace LatiteMinimal.Utils;

public static class Injector
{
    public static void Inject(string path)
    {
        // a lot of this is from https://github.com/notcarlton

        WriteColor($"Injecting {path} into Minecraft!", ConsoleColor.Yellow);

        try
        {
            ApplyAppPackages(path);

            Process targetProcess = Process.GetProcessesByName("Minecraft.Windows")[0];

            nint procHandle = Api.OpenProcess(Api.PROCESS_CREATE_THREAD | Api.PROCESS_QUERY_INFORMATION |
                                              Api.PROCESS_VM_OPERATION | Api.PROCESS_VM_WRITE | Api.PROCESS_VM_READ,
                false, targetProcess.Id);

            nint loadLibraryAddress = Api.GetProcAddress(Api.GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            nint allocMemAddress = Api.VirtualAllocEx(procHandle, IntPtr.Zero,
                (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))), Api.MEM_COMMIT
                                                                          | Api.MEM_RESERVE, Api.PAGE_READWRITE);

            Api.WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(path),
                (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
            Api.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddress,
                allocMemAddress, 0, IntPtr.Zero);
            
            WriteColor("Injected Latite Client into Minecraft successfully!", ConsoleColor.Green);
        }
        catch (Exception e)
        {
            WriteColor("Ran into an error while injecting!", ConsoleColor.Red);
            Logging.ErrorLogging(e);
        }

        void ApplyAppPackages(string path)
        {
            FileInfo infoFile = new(path);
            FileSecurity fSecurity = infoFile.GetAccessControl();
            fSecurity.AddAccessRule(
                new FileSystemAccessRule(new SecurityIdentifier("S-1-15-2-1"),
                    FileSystemRights.FullControl, InheritanceFlags.None,
                    PropagationFlags.NoPropagateInherit, AccessControlType.Allow));

            infoFile.SetAccessControl(fSecurity);
        }
    }
    
    public static async Task WaitForModules()
    {
        await Task.Run(() =>
        {
            WriteColor("Waiting for Minecraft to finish loading...", ConsoleColor.Yellow);
            while (true)
            {
                MinecraftProcess?.Refresh();
                if (MinecraftProcess is { Modules.Count: > 160 }) break;
                Thread.Sleep(4000);
            }
        });
        WriteColor("Minecraft has finished loading!", ConsoleColor.Green);
    }
}