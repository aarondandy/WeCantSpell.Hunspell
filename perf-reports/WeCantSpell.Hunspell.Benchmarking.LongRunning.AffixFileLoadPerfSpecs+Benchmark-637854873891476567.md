# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/13/2022 10:56:29 PM_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 10.0.19043.0
ProcessorCount=16
CLR=6.0.4,IsMono=False,MaxGcGeneration=2
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
|TotalBytesAllocated |           bytes |   37,879,544.00 |   37,879,544.00 |   37,879,544.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           95.00 |           95.00 |           95.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|    Elapsed Time |              ms |        1,345.00 |        1,345.00 |        1,345.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   28,168,067.53 |   28,168,067.53 |   28,168,067.53 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.64 |           70.64 |           70.64 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.51 |           27.51 |           27.51 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.92 |            8.92 |            8.92 |            0.00 |
|    Elapsed Time |              ms |        1,000.17 |        1,000.17 |        1,000.17 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          131.62 |          131.62 |          131.62 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   37,879,544.00 |   28,168,067.53 |           35.50 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           95.00 |           70.64 |   14,155,463.16 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |           27.51 |   36,345,108.11 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |            8.92 |  112,064,083.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,345.00 |        1,000.17 |      999,828.25 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          131.62 |    7,597,564.97 |


