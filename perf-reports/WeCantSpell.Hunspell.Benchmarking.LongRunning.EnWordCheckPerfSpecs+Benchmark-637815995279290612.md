# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_2/27/2022 10:58:47 PM_
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
|TotalBytesAllocated |           bytes |    3,594,912.00 |    3,594,800.00 |    3,594,744.00 |           96.99 |
|TotalCollections [Gen0] |     collections |           65.00 |           65.00 |           65.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          759.00 |          758.00 |          757.00 |            1.00 |
|[Counter] WordsChecked |      operations |      795,648.00 |      795,648.00 |      795,648.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,750,597.67 |    4,744,003.83 |    4,740,270.34 |        5,727.09 |
|TotalCollections [Gen0] |     collections |           85.90 |           85.78 |           85.71 |            0.10 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.82 |        1,000.32 |          999.73 |            0.55 |
|[Counter] WordsChecked |      operations |    1,051,480.59 |    1,050,004.79 |    1,049,145.74 |        1,283.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    3,594,744.00 |    4,750,597.67 |          210.50 |
|               2 |    3,594,912.00 |    4,740,270.34 |          210.96 |
|               3 |    3,594,744.00 |    4,741,143.49 |          210.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           65.00 |           85.90 |   11,641,430.77 |
|               2 |           65.00 |           85.71 |   11,667,338.46 |
|               3 |           65.00 |           85.73 |   11,664,644.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  756,693,000.00 |
|               2 |            0.00 |            0.00 |  758,377,000.00 |
|               3 |            0.00 |            0.00 |  758,201,900.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  756,693,000.00 |
|               2 |            0.00 |            0.00 |  758,377,000.00 |
|               3 |            0.00 |            0.00 |  758,201,900.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          757.00 |        1,000.41 |      999,594.45 |
|               2 |          759.00 |        1,000.82 |      999,179.18 |
|               3 |          758.00 |          999.73 |    1,000,266.36 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      795,648.00 |    1,051,480.59 |          951.04 |
|               2 |      795,648.00 |    1,049,145.74 |          953.16 |
|               3 |      795,648.00 |    1,049,388.03 |          952.94 |


