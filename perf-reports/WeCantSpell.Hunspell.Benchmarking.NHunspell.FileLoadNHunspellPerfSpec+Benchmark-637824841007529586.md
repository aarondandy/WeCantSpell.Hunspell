# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/10/2022 04:41:40_
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
|    Elapsed Time |              ms |        4,036.00 |        4,029.00 |        4,022.00 |            9.90 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,202,219.41 |   11,546,549.44 |      890,879.48 |   15,069,392.98 |
|TotalCollections [Gen0] |     collections |            4.23 |            3.72 |            3.22 |            0.71 |
|TotalCollections [Gen1] |     collections |            4.23 |            3.72 |            3.22 |            0.71 |
|TotalCollections [Gen2] |     collections |            4.23 |            3.72 |            3.22 |            0.71 |
|    Elapsed Time |              ms |          999.96 |          999.89 |          999.82 |            0.10 |
|[Counter] FilePairsLoaded |      operations |           14.67 |           14.64 |           14.62 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,202,219.41 |           45.04 |
|               2 |    3,583,264.00 |      890,879.48 |        1,122.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,516,953.85 |
|               2 |           17.00 |            4.23 |  236,597,923.53 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,516,953.85 |
|               2 |           17.00 |            4.23 |  236,597,923.53 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,516,953.85 |
|               2 |           17.00 |            4.23 |  236,597,923.53 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,036.00 |          999.82 |    1,000,178.49 |
|               2 |        4,022.00 |          999.96 |    1,000,040.95 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.62 |   68,418,989.83 |
|               2 |           59.00 |           14.67 |   68,172,283.05 |


