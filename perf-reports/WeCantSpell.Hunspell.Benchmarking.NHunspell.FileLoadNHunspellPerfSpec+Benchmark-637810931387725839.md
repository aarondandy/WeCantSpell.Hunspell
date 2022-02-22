# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/22/2022 02:18:58_
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
|    Elapsed Time |              ms |        4,013.00 |        3,998.00 |        3,983.00 |           21.21 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,503,433.36 |   22,417,274.67 |   22,331,115.99 |      121,846.78 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.25 |            3.24 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.25 |            3.24 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.25 |            3.24 |            0.02 |
|    Elapsed Time |              ms |        1,000.08 |          999.99 |          999.90 |            0.13 |
|[Counter] FilePairsLoaded |      operations |           14.81 |           14.76 |           14.70 |            0.08 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,331,115.99 |           44.78 |
|               2 |   89,624,208.00 |   22,503,433.36 |           44.44 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,724,630.77 |
|               2 |           13.00 |            3.26 |  306,360,800.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,724,630.77 |
|               2 |           13.00 |            3.26 |  306,360,800.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,724,630.77 |
|               2 |           13.00 |            3.26 |  306,360,800.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,013.00 |          999.90 |    1,000,104.71 |
|               2 |        3,983.00 |        1,000.08 |      999,922.27 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.70 |   68,024,071.19 |
|               2 |           59.00 |           14.81 |   67,503,227.12 |


