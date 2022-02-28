# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/28/2022 01:03:43_
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
|    Elapsed Time |              ms |        4,088.00 |        4,040.00 |        3,992.00 |           67.88 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,445,572.34 |   22,184,118.21 |   21,922,664.08 |      369,751.97 |
|TotalCollections [Gen0] |     collections |            3.26 |            3.22 |            3.18 |            0.05 |
|TotalCollections [Gen1] |     collections |            3.26 |            3.22 |            3.18 |            0.05 |
|TotalCollections [Gen2] |     collections |            3.26 |            3.22 |            3.18 |            0.05 |
|    Elapsed Time |              ms |          999.95 |          999.86 |          999.76 |            0.14 |
|[Counter] FilePairsLoaded |      operations |           14.78 |           14.60 |           14.43 |            0.24 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   21,922,664.08 |           45.61 |
|               2 |   89,624,176.00 |   22,445,572.34 |           44.55 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.18 |  314,476,630.77 |
|               2 |           13.00 |            3.26 |  307,150,438.46 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.18 |  314,476,630.77 |
|               2 |           13.00 |            3.26 |  307,150,438.46 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.18 |  314,476,630.77 |
|               2 |           13.00 |            3.26 |  307,150,438.46 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,088.00 |          999.95 |    1,000,047.99 |
|               2 |        3,992.00 |          999.76 |    1,000,239.40 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.43 |   69,291,461.02 |
|               2 |           59.00 |           14.78 |   67,677,215.25 |


