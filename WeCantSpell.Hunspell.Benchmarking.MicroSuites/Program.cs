namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkDotNet.Running.BenchmarkRunner.Run<EnUsWordListCheckSuite>();
        BenchmarkDotNet.Running.BenchmarkRunner.Run<EnUsWordListSuggestSuite>();
    }
}
