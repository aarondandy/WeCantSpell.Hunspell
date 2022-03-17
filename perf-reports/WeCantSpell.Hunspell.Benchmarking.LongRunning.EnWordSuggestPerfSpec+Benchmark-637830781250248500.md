# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/17/2022 1:42:05 AM_
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
|TotalBytesAllocated |           bytes |    2,214,408.00 |    2,214,066.67 |    2,213,896.00 |          295.60 |
|TotalCollections [Gen0] |     collections |            5.00 |            5.00 |            5.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,120.00 |        1,116.33 |        1,114.00 |            3.21 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    1,987,906.42 |    1,983,956.18 |    1,977,999.93 |        5,249.07 |
|TotalCollections [Gen0] |     collections |            4.49 |            4.48 |            4.47 |            0.01 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.66 |        1,000.31 |          999.97 |            0.34 |
|[Counter] SuggestionQueries |      operations |          179.58 |          179.21 |          178.69 |            0.47 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,213,896.00 |    1,977,999.93 |          505.56 |
|               2 |    2,213,896.00 |    1,987,906.42 |          503.04 |
|               3 |    2,214,408.00 |    1,985,962.18 |          503.53 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            5.00 |            4.47 |  223,851,980.00 |
|               2 |            5.00 |            4.49 |  222,736,440.00 |
|               3 |            5.00 |            4.48 |  223,006,060.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,119,259,900.00 |
|               2 |            0.00 |            0.00 |1,113,682,200.00 |
|               3 |            0.00 |            0.00 |1,115,030,300.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,119,259,900.00 |
|               2 |            0.00 |            0.00 |1,113,682,200.00 |
|               3 |            0.00 |            0.00 |1,115,030,300.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,120.00 |        1,000.66 |      999,339.20 |
|               2 |        1,114.00 |        1,000.29 |      999,714.72 |
|               3 |        1,115.00 |          999.97 |    1,000,027.17 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          178.69 |    5,596,299.50 |
|               2 |          200.00 |          179.58 |    5,568,411.00 |
|               3 |          200.00 |          179.37 |    5,575,151.50 |


