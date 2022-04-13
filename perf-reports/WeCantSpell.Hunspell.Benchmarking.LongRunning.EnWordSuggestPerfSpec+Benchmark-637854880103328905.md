# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_4/13/2022 11:06:50 PM_
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
|TotalBytesAllocated |           bytes |    1,369,088.00 |    1,363,626.67 |    1,360,896.00 |        4,729.65 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,210.00 |        1,206.67 |        1,203.00 |            3.51 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,131,644.25 |    1,129,993.88 |    1,127,442.00 |        2,241.49 |
|TotalCollections [Gen0] |     collections |            1.66 |            1.66 |            1.65 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.15 |          999.93 |          999.68 |            0.23 |
|[Counter] SuggestionQueries |      operations |          166.20 |          165.73 |          165.31 |            0.44 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    1,360,896.00 |    1,130,895.40 |          884.26 |
|               2 |    1,360,896.00 |    1,127,442.00 |          886.96 |
|               3 |    1,369,088.00 |    1,131,644.25 |          883.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.66 |  601,689,600.00 |
|               2 |            2.00 |            1.66 |  603,532,600.00 |
|               3 |            2.00 |            1.65 |  604,910,950.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,203,379,200.00 |
|               2 |            0.00 |            0.00 |1,207,065,200.00 |
|               3 |            0.00 |            0.00 |1,209,821,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,203,379,200.00 |
|               2 |            0.00 |            0.00 |1,207,065,200.00 |
|               3 |            0.00 |            0.00 |1,209,821,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,203.00 |          999.68 |    1,000,315.21 |
|               2 |        1,207.00 |          999.95 |    1,000,054.02 |
|               3 |        1,210.00 |        1,000.15 |      999,852.81 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          166.20 |    6,016,896.00 |
|               2 |          200.00 |          165.69 |    6,035,326.00 |
|               3 |          200.00 |          165.31 |    6,049,109.50 |


