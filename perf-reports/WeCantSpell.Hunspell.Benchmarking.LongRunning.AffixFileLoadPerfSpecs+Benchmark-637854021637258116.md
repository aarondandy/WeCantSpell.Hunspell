# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/12/2022 11:16:03 PM_
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
|TotalBytesAllocated |           bytes |   44,553,264.00 |   44,553,264.00 |   44,553,264.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           96.00 |           96.00 |           96.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,397.00 |        1,397.00 |        1,397.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   31,903,210.39 |   31,903,210.39 |   31,903,210.39 |            0.00 |
|TotalCollections [Gen0] |     collections |           68.74 |           68.74 |           68.74 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.49 |           26.49 |           26.49 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.31 |            9.31 |            9.31 |            0.00 |
|    Elapsed Time |              ms |        1,000.35 |        1,000.35 |        1,000.35 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          126.74 |          126.74 |          126.74 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   44,553,264.00 |   31,903,210.39 |           31.34 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           96.00 |           68.74 |   14,547,015.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |           26.49 |   37,743,608.11 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.31 |  107,424,115.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,397.00 |        1,000.35 |      999,651.75 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          126.74 |    7,889,906.78 |


