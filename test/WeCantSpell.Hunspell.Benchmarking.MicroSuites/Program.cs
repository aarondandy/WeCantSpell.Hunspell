using BenchmarkDotNet.Running;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<EnUsWordListCheckSuite>();
            BenchmarkRunner.Run<EnUsWordListSuggestSuite>();
        }
    }
}
