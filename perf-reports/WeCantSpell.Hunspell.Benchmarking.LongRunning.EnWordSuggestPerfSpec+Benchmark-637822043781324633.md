# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/6/2022 10:59:38 PM_
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
|TotalBytesAllocated |           bytes |    8,162,768.00 |    8,162,768.00 |    8,162,768.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,547.00 |        1,540.33 |        1,535.00 |            6.11 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,316,213.64 |    5,299,621.21 |    5,278,715.09 |       19,117.82 |
|TotalCollections [Gen0] |     collections |           16.28 |           16.23 |           16.17 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.04 |          999.71 |            0.36 |
|[Counter] SuggestionQueries |      operations |          130.26 |          129.85 |          129.34 |            0.47 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,278,715.09 |          189.44 |
|               2 |    8,162,768.00 |    5,316,213.64 |          188.10 |
|               3 |    8,162,768.00 |    5,303,934.89 |          188.54 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.17 |   61,854,204.00 |
|               2 |           25.00 |           16.28 |   61,417,908.00 |
|               3 |           25.00 |           16.24 |   61,560,092.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,546,355,100.00 |
|               2 |            0.00 |            0.00 |1,535,447,700.00 |
|               3 |            0.00 |            0.00 |1,539,002,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,546,355,100.00 |
|               2 |            0.00 |            0.00 |1,535,447,700.00 |
|               3 |            0.00 |            0.00 |1,539,002,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,547.00 |        1,000.42 |      999,583.13 |
|               2 |        1,535.00 |          999.71 |    1,000,291.66 |
|               3 |        1,539.00 |        1,000.00 |    1,000,001.49 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.34 |    7,731,775.50 |
|               2 |          200.00 |          130.26 |    7,677,238.50 |
|               3 |          200.00 |          129.95 |    7,695,011.50 |


