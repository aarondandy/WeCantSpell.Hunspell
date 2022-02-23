# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadNHunspellPerfSpec+Benchmark
__How fast can NHunspell load files?__
_02/23/2022 03:50:16_
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
|    Elapsed Time |              ms |        4,036.00 |        4,023.50 |        4,011.00 |           17.68 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |   22,344,790.93 |   22,275,318.51 |   22,205,846.08 |       98,248.84 |
|TotalCollections [Gen0] |     collections |            3.24 |            3.23 |            3.22 |            0.01 |
|TotalCollections [Gen1] |     collections |            3.24 |            3.23 |            3.22 |            0.01 |
|TotalCollections [Gen2] |     collections |            3.24 |            3.23 |            3.22 |            0.01 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.00 |          999.98 |            0.02 |
|[Counter] FilePairsLoaded |      operations |           14.71 |           14.66 |           14.62 |            0.06 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |   89,624,152.00 |   22,344,790.93 |           44.75 |
|               2 |   89,624,176.00 |   22,205,846.08 |           45.03 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,535,692.31 |
|               2 |           13.00 |            3.22 |  310,466,323.08 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,535,692.31 |
|               2 |           13.00 |            3.22 |  310,466,323.08 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           13.00 |            3.24 |  308,535,692.31 |
|               2 |           13.00 |            3.22 |  310,466,323.08 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |        4,011.00 |        1,000.01 |      999,991.02 |
|               2 |        4,036.00 |          999.98 |    1,000,015.41 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |           14.71 |   67,982,440.68 |
|               2 |           59.00 |           14.62 |   68,407,833.90 |


