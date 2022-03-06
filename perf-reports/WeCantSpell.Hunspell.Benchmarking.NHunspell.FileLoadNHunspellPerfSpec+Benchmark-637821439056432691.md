# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/06/2022 06:11:45_
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
|TotalBytesAllocated |           bytes |   89,624,152.00 |   46,634,952.00 |    3,645,752.00 |   60,795,909.68 |
|TotalCollections [Gen0] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen1] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|    Elapsed Time |              ms |        4,038.00 |        4,025.50 |        4,013.00 |           17.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,333,163.77 |   11,618,079.46 |      902,995.15 |   15,153,417.55 |
|TotalCollections [Gen0] |     collections |            4.21 |            3.73 |            3.24 |            0.69 |
|TotalCollections [Gen1] |     collections |            4.21 |            3.73 |            3.24 |            0.69 |
|TotalCollections [Gen2] |     collections |            4.21 |            3.73 |            3.24 |            0.69 |
|    Elapsed Time |              ms |        1,000.15 |        1,000.07 |          999.99 |            0.11 |
|[Counter] FilePairsLoaded |      operations |           14.70 |           14.66 |           14.61 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,333,163.77 |           44.78 |
|               2 |    3,645,752.00 |      902,995.15 |        1,107.43 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,696,323.08 |
|               2 |           17.00 |            4.21 |  237,494,076.47 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,696,323.08 |
|               2 |           17.00 |            4.21 |  237,494,076.47 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,696,323.08 |
|               2 |           17.00 |            4.21 |  237,494,076.47 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,013.00 |          999.99 |    1,000,013.01 |
|               2 |        4,038.00 |        1,000.15 |      999,851.24 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.70 |   68,017,833.90 |
|               2 |           59.00 |           14.61 |   68,430,496.61 |


