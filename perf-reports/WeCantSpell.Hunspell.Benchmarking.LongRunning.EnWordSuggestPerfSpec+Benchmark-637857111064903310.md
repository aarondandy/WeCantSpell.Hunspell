# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/16/2022 1:05:06 PM_
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
|TotalBytesAllocated |           bytes |    7,441,248.00 |    7,441,248.00 |    7,441,248.00 |            0.00 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,224.00 |        1,222.00 |        1,220.00 |            2.00 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,103,787.84 |    6,090,484.70 |    6,077,118.94 |       13,334.56 |
|TotalCollections [Gen0] |     collections |            0.82 |            0.82 |            0.82 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.72 |        1,000.18 |          999.62 |            0.55 |
|[Counter] SuggestionQueries |      operations |          164.05 |          163.70 |          163.34 |            0.36 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,441,248.00 |    6,090,547.32 |          164.19 |
|               2 |    7,441,248.00 |    6,103,787.84 |          163.83 |
|               3 |    7,441,248.00 |    6,077,118.94 |          164.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.82 |1,221,770,000.00 |
|               2 |            1.00 |            0.82 |1,219,119,700.00 |
|               3 |            1.00 |            0.82 |1,224,469,700.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,221,770,000.00 |
|               2 |            0.00 |            0.00 |1,219,119,700.00 |
|               3 |            0.00 |            0.00 |1,224,469,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,221,770,000.00 |
|               2 |            0.00 |            0.00 |1,219,119,700.00 |
|               3 |            0.00 |            0.00 |1,224,469,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,222.00 |        1,000.19 |      999,811.78 |
|               2 |        1,220.00 |        1,000.72 |      999,278.44 |
|               3 |        1,224.00 |          999.62 |    1,000,383.74 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          163.70 |    6,108,850.00 |
|               2 |          200.00 |          164.05 |    6,095,598.50 |
|               3 |          200.00 |          163.34 |    6,122,348.50 |


