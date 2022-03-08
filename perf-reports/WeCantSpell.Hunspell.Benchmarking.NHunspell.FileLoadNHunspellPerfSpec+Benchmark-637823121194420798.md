# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/08/2022 04:55:19_
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
|TotalBytesAllocated |           bytes |   89,624,184.00 |   46,734,524.00 |    3,844,864.00 |   60,655,138.86 |
|TotalCollections [Gen0] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen1] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|TotalCollections [Gen2] |     collections |           16.00 |           14.50 |           13.00 |            2.12 |
|    Elapsed Time |              ms |        4,032.00 |        4,024.00 |        4,016.00 |           11.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,311,838.72 |   11,632,716.50 |      953,594.28 |   15,102,559.48 |
|TotalCollections [Gen0] |     collections |            3.97 |            3.60 |            3.24 |            0.52 |
|TotalCollections [Gen1] |     collections |            3.97 |            3.60 |            3.24 |            0.52 |
|TotalCollections [Gen2] |     collections |            3.97 |            3.60 |            3.24 |            0.52 |
|    Elapsed Time |              ms |        1,000.01 |          999.89 |          999.78 |            0.16 |
|[Counter] FilePairsLoaded |      operations |           14.69 |           14.66 |           14.63 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,184.00 |   22,311,838.72 |           44.82 |
|               2 |    3,844,864.00 |      953,594.28 |        1,048.66 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,991,476.92 |
|               2 |           16.00 |            3.97 |  251,998,156.25 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,991,476.92 |
|               2 |           16.00 |            3.97 |  251,998,156.25 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,991,476.92 |
|               2 |           16.00 |            3.97 |  251,998,156.25 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,016.00 |          999.78 |    1,000,221.41 |
|               2 |        4,032.00 |        1,000.01 |      999,992.68 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.69 |   68,082,867.80 |
|               2 |           59.00 |           14.63 |   68,338,483.05 |


