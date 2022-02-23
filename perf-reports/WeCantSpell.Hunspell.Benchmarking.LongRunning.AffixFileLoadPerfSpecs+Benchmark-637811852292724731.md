# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/23/2022 3:53:49 AM_
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
|TotalBytesAllocated |           bytes |    8,667,504.00 |    8,667,504.00 |    8,667,504.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          132.00 |          132.00 |          132.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           32.00 |           32.00 |           32.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            9.00 |            9.00 |            9.00 |            0.00 |
|    Elapsed Time |              ms |        1,872.00 |        1,872.00 |        1,872.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,629,981.95 |    4,629,981.95 |    4,629,981.95 |            0.00 |
|TotalCollections [Gen0] |     collections |           70.51 |           70.51 |           70.51 |            0.00 |
|TotalCollections [Gen1] |     collections |           17.09 |           17.09 |           17.09 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.81 |            4.81 |            4.81 |            0.00 |
|    Elapsed Time |              ms |          999.98 |          999.98 |          999.98 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           31.52 |           31.52 |           31.52 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    8,667,504.00 |    4,629,981.95 |          215.98 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          132.00 |           70.51 |   14,182,109.09 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           32.00 |           17.09 |   58,501,200.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            9.00 |            4.81 |  208,004,266.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,872.00 |          999.98 |    1,000,020.51 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           31.52 |   31,729,464.41 |


