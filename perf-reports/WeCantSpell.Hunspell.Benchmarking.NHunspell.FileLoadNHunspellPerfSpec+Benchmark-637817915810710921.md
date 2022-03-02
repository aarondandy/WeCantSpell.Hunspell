# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/02/2022 04:19:41_
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
|TotalBytesAllocated |           bytes |   89,624,176.00 |   89,624,164.00 |   89,624,152.00 |           16.97 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        4,076.00 |        4,047.50 |        4,019.00 |           40.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,297,655.65 |   22,145,216.44 |   21,992,777.22 |      215,581.61 |
|TotalCollections [Gen0] |     collections |            3.23 |            3.21 |            3.19 |            0.03 |
|TotalCollections [Gen1] |     collections |            3.23 |            3.21 |            3.19 |            0.03 |
|TotalCollections [Gen2] |     collections |            3.23 |            3.21 |            3.19 |            0.03 |
|    Elapsed Time |              ms |        1,000.21 |        1,000.05 |          999.89 |            0.22 |
|[Counter] FilePairsLoaded |      operations |           14.68 |           14.58 |           14.48 |            0.14 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   21,992,777.22 |           45.47 |
|               2 |   89,624,176.00 |   22,297,655.65 |           44.85 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.19 |  313,474,076.92 |
|               2 |           13.00 |            3.23 |  309,187,992.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.19 |  313,474,076.92 |
|               2 |           13.00 |            3.23 |  309,187,992.31 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.19 |  313,474,076.92 |
|               2 |           13.00 |            3.23 |  309,187,992.31 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,076.00 |        1,000.21 |      999,794.65 |
|               2 |        4,019.00 |          999.89 |    1,000,110.45 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.48 |   69,070,559.32 |
|               2 |           59.00 |           14.68 |   68,126,167.80 |


