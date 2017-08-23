using System;
using System.Globalization;
using System.Runtime.InteropServices;
using RuntimeEnvironment = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment;

namespace NickStrupat
{
    public class ComputerInfo {
        private static readonly Boolean IsWindows = RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
        private static readonly Boolean IsMacOS = RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);
        private static readonly Boolean IsLinux = RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);

        public UInt64 TotalPhysicalMemory {
            get {
                if (IsWindows)
                    return Windows.TotalPhysicalMemory;
                if (IsMacOS)
                    throw new NotImplementedException();
                if (IsLinux)
                    return Linux.TotalPhysicalMemory;
                throw new PlatformNotSupportedException();
            }
        }

        public UInt64 AvailablePhysicalMemory {
            get {
                if (IsWindows)
                    return Windows.AvailablePhysicalMemory;
                if (IsMacOS)
                    throw new NotImplementedException();
                if (IsLinux)
                    return Linux.AvailablePhysicalMemory;
                throw new PlatformNotSupportedException();
            }
        }

        public UInt64 TotalVirtualMemory => throw new NotImplementedException();
        public UInt64 AvailableVirtualMemory => throw new NotImplementedException();

        public String      OSFullName         => IsWindows ? Windows.OSFullName : RuntimeEnvironment.OperatingSystem + " " + RuntimeEnvironment.OperatingSystemVersion;
        public String      OSPlatform         => Environment.OSVersion.Platform.ToString();
        public String      OSVersion          => Environment.OSVersion.Version.ToString();
        public CultureInfo InstalledUICulture => CultureInfo.InstalledUICulture;
    }
}
