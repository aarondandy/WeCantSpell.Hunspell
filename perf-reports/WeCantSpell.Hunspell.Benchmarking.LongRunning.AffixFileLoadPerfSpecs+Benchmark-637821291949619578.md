# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/6/2022 2:06:34 AM_
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
|TotalBytesAllocated |           bytes |   10,650,048.00 |   10,650,048.00 |   10,650,048.00 |            0.00 |
|TotalCollections [Gen0] |     collections |           41.00 |           41.00 |           41.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           14.00 |           14.00 |           14.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|    Elapsed Time |              ms |        1,374.00 |        1,374.00 |        1,374.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,749,823.16 |    7,749,823.16 |    7,749,823.16 |            0.00 |
|TotalCollections [Gen0] |     collections |           29.83 |           29.83 |           29.83 |            0.00 |
|TotalCollections [Gen1] |     collections |           10.19 |           10.19 |           10.19 |            0.00 |
|TotalCollections [Gen2] |     collections |            4.37 |            4.37 |            4.37 |            0.00 |
|    Elapsed Time |              ms |          999.83 |          999.83 |          999.83 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           42.93 |           42.93 |           42.93 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   10,650,048.00 |    7,749,823.16 |          129.04 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           41.00 |           29.83 |   33,517,831.71 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           14.00 |           10.19 |   98,159,364.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            4.37 |  229,038,516.67 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,374.00 |          999.83 |    1,000,168.20 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           42.93 |   23,292,052.54 |


