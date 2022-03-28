# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/28/2022 10:47:05 PM_
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
|TotalBytesAllocated |           bytes |   44,604,968.00 |   44,604,968.00 |   44,604,968.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           96.00 |           96.00 |           96.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           36.00 |           36.00 |           36.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,323.00 |        1,323.00 |        1,323.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   33,707,677.11 |   33,707,677.11 |   33,707,677.11 |            0.00 |
|TotalCollections [Gen0] |     collections |           72.55 |           72.55 |           72.55 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.20 |           27.20 |           27.20 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.82 |            9.82 |            9.82 |            0.00 |
|    Elapsed Time |              ms |          999.78 |          999.78 |          999.78 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          133.76 |          133.76 |          133.76 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   44,604,968.00 |   33,707,677.11 |           29.67 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           96.00 |           72.55 |   13,784,251.04 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           36.00 |           27.20 |   36,758,002.78 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.82 |  101,791,392.31 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,323.00 |          999.78 |    1,000,217.76 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          133.76 |    7,476,203.95 |


