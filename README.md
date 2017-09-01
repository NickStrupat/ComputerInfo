# ComputerInfo
.NET Standard 2.0 library to fill the need for `Microsoft.VisualBasic.Devices.ComputerInfo` on Windows, macOS, and Linux.

- Windows: All members are implemented
- Linux: All but `GetTotalVirtualMemory()` and `GetAvailableVirtualMemory()` are implemented
- macOS: All but `GetAvailablePhysicalMemory()`, `GetTotalVirtualMemory()`, and `GetAvailableVirtualMemory()` are implemented