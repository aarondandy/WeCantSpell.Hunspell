# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/21/2022 6:32:44 PM_
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
|TotalBytesAllocated |           bytes |    7,124,184.00 |    7,124,184.00 |    7,124,184.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          126.00 |          126.00 |          126.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           42.00 |           42.00 |           42.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,768.00 |        1,768.00 |        1,768.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,028,875.73 |    4,028,875.73 |    4,028,875.73 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.26 |           71.26 |           71.26 |            0.00 |
|TotalCollections [Gen1] |     collections |           23.75 |           23.75 |           23.75 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.66 |            5.66 |            5.66 |            0.00 |
|    Elapsed Time |              ms |          999.84 |          999.84 |          999.84 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           33.37 |           33.37 |           33.37 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,124,184.00 |    4,028,875.73 |          248.21 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          126.00 |           71.26 |   14,033,975.40 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |           23.75 |   42,101,926.19 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            5.66 |  176,828,090.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,768.00 |          999.84 |    1,000,158.88 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           33.37 |   29,970,862.71 |


