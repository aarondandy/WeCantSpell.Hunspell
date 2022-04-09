# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_04/09/2022 14:44:58_
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
|TotalBytesAllocated |           bytes |  119,487,056.00 |  119,220,116.00 |  118,953,176.00 |      377,510.17 |
|TotalCollections [Gen0] |     collections |          486.00 |          486.00 |          486.00 |            0.00 |
|TotalCollections [Gen1] |     collections |          191.00 |          190.50 |          190.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           48.00 |           47.50 |           47.00 |            0.71 |
|    Elapsed Time |              ms |       18,176.00 |       18,148.00 |       18,120.00 |           39.60 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,594,060.68 |    6,569,287.95 |    6,544,515.22 |       35,033.93 |
|TotalCollections [Gen0] |     collections |           26.82 |           26.78 |           26.74 |            0.06 |
|TotalCollections [Gen1] |     collections |           10.51 |           10.50 |           10.49 |            0.02 |
|TotalCollections [Gen2] |     collections |            2.65 |            2.62 |            2.59 |            0.04 |
|    Elapsed Time |              ms |        1,000.00 |          999.99 |          999.98 |            0.02 |
|[Counter] FilePairsLoaded |      operations |            3.26 |            3.25 |            3.25 |            0.01 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  119,487,056.00 |    6,594,060.68 |          151.65 |
|               2 |  118,953,176.00 |    6,544,515.22 |          152.80 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          486.00 |           26.82 |   37,284,785.80 |
|               2 |          486.00 |           26.74 |   37,399,198.15 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          190.00 |           10.49 |   95,370,557.37 |
|               2 |          191.00 |           10.51 |   95,162,357.59 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           48.00 |            2.65 |  377,508,456.25 |
|               2 |           47.00 |            2.59 |  386,723,623.40 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       18,120.00 |          999.98 |    1,000,022.40 |
|               2 |       18,176.00 |        1,000.00 |    1,000,000.57 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.26 |  307,125,523.73 |
|               2 |           59.00 |            3.25 |  308,067,971.19 |


