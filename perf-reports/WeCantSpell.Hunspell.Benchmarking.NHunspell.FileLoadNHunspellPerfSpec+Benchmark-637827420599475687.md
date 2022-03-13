# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/13/2022 04:20:59_
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
|    Elapsed Time |              ms |        4,012.00 |        4,002.00 |        3,992.00 |           14.14 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,339,611.72 |   11,618,631.60 |      897,651.47 |   15,161,755.49 |
|TotalCollections [Gen0] |     collections |            4.26 |            3.75 |            3.24 |            0.72 |
|TotalCollections [Gen1] |     collections |            4.26 |            3.75 |            3.24 |            0.72 |
|TotalCollections [Gen2] |     collections |            4.26 |            3.75 |            3.24 |            0.72 |
|    Elapsed Time |              ms |        1,000.04 |        1,000.04 |        1,000.03 |            0.01 |
|[Counter] FilePairsLoaded |      operations |           14.78 |           14.74 |           14.71 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,339,611.72 |           44.76 |
|               2 |    3,583,264.00 |      897,651.47 |        1,114.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,607,223.08 |
|               2 |           17.00 |            4.26 |  234,813,000.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,607,223.08 |
|               2 |           17.00 |            4.26 |  234,813,000.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,607,223.08 |
|               2 |           17.00 |            4.26 |  234,813,000.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,012.00 |        1,000.03 |      999,973.55 |
|               2 |        3,992.00 |        1,000.04 |      999,955.16 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.71 |   67,998,201.69 |
|               2 |           59.00 |           14.78 |   67,657,983.05 |


