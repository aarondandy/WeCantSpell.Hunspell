﻿using System;
using System.IO;
using NBench;

namespace WeCantSpell.Hunspell.Benchmarking.NHunspell
{
    public class WordCheckNHunspellPerfSpec : EnWordPerfBase, IDisposable
    {
        static WordCheckNHunspellPerfSpec()
        {
            Utilities.ApplyCultureHacks();
        }

        private Counter WordsChecked;

        private global::NHunspell.Hunspell Checker;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            var testAssemblyPath = Path.GetFullPath(GetType().Assembly.Location);
            var filesDirectory = Path.Combine(Path.GetDirectoryName(testAssemblyPath), "files/");
            var dictionaryFilePath = Path.Combine(filesDirectory, "English (American).dic");
            var affixFilePath = Path.ChangeExtension(dictionaryFilePath, "aff");
            Checker = new global::NHunspell.Hunspell(affixFilePath, dictionaryFilePath);

            WordsChecked = context.GetCounter(nameof(WordsChecked));
        }

        [PerfCleanup]
        public void Dispose()
        {
            Checker?.Dispose();
        }

        [PerfBenchmark(
            Description = "How fast can NHunspell check English (US) words?",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(WordsChecked))]
        public void Benchmark(BenchmarkContext context)
        {
            foreach (var word in Words)
            {
                var result = Checker.Spell(word);
                WordsChecked.Increment();
            }
        }
    }
}
