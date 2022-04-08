# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/8/2022 10:36:17 PM_
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
|TotalBytesAllocated |           bytes |    1,653,200.00 |    1,653,186.67 |    1,653,160.00 |           23.09 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,159.00 |        1,157.33 |        1,156.00 |            1.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,430,559.26 |    1,428,898.64 |    1,426,873.59 |        1,869.66 |
|TotalCollections [Gen0] |     collections |            4.33 |            4.32 |            4.32 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |        1,000.32 |        1,000.28 |            0.04 |
|[Counter] SuggestionQueries |      operations |          173.07 |          172.87 |          172.62 |            0.23 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,653,200.00 |    1,426,873.59 |          700.83 |
|               2 |    1,653,200.00 |    1,429,263.06 |          699.66 |
|               3 |    1,653,160.00 |    1,430,559.26 |          699.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.32 |  231,723,400.00 |
|               2 |            5.00 |            4.32 |  231,336,000.00 |
|               3 |            5.00 |            4.33 |  231,120,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,158,617,000.00 |
|               2 |            0.00 |            0.00 |1,156,680,000.00 |
|               3 |            0.00 |            0.00 |1,155,604,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,158,617,000.00 |
|               2 |            0.00 |            0.00 |1,156,680,000.00 |
|               3 |            0.00 |            0.00 |1,155,604,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,159.00 |        1,000.33 |      999,669.54 |
|               2 |        1,157.00 |        1,000.28 |      999,723.42 |
|               3 |        1,156.00 |        1,000.34 |      999,657.44 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          172.62 |    5,793,085.00 |
|               2 |          200.00 |          172.91 |    5,783,400.00 |
|               3 |          200.00 |          173.07 |    5,778,020.00 |


