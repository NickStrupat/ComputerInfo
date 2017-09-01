# ComputerInfo
.NET Standard 2.0 library to fill the need for `Microsoft.VisualBasic.Devices.ComputerInfo` on Windows, macOS, and Linux.

- Windows: All members are implemented
- Linux: All but `GetTotalVirtualMemory()` and `GetAvailableVirtualMemory()` are implemented
- macOS: All but `GetAvailablePhysicalMemory()`, `GetTotalVirtualMemory()`, and `GetAvailableVirtualMemory()` are implemented

## Installation

NuGet package listed on nuget.org at https://www.nuget.org/packages/ComputerInfo/

[![NuGet Status](http://img.shields.io/nuget/v/ComputerInfo.svg?style=flat)](https://www.nuget.org/packages/ComputerInfo/)