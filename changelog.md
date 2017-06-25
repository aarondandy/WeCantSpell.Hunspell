# WeCantSpell.Hunspell Changelog

All notable changes to this project will be documented in this file.

## Unreleased
### Changes
- [Breaking] Replaced .NET Framework 4.6.1 and 4.5.1 with a single 4.5 build (net45).
- [Breaking] Removed build for PCL Profile 259 (portable-net45+win8+wpa81+wp8).
- [Breaking] Removed build for .NET Standard 1.1 (netstandard1.1).
- Reduced nuget package size by nearly half.
- Improved string operation performance.

## 1.1.0 - 2017-06-19
### Added
- New `WordEntryDetail` type to simplify storage.

### Changed
- Reduced memory usage.
- Improved performance.
- Included fixes from source up to commit 77492a4.
- Project beautification 🐝.

### Fixed
- Able to read affix files with a flag mode of NUMBER.

## 1.0.0 - 2017-05-30
- Initial release of the project

The format is based on [Keep a Changelog](http://keepachangelog.com/) and this project tries its best to adhere to [Semantic Versioning](http://semver.org/).
