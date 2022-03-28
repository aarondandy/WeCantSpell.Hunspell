# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/28/2022 10:48:55 PM_
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
|TotalBytesAllocated |           bytes |    7,701,544.00 |    7,701,506.67 |    7,701,488.00 |           32.33 |
|TotalCollections [Gen0] |     collections |            2.00 |            2.00 |            2.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,301.00 |        1,283.67 |        1,271.00 |           15.53 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,062,331.42 |    6,000,458.91 |    5,919,209.79 |       73,501.97 |
|TotalCollections [Gen0] |     collections |            1.57 |            1.56 |            1.54 |            0.02 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.48 |        1,000.04 |          999.72 |            0.39 |
|[Counter] SuggestionQueries |      operations |          157.43 |          155.83 |          153.72 |            1.91 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,701,544.00 |    6,062,331.42 |          164.95 |
|               2 |    7,701,488.00 |    6,019,835.51 |          166.12 |
|               3 |    7,701,488.00 |    5,919,209.79 |          168.94 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            2.00 |            1.57 |  635,196,550.00 |
|               2 |            2.00 |            1.56 |  639,675,950.00 |
|               3 |            2.00 |            1.54 |  650,550,350.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,270,393,100.00 |
|               2 |            0.00 |            0.00 |1,279,351,900.00 |
|               3 |            0.00 |            0.00 |1,301,100,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,270,393,100.00 |
|               2 |            0.00 |            0.00 |1,279,351,900.00 |
|               3 |            0.00 |            0.00 |1,301,100,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,271.00 |        1,000.48 |      999,522.50 |
|               2 |        1,279.00 |          999.72 |    1,000,275.14 |
|               3 |        1,301.00 |          999.92 |    1,000,077.40 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          157.43 |    6,351,965.50 |
|               2 |          200.00 |          156.33 |    6,396,759.50 |
|               3 |          200.00 |          153.72 |    6,505,503.50 |


