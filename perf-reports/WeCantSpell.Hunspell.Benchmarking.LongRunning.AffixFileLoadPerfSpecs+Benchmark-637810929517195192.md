# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_2/22/2022 2:15:51 AM_
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
|TotalBytesAllocated |           bytes |    7,131,904.00 |    7,131,904.00 |    7,131,904.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          126.00 |          126.00 |          126.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           42.00 |           42.00 |           42.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           10.00 |           10.00 |           10.00 |            0.00 |
|    Elapsed Time |              ms |        1,762.00 |        1,762.00 |        1,762.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    4,045,936.86 |    4,045,936.86 |    4,045,936.86 |            0.00 |
|TotalCollections [Gen0] |     collections |           71.48 |           71.48 |           71.48 |            0.00 |
|TotalCollections [Gen1] |     collections |           23.83 |           23.83 |           23.83 |            0.00 |
|TotalCollections [Gen2] |     collections |            5.67 |            5.67 |            5.67 |            0.00 |
|    Elapsed Time |              ms |          999.58 |          999.58 |          999.58 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |           33.47 |           33.47 |           33.47 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |    7,131,904.00 |    4,045,936.86 |          247.16 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          126.00 |           71.48 |   13,989,939.68 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |           23.83 |   41,969,819.05 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           10.00 |            5.67 |  176,273,240.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,762.00 |          999.58 |    1,000,415.66 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           33.47 |   29,876,820.34 |


