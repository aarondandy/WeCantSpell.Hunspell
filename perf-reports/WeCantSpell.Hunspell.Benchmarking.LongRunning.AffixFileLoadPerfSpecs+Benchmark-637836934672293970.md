# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/24/2022 4:37:47 AM_
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
|TotalBytesAllocated |           bytes |   11,287,560.00 |   11,287,560.00 |   11,287,560.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           94.00 |           94.00 |           94.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           35.00 |           35.00 |           35.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|    Elapsed Time |              ms |        1,339.00 |        1,339.00 |        1,339.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,426,646.83 |    8,426,646.83 |    8,426,646.83 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.18 |           70.18 |           70.18 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.13 |           26.13 |           26.13 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.96 |            8.96 |            8.96 |            0.00 |
|    Elapsed Time |              ms |          999.62 |          999.62 |          999.62 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          132.14 |          132.14 |          132.14 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   11,287,560.00 |    8,426,646.83 |          118.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           94.00 |           70.18 |   14,250,084.04 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           35.00 |           26.13 |   38,271,654.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |            8.96 |  111,625,658.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,339.00 |          999.62 |    1,000,379.31 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          132.14 |    7,567,841.24 |


