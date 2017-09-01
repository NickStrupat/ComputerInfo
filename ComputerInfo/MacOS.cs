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

        private static IntPtr SizeOfLineSize = (IntPtr)IntPtr.Size;

        public static UInt64 GetSysCtlIntegerByName(String name)
        {
            sysctlbyname(name, out var lineSize, ref SizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return (UInt64) lineSize.ToInt64();
        }

        [DllImport("libc")]
        private static extern Int32 sysctlbyname(String name, out IntPtr oldp, ref IntPtr oldlenp, IntPtr newp, IntPtr newlen);
    }
}
