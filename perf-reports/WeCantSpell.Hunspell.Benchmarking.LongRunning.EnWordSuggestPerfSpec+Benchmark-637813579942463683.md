# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_2/25/2022 3:53:14 AM_
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
|TotalBytesAllocated |           bytes |    6,761,048.00 |    6,761,048.00 |    6,761,048.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           25.00 |           25.00 |           25.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,547.00 |        1,543.00 |        1,538.00 |            4.58 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,395,815.36 |    4,381,252.00 |    4,370,542.58 |       13,069.74 |
|TotalCollections [Gen0] |     collections |           16.25 |           16.20 |           16.16 |            0.05 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.03 |          999.88 |          999.65 |            0.20 |
|[Counter] SuggestionQueries |      operations |          130.03 |          129.60 |          129.29 |            0.39 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    6,761,048.00 |    4,395,815.36 |          227.49 |
|               2 |    6,761,048.00 |    4,377,398.05 |          228.45 |
|               3 |    6,761,048.00 |    4,370,542.58 |          228.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           16.25 |   61,522,584.00 |
|               2 |           25.00 |           16.19 |   61,781,432.00 |
|               3 |           25.00 |           16.16 |   61,878,340.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,538,064,600.00 |
|               2 |            0.00 |            0.00 |1,544,535,800.00 |
|               3 |            0.00 |            0.00 |1,546,958,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,538,064,600.00 |
|               2 |            0.00 |            0.00 |1,544,535,800.00 |
|               3 |            0.00 |            0.00 |1,546,958,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,538.00 |          999.96 |    1,000,042.00 |
|               2 |        1,544.00 |          999.65 |    1,000,347.02 |
|               3 |        1,547.00 |        1,000.03 |      999,973.17 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          130.03 |    7,690,323.00 |
|               2 |          200.00 |          129.49 |    7,722,679.00 |
|               3 |          200.00 |          129.29 |    7,734,792.50 |


