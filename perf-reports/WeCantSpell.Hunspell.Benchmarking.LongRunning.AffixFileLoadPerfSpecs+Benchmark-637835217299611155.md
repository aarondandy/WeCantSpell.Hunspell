# WeCantSpell.Hunspell.Benchmarking.LongRunning.AffixFileLoadPerfSpecs+Benchmark
__Ensure that affix files can be loaded quickly.__
_3/22/2022 4:55:29 AM_
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
|TotalBytesAllocated |           bytes |   19,252,888.00 |   19,252,888.00 |   19,252,888.00 |            0.00 |
|TotalCollections [Gen0] |     collections |          101.00 |          101.00 |          101.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           40.00 |           40.00 |           40.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        1,458.00 |        1,458.00 |        1,458.00 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          177.00 |          177.00 |          177.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   13,202,463.18 |   13,202,463.18 |   13,202,463.18 |            0.00 |
|TotalCollections [Gen0] |     collections |           69.26 |           69.26 |           69.26 |            0.00 |
|TotalCollections [Gen1] |     collections |           27.43 |           27.43 |           27.43 |            0.00 |
|TotalCollections [Gen2] |     collections |            8.91 |            8.91 |            8.91 |            0.00 |
|    Elapsed Time |              ms |          999.81 |          999.81 |          999.81 |            0.00 |
|[Counter] AffixFilesLoaded |      operations |          121.38 |          121.38 |          121.38 |            0.00 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   19,252,888.00 |   13,202,463.18 |           75.74 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          101.00 |           69.26 |   14,438,415.84 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           40.00 |           27.43 |   36,457,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            8.91 |  112,175,384.62 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        1,458.00 |          999.81 |    1,000,192.04 |

#### [Counter] AffixFilesLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          177.00 |          121.38 |    8,238,870.06 |


