# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/13/2022 22:16:11_
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
|    Elapsed Time |              ms |        3,963.00 |        3,959.50 |        3,956.00 |            4.95 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,612,443.82 |   11,759,218.90 |      905,993.98 |   15,348,777.88 |
|TotalCollections [Gen0] |     collections |            4.30 |            3.79 |            3.28 |            0.72 |
|TotalCollections [Gen1] |     collections |            4.30 |            3.79 |            3.28 |            0.72 |
|TotalCollections [Gen2] |     collections |            4.30 |            3.79 |            3.28 |            0.72 |
|    Elapsed Time |              ms |        1,000.24 |        1,000.06 |          999.88 |            0.25 |
|[Counter] FilePairsLoaded |      operations |           14.92 |           14.90 |           14.89 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,612,443.82 |           44.22 |
|               2 |    3,583,264.00 |      905,993.98 |        1,103.76 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  304,883,700.00 |
|               2 |           17.00 |            4.30 |  232,650,811.76 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  304,883,700.00 |
|               2 |           17.00 |            4.30 |  232,650,811.76 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  304,883,700.00 |
|               2 |           17.00 |            4.30 |  232,650,811.76 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,963.00 |          999.88 |    1,000,123.16 |
|               2 |        3,956.00 |        1,000.24 |      999,763.35 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.89 |   67,177,764.41 |
|               2 |           59.00 |           14.92 |   67,034,979.66 |


