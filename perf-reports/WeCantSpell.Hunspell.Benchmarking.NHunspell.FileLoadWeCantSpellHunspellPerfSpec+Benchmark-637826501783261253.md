# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/12/2022 02:49:38_
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
|TotalBytesAllocated |           bytes |  110,187,392.00 |   70,438,868.00 |   30,690,344.00 |   56,212,901.73 |
|TotalCollections [Gen0] |     collections |          503.00 |          500.50 |          498.00 |            3.54 |
|TotalCollections [Gen1] |     collections |          207.00 |          206.00 |          205.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           62.00 |           61.50 |           61.00 |            0.71 |
|    Elapsed Time |              ms |       15,930.00 |       15,771.50 |       15,613.00 |          224.15 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    7,057,544.77 |    4,492,065.65 |    1,926,586.54 |    3,628,135.36 |
|TotalCollections [Gen0] |     collections |           32.22 |           31.74 |           31.26 |            0.68 |
|TotalCollections [Gen1] |     collections |           13.26 |           13.06 |           12.87 |            0.28 |
|TotalCollections [Gen2] |     collections |            3.97 |            3.90 |            3.83 |            0.10 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.01 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.78 |            3.74 |            3.70 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,690,344.00 |    1,926,586.54 |          519.05 |
|               2 |  110,187,392.00 |    7,057,544.77 |          141.69 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          498.00 |           31.26 |   31,987,764.66 |
|               2 |          503.00 |           32.22 |   31,039,182.70 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          205.00 |           12.87 |   77,706,862.44 |
|               2 |          207.00 |           13.26 |   75,423,714.49 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           61.00 |            3.83 |  261,146,013.11 |
|               2 |           62.00 |            3.97 |  251,817,885.48 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       15,930.00 |        1,000.01 |      999,994.15 |
|               2 |       15,613.00 |        1,000.02 |      999,981.36 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.70 |  269,998,420.34 |
|               2 |           59.00 |            3.78 |  264,622,184.75 |


