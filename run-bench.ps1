Push-Location $PSScriptRoot
try {
    & dotnet run -c Release --project .\WeCantSpell.Hunspell.Benchmarking.LongRunning\WeCantSpell.Hunspell.Benchmarking.LongRunning.csproj --output ./perf-reports/
    & dotnet run -c Release --project .\WeCantSpell.Hunspell.Benchmarking.NHunspell\WeCantSpell.Hunspell.Benchmarking.NHunspell.csproj --output ./perf-reports/

    Push-Location ./WeCantSpell.Hunspell.Benchmarking.MicroSuites
    try {
        & dotnet run -c Release --filter "**"
    } finally {
        Pop-Location
    }
} finally {
  Pop-Location
}