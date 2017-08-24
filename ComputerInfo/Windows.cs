using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace NickStrupat
{
    internal static class Windows {
        public static UInt64 GetTotalPhysicalMemory()     => MemoryStatus.TotalPhysicalMemory;
        public static UInt64 GetAvailablePhysicalMemory() => MemoryStatus.AvailablePhysicalMemory;
        public static UInt64 GetTotalVirtualMemory()      => MemoryStatus.TotalVirtualMemory;
        public static UInt64 GetAvailableVirtualMemory()  => MemoryStatus.AvailableVirtualMemory;

        public static String OSFullName = "Microsoft " + Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductName");

        private static InternalMemoryStatus internalMemoryStatus;
        private static InternalMemoryStatus MemoryStatus => internalMemoryStatus ?? (internalMemoryStatus = new InternalMemoryStatus());

        private class InternalMemoryStatus
        {
            private readonly Boolean isOldOS;
            private MEMORYSTATUS memoryStatus;
            private MEMORYSTATUSEX memoryStatusEx;

            internal InternalMemoryStatus()
            {
                isOldOS = Environment.OSVersion.Version.Major < 5;
            }

            internal UInt64 TotalPhysicalMemory {
                get {
                    Refresh();
                    return !isOldOS ? memoryStatusEx.ullTotalPhys : memoryStatus.dwTotalPhys;
                }
            }

            internal UInt64 AvailablePhysicalMemory {
                get {
                    Refresh();
                    return !isOldOS ? memoryStatusEx.ullAvailPhys : memoryStatus.dwAvailPhys;
                }
            }

            internal UInt64 TotalVirtualMemory {
                get {
                    Refresh();
                    return !isOldOS ? memoryStatusEx.ullTotalVirtual : memoryStatus.dwTotalVirtual;
                }
            }

            internal UInt64 AvailableVirtualMemory {
                get {
                    Refresh();
                    return !isOldOS ? memoryStatusEx.ullAvailVirtual : memoryStatus.dwAvailVirtual;
                }
            }

            private void Refresh()
            {
                if (isOldOS)
                {
                    memoryStatus = new MEMORYSTATUS();
                    GlobalMemoryStatus(ref memoryStatus);
                }
                else
                {
                    memoryStatusEx = new MEMORYSTATUSEX();
                    memoryStatusEx.Init();
                    if (!GlobalMemoryStatusEx(ref memoryStatusEx))
                        throw new Win32Exception("Could not obtain memory information due to internal error.");
                }
            }
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        internal struct MEMORYSTATUS
        {
            internal UInt32 dwLength;
            internal UInt32 dwMemoryLoad;
            internal UInt32 dwTotalPhys;
            internal UInt32 dwAvailPhys;
            internal UInt32 dwTotalPageFile;
            internal UInt32 dwAvailPageFile;
            internal UInt32 dwTotalVirtual;
            internal UInt32 dwAvailVirtual;
        }

        internal struct MEMORYSTATUSEX
        {
            internal UInt32 dwLength;
            internal UInt32 dwMemoryLoad;
            internal UInt64 ullTotalPhys;
            internal UInt64 ullAvailPhys;
            internal UInt64 ullTotalPageFile;
            internal UInt64 ullAvailPageFile;
            internal UInt64 ullTotalVirtual;
            internal UInt64 ullAvailVirtual;
            internal UInt64 ullAvailExtendedVirtual;

            internal void Init()
            {
                dwLength = checked((UInt32)Marshal.SizeOf(typeof(MEMORYSTATUSEX)));
            }
        }
    }
}
