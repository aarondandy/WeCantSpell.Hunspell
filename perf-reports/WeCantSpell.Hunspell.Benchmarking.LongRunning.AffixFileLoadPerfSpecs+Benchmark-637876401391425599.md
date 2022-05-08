# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_5/8/2022 8:55:39 PM_
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
|TotalBytesAllocated |           bytes |    9,988,936.00 |    9,988,936.00 |    9,988,936.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           22.00 |           22.00 |           22.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           12.00 |           12.00 |           12.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.00 |            4.00 |            4.00 |            0.00 |
|    Elapsed Time |              ms |        1,117.00 |        1,117.00 |        1,117.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,938,110.90 |    8,938,110.90 |    8,938,110.90 |            0.00 |
|TotalCollections [Gen0] |     collections |           19.69 |           19.69 |           19.69 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.74 |           10.74 |           10.74 |            0.00 |
|TotalCollections [Gen2] |     collections |            3.58 |            3.58 |            3.58 |            0.00 |
|    Elapsed Time |              ms |          999.49 |          999.49 |          999.49 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           52.79 |           52.79 |           52.79 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    9,988,936.00 |    8,938,110.90 |          111.88 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           22.00 |           19.69 |   50,798,490.91 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           12.00 |           10.74 |   93,130,566.67 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            4.00 |            3.58 |  279,391,700.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,117.00 |          999.49 |    1,000,507.43 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           52.79 |   18,941,810.17 |


