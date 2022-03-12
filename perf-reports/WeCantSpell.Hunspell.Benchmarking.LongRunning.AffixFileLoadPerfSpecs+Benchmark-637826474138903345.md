# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/12/2022 2:03:33 AM_
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
|TotalBytesAllocated |           bytes |   34,250,368.00 |   34,250,368.00 |   34,250,368.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.00 |            8.00 |            8.00 |            0.00 |
|    Elapsed Time |              ms |          952.00 |          952.00 |          952.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   35,966,668.65 |   35,966,668.65 |   35,966,668.65 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.31 |           69.31 |           69.31 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.30 |           27.30 |           27.30 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.40 |            8.40 |            8.40 |            0.00 |
|    Elapsed Time |              ms |          999.71 |          999.71 |          999.71 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          123.91 |          123.91 |          123.91 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   34,250,368.00 |   35,966,668.65 |           27.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           69.31 |   14,428,496.97 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           27.30 |   36,626,184.62 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            8.00 |            8.40 |  119,035,100.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          952.00 |          999.71 |    1,000,294.96 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          123.91 |    8,070,176.27 |


