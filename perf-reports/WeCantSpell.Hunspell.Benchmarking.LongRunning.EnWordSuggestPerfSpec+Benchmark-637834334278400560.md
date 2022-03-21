# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/21/2022 4:23:47 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.3,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    3,745,640.00 |    3,745,002.67 |    3,744,016.00 |          866.53 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,277.00 |        1,264.33 |        1,257.00 |           11.02 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,979,223.52 |    2,962,783.05 |    2,933,649.98 |       25,299.47 |
|TotalCollections [Gen0] |     collections |            3.98 |            3.96 |            3.92 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.56 |        1,000.20 |          999.88 |            0.35 |
|[Counter] SuggestionQueries |      operations |          159.09 |          158.23 |          156.64 |            1.37 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,744,016.00 |    2,975,475.64 |          336.08 |
|               2 |    3,745,352.00 |    2,979,223.52 |          335.66 |
|               3 |    3,745,640.00 |    2,933,649.98 |          340.87 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            3.97 |  251,658,320.00 |
|               2 |            5.00 |            3.98 |  251,431,420.00 |
|               3 |            5.00 |            3.92 |  255,356,980.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,258,291,600.00 |
|               2 |            0.00 |            0.00 |1,257,157,100.00 |
|               3 |            0.00 |            0.00 |1,276,784,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,258,291,600.00 |
|               2 |            0.00 |            0.00 |1,257,157,100.00 |
|               3 |            0.00 |            0.00 |1,276,784,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,259.00 |        1,000.56 |      999,437.33 |
|               2 |        1,257.00 |          999.88 |    1,000,124.98 |
|               3 |        1,277.00 |        1,000.17 |      999,831.56 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          158.95 |    6,291,458.00 |
|               2 |          200.00 |          159.09 |    6,285,785.50 |
|               3 |          200.00 |          156.64 |    6,383,924.50 |


