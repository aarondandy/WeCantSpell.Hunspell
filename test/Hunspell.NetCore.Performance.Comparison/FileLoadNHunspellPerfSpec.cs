using NBench;

namespace Hunspell.NetCore.Performance.Comparison
{
    public class FileLoadNHunspellPerfSpec : FileLoadPerfBase
    {
        protected Counter FilePairsLoaded;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            FilePairsLoaded = context.GetCounter(nameof(FilePairsLoaded));
        }

        [PerfBenchmark(
            Description = "How fast can NHunspell load files?",
            NumberOfIterations = 3,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(FilePairsLoaded))]
        public void Benchmark(BenchmarkContext context)
        {
            foreach (var filePair in TestFiles)
            {
                var checker = new NHunspell.Hunspell(filePair.DictionaryFilePath, filePair.AffixFilePath);
                checker.Spell(TestWord);
                FilePairsLoaded.Increment();
            }
        }
    }
}
