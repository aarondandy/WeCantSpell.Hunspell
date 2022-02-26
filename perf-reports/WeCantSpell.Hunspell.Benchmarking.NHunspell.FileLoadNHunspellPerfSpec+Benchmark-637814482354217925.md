# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/26/2022 04:57:15_
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
|TotalBytesAllocated |           bytes |   89,624,176.00 |   89,624,164.00 |   89,624,152.00 |           16.97 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        4,038.00 |        3,983.00 |        3,928.00 |           77.78 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,815,523.02 |   22,505,901.10 |   22,196,279.17 |      437,871.52 |
|TotalCollections [Gen0] |     collections |            3.31 |            3.26 |            3.22 |            0.06 |
|TotalCollections [Gen1] |     collections |            3.31 |            3.26 |            3.22 |            0.06 |
|TotalCollections [Gen2] |     collections |            3.31 |            3.26 |            3.22 |            0.06 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.00 |          999.95 |            0.07 |
|[Counter] FilePairsLoaded |      operations |           15.02 |           14.82 |           14.61 |            0.29 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,815,523.02 |           43.83 |
|               2 |   89,624,176.00 |   22,196,279.17 |           45.05 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  302,169,953.85 |
|               2 |           13.00 |            3.22 |  310,600,138.46 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  302,169,953.85 |
|               2 |           13.00 |            3.22 |  310,600,138.46 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  302,169,953.85 |
|               2 |           13.00 |            3.22 |  310,600,138.46 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,928.00 |          999.95 |    1,000,053.31 |
|               2 |        4,038.00 |        1,000.05 |      999,950.92 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           15.02 |   66,579,820.34 |
|               2 |           59.00 |           14.61 |   68,437,318.64 |


