# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/23/2022 3:55:51 AM_
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
|TotalBytesAllocated |           bytes |    6,804,824.00 |    6,804,824.00 |    6,804,824.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,563.00 |        1,560.33 |        1,558.00 |            2.52 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,366,173.67 |    4,360,251.68 |    4,354,376.45 |        5,898.75 |
|TotalCollections [Gen0] |     collections |           16.04 |           16.02 |           16.00 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.16 |          999.80 |          999.57 |            0.32 |
|[Counter] SuggestionQueries |      operations |          128.33 |          128.15 |          127.98 |            0.17 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,804,824.00 |    4,366,173.67 |          229.03 |
|               2 |    6,804,824.00 |    4,360,204.92 |          229.35 |
|               3 |    6,804,824.00 |    4,354,376.45 |          229.65 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.04 |   62,341,304.00 |
|               2 |           25.00 |           16.02 |   62,426,644.00 |
|               3 |           25.00 |           16.00 |   62,510,204.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,558,532,600.00 |
|               2 |            0.00 |            0.00 |1,560,666,100.00 |
|               3 |            0.00 |            0.00 |1,562,755,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,558,532,600.00 |
|               2 |            0.00 |            0.00 |1,560,666,100.00 |
|               3 |            0.00 |            0.00 |1,562,755,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,558.00 |          999.66 |    1,000,341.85 |
|               2 |        1,560.00 |          999.57 |    1,000,426.99 |
|               3 |        1,563.00 |        1,000.16 |      999,843.31 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          128.33 |    7,792,663.00 |
|               2 |          200.00 |          128.15 |    7,803,330.50 |
|               3 |          200.00 |          127.98 |    7,813,775.50 |


