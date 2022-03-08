# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/8/2022 5:22:34 AM_
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
|TotalBytesAllocated |           bytes |      547,720.00 |      547,522.67 |      547,280.00 |          223.48 |
|TotalCollections [Gen0] |     collections |           64.00 |           64.00 |           64.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,042.00 |          862.00 |          771.00 |          155.89 |
|[Counter] WordsChecked |      operations |      779,072.00 |      779,072.00 |      779,072.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      710,741.87 |      648,092.90 |      525,585.69 |      106,103.53 |
|TotalCollections [Gen0] |     collections |           83.05 |           75.76 |           61.43 |           12.41 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.48 |        1,000.20 |          999.94 |            0.27 |
|[Counter] WordsChecked |      operations |    1,010,952.84 |      922,180.55 |      747,795.89 |      151,029.81 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      547,568.00 |      525,585.69 |        1,902.64 |
|               2 |      547,280.00 |      707,951.14 |        1,412.53 |
|               3 |      547,720.00 |      710,741.87 |        1,406.98 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           64.00 |           61.43 |   16,278,506.25 |
|               2 |           64.00 |           82.79 |   12,078,870.31 |
|               3 |           64.00 |           83.05 |   12,041,115.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,041,824,400.00 |
|               2 |            0.00 |            0.00 |  773,047,700.00 |
|               3 |            0.00 |            0.00 |  770,631,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,041,824,400.00 |
|               2 |            0.00 |            0.00 |  773,047,700.00 |
|               3 |            0.00 |            0.00 |  770,631,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,042.00 |        1,000.17 |      999,831.48 |
|               2 |          773.00 |          999.94 |    1,000,061.71 |
|               3 |          771.00 |        1,000.48 |      999,521.92 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      779,072.00 |      747,795.89 |        1,337.26 |
|               2 |      779,072.00 |    1,007,792.92 |          992.27 |
|               3 |      779,072.00 |    1,010,952.84 |          989.17 |


