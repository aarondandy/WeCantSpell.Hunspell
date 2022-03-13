# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/13/2022 04:23:01_
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
|TotalBytesAllocated |           bytes |   30,694,880.00 |   30,691,596.00 |   30,688,312.00 |        4,644.28 |
|TotalCollections [Gen0] |     collections |          498.00 |          497.50 |          497.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          205.00 |          204.50 |          204.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           62.00 |           62.00 |           62.00 |            0.00 |
|    Elapsed Time |              ms |       15,284.00 |       15,212.50 |       15,141.00 |          101.12 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,026,940.22 |    2,017,593.74 |    2,008,247.25 |       13,217.93 |
|TotalCollections [Gen0] |     collections |           32.89 |           32.70 |           32.52 |            0.27 |
|TotalCollections [Gen1] |     collections |           13.54 |           13.44 |           13.35 |            0.14 |
|TotalCollections [Gen2] |     collections |            4.10 |            4.08 |            4.06 |            0.03 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.01 |          999.97 |            0.06 |
|[Counter] FilePairsLoaded |      operations |            3.90 |            3.88 |            3.86 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,688,312.00 |    2,026,940.22 |          493.35 |
|               2 |   30,694,880.00 |    2,008,247.25 |          497.95 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          498.00 |           32.89 |   30,402,039.36 |
|               2 |          497.00 |           32.52 |   30,753,345.67 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          205.00 |           13.54 |   73,854,710.24 |
|               2 |          204.00 |           13.35 |   74,923,592.16 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |            4.10 |  244,197,025.81 |
|               2 |           62.00 |            4.06 |  246,522,787.10 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,141.00 |        1,000.05 |      999,948.19 |
|               2 |       15,284.00 |          999.97 |    1,000,027.01 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.90 |  256,613,823.73 |
|               2 |           59.00 |            3.86 |  259,057,844.07 |


