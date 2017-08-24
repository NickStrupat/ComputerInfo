using System;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    internal static class MacOS
    {
        public static UInt64 GetTotalPhysicalMemory()     => GetSysCtlIntegerByName("hw.memsize");
        public static UInt64 GetAvailablePhysicalMemory() => throw new NotImplementedException();
        public static UInt64 GetTotalVirtualMemory()      => throw new NotImplementedException();
        public static UInt64 GetAvailableVirtualMemory()  => throw new NotImplementedException();

        public static UInt64 GetSysCtlIntegerByName(String name)
        {
            IntPtr lineSize;
            IntPtr sizeOfLineSize = (IntPtr) IntPtr.Size;
            sysctlbyname(name, out lineSize, ref sizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return (UInt64) lineSize.ToInt64();
        }

        [DllImport("libc")]
        private static extern int sysctlbyname(string name, out IntPtr oldp, ref IntPtr oldlenp, IntPtr newp, IntPtr newlen);
    }
}
