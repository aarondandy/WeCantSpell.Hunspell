# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/12/2022 04:18:02_
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
|    Elapsed Time |              ms |        3,966.00 |        3,954.50 |        3,943.00 |           16.26 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,729,869.89 |   22,663,046.37 |   22,596,222.85 |       94,502.73 |
|TotalCollections [Gen0] |     collections |            3.30 |            3.29 |            3.28 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.30 |            3.29 |            3.28 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.30 |            3.29 |            3.28 |            0.01 |
|    Elapsed Time |              ms |        1,000.00 |          999.96 |          999.92 |            0.06 |
|[Counter] FilePairsLoaded |      operations |           14.96 |           14.92 |           14.88 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,729,869.89 |           43.99 |
|               2 |   89,624,176.00 |   22,596,222.85 |           44.26 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  303,308,623.08 |
|               2 |           13.00 |            3.28 |  305,102,646.15 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  303,308,623.08 |
|               2 |           13.00 |            3.28 |  305,102,646.15 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  303,308,623.08 |
|               2 |           13.00 |            3.28 |  305,102,646.15 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,943.00 |        1,000.00 |    1,000,003.07 |
|               2 |        3,966.00 |          999.92 |    1,000,084.32 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.96 |   66,830,713.56 |
|               2 |           59.00 |           14.88 |   67,226,006.78 |


