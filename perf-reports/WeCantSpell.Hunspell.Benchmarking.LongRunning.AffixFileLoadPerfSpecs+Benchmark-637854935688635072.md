# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/14/2022 12:39:28 AM_
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
|TotalBytesAllocated |           bytes |   44,552,560.00 |   44,552,560.00 |   44,552,560.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           96.00 |           96.00 |           96.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           37.00 |           37.00 |           37.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,394.00 |        1,394.00 |        1,394.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   31,958,810.44 |   31,958,810.44 |   31,958,810.44 |            0.00 |
|TotalCollections [Gen0] |     collections |           68.86 |           68.86 |           68.86 |            0.00 |
|TotalCollections [Gen1] |     collections |           26.54 |           26.54 |           26.54 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.33 |            9.33 |            9.33 |            0.00 |
|    Elapsed Time |              ms |          999.96 |          999.96 |          999.96 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          126.97 |          126.97 |          126.97 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   44,552,560.00 |   31,958,810.44 |           31.29 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           96.00 |           68.86 |   14,521,478.12 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           37.00 |           26.54 |   37,677,348.65 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            9.33 |  107,235,530.77 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,394.00 |          999.96 |    1,000,044.40 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          126.97 |    7,876,055.93 |


