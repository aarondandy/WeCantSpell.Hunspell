# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/27/2022 4:20:37 AM_
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
|TotalBytesAllocated |           bytes |    3,594,912.00 |    3,594,754.67 |    3,594,608.00 |          152.28 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,045.00 |          942.00 |          766.00 |          153.16 |
|[Counter] WordsChecked |      operations |      795,648.00 |      795,648.00 |      795,648.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,692,788.57 |    3,890,775.00 |    3,436,964.25 |      696,568.43 |
|TotalCollections [Gen0] |     collections |           84.86 |           70.35 |           62.14 |           12.60 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.27 |          999.79 |          999.09 |            0.62 |
|[Counter] WordsChecked |      operations |    1,038,724.62 |      861,171.71 |      760,690.03 |      154,210.21 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,594,744.00 |    3,542,572.19 |          282.28 |
|               2 |    3,594,912.00 |    3,436,964.25 |          290.95 |
|               3 |    3,594,608.00 |    4,692,788.57 |          213.09 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           64.06 |   15,611,186.15 |
|               2 |           65.00 |           62.14 |   16,091,624.62 |
|               3 |           65.00 |           84.86 |   11,784,392.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,727,100.00 |
|               2 |            0.00 |            0.00 |1,045,955,600.00 |
|               3 |            0.00 |            0.00 |  765,985,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,014,727,100.00 |
|               2 |            0.00 |            0.00 |1,045,955,600.00 |
|               3 |            0.00 |            0.00 |  765,985,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,015.00 |        1,000.27 |      999,731.13 |
|               2 |        1,045.00 |          999.09 |    1,000,914.45 |
|               3 |          766.00 |        1,000.02 |      999,981.07 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      795,648.00 |      784,100.47 |        1,275.35 |
|               2 |      795,648.00 |      760,690.03 |        1,314.60 |
|               3 |      795,648.00 |    1,038,724.62 |          962.72 |


