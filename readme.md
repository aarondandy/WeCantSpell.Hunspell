# WeCantSpell: Hunspell

A port of [Hunspell](https://github.com/hunspell/hunspell) v1 for .NET, .NET Standard, and .NET Core.

![bee](icon.png)

**Download and install with NuGet: [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/)**

[![Build status](https://ci.appveyor.com/api/projects/status/shfkt9mgpfhwykqv/branch/master?svg=true)](https://ci.appveyor.com/project/aarondandy/wecantspell-hunspell)
[![NuGet version](https://badge.fury.io/nu/WeCantSpell.Hunspell.svg)](https://www.nuget.org/packages/WeCantSpell.Hunspell/)

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

Read the license: [LICENSE](./license.txt)

This library was ported from the original Hunspell source
and as a result is licensed under an MPL, LGPL, and GPL tri-license. Read the [LICENSE](./license.txt) file to be sure you can use this library.

## Quick Start Example

```csharp
using WeCantSpell.Hunspell;

public class Program
{
    static void Main(string[] args)
    {
        var dictionary = WordList.CreateFromFiles(@"English (British).dic");
        bool notOk = dictionary.Check("teh");
        var suggestions = dictionary.Suggest("teh");
        bool ok = dictionary.Check("the");
    }
}
```

## Upstream

To know how up to date this port is, check the hunspell-origin submodule.

## Performance

"Good enough I guess"

The performance of this port while not fantastic relative to the original
binaries and NHunspell is definitely acceptable.
If you need better performance you should check out [NHunspell](https://www.nuget.org/packages/NHunspell/).

| Benchmark | [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/) netcore2.1 | [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/) net471 | [NHunspell](https://www.nuget.org/packages/NHunspell/) |
|---------------------|-------------|-------------|------------|
| Dictionary Loads /s | 🐌 3.51    | 🐌 3.07     | 🐇 14.49   |
| Words Checked /s    | 🐢 636,598 | 🐢  554,200 | 🐇 973,043 |

_Note: Measurements taken on a Intel 6700K._

## Specialized Examples

Construct from a list:

```csharp
static void Main(string[] args)
{
    var words = "The quick brown fox jumps over the lazy dog".Split(' ');
    var dictionary = WordList.CreateFromWords(words);
    bool notOk = dictionary.Check("teh");
}
```

Construct from streams:

```csharp
static void Main(string[] args)
{
    using(var dictionaryStream = File.OpenRead(@"English (British).dic"))
    using(var affixStream = File.OpenRead(@"English (British).aff"))
    {
        var dictionary = WordList.CreateFromStreams(dictionaryStream, affixStream);
        bool notOk = dictionary.Check("teh");
    }
}
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