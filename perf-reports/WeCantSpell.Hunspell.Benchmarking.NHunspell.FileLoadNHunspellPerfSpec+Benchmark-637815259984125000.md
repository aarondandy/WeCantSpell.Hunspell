# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/27/2022 02:33:18_
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
|    Elapsed Time |              ms |        3,965.00 |        3,949.00 |        3,933.00 |           22.63 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,786,239.82 |   22,693,825.60 |   22,601,411.38 |      130,693.44 |
|TotalCollections [Gen0] |     collections |            3.31 |            3.29 |            3.28 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.31 |            3.29 |            3.28 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.31 |            3.29 |            3.28 |            0.02 |
|    Elapsed Time |              ms |          999.93 |          999.91 |          999.89 |            0.03 |
|[Counter] FilePairsLoaded |      operations |           15.00 |           14.94 |           14.88 |            0.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,601,411.38 |           44.25 |
|               2 |   89,624,176.00 |   22,786,239.82 |           43.89 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  305,032,523.08 |
|               2 |           13.00 |            3.31 |  302,558,361.54 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  305,032,523.08 |
|               2 |           13.00 |            3.31 |  302,558,361.54 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.28 |  305,032,523.08 |
|               2 |           13.00 |            3.31 |  302,558,361.54 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,965.00 |          999.89 |    1,000,106.63 |
|               2 |        3,933.00 |          999.93 |    1,000,065.78 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.88 |   67,210,555.93 |
|               2 |           59.00 |           15.00 |   66,665,401.69 |


