using System;

namespace NickStrupat
{
    internal static class MacOS
    {
        public static UInt64 GetTotalPhysicalMemory()     => throw new NotImplementedException();
        public static UInt64 GetAvailablePhysicalMemory() => throw new NotImplementedException();
        public static UInt64 GetTotalVirtualMemory()      => throw new NotImplementedException();
        public static UInt64 GetAvailableVirtualMemory()  => throw new NotImplementedException();
    }
}
