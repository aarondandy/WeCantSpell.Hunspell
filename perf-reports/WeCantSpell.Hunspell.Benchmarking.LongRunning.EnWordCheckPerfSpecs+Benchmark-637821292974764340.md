# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/6/2022 2:08:17 AM_
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
|TotalBytesAllocated |           bytes |    5,888,056.00 |    5,887,954.67 |    5,887,752.00 |          175.51 |
|TotalCollections [Gen0] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          757.00 |          753.00 |          748.00 |            4.58 |
|[Counter] WordsChecked |      operations |      762,496.00 |      762,496.00 |      762,496.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,868,906.97 |    7,818,806.33 |    7,779,524.45 |       45,662.82 |
|TotalCollections [Gen0] |     collections |           82.86 |           82.33 |           81.92 |            0.48 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.18 |          999.91 |          999.64 |            0.27 |
|[Counter] WordsChecked |      operations |    1,019,013.76 |    1,012,543.19 |    1,007,438.84 |        5,907.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    5,887,752.00 |    7,807,987.56 |          128.07 |
|               2 |    5,888,056.00 |    7,779,524.45 |          128.54 |
|               3 |    5,888,056.00 |    7,868,906.97 |          127.08 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |           82.22 |   12,162,383.87 |
|               2 |           62.00 |           81.92 |   12,207,512.90 |
|               3 |           62.00 |           82.86 |   12,068,848.39 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  754,067,800.00 |
|               2 |            0.00 |            0.00 |  756,865,800.00 |
|               3 |            0.00 |            0.00 |  748,268,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  754,067,800.00 |
|               2 |            0.00 |            0.00 |  756,865,800.00 |
|               3 |            0.00 |            0.00 |  748,268,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          754.00 |          999.91 |    1,000,089.92 |
|               2 |          757.00 |        1,000.18 |      999,822.72 |
|               3 |          748.00 |          999.64 |    1,000,359.09 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      762,496.00 |    1,011,176.98 |          988.95 |
|               2 |      762,496.00 |    1,007,438.84 |          992.62 |
|               3 |      762,496.00 |    1,019,013.76 |          981.34 |


