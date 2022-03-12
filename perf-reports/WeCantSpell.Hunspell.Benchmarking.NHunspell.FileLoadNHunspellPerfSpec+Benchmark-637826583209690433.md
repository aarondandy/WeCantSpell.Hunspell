# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/12/2022 05:05:20_
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
|TotalBytesAllocated |           bytes |   89,624,152.00 |   46,603,708.00 |    3,583,264.00 |   60,840,095.36 |
|TotalCollections [Gen0] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen1] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|    Elapsed Time |              ms |        4,067.00 |        4,051.00 |        4,035.00 |           22.63 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,211,718.78 |   11,546,420.77 |      881,122.77 |   15,083,009.08 |
|TotalCollections [Gen0] |     collections |            4.18 |            3.70 |            3.22 |            0.68 |
|TotalCollections [Gen1] |     collections |            4.18 |            3.70 |            3.22 |            0.68 |
|TotalCollections [Gen2] |     collections |            4.18 |            3.70 |            3.22 |            0.68 |
|    Elapsed Time |              ms |        1,000.07 |        1,000.04 |        1,000.00 |            0.05 |
|[Counter] FilePairsLoaded |      operations |           14.62 |           14.57 |           14.51 |            0.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,211,718.78 |           45.02 |
|               2 |    3,583,264.00 |      881,122.77 |        1,134.92 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,384,153.85 |
|               2 |           17.00 |            4.18 |  239,217,782.35 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,384,153.85 |
|               2 |           17.00 |            4.18 |  239,217,782.35 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,384,153.85 |
|               2 |           17.00 |            4.18 |  239,217,782.35 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,035.00 |        1,000.00 |      999,998.51 |
|               2 |        4,067.00 |        1,000.07 |      999,926.80 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.62 |   68,389,728.81 |
|               2 |           59.00 |           14.51 |   68,927,157.63 |


