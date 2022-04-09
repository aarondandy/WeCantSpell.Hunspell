# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/9/2022 2:40:09 PM_
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
|TotalBytesAllocated |           bytes |   10,497,840.00 |   10,497,840.00 |   10,497,840.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           97.00 |           97.00 |           97.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|    Elapsed Time |              ms |        1,388.00 |        1,388.00 |        1,388.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,563,803.54 |    7,563,803.54 |    7,563,803.54 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.89 |           69.89 |           69.89 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.66 |           26.66 |           26.66 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.09 |           10.09 |           10.09 |            0.00 |
|    Elapsed Time |              ms |        1,000.07 |        1,000.07 |        1,000.07 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          127.53 |          127.53 |          127.53 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,497,840.00 |    7,563,803.54 |          132.21 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           97.00 |           69.89 |   14,308,297.94 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |           26.66 |   37,510,943.24 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.09 |   99,136,064.29 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,388.00 |        1,000.07 |      999,931.48 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          127.53 |    7,841,270.62 |


