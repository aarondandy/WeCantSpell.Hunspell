# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/8/2022 4:54:27 AM_
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
|TotalBytesAllocated |           bytes |    2,848,208.00 |    2,848,208.00 |    2,848,208.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           61.00 |           61.00 |           61.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          746.00 |          741.33 |          738.00 |            4.16 |
|[Counter] WordsChecked |      operations |      745,920.00 |      745,920.00 |      745,920.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,857,970.94 |    3,841,976.87 |    3,820,263.81 |       19,493.25 |
|TotalCollections [Gen0] |     collections |           82.63 |           82.28 |           81.82 |            0.42 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.60 |          999.97 |          999.64 |            0.54 |
|[Counter] WordsChecked |      operations |    1,010,367.81 |    1,006,179.11 |    1,000,492.65 |        5,105.11 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,848,208.00 |    3,820,263.81 |          261.76 |
|               2 |    2,848,208.00 |    3,847,695.87 |          259.90 |
|               3 |    2,848,208.00 |    3,857,970.94 |          259.20 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |           81.82 |   12,222,175.41 |
|               2 |           61.00 |           82.41 |   12,135,037.70 |
|               3 |           61.00 |           82.63 |   12,102,718.03 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  745,552,700.00 |
|               2 |            0.00 |            0.00 |  740,237,300.00 |
|               3 |            0.00 |            0.00 |  738,265,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  745,552,700.00 |
|               2 |            0.00 |            0.00 |  740,237,300.00 |
|               3 |            0.00 |            0.00 |  738,265,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          746.00 |        1,000.60 |      999,400.40 |
|               2 |          740.00 |          999.68 |    1,000,320.68 |
|               3 |          738.00 |          999.64 |    1,000,360.16 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      745,920.00 |    1,000,492.65 |          999.51 |
|               2 |      745,920.00 |    1,007,676.86 |          992.38 |
|               3 |      745,920.00 |    1,010,367.81 |          989.74 |


