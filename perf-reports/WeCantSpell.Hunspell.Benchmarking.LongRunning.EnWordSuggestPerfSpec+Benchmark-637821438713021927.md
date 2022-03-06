# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordSuggestPerfSpec+Benchmark
__Ensure that words can be suggested quickly.__
_3/6/2022 6:11:11 AM_
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
|    Elapsed Time |              ms |        1,572.00 |        1,568.00 |        1,562.00 |            5.29 |
|[Counter] SuggestionQueries |      operations |          200.00 |          200.00 |          200.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,226,547.47 |    5,206,033.27 |    5,192,683.75 |       18,032.95 |
|TotalCollections [Gen0] |     collections |           16.01 |           15.94 |           15.90 |            0.06 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.13 |        1,000.03 |          999.93 |            0.10 |
|[Counter] SuggestionQueries |      operations |          128.06 |          127.56 |          127.23 |            0.44 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,162,768.00 |    5,198,868.59 |          192.35 |
|               2 |    8,162,768.00 |    5,192,683.75 |          192.58 |
|               3 |    8,162,768.00 |    5,226,547.47 |          191.33 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           25.00 |           15.92 |   62,804,188.00 |
|               2 |           25.00 |           15.90 |   62,878,992.00 |
|               3 |           25.00 |           16.01 |   62,471,588.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,570,104,700.00 |
|               2 |            0.00 |            0.00 |1,571,974,800.00 |
|               3 |            0.00 |            0.00 |1,561,789,700.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,570,104,700.00 |
|               2 |            0.00 |            0.00 |1,571,974,800.00 |
|               3 |            0.00 |            0.00 |1,561,789,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,570.00 |          999.93 |    1,000,066.69 |
|               2 |        1,572.00 |        1,000.02 |      999,983.97 |
|               3 |        1,562.00 |        1,000.13 |      999,865.36 |

#### [Counter] SuggestionQueries
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          200.00 |          127.38 |    7,850,523.50 |
|               2 |          200.00 |          127.23 |    7,859,874.00 |
|               3 |          200.00 |          128.06 |    7,808,948.50 |


