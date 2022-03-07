# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/7/2022 4:54:30 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.2,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=3, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      170,304.00 |      169,813.33 |      169,568.00 |          424.93 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          748.00 |          747.33 |          747.00 |            0.58 |
|[Counter] WordsChecked |      operations |      754,208.00 |      754,208.00 |      754,208.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      228,025.94 |      227,191.45 |      226,538.97 |          760.01 |
|TotalCollections [Gen0] |     collections |           83.01 |           82.95 |           82.83 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |          999.85 |          999.31 |            0.47 |
|[Counter] WordsChecked |      operations |    1,009,835.28 |    1,009,045.73 |    1,007,604.65 |        1,249.92 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      170,304.00 |      228,025.94 |        4,385.47 |
|               2 |      169,568.00 |      227,009.45 |        4,405.10 |
|               3 |      169,568.00 |      226,538.97 |        4,414.25 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           83.01 |   12,046,167.74 |
|               2 |           62.00 |           83.00 |   12,047,814.52 |
|               3 |           62.00 |           82.83 |   12,072,835.48 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  746,862,400.00 |
|               2 |            0.00 |            0.00 |  746,964,500.00 |
|               3 |            0.00 |            0.00 |  748,515,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  746,862,400.00 |
|               2 |            0.00 |            0.00 |  746,964,500.00 |
|               3 |            0.00 |            0.00 |  748,515,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          747.00 |        1,000.18 |      999,815.80 |
|               2 |          747.00 |        1,000.05 |      999,952.48 |
|               3 |          748.00 |          999.31 |    1,000,689.57 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      754,208.00 |    1,009,835.28 |          990.26 |
|               2 |      754,208.00 |    1,009,697.25 |          990.40 |
|               3 |      754,208.00 |    1,007,604.65 |          992.45 |


