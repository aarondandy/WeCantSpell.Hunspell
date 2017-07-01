# Contributing to WeCantSpell: Hunspell

🐝 So, you can't spell either? 🐝

## Getting Started 🎣

1. ✍ Before doing any work, make sure to open or claim an [issue](https://github.com/aarondandy/WeCantSpell.Hunspell/issues) and get some discussion started.
2. 📦 Fork and clone the repository [with submodules](https://git-scm.com/book/en/v2/Git-Tools-Submodules) (example: `git clone --recursive https://github.com/aarondandy/WeCantSpell.Hunspell.git`).
3. 🛠 Open the main WeCantSpell.Hunspell.sln solution file in your IDE.
4. 👌 Build the solution and run the XxUnit.net tests using your runner of choice (NCrunch3 is configured).

## Style 🎩

While I do ask that people try to maintain the styles that exist in the code I am not that particular about style so long as changes:

- 👀 can be easily read and understood by others
- 📐 don't require special tools to maintain
- 📝 match the editor config settings: [..editorconfig](.editorconfig)

Some other things to note are that I like test names that use snake_case 🐍 and you may encounter some boolean expressions that contain lots of whitespace and newlines. Please keep huge expressions formatted like that as the code is very difficult to port and maintain otherwise. It's all pretty subjective but we can work it out 🤝.

## Applying Hunspell Patches 🤕

As the Hunspell v1 project is updated I like to get those fixes and enhancements brought over to the port. This is done by fetching the commits in the `hunspell-origin` submodule and reading each commit while porting the changes to this code base. As a change is applied, update the branch in the submodule to reflect that the changes have been ported.

## Performance 🐌

Please use the included performance tests to make sure changes don't degrade performance below what you branched from. Performance improvements are welcome but make sure they don't place too much burden on maintaining the code or applying updates later.