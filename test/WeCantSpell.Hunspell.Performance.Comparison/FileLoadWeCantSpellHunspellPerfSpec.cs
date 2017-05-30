using NBench;

namespace WeCantSpell.Hunspell.Performance.Comparison
{
    public class FileLoadWeCantSpellHunspellPerfSpec : FileLoadPerfBase
    {
        protected Counter FilePairsLoaded;

        [PerfSetup]
        public override void Setup(BenchmarkContext context)
        {
            base.Setup(context);

            FilePairsLoaded = context.GetCounter(nameof(FilePairsLoaded));
        }

        [PerfBenchmark(
            Description = "How fast can this project load files?",
            NumberOfIterations = 2,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [GcMeasurement(GcMetric.TotalCollections, GcGeneration.AllGc)]
        [TimingMeasurement]
        [CounterMeasurement(nameof(FilePairsLoaded))]
        public void Benchmark(BenchmarkContext context)
        {
            foreach(var filePair in TestFiles)
            {
                var checker = WordList.CreateFromFilesAsync(filePair.DictionaryFilePath, filePair.AffixFilePath).GetAwaiter().GetResult();
                checker.Check(TestWord);
                FilePairsLoaded.Increment();
            }
        }
    }
}
