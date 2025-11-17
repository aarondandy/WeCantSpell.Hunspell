# WeCantSpell.Hunspell Changelog

All notable changes to this project will be documented in this file.

## 7.0.0 - 2025-11

- Adds dotnet 10 target
- Removes dotnet 6 target

## 6.0.3 - 2025-09

- Improves overall performance a tiny bit

## 6.0.2 - 2025-09

- Fixes default timeout for suggest steps [PR #110](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/110)

## 6.0.1 - 2025-08

- Fix for numeric phonetic suggestions by [filipnavara](https://github.com/filipnavara) in [PR #104](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/104)
- Fix for comments on MAP by [tbambuch](https://github.com/tbambuch) in [PR #105](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/105)

## 6.0.0 - 2025-03

- Add and remove words from in-memory `WordList` instances, resolving [Issue #41](https://github.com/aarondandy/WeCantSpell.Hunspell/issues/41).
- (breaking) Add word methods respect `IgnoredChars` and `ComplexPrefixes` options now.
- (breaking) The netstandard2.0 assembly depends on System.Runtime.CompilerServices.Unsafe instead of System.Collections.Immutable like in previous versions.
- (breaking) Too many little API changes to mention them all 🤷.
- (breaking) Some "Immutable" method names are now "Extract" or "Build". I recommend using "Extract" for better performance.
- File loading is a bit faster now.

## 5.2.3 - 2025-09

- Fixes default timeout for suggest steps [PR #109](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/109)

## 5.2.2 - 2025-02

- It's all just a little bit faster

## 5.2.1 - 2024-11

- Fixes affix alias parsing bug with comments [PR #99](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/99)

## 5.2.0 - 2024-11

- Assorted performance improvements
- Lots of code cleanup

## 5.1.0 - 2024-10

- (breaking-ish) Fixes UWP issue with structs [PR #95](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/95)

## 5.0.1 - 2024-10

- Fixes timing bug [PR #93](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/93)
- Fixes suggest suffix bug [PR #94](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/94)

## 5.0.0 - 2023-12

- Removes usages of `Environment.TickCount`: [PR #83](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/83)
- Adds `net8.0` as a new target
- (breaking) Improves many collection types
- (breaking) Adds more exceptions for exceptional situations
- (breaking) Moves ArrayBuilder to internal
- Updates to match unreleased changes and fixes from Hunspell
- Assorted performance related changes
- Restricts unsafe code to netstandard2.0 builds

## 4.1.0 - 2023-11

- Updates the library to match changes in [Hunspell 1.7.2](https://github.com/hunspell/hunspell/releases/tag/v1.7.2)

## 4.0.0 - 2022-05

- Changes library target to net6.0 and netstandard2.0
- [Fixes suggest affix performance problems](https://github.com/aarondandy/WeCantSpell.Hunspell/issues/40)
- Adds `ReadOnlySpan<char>` overloads for Check and Suggest.
- General improvements to check and suggest performance
- CancellationToken support added to Check and Suggest calls
- Allows customization of timeouts and other options for Check and Suggest calls

## 3.1.2 - 2022-03-23

- [Fixes an index out of range bug](https://github.com/aarondandy/WeCantSpell.Hunspell/issues/71)

## 3.1.1 - 2022-02-21

- [Fixes a character classification bug](https://github.com/aarondandy/WeCantSpell.Hunspell/pull/54)
- Code simplification

## 3.1.0 - 2022-02-21

- Adds a target for net461
- Applies new changes and fixes to match origin
- Project and build has been modernized for newer .NET versions.

## 3.0.1 - 2018-09-05

- Adds a target for netstandard2.0
- References System.Memory
- Removes the net35 target
- A bunch of performance improvements
- Uses System.Memory instead of older custom solution

## 2.1.0 - 2018-08-22

- Allowed more usage of comments in ICONV and OCONV
- Performs BREAK check on 2nd word break
- Now uses SubStandard affix flag
- Adds phonetic entries to the replacement list
- Restricts compound replacement to using "middle" entries
- General suggesion improvements
- Reduce number of strange ngram suggestions
- Prefer suggestions for word pairs listed in dictionary
- Reduce compound word overgeneration

## 2.0.1 - 2018-07-22

- Applied upstream fixes for dotted `I` and Turkish
- Applied upstream fixes for forbidden words
- Applied upstream changes for Hungarian

## 2.0.0 - 2017-06-30

- Replaced .NET Framework 4.6.1 and 4.5.1 with a single 4.5 build (net45).
- Reduced nuget package size.
- Improved performance.
- Removed build for PCL Profile 259 (portable-net45+win8+wpa81+wp8).
- Removed build for .NET Standard 1.1 (netstandard1.1).
- Removed or made inaccessible members and types, including `WordEntrySet` and `WordEntry`.

## 1.1.0 - 2017-06-19

- New `WordEntryDetail` type to simplify storage.
- Reduced memory usage.
- Improved performance.
- Included fixes from source up to commit 77492a4.
- Project beautification 🐝.
- Able to read affix files with a flag mode of NUMBER.

## 1.0.0 - 2017-05-30

- Initial release of the project

The format is based on [Keep a Changelog](http://keepachangelog.com/) and this project tries its best to adhere to [Semantic Versioning](http://semver.org/).
