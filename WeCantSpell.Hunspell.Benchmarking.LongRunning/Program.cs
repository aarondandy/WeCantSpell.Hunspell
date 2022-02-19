using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WeCantSpell.Hunspell.Benchmarking.LongRunning
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainAssemblyLocation = typeof(Program).Assembly.Location;
            var mainAssemblyDirectory = Path.GetDirectoryName(mainAssemblyLocation);
            var nbenchRunnerPath = @"..\..\packages\NBench.Runner.1.2.2\tools\netcoreapp2.1\NBench.Runner.exe";
            var perfDirectory = Path.Combine(mainAssemblyDirectory, "perf");

            var argumentsForNBench = new string[]
            {
                $"\"{mainAssemblyLocation}\"",
                $"--output \"{perfDirectory}\""
            };

            var totalArguments = argumentsForNBench.Concat(args);

            var processStartInfo = new ProcessStartInfo(
                nbenchRunnerPath,
                string.Join(" ", totalArguments));

            var process = Process.Start(processStartInfo);
            process.WaitForExit();
        }
    }
}
