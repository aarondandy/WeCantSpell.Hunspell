# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/09/2022 14:42:33_
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
|    Elapsed Time |              ms |        4,646.00 |        4,326.50 |        4,007.00 |          451.84 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,369,309.83 |   20,828,611.92 |   19,287,914.01 |    2,178,875.88 |
|TotalCollections [Gen0] |     collections |            3.24 |            3.02 |            2.80 |            0.32 |
|TotalCollections [Gen1] |     collections |            3.24 |            3.02 |            2.80 |            0.32 |
|TotalCollections [Gen2] |     collections |            3.24 |            3.02 |            2.80 |            0.32 |
|    Elapsed Time |              ms |        1,000.11 |          999.98 |          999.86 |            0.18 |
|[Counter] FilePairsLoaded |      operations |           14.73 |           13.71 |           12.70 |            1.43 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,369,309.83 |           44.70 |
|               2 |   89,624,176.00 |   19,287,914.01 |           51.85 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,197,507.69 |
|               2 |           13.00 |            2.80 |  357,434,576.92 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,197,507.69 |
|               2 |           13.00 |            2.80 |  357,434,576.92 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,197,507.69 |
|               2 |           13.00 |            2.80 |  357,434,576.92 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,007.00 |        1,000.11 |      999,892.09 |
|               2 |        4,646.00 |          999.86 |    1,000,139.80 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.73 |   67,907,925.42 |
|               2 |           59.00 |           12.70 |   78,756,771.19 |


