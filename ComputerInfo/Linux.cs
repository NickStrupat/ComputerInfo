using System;
using System.IO;
using System.Linq;

namespace NickStrupat
{
    internal static class Linux {
        public static UInt64 GetTotalPhysicalMemory()     => GetBytesFromLine("MemTotal:");
        public static UInt64 GetAvailablePhysicalMemory() => GetBytesFromLine("MemAvailable:");
        public static UInt64 GetTotalVirtualMemory()      => throw new NotImplementedException();
        public static UInt64 GetAvailableVirtualMemory()  => throw new NotImplementedException();

        private static String[] GetProcMemInfoLines() => File.ReadAllLines("/proc/meminfo");

        private static UInt64 GetBytesFromLine(String token)
        {
            const String KbToken = "kB";
            var memTotalLine = GetProcMemInfoLines().FirstOrDefault(x => x.StartsWith(token))?.Substring(token.Length);
            if (memTotalLine != null && memTotalLine.EndsWith(KbToken) && UInt64.TryParse(memTotalLine.Substring(0, memTotalLine.Length - KbToken.Length), out var memKb))
                return memKb * 1024;
            throw new Exception();
        }
    }
}
