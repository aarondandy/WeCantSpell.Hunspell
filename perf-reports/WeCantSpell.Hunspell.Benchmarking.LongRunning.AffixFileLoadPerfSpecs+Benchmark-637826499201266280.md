# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/12/2022 2:45:20 AM_
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
|TotalBytesAllocated |           bytes |   34,297,088.00 |   34,297,088.00 |   34,297,088.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           67.00 |           67.00 |           67.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.00 |           26.00 |           26.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.00 |            8.00 |            8.00 |            0.00 |
|    Elapsed Time |              ms |          984.00 |          984.00 |          984.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   34,865,500.25 |   34,865,500.25 |   34,865,500.25 |            0.00 |
|TotalCollections [Gen0] |     collections |           68.11 |           68.11 |           68.11 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.43 |           26.43 |           26.43 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.13 |            8.13 |            8.13 |            0.00 |
|    Elapsed Time |              ms |        1,000.31 |        1,000.31 |        1,000.31 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          119.96 |          119.96 |          119.96 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   34,297,088.00 |   34,865,500.25 |           28.68 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           67.00 |           68.11 |   14,682,044.78 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           26.00 |           26.43 |   37,834,500.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            8.00 |            8.13 |  122,962,125.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          984.00 |        1,000.31 |      999,692.07 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          119.96 |    8,336,415.25 |


