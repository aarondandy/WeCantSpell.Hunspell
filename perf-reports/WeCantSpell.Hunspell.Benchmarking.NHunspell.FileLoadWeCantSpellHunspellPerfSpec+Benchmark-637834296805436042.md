# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/21/2022 03:21:20_
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
|TotalBytesAllocated |           bytes |  107,048,216.00 |  106,908,996.00 |  106,769,776.00 |      196,886.81 |
|TotalCollections [Gen0] |     collections |          484.00 |          483.50 |          483.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          186.00 |          185.50 |          185.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           42.00 |           41.50 |           41.00 |            0.71 |
|    Elapsed Time |              ms |       17,076.00 |       17,008.50 |       16,941.00 |           95.46 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    6,318,913.67 |    6,285,822.46 |    6,252,731.24 |       46,798.04 |
|TotalCollections [Gen0] |     collections |           28.51 |           28.43 |           28.34 |            0.12 |
|TotalCollections [Gen1] |     collections |           10.92 |           10.91 |           10.89 |            0.02 |
|TotalCollections [Gen2] |     collections |            2.46 |            2.44 |            2.42 |            0.03 |
|    Elapsed Time |              ms |        1,000.02 |        1,000.01 |        1,000.00 |            0.01 |
|[Counter] FilePairsLoaded |      operations |            3.48 |            3.47 |            3.46 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  106,769,776.00 |    6,252,731.24 |          159.93 |
|               2 |  107,048,216.00 |    6,318,913.67 |          158.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          484.00 |           28.34 |   35,280,376.24 |
|               2 |          483.00 |           28.51 |   35,074,370.19 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          186.00 |           10.89 |   91,804,850.00 |
|               2 |          185.00 |           10.92 |   91,572,544.86 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           42.00 |            2.46 |  406,564,335.71 |
|               2 |           41.00 |            2.42 |  413,193,190.24 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,076.00 |        1,000.02 |      999,982.55 |
|               2 |       16,941.00 |        1,000.00 |      999,995.32 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.46 |  289,418,679.66 |
|               2 |           59.00 |            3.48 |  287,134,250.85 |


