# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/21/2022 13:45:02_
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
|TotalBytesAllocated |           bytes |   13,686,784.00 |   13,686,772.00 |   13,686,760.00 |           16.97 |
|TotalCollections [Gen0] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|TotalCollections [Gen1] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|TotalCollections [Gen2] |     collections |            6.00 |            6.00 |            6.00 |            0.00 |
|    Elapsed Time |              ms |        2,673.00 |        2,663.00 |        2,653.00 |           14.14 |
|[Counter] FilePairsLoaded |      operations |           50.00 |           50.00 |           50.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    5,157,459.39 |    5,138,263.69 |    5,119,067.99 |       27,146.82 |
|TotalCollections [Gen0] |     collections |            2.26 |            2.25 |            2.24 |            0.01 |
|TotalCollections [Gen1] |     collections |            2.26 |            2.25 |            2.24 |            0.01 |
|TotalCollections [Gen2] |     collections |            2.26 |            2.25 |            2.24 |            0.01 |
|    Elapsed Time |              ms |          999.74 |          999.72 |          999.71 |            0.03 |
|[Counter] FilePairsLoaded |      operations |           18.84 |           18.77 |           18.70 |            0.10 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   13,686,760.00 |    5,157,459.39 |          193.89 |
|               2 |   13,686,784.00 |    5,119,067.99 |          195.35 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            2.26 |  442,296,583.33 |
|               2 |            6.00 |            2.24 |  445,614,450.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            2.26 |  442,296,583.33 |
|               2 |            6.00 |            2.24 |  445,614,450.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |            6.00 |            2.26 |  442,296,583.33 |
|               2 |            6.00 |            2.24 |  445,614,450.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        2,653.00 |          999.71 |    1,000,293.82 |
|               2 |        2,673.00 |          999.74 |    1,000,256.90 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           50.00 |           18.84 |   53,075,590.00 |
|               2 |           50.00 |           18.70 |   53,473,734.00 |


