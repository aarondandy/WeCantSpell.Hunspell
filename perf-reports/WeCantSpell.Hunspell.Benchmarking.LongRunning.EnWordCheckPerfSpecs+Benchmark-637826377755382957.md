# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/11/2022 11:22:55 PM_
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
|TotalBytesAllocated |           bytes |      590,696.00 |      590,546.67 |      590,472.00 |          129.33 |
|TotalCollections [Gen0] |     collections |           68.00 |           68.00 |           68.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,074.00 |        1,026.33 |        1,000.00 |           41.36 |
|[Counter] WordsChecked |      operations |      770,784.00 |      770,784.00 |      770,784.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |      590,539.09 |      575,951.62 |      549,833.89 |       22,670.21 |
|TotalCollections [Gen0] |     collections |           68.01 |           66.32 |           63.32 |            2.60 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.11 |          999.91 |          999.53 |            0.33 |
|[Counter] WordsChecked |      operations |      770,871.57 |      751,732.60 |      717,736.26 |       29,519.43 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      590,472.00 |      549,833.89 |        1,818.73 |
|               2 |      590,696.00 |      587,481.89 |        1,702.18 |
|               3 |      590,472.00 |      590,539.09 |        1,693.37 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           68.00 |           63.32 |   15,792,791.18 |
|               2 |           68.00 |           67.63 |   14,786,338.24 |
|               3 |           68.00 |           68.01 |   14,704,211.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,073,909,800.00 |
|               2 |            0.00 |            0.00 |1,005,471,000.00 |
|               3 |            0.00 |            0.00 |  999,886,400.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |1,073,909,800.00 |
|               2 |            0.00 |            0.00 |1,005,471,000.00 |
|               3 |            0.00 |            0.00 |  999,886,400.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,074.00 |        1,000.08 |      999,916.01 |
|               2 |        1,005.00 |          999.53 |    1,000,468.66 |
|               3 |        1,000.00 |        1,000.11 |      999,886.40 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      770,784.00 |      717,736.26 |        1,393.27 |
|               2 |      770,784.00 |      766,589.99 |        1,304.48 |
|               3 |      770,784.00 |      770,871.57 |        1,297.23 |


