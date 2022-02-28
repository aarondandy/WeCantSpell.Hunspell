# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/28/2022 1:00:58 AM_
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
|TotalBytesAllocated |           bytes |    7,391,920.00 |    7,391,920.00 |    7,391,920.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          128.00 |          128.00 |          128.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           11.00 |           11.00 |           11.00 |            0.00 |
|    Elapsed Time |              ms |        1,884.00 |        1,884.00 |        1,884.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,923,249.54 |    3,923,249.54 |    3,923,249.54 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.94 |           67.94 |           67.94 |            0.00 |
|TotalCollections [Gen1] |     collections |           19.11 |           19.11 |           19.11 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.84 |            5.84 |            5.84 |            0.00 |
|    Elapsed Time |              ms |          999.93 |          999.93 |          999.93 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           31.31 |           31.31 |           31.31 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,391,920.00 |    3,923,249.54 |          254.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          128.00 |           67.94 |   14,719,781.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |           19.11 |   52,337,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           11.00 |            5.84 |  171,284,727.27 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,884.00 |          999.93 |    1,000,070.06 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           31.31 |   31,934,440.68 |


