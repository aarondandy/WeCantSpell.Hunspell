# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/14/2022 12:41:18 AM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |    1,297,944.00 |    1,297,944.00 |    1,297,944.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,289.00 |        1,288.33 |        1,287.00 |            1.15 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,008,522.77 |    1,007,478.32 |    1,006,936.99 |          904.72 |
|TotalCollections [Gen0] |     collections |            1.55 |            1.55 |            1.55 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.02 |        1,000.00 |            0.02 |
|[Counter] SuggestionQueries |      operations |          155.40 |          155.24 |          155.16 |            0.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,297,944.00 |    1,006,975.19 |          993.07 |
|               2 |    1,297,944.00 |    1,008,522.77 |          991.55 |
|               3 |    1,297,944.00 |    1,006,936.99 |          993.11 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.55 |  644,476,650.00 |
|               2 |            2.00 |            1.55 |  643,487,700.00 |
|               3 |            2.00 |            1.55 |  644,501,100.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,288,953,300.00 |
|               2 |            0.00 |            0.00 |1,286,975,400.00 |
|               3 |            0.00 |            0.00 |1,289,002,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,288,953,300.00 |
|               2 |            0.00 |            0.00 |1,286,975,400.00 |
|               3 |            0.00 |            0.00 |1,289,002,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,289.00 |        1,000.04 |      999,963.77 |
|               2 |        1,287.00 |        1,000.02 |      999,980.89 |
|               3 |        1,289.00 |        1,000.00 |    1,000,001.71 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          155.16 |    6,444,766.50 |
|               2 |          200.00 |          155.40 |    6,434,877.00 |
|               3 |          200.00 |          155.16 |    6,445,011.00 |


