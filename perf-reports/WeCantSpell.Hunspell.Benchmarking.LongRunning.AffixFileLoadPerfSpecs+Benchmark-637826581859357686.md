# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/12/2022 5:03:05 AM_
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
|TotalBytesAllocated |           bytes |   19,319,376.00 |   19,319,376.00 |   19,319,376.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          101.00 |          101.00 |          101.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           40.00 |           40.00 |           40.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,463.00 |        1,463.00 |        1,463.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,203,751.95 |   13,203,751.95 |   13,203,751.95 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.03 |           69.03 |           69.03 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.34 |           27.34 |           27.34 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.88 |            8.88 |            8.88 |            0.00 |
|    Elapsed Time |              ms |          999.88 |          999.88 |          999.88 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          120.97 |          120.97 |          120.97 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   19,319,376.00 |   13,203,751.95 |           75.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          101.00 |           69.03 |   14,486,863.37 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           40.00 |           27.34 |   36,579,330.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            8.88 |  112,551,784.62 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,463.00 |          999.88 |    1,000,118.39 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          120.97 |    8,266,515.25 |


