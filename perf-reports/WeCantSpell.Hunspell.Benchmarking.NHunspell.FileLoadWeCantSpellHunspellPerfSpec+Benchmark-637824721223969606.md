# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/10/2022 01:22:02_
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
|TotalBytesAllocated |           bytes |   30,697,216.00 |   30,697,048.00 |   30,696,880.00 |          237.59 |
|TotalCollections [Gen0] |     collections |          505.00 |          505.00 |          505.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          212.00 |          212.00 |          212.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           69.00 |           69.00 |           69.00 |            0.00 |
|    Elapsed Time |              ms |       14,914.00 |       14,857.00 |       14,800.00 |           80.61 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    2,074,146.08 |    2,066,164.70 |    2,058,183.31 |       11,287.38 |
|TotalCollections [Gen0] |     collections |           34.12 |           33.99 |           33.86 |            0.19 |
|TotalCollections [Gen1] |     collections |           14.32 |           14.27 |           14.21 |            0.08 |
|TotalCollections [Gen2] |     collections |            4.66 |            4.64 |            4.63 |            0.03 |
|    Elapsed Time |              ms |        1,000.00 |          999.98 |          999.96 |            0.03 |
|[Counter] FilePairsLoaded |      operations |            3.99 |            3.97 |            3.96 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   30,696,880.00 |    2,058,183.31 |          485.87 |
|               2 |   30,697,216.00 |    2,074,146.08 |          482.13 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          505.00 |           33.86 |   29,533,764.36 |
|               2 |          505.00 |           34.12 |   29,306,791.29 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          212.00 |           14.21 |   70,351,655.66 |
|               2 |          212.00 |           14.32 |   69,810,988.68 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           69.00 |            4.63 |  216,152,913.04 |
|               2 |           69.00 |            4.66 |  214,491,733.33 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       14,914.00 |          999.96 |    1,000,036.95 |
|               2 |       14,800.00 |        1,000.00 |      999,995.24 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.96 |  252,789,000.00 |
|               2 |           59.00 |            3.99 |  250,846,264.41 |


