# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/10/2022 1:19:27 AM_
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
|TotalBytesAllocated |           bytes |    1,791,040.00 |    1,791,040.00 |    1,791,040.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,542.00 |        1,532.67 |        1,514.00 |           16.17 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,183,154.50 |    1,168,829.95 |    1,161,456.11 |       12,407.23 |
|TotalCollections [Gen0] |     collections |           17.18 |           16.97 |           16.86 |            0.18 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.32 |        1,000.14 |          999.96 |            0.18 |
|[Counter] SuggestionQueries |      operations |          132.12 |          130.52 |          129.70 |            1.39 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,791,040.00 |    1,183,154.50 |          845.20 |
|               2 |    1,791,040.00 |    1,161,879.25 |          860.67 |
|               3 |    1,791,040.00 |    1,161,456.11 |          860.99 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           17.18 |   58,222,450.00 |
|               2 |           26.00 |           16.87 |   59,288,565.38 |
|               3 |           26.00 |           16.86 |   59,310,165.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,513,783,700.00 |
|               2 |            0.00 |            0.00 |1,541,502,700.00 |
|               3 |            0.00 |            0.00 |1,542,064,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,513,783,700.00 |
|               2 |            0.00 |            0.00 |1,541,502,700.00 |
|               3 |            0.00 |            0.00 |1,542,064,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,514.00 |        1,000.14 |      999,857.13 |
|               2 |        1,542.00 |        1,000.32 |      999,677.50 |
|               3 |        1,542.00 |          999.96 |    1,000,041.70 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          132.12 |    7,568,918.50 |
|               2 |          200.00 |          129.74 |    7,707,513.50 |
|               3 |          200.00 |          129.70 |    7,710,321.50 |


