# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/10/2022 4:39:29 AM_
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
NumberOfIterations=1, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   39,763,048.00 |   39,763,048.00 |   39,763,048.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           98.00 |           98.00 |           98.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,394.00 |        1,394.00 |        1,394.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   28,512,310.01 |   28,512,310.01 |   28,512,310.01 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.27 |           70.27 |           70.27 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.97 |           27.97 |           27.97 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.89 |            7.89 |            7.89 |            0.00 |
|    Elapsed Time |              ms |          999.58 |          999.58 |          999.58 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          126.92 |          126.92 |          126.92 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   39,763,048.00 |   28,512,310.01 |           35.07 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           98.00 |           70.27 |   14,230,533.67 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           27.97 |   35,758,776.92 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            7.89 |  126,781,118.18 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,394.00 |          999.58 |    1,000,424.89 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          126.92 |    7,879,052.54 |


