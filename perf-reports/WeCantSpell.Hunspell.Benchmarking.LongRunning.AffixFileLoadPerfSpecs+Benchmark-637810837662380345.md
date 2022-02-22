# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/21/2022 11:42:46 PM_
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
|TotalBytesAllocated |           bytes |    7,126,928.00 |    7,126,928.00 |    7,126,928.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          126.00 |          126.00 |          126.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           42.00 |           42.00 |           42.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,718.00 |        1,718.00 |        1,718.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,149,043.87 |    4,149,043.87 |    4,149,043.87 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.35 |           73.35 |           73.35 |            0.00 |
|TotalCollections [Gen1] |     collections |           24.45 |           24.45 |           24.45 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.82 |            5.82 |            5.82 |            0.00 |
|    Elapsed Time |              ms |        1,000.16 |        1,000.16 |        1,000.16 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           34.35 |           34.35 |           34.35 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,126,928.00 |    4,149,043.87 |          241.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          126.00 |           73.35 |   13,632,760.32 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |           24.45 |   40,898,280.95 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            5.82 |  171,772,780.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,718.00 |        1,000.16 |      999,841.56 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           34.35 |   29,114,030.51 |


