# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/27/2022 22:59:39_
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
|    Elapsed Time |              ms |        3,956.00 |        3,950.00 |        3,944.00 |            8.49 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,657,095.61 |   11,782,891.70 |      908,687.79 |   15,378,446.65 |
|TotalCollections [Gen0] |     collections |            4.31 |            3.80 |            3.29 |            0.72 |
|TotalCollections [Gen1] |     collections |            4.31 |            3.80 |            3.29 |            0.72 |
|TotalCollections [Gen2] |     collections |            4.31 |            3.80 |            3.29 |            0.72 |
|    Elapsed Time |              ms |        1,000.17 |        1,000.12 |        1,000.08 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.96 |           14.94 |           14.92 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,657,095.61 |           44.14 |
|               2 |    3,583,264.00 |      908,687.79 |        1,100.49 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,282,846.15 |
|               2 |           17.00 |            4.31 |  231,961,117.65 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,282,846.15 |
|               2 |           17.00 |            4.31 |  231,961,117.65 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.29 |  304,282,846.15 |
|               2 |           17.00 |            4.31 |  231,961,117.65 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,956.00 |        1,000.08 |      999,918.35 |
|               2 |        3,944.00 |        1,000.17 |      999,832.40 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.92 |   67,045,372.88 |
|               2 |           59.00 |           14.96 |   66,836,254.24 |


