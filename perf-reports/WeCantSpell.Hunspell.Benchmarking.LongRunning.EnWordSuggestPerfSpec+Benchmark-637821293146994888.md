# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/6/2022 2:08:34 AM_
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
|TotalBytesAllocated |           bytes |    8,163,560.00 |    8,163,506.67 |    8,163,480.00 |           46.19 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,591.00 |        1,589.33 |        1,587.00 |            2.08 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,143,748.71 |    5,136,989.46 |    5,132,329.64 |        5,992.03 |
|TotalCollections [Gen0] |     collections |           15.75 |           15.73 |           15.72 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.24 |        1,000.11 |          999.96 |            0.14 |
|[Counter] SuggestionQueries |      operations |          126.02 |          125.85 |          125.74 |            0.15 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,560.00 |    5,132,329.64 |          194.84 |
|               2 |    8,163,480.00 |    5,134,890.03 |          194.75 |
|               3 |    8,163,480.00 |    5,143,748.71 |          194.41 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.72 |   63,624,596.00 |
|               2 |           25.00 |           15.73 |   63,592,248.00 |
|               3 |           25.00 |           15.75 |   63,482,728.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,590,614,900.00 |
|               2 |            0.00 |            0.00 |1,589,806,200.00 |
|               3 |            0.00 |            0.00 |1,587,068,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,590,614,900.00 |
|               2 |            0.00 |            0.00 |1,589,806,200.00 |
|               3 |            0.00 |            0.00 |1,587,068,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,591.00 |        1,000.24 |      999,757.95 |
|               2 |        1,590.00 |        1,000.12 |      999,878.11 |
|               3 |        1,587.00 |          999.96 |    1,000,042.97 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          125.74 |    7,953,074.50 |
|               2 |          200.00 |          125.80 |    7,949,031.00 |
|               3 |          200.00 |          126.02 |    7,935,341.00 |


