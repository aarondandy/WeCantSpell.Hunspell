# Hunspell .NET Core

A .NET port of Hunspell for .NET and .NET Core

**Download and install with NuGet: [Hunspell.NetCore](https://www.nuget.org/packages/Hunspell.NetCore/)**

[![Build status](https://ci.appveyor.com/api/projects/status/23hemic5w0dxadjv?svg=true)](https://ci.appveyor.com/project/aarondandy/hunspell-netcore)
[![NuGet version](https://badge.fury.io/nu/Hunspell.NetCore.svg)](https://www.nuget.org/packages/Hunspell.NetCore/)

## Features

* Reads Hunspell DIC and AFF file formats
* Supports checking and suggesting words
* Ported to fully managed C#
* Confusing LGPL, GPL, MPL tri-license
* Compatible with .NET Core
* Compatible with multiple .NET framework versions
* Uses .NET to handle cultures and encodings

## License

"It's complicated"

Read the license: [LICENSE](./license.txt)

This library was ported from the original Hunspell source
and as a result is licensed under an MPL, LGPL, and GPL tri-license. Read the [LICENSE](./license.txt) file to be sure you can use this library.

## Quick Start Example

```csharp
using Hunspell;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var hunspell = HunspellDictionary.FromFile(@"English (British).dic");
            bool notOk = hunspell.Check("teh");
            var suggestions = hunspell.Suggest("teh");
            bool ok = hunspell.Check("the");
        }
    }
}
```

## Upstream

To know how up to date this port is, check the [hunspell-origin](./hunspell-origin) submodule.

## Performance

"Good enough I guess"

The performance of this port while not fantastic relative to the original
binaries and NHunspell is definitely acceptable.
If you need better performance you should check out [NHunspell](https://www.nuget.org/packages/NHunspell/).

| [Benchmark](./test/Hunspell.NetCore.Performance.Comparison/) | [Hunspell.NetCore](https://www.nuget.org/packages/Hunspell.NetCore/) | [NHunspell](https://www.nuget.org/packages/NHunspell/) |
|---------------------|-------------------|------------|
| Dictionary Loads /s | 🐢 2.2            | 🐇 15.92   |
| Words Checked /s    | 🐢 560,499        | 🐇 965,254 |

_Note: Measurements taken on a Intel 6700K with a 850 PRO 256GB._