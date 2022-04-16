# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/16/2022 1:03:20 PM_
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
|TotalBytesAllocated |           bytes |   10,523,584.00 |   10,523,584.00 |   10,523,584.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           97.00 |           97.00 |           97.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           39.00 |           39.00 |           39.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|    Elapsed Time |              ms |        1,324.00 |        1,324.00 |        1,324.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,947,140.82 |    7,947,140.82 |    7,947,140.82 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.25 |           73.25 |           73.25 |            0.00 |
|TotalCollections [Gen1] |     collections |           29.45 |           29.45 |           29.45 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.57 |           10.57 |           10.57 |            0.00 |
|    Elapsed Time |              ms |          999.85 |          999.85 |          999.85 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          133.67 |          133.67 |          133.67 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,523,584.00 |    7,947,140.82 |          125.83 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           97.00 |           73.25 |   13,651,520.62 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           39.00 |           29.45 |   33,953,782.05 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.57 |   94,585,535.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,324.00 |          999.85 |    1,000,149.17 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          133.67 |    7,481,341.81 |


