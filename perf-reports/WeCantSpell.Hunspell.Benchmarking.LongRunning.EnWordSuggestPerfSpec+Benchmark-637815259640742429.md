# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/27/2022 2:32:44 AM_
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
|    Elapsed Time |              ms |        1,545.00 |        1,543.00 |        1,540.00 |            2.65 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,298,823.62 |    5,288,975.70 |    5,282,130.93 |        8,742.17 |
|TotalCollections [Gen0] |     collections |           16.23 |           16.20 |           16.18 |            0.03 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          999.76 |          999.68 |          999.60 |            0.08 |
|[Counter] SuggestionQueries |      operations |          129.82 |          129.58 |          129.41 |            0.21 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,163,480.00 |    5,282,130.93 |          189.32 |
|               2 |    8,163,480.00 |    5,285,972.56 |          189.18 |
|               3 |    8,163,480.00 |    5,298,823.62 |          188.72 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.18 |   61,819,596.00 |
|               2 |           25.00 |           16.19 |   61,774,668.00 |
|               3 |           25.00 |           16.23 |   61,624,848.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,545,489,900.00 |
|               2 |            0.00 |            0.00 |1,544,366,700.00 |
|               3 |            0.00 |            0.00 |1,540,621,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,545,489,900.00 |
|               2 |            0.00 |            0.00 |1,544,366,700.00 |
|               3 |            0.00 |            0.00 |1,540,621,200.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,545.00 |          999.68 |    1,000,317.09 |
|               2 |        1,544.00 |          999.76 |    1,000,237.50 |
|               3 |        1,540.00 |          999.60 |    1,000,403.38 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          129.41 |    7,727,449.50 |
|               2 |          200.00 |          129.50 |    7,721,833.50 |
|               3 |          200.00 |          129.82 |    7,703,106.00 |


