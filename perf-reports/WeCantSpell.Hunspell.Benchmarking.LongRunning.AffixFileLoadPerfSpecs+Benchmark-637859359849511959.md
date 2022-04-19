# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_4/19/2022 3:33:04 AM_
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
|TotalBytesAllocated |           bytes |    7,427,864.00 |    7,427,864.00 |    7,427,864.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           78.00 |           78.00 |           78.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           31.00 |           31.00 |           31.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        1,061.00 |        1,061.00 |        1,061.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,000,301.67 |    7,000,301.67 |    7,000,301.67 |            0.00 |
|TotalCollections [Gen0] |     collections |           73.51 |           73.51 |           73.51 |            0.00 |
|TotalCollections [Gen1] |     collections |           29.22 |           29.22 |           29.22 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.48 |            8.48 |            8.48 |            0.00 |
|    Elapsed Time |              ms |          999.93 |          999.93 |          999.93 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          166.81 |          166.81 |          166.81 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,427,864.00 |    7,000,301.67 |          142.85 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           78.00 |           73.51 |   13,603,560.26 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           31.00 |           29.22 |   34,228,312.90 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            8.48 |  117,897,522.22 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,061.00 |          999.93 |    1,000,073.23 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          166.81 |    5,994,789.27 |


