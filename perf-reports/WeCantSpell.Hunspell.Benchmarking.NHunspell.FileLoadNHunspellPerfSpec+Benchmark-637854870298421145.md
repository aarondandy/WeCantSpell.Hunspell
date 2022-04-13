# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/13/2022 22:50:29_
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
|    Elapsed Time |              ms |        4,016.00 |        3,986.50 |        3,957.00 |           41.72 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,648,780.27 |   11,770,490.71 |      892,201.16 |   15,384,224.62 |
|TotalCollections [Gen0] |     collections |            4.23 |            3.76 |            3.29 |            0.67 |
|TotalCollections [Gen1] |     collections |            4.23 |            3.76 |            3.29 |            0.67 |
|TotalCollections [Gen2] |     collections |            4.23 |            3.76 |            3.29 |            0.67 |
|    Elapsed Time |              ms |          999.97 |          999.96 |          999.95 |            0.01 |
|[Counter] FilePairsLoaded |      operations |           14.91 |           14.80 |           14.69 |            0.16 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,648,780.27 |           44.15 |
|               2 |    3,583,264.00 |      892,201.16 |        1,120.82 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,394,561.54 |
|               2 |           17.00 |            4.23 |  236,247,435.29 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,394,561.54 |
|               2 |           17.00 |            4.23 |  236,247,435.29 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,394,561.54 |
|               2 |           17.00 |            4.23 |  236,247,435.29 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,957.00 |          999.97 |    1,000,032.68 |
|               2 |        4,016.00 |          999.95 |    1,000,051.39 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.91 |   67,069,988.14 |
|               2 |           59.00 |           14.69 |   68,071,294.92 |


