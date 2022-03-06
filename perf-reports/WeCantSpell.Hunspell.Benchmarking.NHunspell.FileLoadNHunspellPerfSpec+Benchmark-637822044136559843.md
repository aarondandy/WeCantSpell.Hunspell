# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/06/2022 23:00:13_
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
|TotalBytesAllocated |           bytes |   89,624,152.00 |   51,485,656.00 |   13,347,160.00 |   53,935,978.29 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        4,068.00 |        4,050.00 |        4,032.00 |           25.46 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,225,784.11 |   12,753,401.92 |    3,281,019.72 |   13,395,971.37 |
|TotalCollections [Gen0] |     collections |            3.93 |            3.58 |            3.22 |            0.50 |
|TotalCollections [Gen1] |     collections |            3.93 |            3.58 |            3.22 |            0.50 |
|TotalCollections [Gen2] |     collections |            3.93 |            3.58 |            3.22 |            0.50 |
|    Elapsed Time |              ms |        1,000.00 |          999.95 |          999.89 |            0.08 |
|[Counter] FilePairsLoaded |      operations |           14.63 |           14.57 |           14.50 |            0.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,225,784.11 |           44.99 |
|               2 |   13,347,160.00 |    3,281,019.72 |          304.78 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,187,730.77 |
|               2 |           16.00 |            3.93 |  254,249,462.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,187,730.77 |
|               2 |           16.00 |            3.93 |  254,249,462.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.22 |  310,187,730.77 |
|               2 |           16.00 |            3.93 |  254,249,462.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,032.00 |          999.89 |    1,000,109.25 |
|               2 |        4,068.00 |        1,000.00 |      999,997.89 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.63 |   68,346,449.15 |
|               2 |           59.00 |           14.50 |   68,949,006.78 |


