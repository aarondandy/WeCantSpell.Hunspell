# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/24/2022 4:39:37 AM_
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
|TotalBytesAllocated |           bytes |      751,176.00 |      751,112.00 |      751,056.00 |           60.40 |
|TotalCollections [Gen0] |     collections |            3.00 |            3.00 |            3.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,258.00 |        1,252.00 |        1,243.00 |            7.94 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      604,321.58 |      599,927.50 |      596,998.55 |        3,875.14 |
|TotalCollections [Gen0] |     collections |            2.41 |            2.40 |            2.38 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.15 |          999.97 |          999.86 |            0.16 |
|[Counter] SuggestionQueries |      operations |          160.93 |          159.74 |          158.97 |            1.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      751,176.00 |      598,462.35 |        1,670.95 |
|               2 |      751,104.00 |      596,998.55 |        1,675.05 |
|               3 |      751,056.00 |      604,321.58 |        1,654.75 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            3.00 |            2.39 |  418,392,233.33 |
|               2 |            3.00 |            2.38 |  419,377,900.00 |
|               3 |            3.00 |            2.41 |  414,269,500.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,255,176,700.00 |
|               2 |            0.00 |            0.00 |1,258,133,700.00 |
|               3 |            0.00 |            0.00 |1,242,808,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,255,176,700.00 |
|               2 |            0.00 |            0.00 |1,258,133,700.00 |
|               3 |            0.00 |            0.00 |1,242,808,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,255.00 |          999.86 |    1,000,140.80 |
|               2 |        1,258.00 |          999.89 |    1,000,106.28 |
|               3 |        1,243.00 |        1,000.15 |      999,845.94 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          159.34 |    6,275,883.50 |
|               2 |          200.00 |          158.97 |    6,290,668.50 |
|               3 |          200.00 |          160.93 |    6,214,042.50 |


