# WeCantSpell: Hunspell

A port of [Hunspell](https://github.com/hunspell/hunspell) for .NET.

![bee](https://raw.githubusercontent.com/aarondandy/WeCantSpell.Hunspell/main/icon.png)

**Download and install with NuGet: [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/)**

[![NuGet version](https://img.shields.io/nuget/v/WeCantSpell.Hunspell.svg?style=flat&label=nuget%3A%20WeCantSpell.Hunspell)](https://www.nuget.org/packages/WeCantSpell.Hunspell/)
[![CI](https://github.com/aarondandy/WeCantSpell.Hunspell/actions/workflows/ci.yml/badge.svg)](https://github.com/aarondandy/WeCantSpell.Hunspell/actions/workflows/ci.yml)

## Features

* Reads Hunspell DIC and AFF file formats
* Supports checking and suggesting words
* Ported to fully managed C#
* Can be queried concurrently
* Confusing LGPL, GPL, MPL tri-license
* Compatible with .NET Core and .NET Standard
* Compatible with multiple .NET framework versions
* Uses .NET to handle cultures and encodings

## License

"It's complicated"

Read the license: [LICENSE](license.txt)

This library was ported from the original Hunspell source
and as a result is licensed under an MPL, LGPL, and GPL tri-license. Read the [LICENSE](license.txt) file to be sure you can use this library.

## Quick Start Example

```csharp
using WeCantSpell.Hunspell;

var dictionary = WordList.CreateFromFiles(@"English (British).dic");
bool notOk = dictionary.Check("Color");
var suggestions = dictionary.Suggest("Color");
bool ok = dictionary.Check("Colour");
```

## Upstream

To know how up to date this port is, check the hunspell-origin submodule.

## Performance

"Good enough I guess"

The performance of this port while not fantastic relative to the original binaries and NHunspell is definitely acceptable.
If you need better check performance you should check out [NHunspell](https://www.nuget.org/packages/NHunspell/) but the implementation may be missing recent changes.

| Benchmark | [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/) net8 | [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/) net48 | [NHunspell](https://www.nuget.org/packages/NHunspell/) |
|---------------|---------------|---------------|---------------|
| Check test    | 🐢 7,855.9 μs | 🐌 18,942 μs | 🐇 6,106 μs   |
| Suggest test  | 🐇 365.4 ms   | 🐢 755.4 ms  | 🐌 1,905.0 ms |

_Note: Measurements taken on an AMD 5800H._

## Specialized Examples

Construct from a list:

```csharp
var words = "The quick brown fox jumps over the lazy dog".Split(' ');
var dictionary = WordList.CreateFromWords(words);
bool notOk = dictionary.Check("teh");
```

Construct from streams:

```csharp
using var dictionaryStream = File.OpenRead(@"English (British).dic");
using var affixStream = File.OpenRead(@"English (British).aff");
var dictionary = WordList.CreateFromStreams(dictionaryStream, affixStream);
bool notOk = dictionary.Check("teh");
```

## Encoding Issues

The .NET Framework contains many encodings that can be handy when opening some dictionary or affix files that do not use a UTF8 encoding or were incorrectly given a UTF BOM. On a full framework platform this works out great but when using .NET Core or .NET Standard those encodings may be missing. If you suspect that there is an issue when loading dictionary and affix files you can check the `dictionary.Affix.Warnings` collection to see if there was a failure when parsing the encoding specified in the file, such as `"Failed to get encoding: ISO-8859-15"` or `"Failed to parse line: SET ISO-8859-15"`. To enable these encodings, reference the `System.Text.Encoding.CodePages` package and then use `Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)` to register them before loading files.

```csharp
using System.Text;
using WeCantSpell.Hunspell;

class Program
{
    static Program() => Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    static void Main(string[] args)
    {
        var dictionary = WordList.CreateFromFiles(@"encoding.dic");
        bool notOk = dictionary.Check("teh");
        var warnings = dictionary.Affix.Warnings;
    }
}
```
