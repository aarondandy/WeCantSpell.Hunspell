# WeCantSpell.Hunspell.Benchmarking.NHunspell.FileLoadWeCantSpellHunspellPerfSpec+Benchmark
__How fast can this project load files?__
_03/06/2022 23:02:37_
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
|TotalBytesAllocated |           bytes |  149,205,480.00 |   94,733,136.00 |   40,260,792.00 |   77,035,527.66 |
|TotalCollections [Gen0] |     collections |          732.00 |          731.50 |          731.00 |            0.71 |
|TotalCollections [Gen1] |     collections |          293.00 |          292.00 |          291.00 |            1.41 |
|TotalCollections [Gen2] |     collections |           83.00 |           80.00 |           77.00 |            4.24 |
|    Elapsed Time |              ms |       18,040.00 |       17,920.50 |       17,801.00 |          169.00 |
|[Counter] FilePairsLoaded |      operations |           59.00 |           59.00 |           59.00 |            0.00 |

### Per-second Totals
|          Metric |       Units / s |         Max / s |     Average / s |         Min / s |      StdDev / s |
|---------------- |---------------- |---------------- |---------------- |---------------- |---------------- |
|TotalBytesAllocated |           bytes |    8,381,924.19 |    5,306,842.65 |    2,231,761.12 |    4,348,822.02 |
|TotalCollections [Gen0] |     collections |           41.12 |           40.82 |           40.52 |            0.42 |
|TotalCollections [Gen1] |     collections |           16.46 |           16.30 |           16.13 |            0.23 |
|TotalCollections [Gen2] |     collections |            4.66 |            4.47 |            4.27 |            0.28 |
|    Elapsed Time |              ms |        1,000.01 |        1,000.01 |        1,000.00 |            0.00 |
|[Counter] FilePairsLoaded |      operations |            3.31 |            3.29 |            3.27 |            0.03 |

### Raw Data
#### TotalBytesAllocated
|           Run # |           bytes |       bytes / s |      ns / bytes |
|---------------- |---------------- |---------------- |---------------- |
|               1 |  149,205,480.00 |    8,381,924.19 |          119.30 |
|               2 |   40,260,792.00 |    2,231,761.12 |          448.08 |

#### TotalCollections [Gen0]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          732.00 |           41.12 |   24,318,118.17 |
|               2 |          731.00 |           40.52 |   24,678,413.13 |

#### TotalCollections [Gen1]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |          293.00 |           16.46 |   60,753,796.93 |
|               2 |          291.00 |           16.13 |   61,992,852.23 |

#### TotalCollections [Gen2]
|           Run # |     collections | collections / s |ns / collections |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           83.00 |            4.66 |  214,468,222.89 |
|               2 |           77.00 |            4.27 |  234,284,675.32 |

#### Elapsed Time
|           Run # |              ms |          ms / s |         ns / ms |
|---------------- |---------------- |---------------- |---------------- |
|               1 |       17,801.00 |        1,000.01 |      999,992.28 |
|               2 |       18,040.00 |        1,000.00 |      999,995.57 |

#### [Counter] FilePairsLoaded
|           Run # |      operations |  operations / s | ns / operations |
|---------------- |---------------- |---------------- |---------------- |
|               1 |           59.00 |            3.31 |  301,709,533.90 |
|               2 |           59.00 |            3.27 |  305,761,355.93 |


