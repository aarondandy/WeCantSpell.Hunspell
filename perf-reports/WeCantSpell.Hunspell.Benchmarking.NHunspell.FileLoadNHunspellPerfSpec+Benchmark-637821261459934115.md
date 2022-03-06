# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/06/2022 01:15:45_
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
|TotalBytesAllocated |           bytes |   13,295,312.00 |    8,439,288.00 |    3,583,264.00 |    6,867,455.00 |
|TotalCollections [Gen0] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|TotalCollections [Gen1] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|TotalCollections [Gen2] |     collections |           17.00 |           16.50 |           16.00 |            0.71 |
|    Elapsed Time |              ms |        4,135.00 |        4,096.00 |        4,057.00 |           55.15 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    3,215,159.93 |    2,049,134.92 |      883,109.90 |    1,649,008.39 |
|TotalCollections [Gen0] |     collections |            4.19 |            4.03 |            3.87 |            0.23 |
|TotalCollections [Gen1] |     collections |            4.19 |            4.03 |            3.87 |            0.23 |
|TotalCollections [Gen2] |     collections |            4.19 |            4.03 |            3.87 |            0.23 |
|    Elapsed Time |              ms |          999.95 |          999.91 |          999.86 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.54 |           14.40 |           14.27 |            0.19 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,295,312.00 |    3,215,159.93 |          311.03 |
|               2 |    3,583,264.00 |      883,109.90 |        1,132.36 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.87 |  258,449,662.50 |
|               2 |           17.00 |            4.19 |  238,679,505.88 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.87 |  258,449,662.50 |
|               2 |           17.00 |            4.19 |  238,679,505.88 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           16.00 |            3.87 |  258,449,662.50 |
|               2 |           17.00 |            4.19 |  238,679,505.88 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,135.00 |          999.95 |    1,000,047.06 |
|               2 |        4,057.00 |          999.86 |    1,000,135.96 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.27 |   70,088,044.07 |
|               2 |           59.00 |           14.54 |   68,772,061.02 |


