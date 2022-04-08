# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/8/2022 11:23:10 PM_
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
|TotalBytesAllocated |           bytes |    1,425,976.00 |    1,425,906.67 |    1,425,872.00 |           60.04 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,284.00 |        1,270.00 |        1,262.00 |           12.17 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,130,040.68 |    1,122,753.51 |    1,110,681.51 |       10,529.26 |
|TotalCollections [Gen0] |     collections |            1.59 |            1.57 |            1.56 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.17 |          999.93 |          999.53 |            0.35 |
|[Counter] SuggestionQueries |      operations |          158.51 |          157.48 |          155.78 |            1.48 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,425,976.00 |    1,110,681.51 |          900.35 |
|               2 |    1,425,872.00 |    1,130,040.68 |          884.92 |
|               3 |    1,425,872.00 |    1,127,538.33 |          886.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.56 |  641,937,400.00 |
|               2 |            2.00 |            1.59 |  630,894,100.00 |
|               3 |            2.00 |            1.58 |  632,294,250.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,283,874,800.00 |
|               2 |            0.00 |            0.00 |1,261,788,200.00 |
|               3 |            0.00 |            0.00 |1,264,588,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,283,874,800.00 |
|               2 |            0.00 |            0.00 |1,261,788,200.00 |
|               3 |            0.00 |            0.00 |1,264,588,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,284.00 |        1,000.10 |      999,902.49 |
|               2 |        1,262.00 |        1,000.17 |      999,832.17 |
|               3 |        1,264.00 |          999.53 |    1,000,465.59 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          155.78 |    6,419,374.00 |
|               2 |          200.00 |          158.51 |    6,308,941.00 |
|               3 |          200.00 |          158.15 |    6,322,942.50 |


