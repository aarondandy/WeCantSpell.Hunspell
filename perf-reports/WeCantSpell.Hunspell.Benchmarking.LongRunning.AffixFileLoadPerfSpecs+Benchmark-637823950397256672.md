# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/9/2022 3:57:19 AM_
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
|TotalBytesAllocated |           bytes |    4,287,112.00 |    4,287,112.00 |    4,287,112.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.00 |           66.00 |           66.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.00 |           27.00 |           27.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.00 |            7.00 |            7.00 |            0.00 |
|    Elapsed Time |              ms |          993.00 |          993.00 |          993.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.00 |          118.00 |          118.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,317,822.08 |    4,317,822.08 |    4,317,822.08 |            0.00 |
|TotalCollections [Gen0] |     collections |           66.47 |           66.47 |           66.47 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.19 |           27.19 |           27.19 |            0.00 |
|TotalCollections [Gen2] |     collections |            7.05 |            7.05 |            7.05 |            0.00 |
|    Elapsed Time |              ms |        1,000.11 |        1,000.11 |        1,000.11 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          118.85 |          118.85 |          118.85 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    4,287,112.00 |    4,317,822.08 |          231.60 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           66.00 |           66.47 |   15,043,751.52 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           27.00 |           27.19 |   36,773,614.81 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            7.00 |            7.05 |  141,841,085.71 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          993.00 |        1,000.11 |      999,886.81 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          118.00 |          118.85 |    8,414,301.69 |


