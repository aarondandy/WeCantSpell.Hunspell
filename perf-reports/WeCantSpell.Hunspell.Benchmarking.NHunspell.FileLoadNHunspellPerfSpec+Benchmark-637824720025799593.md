# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_03/10/2022 01:20:02_
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
|    Elapsed Time |              ms |        4,033.00 |        4,016.00 |        3,999.00 |           24.04 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,414,370.54 |   22,318,723.75 |   22,223,076.96 |      135,264.99 |
|TotalCollections [Gen0] |     collections |            3.25 |            3.24 |            3.22 |            0.02 |
|TotalCollections [Gen1] |     collections |            3.25 |            3.24 |            3.22 |            0.02 |
|TotalCollections [Gen2] |     collections |            3.25 |            3.24 |            3.22 |            0.02 |
|    Elapsed Time |              ms |        1,000.12 |        1,000.07 |        1,000.02 |            0.07 |
|[Counter] FilePairsLoaded |      operations |           14.76 |           14.69 |           14.63 |            0.09 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,414,370.54 |           44.61 |
|               2 |   89,624,176.00 |   22,223,076.96 |           45.00 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,577,923.08 |
|               2 |           13.00 |            3.22 |  310,225,600.00 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,577,923.08 |
|               2 |           13.00 |            3.22 |  310,225,600.00 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.25 |  307,577,923.08 |
|               2 |           13.00 |            3.22 |  310,225,600.00 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        3,999.00 |        1,000.12 |      999,878.22 |
|               2 |        4,033.00 |        1,000.02 |      999,983.34 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.76 |   67,771,406.78 |
|               2 |           59.00 |           14.63 |   68,354,793.22 |


