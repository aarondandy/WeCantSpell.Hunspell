# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/10/2022 4:40:50 AM_
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
|TotalBytesAllocated |           bytes |    2,153,536.00 |    2,153,232.00 |    2,152,624.00 |          526.54 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,036.00 |          875.00 |          794.00 |          139.43 |
|[Counter] WordsChecked |      operations |      803,936.00 |      803,936.00 |      803,936.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,712,487.09 |    2,500,558.96 |    2,079,680.31 |      364,494.64 |
|TotalCollections [Gen0] |     collections |           84.39 |           77.81 |           64.70 |           11.35 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.67 |        1,000.41 |        1,000.08 |            0.30 |
|[Counter] WordsChecked |      operations |    1,012,597.90 |      933,625.90 |      776,364.95 |      136,192.41 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    2,153,536.00 |    2,079,680.31 |          480.84 |
|               2 |    2,152,624.00 |    2,709,509.48 |          369.07 |
|               3 |    2,153,536.00 |    2,712,487.09 |          368.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           64.70 |   15,455,417.91 |
|               2 |           67.00 |           84.33 |   11,857,761.19 |
|               3 |           67.00 |           84.39 |   11,849,762.69 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,513,000.00 |
|               2 |            0.00 |            0.00 |  794,470,000.00 |
|               3 |            0.00 |            0.00 |  793,934,100.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,035,513,000.00 |
|               2 |            0.00 |            0.00 |  794,470,000.00 |
|               3 |            0.00 |            0.00 |  793,934,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,036.00 |        1,000.47 |      999,529.92 |
|               2 |          795.00 |        1,000.67 |      999,333.33 |
|               3 |          794.00 |        1,000.08 |      999,917.00 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      803,936.00 |      776,364.95 |        1,288.05 |
|               2 |      803,936.00 |    1,011,914.86 |          988.23 |
|               3 |      803,936.00 |    1,012,597.90 |          987.56 |


