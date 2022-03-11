# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/11/2022 03:57:28_
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
|TotalBytesAllocated |           bytes |   89,624,184.00 |   46,603,724.00 |    3,583,264.00 |   60,840,117.99 |
|TotalCollections [Gen0] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen1] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|TotalCollections [Gen2] |     collections |           17.00 |           15.00 |           13.00 |            2.83 |
|    Elapsed Time |              ms |        3,998.00 |        3,987.50 |        3,977.00 |           14.85 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,535,764.11 |   11,715,998.14 |      896,232.17 |   15,301,459.78 |
|TotalCollections [Gen0] |     collections |            4.25 |            3.76 |            3.27 |            0.70 |
|TotalCollections [Gen1] |     collections |            4.25 |            3.76 |            3.27 |            0.70 |
|TotalCollections [Gen2] |     collections |            4.25 |            3.76 |            3.27 |            0.70 |
|    Elapsed Time |              ms |        1,000.01 |          999.99 |          999.96 |            0.03 |
|[Counter] FilePairsLoaded |      operations |           14.84 |           14.80 |           14.76 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,184.00 |   22,535,764.11 |           44.37 |
|               2 |    3,583,264.00 |      896,232.17 |        1,115.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,921,200.00 |
|               2 |           17.00 |            4.25 |  235,184,858.82 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,921,200.00 |
|               2 |           17.00 |            4.25 |  235,184,858.82 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,921,200.00 |
|               2 |           17.00 |            4.25 |  235,184,858.82 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,977.00 |        1,000.01 |      999,993.86 |
|               2 |        3,998.00 |          999.96 |    1,000,035.67 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.84 |   67,406,366.10 |
|               2 |           59.00 |           14.76 |   67,765,128.81 |


