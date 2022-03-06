# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/06/2022 06:14:12_
### System Info
```ini
NBench=NBench, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
OS=Microsoft Windows NT 6.2.9200.0
ProcessorCount=16
CLR=4.0.30319.42000,IsMono=False,MaxGcGeneration=2
```

### NBench Settings
```ini
RunMode=Throughput, TestMode=Measurement
NumberOfIterations=2, MaximumRunTime=00:00:01
Concurrent=False
Tracing=False
```

## Data
-------------------

### Totals
|          Metric |           Units |             Max |         Average |             Min |          StdDev |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |  149,166,264.00 |   94,712,324.00 |   40,258,384.00 |   77,009,500.47 |
|TotalCollections [Gen0] |     collections |          733.00 |          732.00 |          731.00 |            1.41 |
|TotalCollections [Gen1] |     collections |          292.00 |          291.50 |          291.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           83.00 |           80.00 |           77.00 |            4.24 |
|    Elapsed Time |              ms |       18,238.00 |       18,218.00 |       18,198.00 |           28.28 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,196,895.09 |    5,202,145.58 |    2,207,396.08 |    4,235,215.36 |
|TotalCollections [Gen0] |     collections |           40.28 |           40.18 |           40.08 |            0.14 |
|TotalCollections [Gen1] |     collections |           16.05 |           16.00 |           15.96 |            0.06 |
|TotalCollections [Gen2] |     collections |            4.56 |            4.39 |            4.22 |            0.24 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |        1,000.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.24 |            3.24 |            3.24 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   40,258,384.00 |    2,207,396.08 |          453.02 |
|               2 |  149,166,264.00 |    8,196,895.09 |          122.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          731.00 |           40.08 |   24,949,318.88 |
|               2 |          733.00 |           40.28 |   24,826,600.82 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          291.00 |           15.96 |   62,673,374.91 |
|               2 |          292.00 |           16.05 |   62,321,569.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           77.00 |            4.22 |  236,856,520.78 |
|               2 |           83.00 |            4.56 |  219,251,787.95 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,238.00 |        1,000.00 |      999,997.37 |
|               2 |       18,198.00 |        1,000.01 |      999,994.42 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.24 |  309,117,832.20 |
|               2 |           59.00 |            3.24 |  308,438,955.93 |


