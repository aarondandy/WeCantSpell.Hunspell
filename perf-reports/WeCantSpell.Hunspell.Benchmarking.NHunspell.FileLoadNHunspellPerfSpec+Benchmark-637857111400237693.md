# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_04/16/2022 13:05:40_
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
|    Elapsed Time |              ms |        3,940.00 |        3,932.00 |        3,924.00 |           11.31 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,842,006.24 |   22,793,980.62 |   22,745,955.00 |       67,918.48 |
|TotalCollections [Gen0] |     collections |            3.31 |            3.31 |            3.30 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.31 |            3.31 |            3.30 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.31 |            3.31 |            3.30 |            0.01 |
|    Elapsed Time |              ms |        1,000.09 |        1,000.02 |          999.94 |            0.10 |
|[Counter] FilePairsLoaded |      operations |           15.04 |           15.01 |           14.97 |            0.04 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,842,006.24 |           43.78 |
|               2 |   89,624,176.00 |   22,745,955.00 |           43.96 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,819,615.38 |
|               2 |           13.00 |            3.30 |  303,094,215.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,819,615.38 |
|               2 |           13.00 |            3.30 |  303,094,215.38 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.31 |  301,819,615.38 |
|               2 |           13.00 |            3.30 |  303,094,215.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,924.00 |        1,000.09 |      999,912.08 |
|               2 |        3,940.00 |          999.94 |    1,000,057.06 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           15.04 |   66,502,627.12 |
|               2 |           59.00 |           14.97 |   66,783,471.19 |


