# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/25/2022 3:51:11 AM_
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
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   11,991,176.00 |   11,991,176.00 |   11,991,176.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          132.00 |          132.00 |          132.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,806.00 |        1,806.00 |        1,806.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,639,732.34 |    6,639,732.34 |    6,639,732.34 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.09 |           73.09 |           73.09 |            0.00 |
|TotalCollections [Gen1] |     collections |           19.38 |           19.38 |           19.38 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.54 |            5.54 |            5.54 |            0.00 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.02 |        1,000.02 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           32.67 |           32.67 |           32.67 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   11,991,176.00 |    6,639,732.34 |          150.61 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          132.00 |           73.09 |   13,681,612.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |           19.38 |   51,599,222.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            5.54 |  180,597,280.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,806.00 |        1,000.02 |      999,984.94 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           32.67 |   30,609,708.47 |


