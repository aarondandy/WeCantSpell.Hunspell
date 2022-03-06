# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/06/2022 02:00:51_
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
|TotalBytesAllocated |           bytes |   89,624,208.00 |   89,624,180.00 |   89,624,152.00 |           39.60 |
|TotalCollections [Gen0] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen1] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|TotalCollections [Gen2] |     collections |           13.00 |           13.00 |           13.00 |            0.00 |
|    Elapsed Time |              ms |        3,981.00 |        3,957.50 |        3,934.00 |           33.23 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,780,162.30 |   22,646,018.78 |   22,511,875.26 |      189,707.59 |
|TotalCollections [Gen0] |     collections |            3.30 |            3.28 |            3.27 |            0.03 |
|TotalCollections [Gen1] |     collections |            3.30 |            3.28 |            3.27 |            0.03 |
|TotalCollections [Gen2] |     collections |            3.30 |            3.28 |            3.27 |            0.03 |
|    Elapsed Time |              ms |          999.95 |          999.94 |          999.92 |            0.02 |
|[Counter] FilePairsLoaded |      operations |           15.00 |           14.91 |           14.82 |            0.12 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,780,162.30 |           43.90 |
|               2 |   89,624,208.00 |   22,511,875.26 |           44.42 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,639,000.00 |
|               2 |           13.00 |            3.27 |  306,245,915.38 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,639,000.00 |
|               2 |           13.00 |            3.27 |  306,245,915.38 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.30 |  302,639,000.00 |
|               2 |           13.00 |            3.27 |  306,245,915.38 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,934.00 |          999.92 |    1,000,078.04 |
|               2 |        3,981.00 |          999.95 |    1,000,049.46 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           15.00 |   66,683,169.49 |
|               2 |           59.00 |           14.82 |   67,477,913.56 |


