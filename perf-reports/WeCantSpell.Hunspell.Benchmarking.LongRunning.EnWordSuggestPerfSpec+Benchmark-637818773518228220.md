# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/3/2022 4:09:11 AM_
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
|TotalBytesAllocated |           bytes |    8,163,480.00 |    8,163,480.00 |    8,163,480.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,564.00 |        1,561.33 |        1,559.00 |            2.52 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,235,227.32 |    5,228,077.09 |    5,217,570.04 |        9,294.95 |
|TotalCollections [Gen0] |     collections |           16.03 |           16.01 |           15.98 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.34 |          999.91 |          999.61 |            0.38 |
|[Counter] SuggestionQueries |      operations |          128.26 |          128.08 |          127.83 |            0.23 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,235,227.32 |          191.01 |
|               2 |    8,163,480.00 |    5,217,570.04 |          191.66 |
|               3 |    8,163,480.00 |    5,231,433.92 |          191.15 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.03 |   62,373,452.00 |
|               2 |           25.00 |           15.98 |   62,584,536.00 |
|               3 |           25.00 |           16.02 |   62,418,680.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,559,336,300.00 |
|               2 |            0.00 |            0.00 |1,564,613,400.00 |
|               3 |            0.00 |            0.00 |1,560,467,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,559,336,300.00 |
|               2 |            0.00 |            0.00 |1,564,613,400.00 |
|               3 |            0.00 |            0.00 |1,560,467,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,559.00 |          999.78 |    1,000,215.72 |
|               2 |        1,564.00 |          999.61 |    1,000,392.20 |
|               3 |        1,561.00 |        1,000.34 |      999,658.55 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.26 |    7,796,681.50 |
|               2 |          200.00 |          127.83 |    7,823,067.00 |
|               3 |          200.00 |          128.17 |    7,802,335.00 |


