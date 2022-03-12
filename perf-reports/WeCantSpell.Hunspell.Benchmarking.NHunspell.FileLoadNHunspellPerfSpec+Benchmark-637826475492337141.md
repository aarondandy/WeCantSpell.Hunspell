# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/12/2022 02:05:49_
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
|    Elapsed Time |              ms |        4,024.00 |        4,005.50 |        3,987.00 |           26.16 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,483,775.65 |   22,378,803.88 |   22,273,832.10 |      148,452.51 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.25 |            3.23 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.25 |            3.23 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.25 |            3.23 |            0.02 |
|    Elapsed Time |              ms |        1,000.21 |        1,000.14 |        1,000.06 |            0.10 |
|[Counter] FilePairsLoaded |      operations |           14.80 |           14.73 |           14.66 |            0.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,483,775.65 |           44.48 |
|               2 |   89,624,176.00 |   22,273,832.10 |           44.90 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,628,461.54 |
|               2 |           13.00 |            3.23 |  309,518,692.31 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,628,461.54 |
|               2 |           13.00 |            3.23 |  309,518,692.31 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.26 |  306,628,461.54 |
|               2 |           13.00 |            3.23 |  309,518,692.31 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,987.00 |        1,000.21 |      999,791.82 |
|               2 |        4,024.00 |        1,000.06 |      999,936.13 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.80 |   67,562,203.39 |
|               2 |           59.00 |           14.66 |   68,199,033.90 |


