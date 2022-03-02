# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/2/2022 4:19:05 AM_
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
|    Elapsed Time |              ms |        1,594.00 |        1,590.67 |        1,588.00 |            3.06 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,139,899.61 |    5,132,756.81 |    5,121,928.36 |        9,535.70 |
|TotalCollections [Gen0] |     collections |           15.74 |           15.72 |           15.69 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.42 |        1,000.12 |          999.84 |            0.29 |
|[Counter] SuggestionQueries |      operations |          125.92 |          125.75 |          125.48 |            0.23 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,121,928.36 |          195.24 |
|               2 |    8,163,480.00 |    5,139,899.61 |          194.56 |
|               3 |    8,163,480.00 |    5,136,442.45 |          194.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.69 |   63,753,176.00 |
|               2 |           25.00 |           15.74 |   63,530,268.00 |
|               3 |           25.00 |           15.73 |   63,573,028.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,593,829,400.00 |
|               2 |            0.00 |            0.00 |1,588,256,700.00 |
|               3 |            0.00 |            0.00 |1,589,325,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,593,829,400.00 |
|               2 |            0.00 |            0.00 |1,588,256,700.00 |
|               3 |            0.00 |            0.00 |1,589,325,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,594.00 |        1,000.11 |      999,892.97 |
|               2 |        1,588.00 |          999.84 |    1,000,161.65 |
|               3 |        1,590.00 |        1,000.42 |      999,575.91 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          125.48 |    7,969,147.00 |
|               2 |          200.00 |          125.92 |    7,941,283.50 |
|               3 |          200.00 |          125.84 |    7,946,628.50 |


