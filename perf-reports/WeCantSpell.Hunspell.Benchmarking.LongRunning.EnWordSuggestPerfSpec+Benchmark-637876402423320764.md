# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_5/8/2022 8:57:22 PM_
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
|TotalBytesAllocated |           bytes |    2,816,088.00 |    2,816,061.33 |    2,816,032.00 |           28.10 |
|TotalCollections [Gen0] |     collections |            1.00 |            1.00 |            1.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,183.00 |        1,180.67 |        1,176.00 |            4.04 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,395,685.82 |    2,386,283.24 |    2,381,069.47 |        8,158.99 |
|TotalCollections [Gen0] |     collections |            0.85 |            0.85 |            0.85 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.71 |        1,000.47 |        1,000.25 |            0.23 |
|[Counter] SuggestionQueries |      operations |          170.14 |          169.48 |          169.10 |            0.58 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,816,064.00 |    2,395,685.82 |          417.42 |
|               2 |    2,816,088.00 |    2,381,069.47 |          419.98 |
|               3 |    2,816,032.00 |    2,382,094.43 |          419.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            1.00 |            0.85 |1,175,473,000.00 |
|               2 |            1.00 |            0.85 |1,182,698,800.00 |
|               3 |            1.00 |            0.85 |1,182,166,400.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,175,473,000.00 |
|               2 |            0.00 |            0.00 |1,182,698,800.00 |
|               3 |            0.00 |            0.00 |1,182,166,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,175,473,000.00 |
|               2 |            0.00 |            0.00 |1,182,698,800.00 |
|               3 |            0.00 |            0.00 |1,182,166,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,176.00 |        1,000.45 |      999,551.87 |
|               2 |        1,183.00 |        1,000.25 |      999,745.39 |
|               3 |        1,183.00 |        1,000.71 |      999,295.35 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          170.14 |    5,877,365.00 |
|               2 |          200.00 |          169.10 |    5,913,494.00 |
|               3 |          200.00 |          169.18 |    5,910,832.00 |


