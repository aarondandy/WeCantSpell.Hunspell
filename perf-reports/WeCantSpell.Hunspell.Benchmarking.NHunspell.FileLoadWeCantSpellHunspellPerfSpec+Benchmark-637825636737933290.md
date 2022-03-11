# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/11/2022 02:47:53_
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
|TotalBytesAllocated |           bytes |   30,696,216.00 |   30,695,148.00 |   30,694,080.00 |        1,510.38 |
|TotalCollections [Gen0] |     collections |          505.00 |          502.00 |          499.00 |            4.24 |
|TotalCollections [Gen1] |     collections |          212.00 |          209.00 |          206.00 |            4.24 |
|TotalCollections [Gen2] |     collections |           69.00 |           65.50 |           62.00 |            4.95 |
|    Elapsed Time |              ms |       15,083.00 |       14,985.50 |       14,888.00 |          137.89 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,061,610.71 |    2,048,326.65 |    2,035,042.58 |       18,786.51 |
|TotalCollections [Gen0] |     collections |           33.52 |           33.50 |           33.48 |            0.03 |
|TotalCollections [Gen1] |     collections |           14.05 |           13.95 |           13.84 |            0.15 |
|TotalCollections [Gen2] |     collections |            4.57 |            4.37 |            4.16 |            0.29 |
|    Elapsed Time |              ms |          999.97 |          999.96 |          999.95 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.96 |            3.94 |            3.91 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,694,080.00 |    2,061,610.71 |          485.06 |
|               2 |   30,696,216.00 |    2,035,042.58 |          491.39 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          499.00 |           33.52 |   29,836,468.14 |
|               2 |          505.00 |           33.48 |   29,868,950.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          206.00 |           13.84 |   72,273,774.76 |
|               2 |          212.00 |           14.05 |   71,150,094.34 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           62.00 |            4.16 |  240,135,445.16 |
|               2 |           69.00 |            4.57 |  218,606,086.96 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,888.00 |          999.97 |    1,000,026.71 |
|               2 |       15,083.00 |          999.95 |    1,000,054.37 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.96 |  252,345,722.03 |
|               2 |           59.00 |            3.91 |  255,657,966.10 |


