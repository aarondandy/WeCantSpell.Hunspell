# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/21/2022 11:44:45 PM_
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
|TotalBytesAllocated |           bytes |    1,007,472.00 |    1,004,706.67 |      999,176.00 |        4,789.70 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,554.00 |        1,533.00 |        1,521.00 |           18.25 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      661,070.95 |      655,559.53 |      648,605.72 |        6,356.56 |
|TotalCollections [Gen0] |     collections |           16.44 |           16.31 |           16.09 |            0.19 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.46 |        1,000.19 |        1,000.00 |            0.24 |
|[Counter] SuggestionQueries |      operations |          131.51 |          130.50 |          128.76 |            1.51 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,007,472.00 |      648,605.72 |        1,541.77 |
|               2 |    1,007,472.00 |      661,070.95 |        1,512.70 |
|               3 |      999,176.00 |      657,001.91 |        1,522.07 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.09 |   62,131,552.00 |
|               2 |           25.00 |           16.40 |   60,959,992.00 |
|               3 |           25.00 |           16.44 |   60,832,456.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,553,288,800.00 |
|               2 |            0.00 |            0.00 |1,523,999,800.00 |
|               3 |            0.00 |            0.00 |1,520,811,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,553,288,800.00 |
|               2 |            0.00 |            0.00 |1,523,999,800.00 |
|               3 |            0.00 |            0.00 |1,520,811,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,554.00 |        1,000.46 |      999,542.34 |
|               2 |        1,524.00 |        1,000.00 |      999,999.87 |
|               3 |        1,521.00 |        1,000.12 |      999,876.00 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.76 |    7,766,444.00 |
|               2 |          200.00 |          131.23 |    7,619,999.00 |
|               3 |          200.00 |          131.51 |    7,604,057.00 |


