# WeCantSpell.Hunspell.Benchmarking.LongRunning.EnWordCheckPerfSpecs+Benchmark
__Ensure that words can be checked quickly.__
_3/10/2022 1:19:10 AM_
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
|TotalBytesAllocated |           bytes |    7,969,856.00 |    7,969,248.00 |    7,968,944.00 |          526.54 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |          785.00 |          782.67 |          780.00 |            2.52 |
|[Counter] WordsChecked |      operations |      812,224.00 |      812,224.00 |      812,224.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   10,216,604.91 |   10,183,399.92 |   10,158,099.84 |       30,042.91 |
|TotalCollections [Gen0] |     collections |           85.89 |           85.62 |           85.41 |            0.25 |
|TotalCollections [Gen1] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            0.00 |            0.00 |            0.00 |            0.00 |
|    Elapsed Time |              ms |        1,000.65 |        1,000.11 |          999.81 |            0.46 |
|[Counter] WordsChecked |      operations |    1,041,194.68 |    1,037,889.75 |    1,035,350.79 |        2,996.30 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,969,856.00 |   10,216,604.91 |           97.88 |
|               2 |    7,968,944.00 |   10,158,099.84 |           98.44 |
|               3 |    7,968,944.00 |   10,175,495.00 |           98.28 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           85.89 |   11,643,111.94 |
|               2 |           67.00 |           85.41 |   11,708,829.85 |
|               3 |           67.00 |           85.55 |   11,688,813.43 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  780,088,500.00 |
|               2 |            0.00 |            0.00 |  784,491,600.00 |
|               3 |            0.00 |            0.00 |  783,150,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            0.00 |            0.00 |  780,088,500.00 |
|               2 |            0.00 |            0.00 |  784,491,600.00 |
|               3 |            0.00 |            0.00 |  783,150,500.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          780.00 |          999.89 |    1,000,113.46 |
|               2 |          785.00 |        1,000.65 |      999,352.36 |
|               3 |          783.00 |          999.81 |    1,000,192.21 |

#### [Counter] WordsChecked
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |      812,224.00 |    1,041,194.68 |          960.44 |
|               2 |      812,224.00 |    1,035,350.79 |          965.86 |
|               3 |      812,224.00 |    1,037,123.77 |          964.21 |


