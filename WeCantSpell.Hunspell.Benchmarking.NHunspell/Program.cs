﻿using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainAssemblyLocation = typeof(Program).Assembly.Location;
            var mainAssemblyDirectory = Path.GetDirectoryName(mainAssemblyLocation);
            var nbenchRunnerPath = Path.Combine(mainAssemblyDirectory, "NBench.Runner.exe");
            var perfDirectory = Path.Combine(mainAssemblyDirectory, "perf");

            var argumentsForNBench = new string[]
            {
                $"\"{mainAssemblyLocation}\"",
                $"output-directory=\"{perfDirectory}\""
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
