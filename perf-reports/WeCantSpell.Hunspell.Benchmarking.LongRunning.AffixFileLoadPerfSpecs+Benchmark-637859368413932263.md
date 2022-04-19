# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/19/2022 3:47:21 AM_
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
|TotalBytesAllocated |           bytes |   12,901,224.00 |   12,901,224.00 |   12,901,224.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           85.00 |           85.00 |           85.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|    Elapsed Time |              ms |        1,328.00 |        1,328.00 |        1,328.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          236.00 |          236.00 |          236.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    9,717,633.13 |    9,717,633.13 |    9,717,633.13 |            0.00 |
|TotalCollections [Gen0] |     collections |           64.02 |           64.02 |           64.02 |            0.00 |
|TotalCollections [Gen1] |     collections |           29.38 |           29.38 |           29.38 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.04 |            9.04 |            9.04 |            0.00 |
|    Elapsed Time |              ms |        1,000.29 |        1,000.29 |        1,000.29 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.76 |          177.76 |          177.76 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   12,901,224.00 |    9,717,633.13 |          102.91 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           85.00 |           64.02 |   15,618,937.65 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           29.38 |   34,041,274.36 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |            9.04 |  110,634,141.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,328.00 |        1,000.29 |      999,706.10 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          236.00 |          177.76 |    5,625,464.83 |


