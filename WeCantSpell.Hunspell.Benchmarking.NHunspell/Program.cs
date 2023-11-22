using BenchmarkDotNet.Running;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell;

class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
