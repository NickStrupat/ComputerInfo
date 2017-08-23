using System;
using System.IO;
using System.Linq;

namespace NickStrupat
{
    internal static class Linux {
        public static UInt64 TotalPhysicalMemory => GetBytesFromLine(MemTotalToken);
        public static UInt64 AvailablePhysicalMemory => GetBytesFromLine(MemFreeToken);

        public static UInt64 TotalVirtualMemory => throw new NotImplementedException();
        public static UInt64 AvailableVirtualMemory => throw new NotImplementedException();

        private static string[] GetProcMemInfoLines() => File.ReadAllLines("/proc/meminfo");

        private const string MemTotalToken = "MemTotal:";
        private const string MemFreeToken = "MemFree:";
        private const string KbToken = "kB";

        private static UInt64 GetBytesFromLine(String token)
        {
            var memTotalLine = GetProcMemInfoLines().FirstOrDefault(x => x.StartsWith(token));
            if (memTotalLine.EndsWith(KbToken) && UInt64.TryParse(memTotalLine.Substring(0, memTotalLine.Length - KbToken.Length), out var memKb))
                return memKb * 1024;
            throw new Exception();
        }
    }
}
