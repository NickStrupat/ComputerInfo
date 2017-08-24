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

        private static InternalMemoryStatus m_InternalMemoryStatus;
        private static InternalMemoryStatus MemoryStatus {
            get {
                if (m_InternalMemoryStatus == null)
                    m_InternalMemoryStatus = new InternalMemoryStatus();
                return m_InternalMemoryStatus;
            }
        }

        private class InternalMemoryStatus
        {
            private bool m_IsOldOS;
            private MEMORYSTATUS m_MemoryStatus;
            private MEMORYSTATUSEX m_MemoryStatusEx;

            internal InternalMemoryStatus()
            {
                m_IsOldOS = Environment.OSVersion.Version.Major < 5;
            }

            internal ulong TotalPhysicalMemory {
                get {
                    Refresh();
                    return !m_IsOldOS ? m_MemoryStatusEx.ullTotalPhys : m_MemoryStatus.dwTotalPhys;
                }
            }

            internal ulong AvailablePhysicalMemory {
                get {
                    Refresh();
                    return !m_IsOldOS ? m_MemoryStatusEx.ullAvailPhys : m_MemoryStatus.dwAvailPhys;
                }
            }

            internal ulong TotalVirtualMemory {
                get {
                    Refresh();
                    return !m_IsOldOS ? m_MemoryStatusEx.ullTotalVirtual : m_MemoryStatus.dwTotalVirtual;
                }
            }

            internal ulong AvailableVirtualMemory {
                get {
                    Refresh();
                    return !m_IsOldOS ? m_MemoryStatusEx.ullAvailVirtual : m_MemoryStatus.dwAvailVirtual;
                }
            }

            private void Refresh()
            {
                if (m_IsOldOS)
                {
                    m_MemoryStatus = new MEMORYSTATUS();
                    GlobalMemoryStatus(ref m_MemoryStatus);
                }
                else
                {
                    m_MemoryStatusEx = new MEMORYSTATUSEX();
                    m_MemoryStatusEx.Init();
                    if (!GlobalMemoryStatusEx(ref m_MemoryStatusEx))
                        throw new Win32Exception("Could not obtain memory information due to internal error.");
                }
            }
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        internal struct MEMORYSTATUS
        {
            internal uint dwLength;
            internal uint dwMemoryLoad;
            internal uint dwTotalPhys;
            internal uint dwAvailPhys;
            internal uint dwTotalPageFile;
            internal uint dwAvailPageFile;
            internal uint dwTotalVirtual;
            internal uint dwAvailVirtual;
        }

        internal struct MEMORYSTATUSEX
        {
            internal uint dwLength;
            internal uint dwMemoryLoad;
            internal ulong ullTotalPhys;
            internal ulong ullAvailPhys;
            internal ulong ullTotalPageFile;
            internal ulong ullAvailPageFile;
            internal ulong ullTotalVirtual;
            internal ulong ullAvailVirtual;
            internal ulong ullAvailExtendedVirtual;

            internal void Init()
            {
                dwLength = checked((uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX)));
            }
        }
    }
}
