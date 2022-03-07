# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/07/2022 04:55:20_
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
|    Elapsed Time |              ms |        4,057.00 |        4,044.50 |        4,032.00 |           17.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,092,406.47 |   11,490,544.28 |      888,682.10 |   14,993,297.28 |
|TotalCollections [Gen0] |     collections |            4.22 |            3.71 |            3.20 |            0.72 |
|TotalCollections [Gen1] |     collections |            4.22 |            3.71 |            3.20 |            0.72 |
|TotalCollections [Gen2] |     collections |            4.22 |            3.71 |            3.20 |            0.72 |
|    Elapsed Time |              ms |        1,000.05 |        1,000.01 |          999.97 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.63 |           14.59 |           14.54 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,092,406.47 |           45.26 |
|               2 |    3,583,264.00 |      888,682.10 |        1,125.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.20 |  312,060,415.38 |
|               2 |           17.00 |            4.22 |  237,182,941.18 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.20 |  312,060,415.38 |
|               2 |           17.00 |            4.22 |  237,182,941.18 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.20 |  312,060,415.38 |
|               2 |           17.00 |            4.22 |  237,182,941.18 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,057.00 |        1,000.05 |      999,947.10 |
|               2 |        4,032.00 |          999.97 |    1,000,027.28 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.54 |   68,759,074.58 |
|               2 |           59.00 |           14.63 |   68,340,847.46 |


