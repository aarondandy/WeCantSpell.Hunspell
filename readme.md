# WeCantSpell: Hunspell

A port of [Hunspell](https://github.com/hunspell/hunspell) for .NET.

![bee icon](https://raw.githubusercontent.com/aarondandy/WeCantSpell.Hunspell/main/icon.png)

**Download and install with NuGet: [WeCantSpell.Hunspell](https://www.nuget.org/packages/WeCantSpell.Hunspell/)**

[![NuGet version](https://img.shields.io/nuget/v/WeCantSpell.Hunspell.svg?style=flat&label=nuget%3A%20WeCantSpell.Hunspell)](https://www.nuget.org/packages/WeCantSpell.Hunspell/)
[![CI](https://github.com/aarondandy/WeCantSpell.Hunspell/actions/workflows/ci.yml/badge.svg)](https://github.com/aarondandy/WeCantSpell.Hunspell/actions/workflows/ci.yml)

## Features

* Reads Hunspell DIC and AFF file formats
* Supports checking and suggesting words
* No unmanaged dependencies and mostly "safe" code
* Can be queried concurrently
* Confusing LGPL, GPL, MPL tri-license
* Compatible with .NET, .NET Core, and .NET Framework
* Uses .NET to handle most culture, encoding, and text concerns.

## License

"It's complicated"

Read the license: [LICENSE](license.txt)

This library was ported from the original [Hunspell](https://github.com/hunspell/hunspell) source
and as a result is licensed under their MPL, LGPL, and GPL tri-license. Read the [LICENSE](license.txt) file to be sure you can use this library.

## Quick Start Example

```csharp
using WeCantSpell.Hunspell;

var dictionary = WordList.CreateFromFiles(@"English (British).dic");
bool notOk = dictionary.Check("Color");
var suggestions = dictionary.Suggest("Color");
bool ok = dictionary.Check("Colour");
```

## Performance

This port performs competitively on newer versions of the .NET framework compared to the original [NHunspell](https://www.nuget.org/packages/NHunspell/) binaries.

| Benchmark en-US | .NET 8 on 9  | .NET 8       | .NET 4.8     | [NHunspell](https://www.nuget.org/packages/NHunspell/) |
|-----------------|--------------|--------------|--------------|--------------|
| Check 7000      | 🐇 5,690 μs | 🐇 5,864 μs | 🐌 19,807 μs | 🐇 5,969 μs |
| Suggest 300     | 🐇 1.454 s  | 🐇 1.528 s  | 🐢 3.470 s   | 🐌 7.795 s  |

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

## Development

This port wouldn't be feasible for me to produce or maintain without the live testing functionality in [NCrunch](https://www.ncrunch.net/). Being able to get actual near instant feedback from tests saved me from so many typos, bugs due to porting, and even bugs from upstream. I was very relieved to see that NCrunch had survived the release of "Live Unit Testing" in Visual Studio. If you want to try live testing but have been dissatisfied with the native implementation in Visual Studio, please give NCrunch a try. Without NCrunch I will likely stop maintaining this port, it really is that critical to my workflow.

I initially started this port so I could revive my old C# spell check tool but I ended up so distracted and burnt out from this port I never got around to writing the Roslyn analyzer. Eventually, Visual Studio got it's own spell checker and vscode has a plethora of them too, so I doubt I will be developing such an analyzer in the future. Some others have taken up that task, so give them a look:

- https://github.com/rpsft/WeCantSpell.Roslyn
- https://github.com/BrightLight/YouShouldSpellcheck.Analyzer

For details on contributing, see the [contributing](./contributing.md) document. Check the hunspell-origin submodule to see how up to date this library is compared with [source](https://github.com/hunspell/hunspell) .
