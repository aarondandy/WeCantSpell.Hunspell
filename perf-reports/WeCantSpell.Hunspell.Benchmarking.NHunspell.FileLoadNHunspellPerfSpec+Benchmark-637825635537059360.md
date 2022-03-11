# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/11/2022 02:45:53_
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
|TotalBytesAllocated |           bytes |   89,624,184.00 |   51,294,144.00 |   12,964,104.00 |   54,206,862.41 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        3,994.00 |        3,983.50 |        3,973.00 |           14.85 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,557,154.69 |   12,901,830.31 |    3,246,505.94 |   13,654,690.68 |
|TotalCollections [Gen0] |     collections |            4.01 |            3.64 |            3.27 |            0.52 |
|TotalCollections [Gen1] |     collections |            4.01 |            3.64 |            3.27 |            0.52 |
|TotalCollections [Gen2] |     collections |            4.01 |            3.64 |            3.27 |            0.52 |
|    Elapsed Time |              ms |        1,000.19 |        1,000.07 |          999.95 |            0.17 |
|[Counter] FilePairsLoaded |      operations |           14.85 |           14.81 |           14.77 |            0.05 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,184.00 |   22,557,154.69 |           44.33 |
|               2 |   12,964,104.00 |    3,246,505.94 |          308.02 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,631,100.00 |
|               2 |           16.00 |            4.01 |  249,578,012.50 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,631,100.00 |
|               2 |           16.00 |            4.01 |  249,578,012.50 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.27 |  305,631,100.00 |
|               2 |           16.00 |            4.01 |  249,578,012.50 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,973.00 |          999.95 |    1,000,051.42 |
|               2 |        3,994.00 |        1,000.19 |      999,811.77 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.85 |   67,342,445.76 |
|               2 |           59.00 |           14.77 |   67,682,172.88 |


