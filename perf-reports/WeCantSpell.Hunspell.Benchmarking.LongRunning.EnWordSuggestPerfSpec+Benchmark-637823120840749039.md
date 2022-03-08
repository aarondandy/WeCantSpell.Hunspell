# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/8/2022 4:54:44 AM_
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
|    Elapsed Time |              ms |        1,597.00 |        1,593.67 |        1,591.00 |            3.06 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,130,160.39 |    5,121,282.83 |    5,109,997.98 |       10,294.51 |
|TotalCollections [Gen0] |     collections |           15.71 |           15.68 |           15.65 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.92 |          999.86 |          999.74 |            0.10 |
|[Counter] SuggestionQueries |      operations |          125.70 |          125.48 |          125.20 |            0.25 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,130,160.39 |          194.93 |
|               2 |    8,162,768.00 |    5,123,690.14 |          195.17 |
|               3 |    8,162,768.00 |    5,109,997.98 |          195.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.71 |   63,645,324.00 |
|               2 |           25.00 |           15.69 |   63,725,696.00 |
|               3 |           25.00 |           15.65 |   63,896,448.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,591,133,100.00 |
|               2 |            0.00 |            0.00 |1,593,142,400.00 |
|               3 |            0.00 |            0.00 |1,597,411,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,591,133,100.00 |
|               2 |            0.00 |            0.00 |1,593,142,400.00 |
|               3 |            0.00 |            0.00 |1,597,411,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,591.00 |          999.92 |    1,000,083.66 |
|               2 |        1,593.00 |          999.91 |    1,000,089.39 |
|               3 |        1,597.00 |          999.74 |    1,000,257.48 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          125.70 |    7,955,665.50 |
|               2 |          200.00 |          125.54 |    7,965,712.00 |
|               3 |          200.00 |          125.20 |    7,987,056.00 |


