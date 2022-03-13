# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/13/2022 00:16:37_
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
|    Elapsed Time |              ms |        4,000.00 |        3,995.00 |        3,990.00 |            7.07 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,460,068.46 |   22,433,785.12 |   22,407,501.77 |       37,170.27 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.25 |            3.25 |            0.01 |
|    Elapsed Time |              ms |        1,000.07 |          999.99 |          999.90 |            0.11 |
|[Counter] FilePairsLoaded |      operations |           14.79 |           14.77 |           14.75 |            0.02 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,407,501.77 |           44.63 |
|               2 |   89,624,208.00 |   22,460,068.46 |           44.52 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,672,207.69 |
|               2 |           13.00 |            3.26 |  306,952,307.69 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,672,207.69 |
|               2 |           13.00 |            3.26 |  306,952,307.69 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,672,207.69 |
|               2 |           13.00 |            3.26 |  306,952,307.69 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,000.00 |        1,000.07 |      999,934.68 |
|               2 |        3,990.00 |          999.90 |    1,000,095.24 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.75 |   67,792,181.36 |
|               2 |           59.00 |           14.79 |   67,633,559.32 |


